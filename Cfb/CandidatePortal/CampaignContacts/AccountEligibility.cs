using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.CampaignContacts
{
    /// <summary>
    /// Enumeration for C-Access account eligibility possibilities.
    /// </summary>
    public enum AccountEligibility : byte
    {
        /// <summary>
        /// Eligible.
        /// </summary>
        Eligible,
        /// <summary>
        /// Ineligible due to null object.
        /// </summary>
        IneligibleNull,
        /// <summary>
        /// Ineligible due to missing e-mail address.
        /// </summary>
        IneligibleNoEmail,
        /// <summary>
        /// Ineligible due to missing first name.
        /// </summary>
        IneligibleNoFirstName,
        /// <summary>
        /// Ineligible due to missing last name.
        /// </summary>
        IneligibleNoLastName,
        /// <summary>
        /// Ineligible due to already existing account.
        /// </summary>
        IneligibleExists,
        /// <summary>
        /// Ineligible due to a resulting duplicate e-mail address.
        /// </summary>
        IneligibleDuplicateEmail,
        /// <summary>
        /// Ineligible due to a resulting duplicate account.
        /// </summary>
        IneligibleDuplicateAccount
    }
}
