using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <summary>
    /// Provides data for <see cref="AccountManagementControl"/> management events.
    /// </summary>
    public class AccountManagementEventArgs : EventArgs
    {
        /// <summary>
        /// The candidate affected by the account management operation.
        /// </summary>
        readonly Candidate _candidate;

        /// <summary>
        /// Gets the candidate affected by the account management operation.
        /// </summary>
        public Candidate Candidate
        {
            get { return _candidate; }
        }

        /// <summary>
        /// The user affected by the account management operation.
        /// </summary>
        readonly MembershipUser _user;

        /// <summary>
        /// Gets the user affected by the account management operation.
        /// </summary>
        public MembershipUser User
        {
            get { return _user; }
        }

        /// <summary>
        /// A message to be passed between <see cref="AccountManagementControl"/> objects.
        /// </summary>
        readonly string _description;

        /// <summary>
        /// Gets a message to be passed between <see cref="AccountManagementControl"/> objects.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementEventArgs"/> class.
        /// </summary>
        public AccountManagementEventArgs() : this(null, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementEventArgs"/> class with the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate affected by the account management operation.</param>
        public AccountManagementEventArgs(Candidate candidate) : this(candidate, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementEventArgs"/> class with the specified user.
        /// </summary>
        /// <param name="user">The user affected by the account management operation.</param>
        public AccountManagementEventArgs(MembershipUser user) : this(null, user, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementEventArgs"/> class with the specified candidate and user.
        /// </summary>
        /// <param name="candidate">The candidate affected by the account management operation.</param>
        /// <param name="user">The user affected by the account management operation.</param>
        /// <param name="description">A <see cref="String"/> describing the account management operation.</param>
        public AccountManagementEventArgs(Candidate candidate, MembershipUser user, string description)
        {
            _candidate = candidate;
            _user = user;
            _description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManagementEventArgs"/> class with the specified user and message.
        /// </summary>
        /// <param name="user">The user affected by the account management operation.</param>
        /// <param name="description">A <see cref="String"/> describing the account management operation.</param>
        public AccountManagementEventArgs(MembershipUser user, string description) : this(null, user, description) { }
    }
}
