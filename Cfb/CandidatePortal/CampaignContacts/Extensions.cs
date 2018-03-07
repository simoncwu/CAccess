using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.CampaignContacts
{
    /// <summary>
    /// Campaign contact extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Retrieves the description of an ineligible status.
        /// </summary>
        /// <param name="eligibility">The eligibility status to describe.</param>
        /// <returns>The <see cref="String"/> description of <paramref name="eligibility"/>.</returns>
        public static string GetDescription(this AccountEligibility eligibility)
        {
            switch (eligibility)
            {
                case AccountEligibility.Eligible:
                    return string.Empty;
                case AccountEligibility.IneligibleDuplicateEmail:
                    return "Duplicate e-mail address";
                case AccountEligibility.IneligibleDuplicateAccount:
                case AccountEligibility.IneligibleExists:
                    return "Account already exists";
                case AccountEligibility.IneligibleNoEmail:
                    return "No e-mail address";
                case AccountEligibility.IneligibleNoFirstName:
                    return "No first name";
                case AccountEligibility.IneligibleNoLastName:
                    return "No last name";
                case AccountEligibility.IneligibleNull:
                    return "Contact could not be retrieved";
                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// Retrieves the corresponding C-Access ID for a C-Access user account.
        /// </summary>
        /// <param name="user">The user to retrieve an ID for.</param>
        /// <returns>The corresponding C-Access ID for the account.</returns>
        internal static string GetCaid(this CPUser user)
        {
            if (user == null)
                return null;
            switch (user.SourceType)
            {
                case EntityType.Candidate:
                    return AccountAnalysis.CandidatePrefix + user.Cid;
                case EntityType.Treasurer:
                    return AccountAnalysis.TreasurerPrefix + user.SourceCommitteeID + user.SourceElectionCycle;
                case EntityType.Liaison:
                    return AccountAnalysis.LiaisonPrefix + user.SourceCommitteeID + user.SourceLiaisonID;
                case EntityType.Consultant:
                    return AccountAnalysis.ConsultantPrefix + user.SourceCommitteeID + user.SourceLiaisonID;
                default:
                    return null;
            }
        }
    }
}
