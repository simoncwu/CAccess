namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Doing Business data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a collection of all completed Doing Business reviews on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Doing Business reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all completed Doing Business reviews for the specified candidate and election cycle.</returns>
		public DoingBusinessReviews GetDoingBusinessReviews(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetDoingBusinessReviews(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves all compliance visits on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose compliance visits are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A <see cref="ComplianceVisits"/> collection of all compliance visits on record for the specified candidate and election cycle.</returns>
		public ComplianceVisits GetComplianceVisits(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetComplianceVisits(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
		public StatementReviews GetPrincipalStatementReviews(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetPrincipalStatementReviews(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
		public StatementReviews GetStatementReviews(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetStatementReviews(candidateID, electionCycle); }
		}
	}
}