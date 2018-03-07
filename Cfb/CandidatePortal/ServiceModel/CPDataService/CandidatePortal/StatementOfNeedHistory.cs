using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.StatementOfNeedTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all statements of need on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statements of need are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all statements of need on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		StatementOfNeedHistory GetStatementsOfNeed(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all statements of need on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statements of need are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all statements of need on record for the specified candidate and election cycle.</returns>
		public StatementOfNeedHistory GetStatementsOfNeed(string candidateID, string electionCycle)
		{
			using (StatementOfNeedTds ds = new StatementOfNeedTds())
			{
				using (StatementsOfNeedTableAdapter ta = new StatementsOfNeedTableAdapter())
				{
					ta.Fill(ds.StatementsOfNeed, candidateID, electionCycle);
				}
				StatementOfNeedHistory c = new StatementOfNeedHistory(ds.StatementsOfNeed.Count);
				foreach (StatementOfNeedTds.StatementsOfNeedRow row in ds.StatementsOfNeed.Rows)
				{
					c.Add(new StatementOfNeed(row.Number, row.LastUpdated, DocumentType.StatementOfNeedGeneral == CPConvert.ToDocumentType(row.TypeCode.Trim()))
					{
						PageCount = row.PageCount,
						StatusReason = CPConvert.ToDocumentStatusReason(row.ReasonCode.Trim()),
						Status = CPConvert.ToDocumentStatus(row.StatusCode.Trim()),
						DeliveryType = CPConvert.ToDeliveryType(row.DeliveryCode.Trim()),
						SubmissionType = CPConvert.ToSubmissionType(row.SubmissionCode.Trim()),
						ReceivedDate = row.IsReceivedDateNull() ? null : row.ReceivedDate as DateTime?,
						StatusDate = row.IsStatusDateNull() ? null : row.StatusDate as DateTime?,
						PostmarkDate = row.IsPostmarkDateNull() ? null : row.PostmarkDate as DateTime?
					});
				}
				return c;
			}
		}
	}
}
