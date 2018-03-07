using System.Collections.Generic;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Methods for retrieving a candidate's profile.
    /// </summary>
    public partial class Cfis
    {
        /// <summary>
        /// Retrieves the full name of a candidate by CFIS ID.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to find.</param>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the specified candidate.</returns>
        public static string GetCandidateName(string candidateID, bool formal)
        {
            return CPProviders.DataProvider.GetCandidateName(candidateID, formal);
        }

        /// <summary>
        /// Retrieves basic profile information for a candidate.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <returns>The requested candidate's profile information if found, otherwise null.</returns>
        public static Candidate GetCandidate(string candidateID)
        {
            return CPProviders.DataProvider.GetCandidate(candidateID);
        }

        /// <summary>
        /// Retrieves basic profile information for all known candidates.
        /// </summary>
        /// <returns>A collection of <see cref="Candidate"/> objects representing all known candidates indexed by CFIS ID.</returns>
        public static Dictionary<string, Candidate> GetCandidates()
        {
            return CPProviders.DataProvider.GetCandidates();
        }
    }
}
