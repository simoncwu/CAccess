namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to financial summary data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Returns a candidate's campaign financial summary for a given election cycle.
		/// </summary>
		/// <param name="candidateID">The desired candidate's CFIS ID.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The specified candidate's campaign financial summary for the specified election cycle.</returns>
		public FinancialSummary GetFinancialSummary(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetFinancialSummary(candidateID, electionCycle); }
		}
	}
}