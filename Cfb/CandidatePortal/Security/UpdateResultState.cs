using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.Security
{
    /// <summary>
    /// Enumeration representing the various possible result states of a user update operation.
    /// </summary>
    [Flags]
    public enum UpdateResultState : byte
    {
        /// <summary>
        /// Indicates a successful update.
        /// </summary>
        Success = 0,
        /// <summary>
        /// Indicates that the e-mail address changed as a result of the update.
        /// </summary>
        EmailAddressChanged,
        /// <summary>
        /// Indicates that the user's display name changed as a result of the update.
        /// </summary>
        DisplayNameChanged,
        /// <summary>
        /// Indicates a failure to find the corresponding ASP.NET Membership user.
        /// </summary>
        MembershipUserNotFound,
        /// <summary>
        /// Indicates a failure to update ASP.NET Membership information.
        /// </summary>
        AspNetMembershipFailure,
        /// <summary>
        /// Indicates a failure to find a matching contact in CFIS.
        /// </summary>
        CfisContactNotFound,
        /// <summary>
        /// Indicates a failure to update the e-mail address due to the new address being in use by another campaign user.
        /// </summary>
        EmailAddressInUse,
        /// <summary>
        /// Indicates a failure to find a matching user profile in the security context.
        /// </summary>
        ProfileNotFound,
        /// <summary>
        /// Indicates a failure to update the user profile via the security context.
        /// </summary>
        ProfileFailure,
        /// <summary>
        /// Indicates a failure to find an authorized election cycle.
        /// </summary>
        ElectionCycleNotFound
    }
}
