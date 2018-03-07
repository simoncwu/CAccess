using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient.SecurityService;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityClient
{
    /// <summary>
    /// Provides access to C-Access Security data.
    /// </summary>
    public class SecurityProvider : ISecurityProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityProvider"/> class.
        /// </summary>
        public SecurityProvider() { }

        #region ISecurityProvider Members

        #region Basic Security Info

        /// <summary>
        /// Gets a <see cref="CPUserRights"/> value that represents the access rights granted to the specified user.
        /// </summary>
        /// <param name="userName">The name of the user to get access rights for.</param>
        /// <returns>A <see cref="CPUserRights"/> value indicating the total access rights granted to the specified user.</returns>
        public CPUserRights GetUserRights(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetUserRights(userName); }
        }

        /// <summary>
        /// Gets a value that indicates whether or not a user has been granted certain C-Access account rights and privileges.
        /// </summary>
        /// <param name="userName">The name of the user to analyze.</param>
        /// <param name="rights">A <see cref="CPUserRights"/> value indicating the security rights to check for.</param>
        /// <returns>true if <paramref name="userName"/> has been granted the rights indicated by <paramref name="rights"/>; otherwise, false.</returns>
        public bool HasUserRights(string userName, CPUserRights rights)
        {
            using (SecurityClient client = new SecurityClient()) { return client.HasUserRights(userName, rights); }
        }

        /// <summary>
        /// Gets the candidate ID associated with a C-Access user account.
        /// </summary>
        /// <param name="username">The name of the user to query.</param>
        /// <returns>The candidate ID associated with the specified user.</returns>
        public string GetCid(string username)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetCid(username); }
        }

        /// <summary>
        /// Gets the friendly display name for a C-Access user account.
        /// </summary>
        /// <param name="userName">The C-Access user to query.</param>
        /// <returns>The user friendly display name (e.g., full name) on record for the specified user.</returns>
        public string GetDisplayName(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetDisplayName(userName); }
        }

        /// <summary>
        /// Gets the e-mail address for the user.
        /// </summary>
        /// <param name="userName">The C-Access user to query.</param>
        /// <returns>The e-mail address on record for the specified user.</returns>
        public string GetEmail(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetEmail(userName); }
        }

        /// <summary>
        /// Gets the election cycles that a C-Access user account is authorized for.
        /// </summary>
        /// <param name="userName">The C-Access user to query.</param>
        /// <returns>A collection of election cycles that the specified user is explicitly authorized to access.</returns>
        public List<string> GetAuthorizedElectionCycles(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetAuthorizedElectionCycles(userName); }
        }

        /// <summary>
        /// Gets a collection of C-Access user accounts for a specific campaign.
        /// </summary>
        /// <param name="candidateID">The ID of the active candidate in the campaign.</param>
        /// <param name="electionCycle">The election cycle the campaign is active in, or null for all election cycles.</param>
        /// <param name="includeDisabled">Indicates whether disabled accounts should also be included in the results.</param>
        /// <returns>A collection of C-Access accounts for the specified campaign.</returns>
        public List<CPUser> GetCampaignUsers(string candidateID, string electionCycle, bool includeDisabled)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetCampaignUsers(candidateID, electionCycle, includeDisabled); }
        }

        /// <summary>
        /// Gets a C-Access user account.
        /// </summary>
        /// <param name="userName">The login name for the account to retrieve.</param>
        /// <returns>The C-Access user account matching the specified username if found; otherwise, null.</returns>
        public CPUser GetUser(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetUser(userName); }
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
            using (SecurityClient client = new SecurityClient()) { return client.FindUsers(name, email, candidateID, electionCycle, groupID, disabled, lockedOut, used, createdStartDate, createdEndDate); }
        }

        #endregion

        #region Single Sign-On

        /// <summary>
        /// Creates a new login token for authenticating a specific user for a specific application.
        /// </summary>
        /// <param name="userName">The login name of the user requesting authentication.</param>
        /// <param name="applicationID">The unique identifier of the application requesting authentication.</param>
        /// <returns>A new login token if the specified user has access to the specified application; otherwise, null.</returns>
        public Token CreateToken(string userName, byte applicationID)
        {
            using (SecurityClient client = new SecurityClient()) { return client.CreateToken(userName, applicationID); }
        }

        /// <summary>
        /// Checks whether or not a login token exists and is valid for authentication.
        /// </summary>
        /// <param name="tokenID">The unique identifier of the token to validate.</param>
        /// <returns>The login name of the user attempting to authenticate with the specified token, if valid; otherwise, null.</returns>
        /// <remarks>The login token is automatically discarded once validated.</remarks>
        public string ValidateToken(Guid tokenID)
        {
            using (SecurityClient client = new SecurityClient()) { return client.ValidateToken(tokenID); }
        }

        /// <summary>
        /// Gets information about a specific SSO-enabled application.
        /// </summary>
        /// <param name="applicationID">The unique identifier of the application.</param>
        /// <returns>The application requested if found; otherwise, null.</returns>
        public Application GetApplication(byte applicationID)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetApplication(applicationID); }
        }

        #endregion

        #region Privileged Operations

        /// <summary>
        /// Gets a collection of all C-Access Security groups.
        /// </summary>
        /// <returns>A collection of all C-Access Security groups.</returns>
        public List<CPGroup> GetGroups()
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetGroups(); }
        }

        /// <summary>
        /// Gets a C-Access Security group.
        /// </summary>
        /// <param name="groupName">The name of the group to retrieve.</param>
        /// <returns>The security group if found; otherwise, null.</returns>
        public CPGroup GetGroup(string groupName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetGroup(groupName); }
        }

        /// <summary>
        /// Gets a collection of users who are members of the specified security group.
        /// </summary>
        /// <param name="groupName">The group to get the list of members for.</param>
        /// <returns>A collection of the names of all the users who are members of the specified security group.</returns>
        public List<string> GetGroupMembers(string groupName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetGroupMembers(groupName); }
        }

        /// <summary>
        /// Creates a new security group.
        /// </summary>
        /// <param name="groupName">The name of the group to create.</param>
        /// <returns>A <see cref="CPGroup"/> instance representing the new group if created successfully; otherwise, null.</returns>
        public CPGroup CreateGroup(string groupName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.CreateGroup(groupName); }
        }

        /// <summary>
        /// Deletes a security group.
        /// </summary>
        /// <param name="groupName">The name of the group to delete.</param>
        /// <returns>true if <paramref name="groupName"/> was deleted successfully; otherwise, false.</returns>
        public bool DeleteGroup(string groupName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.DeleteGroup(groupName); }
        }

        /// <summary>
        /// Updates a C-Access Security group.
        /// </summary>
        /// <param name="group">The group to update.</param>
        /// <returns>The resultant <see cref="CPGroup"/> after it has been updated.</returns>
        public CPGroup UpdateGroup(CPGroup group)
        {
            using (SecurityClient client = new SecurityClient()) { return client.UpdateGroup(group); }
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
        public CPUser CreateUser(string firstName, char? middleInitial, string lastName, string candidateID, string password, string email, string creator, EntityType type = EntityType.Generic, char? committeeID = null, string electionCycle = null, byte? liaisonID = null, string username = null)
        {
            using (SecurityClient client = new SecurityClient()) { return client.CreateUser(firstName, middleInitial, lastName, candidateID, password, email, creator, type, committeeID, electionCycle, liaisonID, username); }
        }

        /// <summary>
        /// Deletes a C-Access user account.
        /// </summary>
        /// <param name="userName">The name of the user to delete.</param>
        /// <returns>true if <paramref name="userName"/> was deleted successfully; otherwise, false.</returns>
        public bool DeleteUser(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.DeleteUser(userName); }
        }

        /// <summary>
        /// Updates a C-Access Security user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>An <see cref="UpdateResult"/> object representing the result of the synchronization.</returns>
        public UpdateResult UpdateUser(CPUser user)
        {
            using (SecurityClient client = new SecurityClient()) { return client.UpdateUser(user); }
        }

        /// <summary>
        /// Synchronizes a user account with the currently existing data for the CFIS source contact associated with the account.
        /// </summary>
        /// <param name="userName">The name of the user to synchronize.</param>
        /// <returns>An <see cref="UpdateResult"/> representing the result of the synchronization.</returns>
        public UpdateResult SynchronizeUser(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.SynchronizeUser(userName); }
        }

        /// <summary>
        /// Adds a user to the specified groups.
        /// </summary>
        /// <param name="userName">The name of the user to add to the specified groups.</param>
        /// <param name="groupIDs">An array of group IDs to add the specified user to.</param>
        /// <returns>true if <paramref name="userName"/> was added successfully; otherwise, false.</returns>
        public bool AddUserToGroups(string userName, List<byte> groupIDs)
        {
            using (SecurityClient client = new SecurityClient()) { return client.AddUserToGroups(userName, groupIDs); }
        }

        /// <summary>
        /// Removes a user from the specified groups.
        /// </summary>
        /// <param name="userName">The name of the user to remove from the specified groups.</param>
        /// <param name="groupIDs">An array of group IDs to remote the specified user from.</param>
        /// <returns>true if <paramref name="userName"/> was removed successfully; otherwise, false.</returns>
        public bool RemoveUserFromGroups(string userName, List<byte> groupIDs)
        {
            using (SecurityClient client = new SecurityClient()) { return client.RemoveUserFromGroups(userName, groupIDs); }
        }

        /// <summary>
        /// Gets a list of the groups that a user is in.
        /// </summary>
        /// <param name="userName">The name of the user to return a list of groups for.</param>
        /// <returns>A collection containing the names of all the groups that the specified user is in.</returns>
        public List<CPGroup> GetGroupMembership(string userName)
        {
            using (SecurityClient client = new SecurityClient()) { return client.GetGroupMembership(userName); }
        }

        /// <summary>
        /// Resets a C-Access user's password to a new randomly generated password.
        /// </summary>
        /// <param name="username">The user name of the account whose password is to be reset.</param>
        /// <returns>true if the password was successfully reset for the specified user; otherwise, false.</returns>
        /// <remarks>This method will automatically generate an e-mail notification to the user whose password was reset. As a heightened security measure, the new password is inaccessible to the caller.</remarks>
        public bool ResetPassword(string username)
        {
            using (SecurityClient client = new SecurityClient()) { return client.ResetPassword(username); }
        }

        /// <summary>
        /// Updates the password for the C-Access user.
        /// </summary>
        /// <param name="username">The user name of the account whose password is to be changed.</param>
        /// <param name="oldPassword">The current password for the C-Access user.</param>
        /// <param name="newPassword">The new password for the C-Access user.</param>
        /// <returns>true if the update was successful; otherwise, false.</returns>
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (SecurityClient client = new SecurityClient()) { return client.ChangePassword(username, oldPassword, newPassword); }
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
            using (SecurityClient client = new SecurityClient()) { return client.GenerateUserName(firstName, middleInitial, lastName, candidateID); }
        }

        #endregion

        /// <summary>
        /// Determines whether or not an exception indicates an offline data provider.
        /// </summary>
        /// <param name="e">The exception to analyze.</param>
        /// <returns>true if <paramref name="e"/> indicates that a data provider is offline.</returns>
        public bool IsOfflineDataException(Exception e)
        {
            return e as FaultException<OfflineDataException> != null;
        }

        #endregion
    }
}
