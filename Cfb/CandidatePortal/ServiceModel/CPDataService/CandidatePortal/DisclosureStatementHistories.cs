using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.DisclosureStatementTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all filing disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filing disclosure statements on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		DisclosureStatementHistories GetDisclosureStatements(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all filing disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filing disclosure statements on record for the specified candidate and election cycle.</returns>
		public DisclosureStatementHistories GetDisclosureStatements(string candidateID, string electionCycle)
		{
			using (DisclosureStatementTds ds = new DisclosureStatementTds())
			{
				using (DisclosureStatementsTableAdapter ta = new DisclosureStatementsTableAdapter())
				{
					ta.Fill(ds.DisclosureStatements, candidateID, electionCycle);
				}
				DisclosureStatementHistories dsh = new DisclosureStatementHistories();
				var s = this.GetStatements(electionCycle);
				foreach (DisclosureStatementTds.DisclosureStatementsRow row in ds.DisclosureStatements.Rows)
				{
					char[] idArray = row.CommitteeID.Trim().ToCharArray();
					if (idArray.Length == 0)
						continue;
					char id = idArray[0];
					string committeeName = row.CommitteeName.Trim();
					if (row.StatementNumber > byte.MaxValue)
						continue;
					byte statementNumber = Convert.ToByte(row.StatementNumber);
					if (object.Equals(s, null) || !s.ContainsKey(statementNumber))
						continue;

					string key = DisclosureStatementHistories.ToKey(id, statementNumber);

					//if committee has not yet been encountered, add new name and statement number entries for it
					if (!dsh.CommitteeNames.ContainsKey(id))
					{
						dsh.CommitteeNames.Add(id, committeeName);
						dsh.CommitteeStatements.Add(id, new Dictionary<byte, Statement>(Properties.Settings.Default.MaxStatementsPerCycle));
					}

					//if statement has not yet been encountered, add new statement history
					var statements = dsh.CommitteeStatements[id];
					if (!statements.ContainsKey(statementNumber))
					{
						statements.Add(statementNumber, s[statementNumber]);
						dsh.Submissions[DisclosureStatementHistories.ToKey(id, statementNumber)] = new DisclosureStatementHistory();
					}

					//fetch applicable dates and create a new DisclosureStatement object as appropriate
					DisclosureStatement dsDoc = new DisclosureStatement(s[statementNumber], row.Number, id, row.LastUpdated)
					{
						CommitteeName = committeeName,
						PageCount = row.PageCount,
						StatusReason = CPConvert.ToDocumentStatusReason(row.ReasonCode.Trim()),
						Status = CPConvert.ToDocumentStatus(row.StatusCode.Trim()),
						DeliveryType = CPConvert.ToDeliveryType(row.DeliveryCode.Trim()),
						SubmissionType = CPConvert.ToSubmissionType(row.SubmissionCode.Trim()),
						ReceivedDate = row.IsReceivedDateNull() ? null : row.ReceivedDate as DateTime?,
						StatusDate = row.IsStatusDateNull() ? null : row.StatusDate as DateTime?,
						PostmarkDate = row.IsPostmarkDateNull() ? null : row.PostmarkDate as DateTime?,
						IsAuditResponse = "Y".Equals(row.IsAuditResponse.Trim(), StringComparison.OrdinalIgnoreCase),
						ReceiptCompleteDate = row.IsReceiptCompleteDateNull() ? null : row.ReceiptCompleteDate as DateTime?,
						SmallCampaign = "Y".Equals(row.IsSmallCampaign.Trim(), StringComparison.OrdinalIgnoreCase),
						DeferredFiling = "Y".Equals(row.IsDeferredFiling.Trim(), StringComparison.OrdinalIgnoreCase)
					};

					// submission formats
					if ("Y".Equals(row.IsCsmartWeb.Trim(), StringComparison.OrdinalIgnoreCase))
						dsDoc.SubmissionFormats |= SubmissionFormat.CsmartWeb;
					else if ("Y".Equals(row.IsInternet.Trim(), StringComparison.OrdinalIgnoreCase))
						dsDoc.SubmissionFormats |= SubmissionFormat.IDS;
					else
					{
						if ("Y".Equals(row.IsCD.Trim(), StringComparison.OrdinalIgnoreCase))
							dsDoc.SubmissionFormats |= SubmissionFormat.CD;
						if ("Y".Equals(row.IsFloppy.Trim(), StringComparison.OrdinalIgnoreCase))
							dsDoc.SubmissionFormats |= SubmissionFormat.FloppyDisk;
						if ("Y".Equals(row.IsPaper.Trim(), StringComparison.OrdinalIgnoreCase))
							dsDoc.SubmissionFormats |= dsDoc.DeliveryType == DeliveryType.Internet ? SubmissionFormat.ElectronicDocument : SubmissionFormat.Paper;
					}

					// backup info
					if (!row.IsBackupReceivedDateNull())
					{
						dsDoc.BackupDeliveryType = CPConvert.ToDeliveryType(row.BackupDeliveryType.Trim());
						dsDoc.BackupReceivedDate = row.BackupReceivedDate;
						if (!row.IsBackupPostmarkDateNull())
							dsDoc.BackupPostmarkDate = row.BackupPostmarkDate;
					}

					//add new DisclosureStatement object to appropriate statement history collection
					dsh.Submissions[DisclosureStatementHistories.ToKey(id, statementNumber)].Documents.Add(dsDoc);
				}
				return dsh;
			}
		}
	}
}
