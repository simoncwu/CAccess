using System.Collections.Generic;
namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to committee data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves all authorized committees for a candidate in a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
		public AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetAuthorizedCommittees(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves the CFIS ID of a candidate's primary committee for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate desired.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The CFIS ID of the candidate's primary committee if found, else null.</returns>
		public char? GetPrimaryCommitteeID(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetPrimaryCommitteeID(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves committees for a candidate.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate desired.</param>
		/// <param name="committeeID">The ID of a committee to search for.</param>
		/// <returns>A collection of all committees on record for the specified candidate with the specified committee ID (if provided).</returns>
		public List<Committee> GetCommittees(string candidateID, char? committeeID = null)
		{
			using (DataClient client = new DataClient()) { return client.GetCommittees(candidateID, committeeID); }
		}
	}
}