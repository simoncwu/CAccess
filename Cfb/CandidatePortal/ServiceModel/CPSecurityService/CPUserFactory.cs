using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPSecurityService.Sso;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService
{
    /// <summary>
    /// Factory for creating C-Access Security user account instances.
    /// </summary>
    internal class CPUserFactory
    {
        /// <summary>
        /// Hidden constructor.
        /// </summary>
        private CPUserFactory() { }

        /// <summary>
        /// Creates a new <see cref="CPUser"/> instance using C-Access Security profile data.
        /// </summary>
        /// <param name="profile">The profile to use.</param>
        /// <returns>A new C-Access user account instance using the specified C-Access Security profile data if valid; otherwise, null.</returns>
        public static CPUser CreateUser(SecurityUserProfile profile)
        {
            CPUser user = null;
            if (profile != null)
            {
                user = CreateUser(Membership.GetUser(profile.UserName));
                if (user != null)
                {
                    user.DisplayName = profile.DisplayName;
                    user.Cid = profile.CandidateId;
                    user.PasswordExpired = profile.PasswordExpired;

                    // load additional user data
                    user.ElectionCycles.AddRange(CPSecurity.Provider.GetAuthorizedElectionCycles(user.UserName));
                    user.ImplicitElectionCycles = !SecurityService.GetExplicitElectionCycles(user.UserName).Any();
                    user.UserRights = CPSecurity.Provider.GetUserRights(user.UserName);
                    using (CPSecurityEntities context = new CPSecurityEntities())
                    {
                        foreach (var app in context.SecuritySsoApplications)
                        {
                            if (user.UserRights.HasFlag((CPUserRights)app.UserRights))
                                user.Applications.Add(ApplicationFactory.CreateApplication(app));
                        }
                    }

                    // get CFIS source info
                    user.SourceType = Enum.IsDefined(typeof(EntityType), profile.CfisType) ? (EntityType)profile.CfisType : EntityType.Generic;
                    if (!string.IsNullOrWhiteSpace(profile.CfisCommitteeID))
                        user.SourceCommitteeID = profile.CfisCommitteeID.ToCharArray()[0];
                    byte liaisonID;
                    if (byte.TryParse(profile.CfisCommitteeContactID, out liaisonID))
                    {
                        user.SourceLiaisonID = liaisonID;
                        // attempt to determine election cycle of associated committee
                        if (user.SourceCommitteeID.HasValue)
                        {
                            char commID = user.SourceCommitteeID.Value;
                            var elections = from ec in CPProviders.DataProvider.GetActiveElectionCycles(profile.CandidateId, CPProviders.SettingsProvider.MinimumElectionCycle)
                                            let comm = CPProviders.DataProvider.GetAuthorizedCommittees(profile.CandidateId, ec)
                                            where comm.Committees.ContainsKey(commID)
                                            select ec;
                            if (elections.Any())
                                user.SourceElectionCycle = elections.First();
                        }
                    }
                    else
                    {
                        user.SourceElectionCycle = profile.CfisCommitteeContactID;
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Creats a new <see cref="CPUser"/> instance using .NET Membership data.
        /// </summary>
        /// <param name="user">The membership user to use.</param>
        /// <returns>A new C-Access user account instance using .NET Membership data if valid; otherwise, null.</returns>
        public static CPUser CreateUser(MembershipUser user)
        {
            if (user == null)
                return null;
            DateTime creationDate = user.CreationDate;
            CPUser cpuser = new CPUser(user.UserName)
            {
                Email = user.Email,
                Enabled = user.IsApproved,
                LockedOut = user.IsLockedOut,
                CreationDate = creationDate
            };
            if (user.LastActivityDate != creationDate)
                cpuser.LastActivityDate = user.LastActivityDate;
            if (user.LastLockoutDate != creationDate)
                cpuser.LastLockoutDate = user.LastLockoutDate;
            if (user.LastLoginDate != creationDate)
                cpuser.LastLoginDate = user.LastLoginDate;
            if (user.LastPasswordChangedDate != creationDate)
                cpuser.LastPasswordChangedDate = user.LastPasswordChangedDate;
            return cpuser;
        }
    }
}
