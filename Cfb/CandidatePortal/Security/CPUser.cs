using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.Security
{
    /// <summary>
    /// Represents a C-Access Candidate Portal user account.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class CPUser
    {
        /// <summary>
        /// A collection of the election cycles the user is explicitly authorized to access.
        /// </summary>
        [DataMember(Name = "ElectionCycles")]
        private readonly List<string> _electionCycles;

        /// <summary>
        /// Gets a collection of the election cycles the user is explicitly authorized to access.
        /// </summary>
        public List<string> ElectionCycles
        {
            get { return _electionCycles; }
        }

        /// <summary>
        /// A collection of the single sign-on enabled applications that the user is authorized to access.
        /// </summary>
        [DataMember(Name = "Applications")]
        private readonly List<Application> _applications;

        /// <summary>
        /// Gets a collection of the single sign-on enabled applications that the user is authorized to access.
        /// </summary>
        public List<Application> Applications
        {
            get { return _applications; }
        }

        /// <summary>
        /// The login name for the C-Access user account.
        /// </summary>
        [DataMember(Name = "UserName")]
        private readonly string _username;

        /// <summary>
        /// Gets the login name for the C-Access user account.
        /// </summary>
        public string UserName
        {
            get { return _username; }
        }

        /// <summary>
        /// Gets or sets whether or not the user has implicit access to all election cycles.
        /// </summary>
        [DataMember]
        public bool ImplicitElectionCycles { get; set; }

        /// <summary>
        /// Gets or sets the e-mail address for the user.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name for the user.
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the candidate represented by the account.
        /// </summary>
        [DataMember]
        public string Cid { get; set; }

        /// <summary>
        /// Gets or sets whether or not the password for the account has expired.
        /// </summary>
        [DataMember]
        public bool PasswordExpired { get; set; }

        /// <summary>
        /// Gets or sets whether or not the account is enabled.
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets whether or not the user is locked out.
        /// </summary>
        [DataMember]
        public bool LockedOut { get; set; }

        /// <summary>
        /// Gets or sets the date when the user was created.
        /// </summary>
        [DataMember]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the user was last active.
        /// </summary>
        [DataMember]
        public DateTime? LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the user was last locked out.
        /// </summary>
        [DataMember]
        public DateTime? LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the user last logged in.
        /// </summary>
        [DataMember]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the user's password was last changed.
        /// </summary>
        [DataMember]
        public DateTime? LastPasswordChangedDate { get; set; }

        /// <summary>
        /// Gets or sets the account's access rights.
        /// </summary>
        [DataMember]
        public CPUserRights UserRights { get; set; }

        [DataMember]
        public EntityType SourceType { get; set; }

        /// <summary>
        /// Gets or sets the committee ID of the CFIS source contact associated with the account.
        /// </summary>
        [DataMember]
        public char? SourceCommitteeID { get; set; }

        /// <summary>
        /// Gets or sets the election cycle for which the committee of the CFIS source contact associated with the account is authorized.
        /// </summary>
        [DataMember]
        public string SourceElectionCycle { get; set; }

        /// <summary>
        /// Gets or sets the committee-relative ID of the CFIS source liaison associated with the account.
        /// </summary>
        [DataMember]
        public byte? SourceLiaisonID { get; set; }

        /// <summary>
        /// Gets whether or not the user is a candidate.
        /// </summary>
        public bool IsCandidate
        {
            get { return this.SourceType == EntityType.Candidate; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CPUser"/> class.
        /// </summary>
        /// <param name="username">The login name for the account.</param>
        public CPUser(string username)
        {
            _username = username;
            _electionCycles = new List<string>();
            _applications = new List<Application>();
        }

        /// <summary>
        /// Gets whether or not the account is authorized for a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle being accessed.</param>
        /// <returns>true if the account is authorized for the specified election cycle; otherwise, false.</returns>
        public bool IsAuthorizedFor(string electionCycle)
        {
            return _electionCycles.Contains(electionCycle);
        }

        /// <summary>
        /// Persists changes to the C-Access Security user.
        /// </summary>
        /// <returns>true if the changes were saved successfully; otherwise, false.</returns>
        public bool Save()
        {
            var result = CPSecurity.Provider.UpdateUser(this);
            if (result.Succeeded)
            {
                Refresh(result.User);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Resets the C-Access user's password to a new randomly generated password.
        /// </summary>
        /// <returns>true if the password was successfully reset; otherwise, false.</returns>
        /// <remarks>This method will automatically generate an e-mail notification to the user. As a heightened security measure, the new password is inaccessible to the caller.</remarks>
        public bool ResetPassword()
        {
            if (CPSecurity.Provider.ResetPassword(_username))
            {
                Refresh();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the password for the C-Access user.
        /// </summary>
        /// <param name="oldPassword">The current password for the C-Access user.</param>
        /// <param name="newPassword">The new password for the C-Access user.</param>
        /// <returns>true if the update was successful; otherwise, false.</returns>
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (CPSecurity.Provider.ChangePassword(_username, oldPassword, newPassword))
            {
                Refresh();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Refreshes the current user with data from the security provider.
        /// </summary>
        /// <param name="source">A source <see cref="CPUser"/> object to refresh the current user with.</param>
        public void Refresh(CPUser source = null)
        {
            if ((source = source ?? CPSecurity.Provider.GetUser(_username)) == null)
                return;
            this.Cid = source.Cid;
            this.CreationDate = source.CreationDate;
            this.DisplayName = source.DisplayName;
            this.Email = source.Email;
            this.Enabled = source.Enabled;
            this.LastActivityDate = source.LastActivityDate;
            this.LastLockoutDate = source.LastLockoutDate;
            this.LastLoginDate = source.LastLoginDate;
            this.LastPasswordChangedDate = source.LastPasswordChangedDate;
            this.LockedOut = source.LockedOut;
            this.PasswordExpired = source.PasswordExpired;
            this.SourceCommitteeID = source.SourceCommitteeID;
            this.SourceElectionCycle = source.SourceElectionCycle;
            this.SourceLiaisonID = source.SourceLiaisonID;
            this.SourceType = source.SourceType;
            this.UserRights = source.UserRights;
            this.ImplicitElectionCycles = source.ImplicitElectionCycles;
            _applications.Clear();
            _applications.AddRange(source._applications);
            _electionCycles.Clear();
            _electionCycles.AddRange(source._electionCycles);
        }
    }
}
