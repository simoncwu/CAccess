using System.Collections.Generic;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// Provides access to candidate data by means of WCF client-server communication.
    /// </summary>
    partial class CPDataProvider
    {
        /// <summary>
        /// Retrieves basic profile information for all known candidates.
        /// </summary>
        /// <returns>A collection of <see cref="Candidate"/> objects representing all known candidates indexed by CFIS ID.</returns>
        public Dictionary<string, Candidate> GetCandidates()
        {
            using (DataClient client = new DataClient())
            {
                return client.GetCandidates();
            }
        }

        /// <summary>
        /// Retrieves basic profile information for all candidates active in a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <returns>A collection of <see cref="ActiveCandidate"/> objects representing all candidates active in the <paramref name="electionCycle"/> election cycle, indexed by CFIS ID.</returns>
        public Dictionary<string, ActiveCandidate> GetActiveCandidates(string electionCycle)
        {
            using (DataClient client = new DataClient())
            {
                return client.GetActiveCandidates(electionCycle);
            }
        }

        /// <summary>
        /// Retrieves the full name of a candidate by CFIS ID.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to find.</param>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the specified candidate.</returns>
        public string GetCandidateName(string candidateID, bool formal)
        {
            using (DataClient client = new DataClient())
            {
                return client.GetCandidateName(candidateID, formal);
            }
        }

        /// <summary>
        /// Retrieves basic profile information for a candidate.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <returns>The requested candidate's profile information if found, otherwise null.</returns>
        public Candidate GetCandidate(string candidateID)
        {
            using (DataClient client = new DataClient())
            {
                return client.GetCandidate(candidateID);
            }
        }

        /// <summary>
        /// Returns profile information for a candidate who is active in an election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's profile for the specified election cycle.</returns>
        public ActiveCandidate GetActiveCandidate(string candidateID, string electionCycle)
        {
            using (DataClient client = new DataClient()) { return client.GetActiveCandidate(candidateID, electionCycle); }
        }
    }
}