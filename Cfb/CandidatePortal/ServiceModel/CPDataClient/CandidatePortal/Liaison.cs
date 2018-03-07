using System.Collections.Generic;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// Provides access to campaign liaison data by means of WCF client-server communication.
    /// </summary>
    partial class CPDataProvider
    {
        /// <summary>
        /// Retrieves all campaign liaisons on record for the specified candidate and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose liaisons are to be retrieved.</param>
        /// <param name="committeeID">The ID of the committee whose liaisons are to be retrieved.</param>
        /// <returns>A collection of all campaign liaisons on record for the specified candidate and committee, indexed by liaison ID.</returns>
        public Dictionary<byte, Liaison> GetLiaisonsByCommittee(string candidateID, char committeeID)
        {
            using (DataClient client = new DataClient()) { return client.GetLiaisons(candidateID, committeeID); }
        }
    }
}