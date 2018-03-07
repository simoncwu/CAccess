using System;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to filing deadline data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves filing dates and requirements for a particular candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing dates and requirements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all filing dates and requirements on record for the specified candidate and election cycle.</returns>
		public FilingDeadlines GetFilingDeadlines(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetFilingDeadlines(candidateID, electionCycle); }
		}
	}
}