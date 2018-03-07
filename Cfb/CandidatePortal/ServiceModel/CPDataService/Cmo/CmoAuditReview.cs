using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;
using CmoCategory = Cfb.CandidatePortal.Cmo.CmoCategory;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Gets a collection of IDs for all statement review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, string> GetStatementReviewMessageIDs(string candidateID, string electionCycle);

		/// <summary>
		/// Gets a collection of IDs for all compliance visit messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all compliance visit CMO messages found.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, string> GetComplianceVisitMessageIDs(string candidateID, string electionCycle);

		/// <summary>
		/// Gets a collection of IDs for all Doing Business review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all Doing Business review CMO messages found.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, string> GetDoingBusinessReviewMessageIDs(string candidateID, string electionCycle);

		/// <summary>
		/// Gets a collection of IDs for all post election audit reports for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all post election audit report CMO messages found.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<AuditReportType, string> GetAuditReportMessageIDs(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Gets a collection of IDs for all statement review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public Dictionary<byte, string> GetStatementReviewMessageIDs(string candidateID, string electionCycle)
		{
			return GetAuditReviewMessageIDs(CmoCategory.StatementReviewCategoryID, candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all compliance visit messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all compliance visit review CMO messages found.</returns>
		public Dictionary<byte, string> GetComplianceVisitMessageIDs(string candidateID, string electionCycle)
		{
			return GetAuditReviewMessageIDs(CmoCategory.ComplianceVisitCategoryID, candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all Doing Business review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all Doing Business review CMO messages found.</returns>
		public Dictionary<byte, string> GetDoingBusinessReviewMessageIDs(string candidateID, string electionCycle)
		{
			return GetAuditReviewMessageIDs(CmoCategory.DoingBusinessReviewCategoryID, candidateID, electionCycle);
		}

		/// <summary>
		/// Gets a collection of IDs for all post election audit reports for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all post election audit report CMO messages found.</returns>
		public Dictionary<AuditReportType, string> GetAuditReportMessageIDs(string candidateID, string electionCycle)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var categories = new List<byte> { CmoCategory.IdrCategoryID, CmoCategory.IdrInadequateCategoryID, CmoCategory.IdrAdditionalInadequateCategoryID, CmoCategory.DarCategoryID, CmoCategory.DarInadequateCategoryID, CmoCategory.DarAdditionalInadequateCategoryID, CmoCategory.FarCategoryID };
				var messages = from m in context.CmoMessages
							   join c in context.CmoCategories
							   on m.CmoCategory.CategoryId equals c.CategoryId
							   where m.CandidateId == candidateID && m.ElectionCycle == electionCycle && m.PostDate.HasValue && categories.Contains(m.CmoCategory.CategoryId)
							   group m by m.CmoCategory.CategoryId into mgroup
							   select new { CategoryID = mgroup.Key, MessageID = mgroup.Max(m => m.MessageId) };
				return (from m in messages.AsEnumerable().Select(m => new { AuditReportType = CmoCategory.ToAuditReportType(m.CategoryID), m.MessageID })
						where m.AuditReportType.HasValue
						select m).ToDictionary(m => m.AuditReportType.Value, m => CmoMessage.ToUniqueID(candidateID, m.MessageID));
			}
		}

		/// <summary>
		/// Gets a collection of IDs for all audit review messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="reviewCategoryID">The category ID of the desired audit review type.</param>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <returns>A collection of unique IDs for all audit review CMO messages found.</returns>
		private Dictionary<byte, string> GetAuditReviewMessageIDs(byte reviewCategoryID, string candidateID, string electionCycle)
		{
			if (!new[] { CmoCategory.StatementReviewCategoryID, CmoCategory.ComplianceVisitCategoryID, CmoCategory.DoingBusinessReviewCategoryID }.Contains(reviewCategoryID))
				return new Dictionary<byte, string>(0);
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				return (from m in context.CmoMessages
						join r in context.CmoAuditReviews
						on new { m.CandidateId, m.MessageId } equals new { r.CandidateId, r.MessageId }
						where m.PostDate.HasValue && m.CandidateId == candidateID && m.ElectionCycle == electionCycle && m.CmoCategory.CategoryId == reviewCategoryID
						group r by r.ReviewNumber into rgroup
						select new { ReviewNumber = rgroup.Key, MessageID = rgroup.Max(r => r.MessageId) }).ToDictionary(r => r.ReviewNumber, r => CmoMessage.ToUniqueID(candidateID, r.MessageID));
			}
		}
	}
}
