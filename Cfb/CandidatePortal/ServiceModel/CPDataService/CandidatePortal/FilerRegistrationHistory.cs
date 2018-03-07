using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.FilerRegistrationDocumentTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all filer registration documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filer registration documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filer registration documents on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		FilerRegistrationHistory GetFilerRegistrations(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all filer registration documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filer registration documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filer registration documents on record for the specified candidate and election cycle.</returns>
		public FilerRegistrationHistory GetFilerRegistrations(string candidateID, string electionCycle)
		{
			using (FilerRegistrationDocumentTds ds = new FilerRegistrationDocumentTds())
			{
				using (FilerRegistrationDocumentsTableAdapter ta = new FilerRegistrationDocumentsTableAdapter())
				{
					ta.Fill(ds.FilerRegistrationDocuments, candidateID, electionCycle);
				}
				FilerRegistrationHistory c = new FilerRegistrationHistory(ds.FilerRegistrationDocuments.Count);
				foreach (FilerRegistrationDocumentTds.FilerRegistrationDocumentsRow row in ds.FilerRegistrationDocuments.Rows)
				{
					c.Documents.Add(new FilerRegistrationDocument(row.Number, row.LastUpdated)
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
