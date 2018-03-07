using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Security;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// Lightweight replacement class for <see cref="CPUser"/>.
    /// </summary>
    internal struct ReportableUser
    {
        /// <summary>
        /// Gets the ID of the user's candidate.
        /// </summary>
        public string Cid { get; private set; }

        /// <summary>
        /// Gets the full name for the user's candidate.
        /// </summary>
        public string CandidateName { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the user's friendly display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the e-mail address on the account.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the date when the user last logged into C-Access.
        /// </summary>
        public DateTime? LoginDate { get; private set; }

        /// <summary>
        /// Gets whether or not the user account is locked out.
        /// </summary>
        public string LockedOut { get; private set; }

        /// <summary>
        /// Gets whether or not the user account is disabled.
        /// </summary>
        public string Disabled { get; private set; }

        /// <summary>
        /// Factory method for creating a <see cref="ReportableUser"/> object for use in reports.
        /// </summary>
        /// <param name="user">The source <see cref="CPUser"/> to create from.</param>
        /// <param name="candidateName">The name of the candidate with whom the user is associated.</param>
        /// <returns>A reportable user object representing the specified user.</returns>
        public static ReportableUser CreateReportableUser(CPUser user, string candidateName)
        {
            return user == null ? new ReportableUser() : new ReportableUser()
            {
                Cid = user.Cid,
                CandidateName = candidateName,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                LoginDate = user.LastLoginDate,
                LockedOut = user.LockedOut ? "Yes" : "No",
                Disabled = user.Enabled ? "No" : "Yes"
            };
        }
    }
}
