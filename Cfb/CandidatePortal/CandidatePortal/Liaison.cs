using System.Collections.Generic;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Methods for retrieving a campaign liaison or consultant.
    /// </summary>
    public partial class Cfis
    {
        /// <summary>
        /// Retrieves all campaign liaisons on record for the specified candidate and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose liaisons are to be retrieved.</param>
        /// <param name="committeeID">The ID of the committee whose liaisons are to be retrieved.</param>
        /// <returns>A collection of all campaign liaisons on record for the specified candidate and committee, indexed by liaison ID.</returns>
        public static Dictionary<byte, Liaison> GetLiaisonsByCommittee(string candidateID, char committeeID)
        {
            return CPProviders.DataProvider.GetLiaisonsByCommittee(candidateID, committeeID);
        }
    }
}
