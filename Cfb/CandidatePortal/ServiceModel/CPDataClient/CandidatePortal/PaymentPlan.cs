namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to payment plan data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves the payment plan on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		public PaymentPlan GetPaymentPlan(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetPaymentPlan(candidateID, electionCycle); }
		}
	}
}