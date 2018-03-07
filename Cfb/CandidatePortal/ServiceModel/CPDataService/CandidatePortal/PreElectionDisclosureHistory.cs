using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.PreElectionDisclosureTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all pre-election disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose pre-election disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all pre-election disclosure statements on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		PreElectionDisclosureHistory GetPreElectionDisclosures(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a generic collection of all pre-election disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose pre-election disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all pre-election disclosure statements on record for the specified candidate and election cycle.</returns>
		public PreElectionDisclosureHistory GetPreElectionDisclosures(string candidateID, string electionCycle)
		{
			using (PreElectionDisclosureTds ds = new PreElectionDisclosureTds())
			{
				using (PreElectionDisclosuresTableAdapter ta = new PreElectionDisclosuresTableAdapter())
				{
					ta.Fill(ds.PreElectionDisclosures, candidateID, electionCycle);
				}
				PreElectionDisclosureHistory c = new PreElectionDisclosureHistory(ds.PreElectionDisclosures.Count);
				foreach (PreElectionDisclosureTds.PreElectionDisclosuresRow row in ds.PreElectionDisclosures.Rows)
				{
					// parse out the commmittee ID as a char
					char[] idArray = row.CommitteeID.Trim().ToCharArray();
					if (idArray.Length == 0)
						continue;
					char id = idArray[0];
					//fetch applicable dates and create a new PreElectionDisclosure object as appropriate
					c.Add(new PreElectionDisclosure(row.Number, id, row.LastUpdated, DocumentType.PreGeneralDisclosure == CPConvert.ToDocumentType(row.TypeCode.Trim()))
					{
						CommitteeName = row.CommitteeName.Trim(),
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
