using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.CertificationDocumentTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all certification documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose certification documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all certification documents on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CertificationHistory GetCertificationDocuments(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all certification documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose certification documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all certification documents on record for the specified candidate and election cycle.</returns>
		public CertificationHistory GetCertificationDocuments(string candidateID, string electionCycle)
		{
			using (CertificationDocumentTds ds = new CertificationDocumentTds())
			{
				using (CertificationDocumentsTableAdapter ta = new CertificationDocumentsTableAdapter())
				{
					ta.Fill(ds.CertificationDocuments, candidateID, electionCycle);
				}
				CertificationHistory c = new CertificationHistory(ds.CertificationDocuments.Count);
				foreach (CertificationDocumentTds.CertificationDocumentsRow row in ds.CertificationDocuments.Rows)
				{
					c.Documents.Add(new CertificationDocument(row.Number, row.LastUpdated)
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
