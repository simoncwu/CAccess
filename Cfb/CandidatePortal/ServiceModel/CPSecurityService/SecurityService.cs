using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.Security;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.ServiceModel.CPSecurityService.Sso;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService
{
    /// <summary>
    /// Provides access to C-Access Security data via WCF service methods.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = int.MaxValue, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SecurityService : ISecurityService, ISecurityProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        public SecurityService()
        {
            CPDataProvider provider = new CPDataProvider();
            CPProviders.DataProvider = provider;
            CPSecurity.Provider = this;
            GC.KeepAlive(provider);
            GC.KeepAlive(CPProviders.SettingsProvider = new CPSettingsProvider());
        }


        #region ISecurityService Members

        #region Basic Security Info

        /// <summary>
        /// Gets a <see cref="CPUserRights"/> value that represents the access rights granted to the specified user.
        /// </summary>
        /// <param name="username">The name of the user to get access rights for.</param>
        /// <returns>A <see cref="CPUserRights"/> value indicating the total access rights granted to the specified user.</returns>
        public CPUserRights GetUserRights(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                CPUserRights rights = CPUserRights.None;
                var groupRights = from m in context.SecurityGroupMemberships
                                  where m.UserName == username
                                  select (CPUserRights)m.SecurityGroup.UserRights;
                foreach (var right in groupRights)
                {
                    rights |= right;
                }
                return CPSecurity.Sanitize(rights);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether or not a user has been granted certain C-Access account rights and privileges.
        /// </summary>
        /// <param name="username">The name of the user to analyze.</param>
        /// <param name="rights">A <see cref="CPUserRights"/> value indicating the security rights to check for.</param>
        /// <returns>true if <paramref name="username"/> has been granted the rights indicated by <paramref name="rights"/>; otherwise, false.</returns>
        public bool HasUserRights(string username, CPUserRights rights)
        {
            return GetUserRights(username).HasFlag(rights);
        }

        /// <summary>
        /// Gets the candidate ID associated with a C-Access user account.
        /// </summary>
        /// <param name="username">The name of the user to query.</param>
        /// <returns>The candidate ID associated with the specified user.</returns>
        public string GetCid(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var match = from p in context.SecurityUserProfiles
                            where p.UserName == username
                            select p.CandidateId;
                if (match.Count() > 0)
                    return match.First();

                // attempt to convert legacy data
                var profile = context.AddToSecurityUserProfiles(Membership.GetUser(username));
                return profile == null ? null : profile.CandidateId;
            }
        }

        /// <summary>
        /// Gets the friendly display name for a C-Access user account.
        /// </summary>
        /// <param name="username">The C-Access user to query.</param>
        /// <returns>The user friendly display name (e.g., full name) on record for the specified user.</returns>
        public string GetDisplayName(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var match = from p in context.SecurityUserProfiles
                            where p.UserName == username
                            select p.DisplayName;
                if (match.Count() > 0)
                    return match.First();
                CPUser user = GetUser(username);
                return user == null ? null : user.DisplayName;
            }
        }

        /// <summary>
        /// Gets the e-mail address for the user.
        /// </summary>
        /// <param name="username">The C-Access user to query.</param>
        /// <returns>The e-mail address on record for the specified user.</returns>
        public string GetEmail(string username)
        {
            CPUser user = GetUser(username);
            return user == null ? null : user.Email;
        }

        /// <summary>
        /// Gets the election cycles that a C-Access user account is authorized for.
        /// </summary>
        /// <param name="username">The C-Access user to query.</param>
        /// <returns>A collection of election cycles that the specified user is authorized to access.</returns>
        public List<string> GetAuthorizedElectionCycles(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var cycles = GetExplicitElectionCycles(username);
                // cross reference against CFIS
                var active = Elections.GetActiveElectionCycles(GetCid(username));
                return cycles.Count() > 0 ? cycles.Intersect(active).ToList() : active.ToList();
            }
        }

        /// <summary>
        /// Gets a collection of C-Access user accounts for a specific campaign.
        /// </summary>
        /// <param name="candidateID">The ID of the active candidate in the campaign.</param>
        /// <param name="electionCycle">The election cycle the campaign is active in, or null for all election cycles.</param>
        /// <param name="includeDisabled">Indicates whether disabled accounts should also be included in the results.</param>
        /// <returns>A collection of C-Access accounts for the specified campaign.</returns>
        public List<CPUser> GetCampaignUsers(string candidateID, string electionCycle = null, bool includeDisabled = true)
        {
            return FindUsers(candidateID: candidateID, electionCycle: electionCycle, disabled: includeDisabled ? null : (bool?)false);
        }

        /// <summary>
        /// Gets a C-Access user account.
        /// </summary>
        /// <param name="username">The login name for the account to retrieve.</param>
        /// <returns>The C-Access user account matching the specified username if found; otherwise, null.</returns>
        public CPUser GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            username = username.Trim();
            MembershipUser muser = Membership.GetUser(username);
            if (muser == null)
                return null;
            username = muser.UserName;
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                SecurityUserProfile profile = (from p in context.SecurityUserProfiles
                                               where p.UserName == username
                                               select p).FirstOrDefault();

                // automatic legacy .NET Membership/SharePoint conversion
                if (profile == null)
                    profile = context.AddToSecurityUserProfiles(Membership.GetUser(username));

                return CPUserFactory.CreateUser(profile);
            }
        }

        /// <summary>
        /// Searches for C-Access user accounts matching specific criteria.
        /// </summary>
        /// <param name="name">All or part of a display name to search for.</param>
        /// <param name="email">An email address to search for.</param>
        /// <param name="candidateID">The ID of a candidate associated with the accounts being sought.</param>
        /// <param name="electionCycle">An election cycle that is accessible to the accounts being sought.</param>
        /// <param name="groupID">The ID of a group of which the accounts being sought are members.</param>
        /// <param name="disabled">Whether or not the accounts being sought are disabled.</param>
        /// <param name="lockedOut">Whether or not the accounts being sought are locked out.</param>
        /// <param name="used">Whether or not the accounts being sought have ever been used.</param>
        /// <param name="createdStartDate">A creation date filter start date.</param>
        /// <param name="createdEndDate">A creation date filter end date.</param>
        /// <returns>A collection of C-Access accounts matching the specified search criteria.</returns>
        public List<CPUser> FindUsers(string name = null, string email = null, string candidateID = null, string electionCycle = null, byte? groupID = null, bool? disabled = null, bool? lockedOut = null, bool? used = null, DateTime? createdStartDate = null, DateTime? createdEndDate = null)
        {
            IEnumerable<CPUser> results;

            // filter by user profile fields
            using (Data.CPSecurityEntities context = new Data.CPSecurityEntities())
            {
                var profiles = context.SecurityUserProfiles.AsQueryable();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    foreach (var n in name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        profiles = profiles.Where(u => u.DisplayName.Contains(n));
                    }
                }
                if (!string.IsNullOrWhiteSpace(candidateID))
                {
                    profiles = profiles.Where(u => u.CandidateId == candidateID);
                }
                if (groupID.HasValue)
                {
                    profiles = from p in profiles
                               join g in context.SecurityGroupMemberships on p.UserName equals g.UserName
                               where g.GroupId == groupID.Value
                               select p;
                }
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var emailMatches = Membership.FindUsersByEmail(email);
                    var musers = new MembershipUser[emailMatches.Count];
                    if (emailMatches.Count > 0)
                        emailMatches.CopyTo(musers, 0);
                    var usernames = musers.Select(mu => mu.UserName);
                    profiles = from p in profiles
                               where usernames.Contains(p.UserName)
                               select p;
                }
                if (!string.IsNullOrWhiteSpace(electionCycle))
                {
                    // include users with no election cycle filter
                    var unrestricted = from p in profiles
                                       where !context.SecurityUserElectionCycles.Any(uec => uec.UserName == p.UserName)
                                       select p;
                    profiles = from p in profiles
                               join e in context.SecurityUserElectionCycles on p.UserName equals e.UserName
                               where e.ElectionCycle == electionCycle
                               select p;
                    profiles = profiles.ToList().Concat(unrestricted.ToList().Where(c => Elections.GetActiveElectionCycles(c.CandidateId).Contains(electionCycle))).AsQueryable();
                }

                // convert results to CPUser objects
                results = profiles.ToList().Select(p => CPUserFactory.CreateUser(p)).Where(u => u != null);
            }

            // filter by membership fields
            if (disabled.HasValue)
                results = results.Where(u => u.Enabled == !disabled.Value);
            if (lockedOut.HasValue)
                results = results.Where(u => u.LockedOut == lockedOut.Value);
            if (used.HasValue)
                results = results.Where(u => u.LastLoginDate.HasValue == used.Value);
            if (createdStartDate.HasValue)
                results = results.Where(u => u.CreationDate >= createdStartDate.Value);
            if (createdEndDate.HasValue)
                results = results.Where(u => u.CreationDate.Date <= createdEndDate.Value);

            return results.ToList();
        }

        #endregion

        #region Single Sign-On

        /// <summary>
        /// Creates a new login token for authenticating a specific user for a specific application.
        /// </summary>
        /// <param name="username">The login name of the user requesting authentication.</param>
        /// <param name="applicationID">The unique identifier of the application requesting authentication.</param>
        /// <returns>A new login token if the specified user has access to the specified application; otherwise, null.</returns>
        public Token CreateToken(string username, byte applicationID)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var app = context.SecuritySsoApplications.FirstOrDefault(a => a.ApplicationId == applicationID);
                if (app != null && HasUserRights(username, (CPUserRights)app.UserRights))
                {
                    SecuritySsoToken token = new SecuritySsoToken()
                    {
                        UserName = username,
                        SecuritySsoApplication = app
                    };
                    try
                    {
                        context.SecuritySsoTokens.AddObject(token);
                        context.SaveChanges();
                        return TokenFactory.CreateToken(token);
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Checks whether or not a login token exists and is valid for authentication.
        /// </summary>
        /// <param name="tokenID">The unique identifier of the token to validate.</param>
        /// <returns>The login name of the user attempting to authenticate with the specified token, if valid; otherwise, null.</returns>
        /// <remarks>The login token is automatically discarded once validated.</remarks>
        public string ValidateToken(Guid tokenID)
        {
            string username = null;
            if (tokenID != null)
            {
                using (CPSecurityEntities context = new CPSecurityEntities())
                {
                    var token = context.SecuritySsoTokens.FirstOrDefault(t => t.TokenId == tokenID);
                    if (token != null)
                    {
                        if (DateTime.Now <= token.Created.Add(Properties.Settings.Default.TokenValidationWindow))
                            username = token.UserName;
                        context.DeleteObject(token);
                        context.SaveChanges();
                    }
                }
            }
            return username;
        }

        /// <summary>
        /// Gets information about a specific SSO-enabled application.
        /// </summary>
        /// <param name="applicationID">The unique identifier of the application.</param>
        /// <returns>The application requested if found; otherwise, null.</returns>
        public Application GetApplication(byte applicationID)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var app = context.SecuritySsoApplications.FirstOrDefault(a => a.ApplicationId == applicationID);
                return ApplicationFactory.CreateApplication(app);
            }
        }

        #endregion

        #region Privileged Operations

        /// <summary>
        /// Gets a collection of all C-Access Security groups.
        /// </summary>
        /// <returns>A collection of all C-Access Security groups.</returns>
        public List<CPGroup> GetGroups()
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                return context.SecurityGroups.AsEnumerable().Select(g => CPGroupFactory.CreateGroup(g)).ToList();
            }
        }

        /// <summary>
        /// Gets a C-Access Security group.
        /// </summary>
        /// <param name="groupName">The name of the group to retrieve.</param>
        /// <returns>The security group if found; otherwise, null.</returns>
        public CPGroup GetGroup(string groupName)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                return CPGroupFactory.CreateGroup(context.SecurityGroups.FirstOrDefault(g => g.GroupName == groupName));
            }
        }

        /// <summary>
        /// Gets a collection of users who are members of the specified security group.
        /// </summary>
        /// <param name="groupName">The group to get the list of members for.</param>
        /// <returns>A collection of the names of all the users who are members of the specified security group.</returns>
        public List<string> GetGroupMembers(string groupName)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                return (from m in context.SecurityGroupMemberships
                        where m.SecurityGroup.GroupName == groupName
                        select m.UserName).ToList();
            }
        }

        /// <summary>
        /// Creates a new security group.
        /// </summary>
        /// <param name="groupName">The name of the group to create.</param>
        /// <returns>A <see cref="CPGroup"/> instance representing the new group if created successfully; otherwise, null.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Administrators")]
        public CPGroup CreateGroup(string groupName)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                if (!context.SecurityGroups.Any(g => g.GroupName == groupName))
                {
                    SecurityGroup group = new SecurityGroup()
                    {
                        GroupName = groupName
                    };
                    try
                    {
                        context.SecurityGroups.AddObject(group);
                        context.SaveChanges();
                        return CPGroupFactory.CreateGroup(group);
                    }
                    catch (ConstraintException)
                    {
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes a security group.
        /// </summary>
        /// <param name="groupName">The name of the group to delete.</param>
        /// <returns>true if <paramref name="groupName"/> was deleted successfully; otherwise, false.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Administrators")]
        public bool DeleteGroup(string groupName)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var match = context.SecurityGroups.FirstOrDefault(g => g.GroupName == groupName);
                if (match == null)
                    return true;
                context.DeleteObject(match);
                try
                {
                    return context.SaveChanges() > 0;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
                catch (ConstraintException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Updates a C-Access Security group.
        /// </summary>
        /// <param name="group">The group to update.</param>
        /// <returns>The resultant <see cref="CPGroup"/> after it has been updated.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public CPGroup UpdateGroup(CPGroup group)
        {
            if (group == null)
                return null;
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                var match = context.SecurityGroups.FirstOrDefault(g => g.GroupId == group.ID);
                if (match == null)
                    return null;
                match.GroupName = group.Name;
                match.UserRights = (int)group.UserRights;
                try
                {
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    context.Refresh(RefreshMode.ClientWins, match);
                    context.SaveChanges();
                }
                catch (ConstraintException)
                {
                }
                return GetGroup(match.GroupName);
            }
        }

        /// <summary>
        /// Creates a new C-Access user account.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="middleInitial">The user's middle initial.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="candidateID">The ID of the candidate associated with the account.</param>
        /// <param name="password">The initial password for the account.</param>
        /// <param name="email">The e-mail address for the account.</param>
        /// <param name="creator">An identifier for the creator of the account.</param>
        /// <param name="type">The entity type of the CFB-registered contact to create an account for.</param>
        /// <param name="committeeID">If not a candidate, the ID of the account owner contact's committee.</param>
        /// <param name="electionCycle">If a treasurer, the election cycle for which the account owner contact's committee is authorized.</param>
        /// <param name="liaisonID">If a liaison, the committee-relative liaison ID of the account owner contact.</param>
        /// <param name="username">The username to assign to the new account.</param>
        /// <returns>A new <see cref="CPUser"/> instance representing the newly created account if successful; otherwise, null.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public CPUser CreateUser(string firstName, char? middleInitial, string lastName, string candidateID, string password, string email, string creator, EntityType type = EntityType.Generic, char? committeeID = null, string electionCycle = null, byte? liaisonID = null, string username = null)
        {
            if (username == null)
                username = GenerateUserName(firstName, middleInitial, lastName, candidateID);
            if (!string.IsNullOrWhiteSpace(username))
            {
                if (password == null)
                    password = Membership.GeneratePassword(10, 0);
                if (UserManagement.CreateUser(username, password, email, creator) != null)
                {
                    using (CPSecurityEntities context = new CPSecurityEntities())
                    {
                        switch (type) // sanitize CFIS source entity properties first
                        {
                            case EntityType.Liaison:
                                electionCycle = null;
                                break;
                            case EntityType.Treasurer:
                                liaisonID = null;
                                break;
                            default:
                                committeeID = null;
                                electionCycle = null;
                                liaisonID = null;
                                break;
                        }
                        context.DeleteSecurityUserProfile(username);
                        CPUser user = CPUserFactory.CreateUser(context.AddToSecurityUserProfiles(username, candidateID, Entity.ToFullName(firstName, lastName, middleInitial, false), type, committeeID, electionCycle, liaisonID));
                        if (user != null)
                        {
                            CPGroup group = GetGroup(type.ToString() + "s");
                            if (group != null)
                                AddUserToGroups(user.UserName, new List<byte>(new[] { group.ID }));
                            if (electionCycle != null)
                                user.ElectionCycles.Add(electionCycle);
                            if (!user.Save())
                            {
                                this.DeleteUser(user.UserName);
                                user = null;
                            }
                        }
                        else
                        {
                            UserManagement.DeleteUser(Membership.GetUser(username));
                        }
                        return user;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes a C-Access user account.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <returns>true if <paramref name="username"/> was deleted successfully; otherwise, false.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Administrators")]
        public bool DeleteUser(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                context.DeleteSecurityUserProfile(username);
                MembershipUser user = Membership.GetUser(username);
                if (user != null)
                    return UserManagement.DeleteUser(user);
                return UserManagement.DeleteUser(Membership.GetUser(username));
            }
        }

        /// <summary>
        /// Updates a C-Access Security user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>An <see cref="UpdateResult"/> object representing the result of the synchronization.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public UpdateResult UpdateUser(CPUser user)
        {
            UpdateResultState state = UpdateResultState.Success;

            // .NET Membership properties
            MembershipUser muser = user == null ? null : Membership.GetUser(user.UserName);
            if (muser == null)
            {
                state |= UpdateResultState.MembershipUserNotFound;
            }
            else
            {
                bool dirty = false;
                if (muser.Email != user.Email)
                {
                    muser.Email = user.Email;
                    dirty = true;
                }
                if (muser.IsApproved != user.Enabled)
                {
                    muser.IsApproved = user.Enabled;
                    dirty = true;
                }
                if (muser.IsLockedOut && !user.LockedOut)
                {
                    muser.UnlockUser();
                    dirty = true;
                }
                if (dirty)
                {
                    try
                    {
                        Membership.UpdateUser(muser);
                    }
                    catch
                    {
                        state |= UpdateResultState.AspNetMembershipFailure;
                    }
                }

                // C-Access Security
                using (CPSecurityEntities context = new CPSecurityEntities())
                {
                    var match = context.SecurityUserProfiles.FirstOrDefault(u => u.UserName == user.UserName);
                    if (match == null)
                    {
                        state |= UpdateResultState.ProfileNotFound;
                    }
                    else
                    {
                        // basic properties
                        match.CandidateId = user.Cid;
                        match.DisplayName = user.DisplayName;
                        match.CfisType = (byte)user.SourceType;
                        match.CfisCommitteeID = user.SourceCommitteeID.HasValue ? user.SourceCommitteeID.Value.ToString() : null;
                        match.CfisCommitteeContactID = user.SourceLiaisonID.HasValue ? user.SourceLiaisonID.Value.ToString() : user.SourceElectionCycle;

                        // election cycles
                        var currentCycles = context.SecurityUserElectionCycles.Where(c => c.UserName == user.UserName);
                        var updateCycles = user.ElectionCycles;
                        if (user.ImplicitElectionCycles == updateCycles.Any()) // implicit EC access requires both an empty EC collection AND a set flag
                        {
                            state |= UpdateResultState.ElectionCycleNotFound;
                        }
                        else
                        {
                            // delete old cycles
                            foreach (var cycle in currentCycles.Where(c => !updateCycles.Contains(c.ElectionCycle)))
                            {
                                context.SecurityUserElectionCycles.DeleteObject(cycle);
                            }
                            // add new cycles
                            foreach (var cycle in updateCycles.Except(currentCycles.Select(c => c.ElectionCycle)))
                            {
                                context.SecurityUserElectionCycles.AddObject(SecurityUserElectionCycle.CreateSecurityUserElectionCycle(user.UserName, cycle));
                            }
                        }

                        // save changes
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (OptimisticConcurrencyException)
                        {
                            context.Refresh(RefreshMode.ClientWins, match);
                            context.SaveChanges();
                        }
                        catch (ConstraintException)
                        {
                            state |= UpdateResultState.ProfileFailure;
                        }
                    }
                }
            }
            return new UpdateResult(GetUser(user.UserName), state);
        }

        /// <summary>
        /// Synchronizes a user account with the currently existing data for the CFIS source contact associated with the account.
        /// </summary>
        /// <param name="userName">The name of the user to synchronize.</param>
        /// <returns>An <see cref="UpdateResult"/> representing the result of the synchronization.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public UpdateResult SynchronizeUser(string userName)
        {
            CPUser user = this.GetUser(userName);
            if (user == null)
                return new UpdateResult(null, UpdateResultState.MembershipUserNotFound);
            UpdateResultState state = UpdateResultState.Success;

            // find source entity
            Entity source = null;
            switch (user.SourceType)
            {
                case EntityType.Candidate:
                    source = CPProviders.DataProvider.GetEntity(user.Cid);
                    break;
                case EntityType.Treasurer:
                    source = CPProviders.DataProvider.GetEntity(user.Cid, user.SourceCommitteeID, user.SourceElectionCycle);
                    break;
                case EntityType.Liaison:
                case EntityType.Consultant:
                    source = CPProviders.DataProvider.GetEntity(user.Cid, user.SourceCommitteeID, liaisonID: user.SourceLiaisonID);
                    break;
            }
            if (source == null)
            {
                state |= UpdateResultState.CfisContactNotFound;
            }
            else
            {
                // update email/name
                MembershipUserCollection conflicts = Membership.FindUsersByEmail(source.Email);
                foreach (MembershipUser u in conflicts)
                {
                    if (string.Equals(user.Cid, GetCid(u.UserName), StringComparison.OrdinalIgnoreCase) && !string.Equals(user.UserName, u.UserName, StringComparison.OrdinalIgnoreCase))
                    {
                        state |= UpdateResultState.EmailAddressInUse;
                        break;
                    }
                }
                if (string.Equals(user.DisplayName, source.Name, StringComparison.OrdinalIgnoreCase)) // update only name or email address, but not both simultaneously
                {
                    if (!string.IsNullOrEmpty(source.Email))
                    {
                        MembershipUser u = Membership.GetUser(user.UserName);
                        if (u != null)
                        {
                            user.Email = u.Email = source.Email;
                            try
                            {
                                Membership.UpdateUser(u);
                                state |= UpdateResultState.EmailAddressChanged;
                            }
                            catch
                            {
                                state |= UpdateResultState.AspNetMembershipFailure;
                            }
                        }
                        else
                        {
                            state |= UpdateResultState.MembershipUserNotFound;
                        }
                    }
                }
                else
                {
                    // update name
                    if (!string.IsNullOrEmpty(source.Name))
                    {
                        user.DisplayName = source.Name;
                        state |= UpdateResultState.DisplayNameChanged;
                    }
                }
            }
            // persist changes
            UpdateResult result = new UpdateResult(user, state);
            if (result.Succeeded)
                result = UpdateUser(user);
            return result;
        }


        /// <summary>
        /// Adds a user to the specified groups.
        /// </summary>
        /// <param name="username">The name of the user to add to the specified groups.</param>
        /// <param name="groupIDs">An array of group IDs to add the specified user to.</param>
        /// <returns>true if <paramref name="username"/> was added successfully; otherwise, false.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public bool AddUserToGroups(string username, List<byte> groupIDs)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                foreach (var id in (groupIDs ?? new List<byte>()).Except(from m in context.SecurityGroupMemberships
                                                                         where m.UserName == username
                                                                         select m.GroupId))
                {
                    context.SecurityGroupMemberships.AddObject(new SecurityGroupMembership()
                    {
                        UserName = username,
                        GroupId = id
                    });
                }
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (ConstraintException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Removes a user from the specified groups.
        /// </summary>
        /// <param name="username">The name of the user to remove from the specified groups.</param>
        /// <param name="groupIDs">An array of group IDs to remote the specified user from.</param>
        /// <returns>true if <paramref name="username"/> was removed successfully; otherwise, false.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public bool RemoveUserFromGroups(string username, List<byte> groupIDs)
        {
            if (groupIDs == null || groupIDs.Count == 0)
                return true;
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                foreach (var m in from m in context.SecurityGroupMemberships
                                  where m.UserName == username && groupIDs.Contains(m.GroupId)
                                  select m)
                {
                    context.DeleteObject(m);
                }
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (ConstraintException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a list of the groups that a user is in.
        /// </summary>
        /// <param name="username">The name of the user to return a list of groups for.</param>
        /// <returns>A collection containing the names of all the groups that the specified user is in.</returns>
        public List<CPGroup> GetGroupMembership(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                return (from g in context.SecurityGroups
                        join m in context.SecurityGroupMemberships
                        on g.GroupId equals m.GroupId
                        where m.UserName == username
                        select g).AsEnumerable().Select(g => CPGroupFactory.CreateGroup(g)).ToList();
            }
        }

        #endregion

        /// <summary>
        /// Resets a C-Access user's password to a new randomly generated password.
        /// </summary>
        /// <param name="username">The user name of the account whose password is to be reset.</param>
        /// <returns>The new password for the user if successful; otherwise, null.</returns>
        /// <remarks>This method will automatically generate an e-mail notification to the user whose password was reset. As a heightened security measure, the new password is inaccessible to the caller.</remarks>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Account Managers")]
        public bool ResetPassword(string username)
        {
            using (var password = UserManagement.ResetPassword(Membership.GetUser(username)))
            {
                if (password != null)
                {
                    // expire password
                    var user = GetUser(username);
                    user.PasswordExpired = true;
                    user = UpdateUser(user).User;
                    // prepare and send e-mail
                    PasswordResetMailMessage.Send(user, password);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Updates the password for the C-Access user.
        /// </summary>
        /// <param name="username">The user name of the account whose password is to be changed.</param>
        /// <param name="oldPassword">The current password for the C-Access user.</param>
        /// <param name="newPassword">The new password for the C-Access user.</param>
        /// <returns>true if the update was successful; otherwise, false.</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = @"NYCCFB\C-Access Administrators")]
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            MembershipUser user = Membership.GetUser(username);
            if (user != null && user.ChangePassword(oldPassword, newPassword))
            {
                using (CPSecurityEntities context = new CPSecurityEntities())
                {
                    var match = context.SecurityUserProfiles.FirstOrDefault(p => p.UserName == username);
                    if (match != null)
                    {
                        match.PasswordExpired = false;
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (OptimisticConcurrencyException)
                        {
                            context.Refresh(RefreshMode.ClientWins, match);
                            context.SaveChanges();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Generates a unique username for a new C-Access user.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="middleInitial">The user's middle initial.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="candidateID">The user's candidate ID.</param>
        /// <returns>A unique C-Access username for the user. If <paramref name="firstName"/>, <paramref name="lastName"/>, or <paramref name="candidateID"/> is null, returns null.</returns>
        /// <remarks>
        /// Usernames now in the format of "(first initial)(middle initial)(first 4 characters of last name)(cfis ID)[-optional unique number]" for users with middle initials, and "(first initial)(first 5 characters of last name)(cfis ID)[-optional unique number]" for users without.
        /// </remarks>
        public string GenerateUserName(string firstName, char? middleInitial, string lastName, string candidateID)
        {
            return UserManagement.GenerateUserName(firstName, middleInitial, lastName, candidateID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsOfflineDataException(Exception e)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Gets the election cycles that a C-Access user account is explicitly authorized for.
        /// </summary>
        /// <param name="username">The C-Access user to query.</param>
        /// <returns>A collection of election cycles that the specified user is explicitly authorized to access.</returns>
        /// <remarks>Unrestricted users will result in an empty collection.</remarks>
        internal static List<string> GetExplicitElectionCycles(string username)
        {
            using (CPSecurityEntities context = new CPSecurityEntities())
            {
                return (from o in context.SecurityUserElectionCycles
                        where o.UserName == username
                        select o.ElectionCycle).ToList();
            }
        }
    }
}
