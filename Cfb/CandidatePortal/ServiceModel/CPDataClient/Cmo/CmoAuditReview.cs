using System.Collections.Generic;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Campaign Messages Online audit review data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Gets a collection of IDs for all statement review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public Dictionary<byte, string> GetStatementReviewMessageIDs(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetStatementReviewMessageIDs(candidateID, electionCycle); }
		}

		/// <summary>
		/// Gets a collection of IDs for all compliance visit messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all compliance visit CMO messages found.</returns>
		public Dictionary<byte, string> GetComplianceVisitMessageIDs(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetComplianceVisitMessageIDs(candidateID, electionCycle); }
		}
		/// <summary>
		/// Gets a collection of IDs for all Doing Business review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all Doing Business review CMO messages found.</returns>
		public Dictionary<byte, string> GetDoingBusinessReviewMessageIDs(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetDoingBusinessReviewMessageIDs(candidateID, electionCycle); }
		}

		/// <summary>
		/// Gets a collection of IDs for all post election audit reports for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all post election audit report CMO messages found.</returns>
		public Dictionary<AuditReportType, string> GetAuditReportMessageIDs(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetAuditReportMessageIDs(candidateID, electionCycle); }
		}
	}
}