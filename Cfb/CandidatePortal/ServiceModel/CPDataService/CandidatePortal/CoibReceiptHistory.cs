using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.CoibReceiptTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CoibReceiptHistory GetCoibReceipts(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.</returns>
		public CoibReceiptHistory GetCoibReceipts(string candidateID, string electionCycle)
		{
			using (CoibReceiptTds ds = new CoibReceiptTds())
			{
				using (CoibReceiptsTableAdapter ta = new CoibReceiptsTableAdapter())
				{
					ta.Fill(ds.CoibReceipts, candidateID, electionCycle);
				}
				CoibReceiptHistory c = new CoibReceiptHistory(ds.CoibReceipts.Count);
				foreach (CoibReceiptTds.CoibReceiptsRow row in ds.CoibReceipts.Rows)
				{
					c.Documents.Add(new CoibReceipt(row.Number, row.LastUpdated)
					{
						PageCount = row.PageCount,
						StatusReason = CPConvert.ToDocumentStatusReason(row.ReasonCode.Trim()),
						Status = CPConvert.ToDocumentStatus(row.StatusCode.Trim()),
						DeliveryType = CPConvert.ToDeliveryType(row.DeliveryCode.Trim()),
						SubmissionType = CPConvert.ToSubmissionType(row.SubmissionCode.Trim()),
						ReceivedDate = row.IsReceivedDateNull() ? null : row.ReceivedDate as DateTime?,
						StatusDate = row.IsStatusDateNull() ? null : row.StatusDate as DateTime?,
						PostmarkDate = row.IsPostmarkDateNull() ? null : row.PostmarkDate as DateTime?,
						CoibFilingDate = row.CoibFilingDate
					});
				}
				return c;
			}
		}
	}
}
