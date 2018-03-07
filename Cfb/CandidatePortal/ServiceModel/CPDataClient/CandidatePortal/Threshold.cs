namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to threshold data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a collection of the specified candidate's complete threshold status history for the specified election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of the specified candidate's complete threshold status history for the specified election cycle.</returns>
		public ThresholdHistory GetThresholdHistory(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetThresholdHistory(candidateID, electionCycle); }
		}
	}
}