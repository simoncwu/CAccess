using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to submission document data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a generic collection of all certification documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose certification documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all certification documents on record for the specified candidate and election cycle.</returns>
		public CertificationHistory GetCertificationDocuments(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetCertificationDocuments(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a generic collection of all filer registration documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filer registration documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filer registration documents on record for the specified candidate and election cycle.</returns>
		public FilerRegistrationHistory GetFilerRegistrationDocuments(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetFilerRegistrations(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a generic collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose verifications of terminated candidacy are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.</returns>
		public TerminationHistory GetTerminationVerifications(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetTerminationVerifications(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a generic collection of all pre-election disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose pre-election disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all pre-election disclosure statements on record for the specified candidate and election cycle.</returns>
		public PreElectionDisclosureHistory GetPreElectionDisclosures(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetPreElectionDisclosures(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="election">The election cycle in which to search.</param>
		/// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
		public StatementReviews GetPrincipalStatementReviews(string candidateID, Election election)
		{
			using (DataClient client = new DataClient()) { return client.GetPrincipalStatementReviews(candidateID, election != null ? election.Cycle : null); }
		}

		/// <summary>
		/// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="election">The election cycle in which to search.</param>
		/// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
		public StatementReviews GetStatementReviews(string candidateID, Election election)
		{
			using (DataClient client = new DataClient()) { return client.GetStatementReviews(candidateID, election != null ? election.Cycle : null); }
		}

		/// <summary>
		/// Retrieves a generic collection of all statements of need on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statements of need are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all statements of need on record for the specified candidate and election cycle.</returns>
		public StatementOfNeedHistory GetStatementsOfNeed(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetStatementsOfNeed(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a generic collection of all filing disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filing disclosure statements on record for the specified candidate and election cycle.</returns>
		public DisclosureStatementHistories GetDisclosureStatements(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetDisclosureStatements(candidateID, electionCycle);
			}
		}

		/// <summary>
		/// Retrieves a generic collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose C-SMART/IDS requests are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.</returns>
		public CsmartIdsRequestHistory GetCsmartIdsRequests(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetCsmartIdsRequests(candidateID, electionCycle); }
		}

		/// <summary>
		/// Retrieves a generic collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.</returns>
		public CoibReceiptHistory GetCoibReceipts(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetCoibReceipts(candidateID, electionCycle); }
		}
	}
}