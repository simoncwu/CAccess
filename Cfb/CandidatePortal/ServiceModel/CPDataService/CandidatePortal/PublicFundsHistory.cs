using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.PaymentPlanTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a history of public funds determinations for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose public funds determination history is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A history of public funds determinations for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		PublicFundsHistory GetPublicFundsHistory(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a history of public funds determinations for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose public funds determination history is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A history of public funds determinations for the specified candidate and election cycle.</returns>
		public PublicFundsHistory GetPublicFundsHistory(string candidateID, string electionCycle)
		{
			// retrieve CMO message IDs
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var messages = from m in context.CmoMessages
							   join p in context.CmoAuditReviews
							   on new { m.CandidateId, m.MessageId } equals new { p.CandidateId, p.MessageId }
							   where m.CandidateId == candidateID && m.ElectionCycle == electionCycle && m.PostDate.HasValue
							   group p by p.ReviewNumber into pgroup
							   select new { Run = pgroup.Key, MessageID = pgroup.Max(p => p.MessageId) };

				// retrieve payment records
				using (PaymentPlanTds ds = new PaymentPlanTds())
				{
					PaymentPlanTds.PublicFundsHistoryDataTable table = ds.PublicFundsHistory;
					using (PublicFundsHistoryTableAdapter ta = new PublicFundsHistoryTableAdapter())
					{
						ta.Fill(table, candidateID, electionCycle);
					}
					PublicFundsHistory history = new PublicFundsHistory(table.Count);
					foreach (PaymentPlanTds.PublicFundsHistoryRow row in table)
					{
						PublicFundsDetermination payment = new PublicFundsDetermination(row.Date, row.Amount > 0)
						{
							ElectionType = CPConvert.ToElectionType(row.ElectionTypeCode.Trim()),
							PaymentAmount = row.Amount,
							PaymentMethod = string.IsNullOrEmpty(row.CheckNumber.Trim()) ? PaymentMethod.Eft : PaymentMethod.Check,
							Run = row.Run
						};
						// associate message ID if found
						var message = messages.FirstOrDefault(m => m.Run == row.Run);
						if (message != null)
							payment.MessageID = message.MessageID;
						history.Determinations.Add(payment);
					}
					return history;
				}
			}
		}
	}
}
