using System.Collections.Generic;
using System.Runtime.Serialization;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A Campaign Messages Online audit review message.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CmoAuditReview
	{
		/// <summary>
		/// The candidate ID of the audit review message.
		/// </summary>
		[DataMember(Name = "CandidateID")]
		private readonly string _candidateID;

		/// <summary>
		/// Gets the candidate ID of the audit review message.
		/// </summary>
		public string CandidateID
		{
			get { return _candidateID; }
		}

		/// <summary>
		/// The unique-by-candidate ID of the audit review message.
		/// </summary>
		[DataMember(Name = "MessageID")]
		private readonly int _messageID;

		/// <summary>
		/// Gets the unique-by-candidate ID of the audit review message.
		/// </summary>
		public int MessageID
		{
			get { return _messageID; }
		}

		/// <summary>
		/// Gets the review number of the audit review message.
		/// </summary>
		[DataMember]
		public byte Number { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CmoAuditReview"/> class.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate reviewed.</param>
		/// <param name="messageID">The unique-by-candidate message ID.</param>
		internal CmoAuditReview(string candidateID, int messageID)
		{
			_candidateID = candidateID;
			_messageID = messageID;
		}

		/// <summary>
		/// Gets a collection of IDs for all statement review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public static Dictionary<byte, string> GetStatementReviewMessageIDs(string candidateID, string electionCycle)
		{
			return CmoProviders.DataProvider.GetStatementReviewMessageIDs(candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all compliance visit messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public static Dictionary<byte, string> GetComplianceVisitMessageIDs(string candidateID, string electionCycle)
		{
			return CmoProviders.DataProvider.GetComplianceVisitMessageIDs(candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all Doing Business review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public static Dictionary<byte, string> GetDoingBusinessReviewMessageIDs(string candidateID, string electionCycle)
		{
			return CmoProviders.DataProvider.GetDoingBusinessReviewMessageIDs(candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all post election audit tolling messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <param name="far">true to specify Final Audit Report tolling; otherwise, false to specify Draft Audit Report tolling.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public static Dictionary<int, string> GetTollingMessageIDs(string candidateID, string electionCycle, bool far)
		{
			return CmoProviders.DataProvider.GetTollingMessageIDs(candidateID, electionCycle, far);
		}

		/// <summary>
		/// Gets a collection of IDs for all post election audit reports for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all post election audit report CMO messages found.</returns>
		public static Dictionary<AuditReportType, string> GetAuditReportMessageIDs(string candidateID, string electionCycle)
		{
			return CmoProviders.DataProvider.GetAuditReportMessageIDs(candidateID, electionCycle);
		}
	}
}
