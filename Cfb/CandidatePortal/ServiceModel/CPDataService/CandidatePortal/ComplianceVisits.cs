using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.ComplianceVisitTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves all compliance visits on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose compliance visits are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A <see cref="ComplianceVisits"/> collection of all compliance visits on record for the specified candidate and election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		ComplianceVisits GetComplianceVisits(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves all compliance visits on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose compliance visits are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A <see cref="ComplianceVisits"/> collection of all compliance visits on record for the specified candidate and election cycle.</returns>
		public ComplianceVisits GetComplianceVisits(string candidateID, string electionCycle)
		{
			using (ComplianceVisitTds ds = new ComplianceVisitTds())
			{
				using (ComplianceVisitsTableAdapter ta = new ComplianceVisitsTableAdapter())
				{
					ta.Fill(ds.ComplianceVisits, candidateID, electionCycle);
				}
				ComplianceVisits c = new ComplianceVisits();
				foreach (ComplianceVisitTds.ComplianceVisitsRow row in ds.ComplianceVisits.Rows)
				{
					short number = row.Number;
					//fetch applicable dates and create a new ComplianceVisit object as appropriate
					ComplianceVisit visit = new ComplianceVisit(row.ElectionCycle.Trim(), ParseCommitteeID(row.CommitteeID), number > byte.MaxValue ? byte.MaxValue : Convert.ToByte(number), row.AppointmentDate.Date)
					{
						LastUpdated = row.LastUpdated,
						SentDate = (row.IsLetterCertifiedDateNull()) ? (row.IsLetterSentDateNull()) ? null : row.LetterSentDate as DateTime? : row.LetterCertifiedDate,
						CommitteeName = row.CommitteeName.Trim()
					};
					// response due if due date is after letter sent date
					if (!row.IsResponseDueDateNull() && visit.SentDate.HasValue && (visit.SentDate.Value < row.ResponseDueDate))
					{
						CVResponseDeadline deadline = new CVResponseDeadline(row.ResponseDueDate.Date, visit);
						if (!row.IsResponseReceivedDateNull())
							deadline.ResponseReceivedDate = row.ResponseReceivedDate;
						if (!row.IsExtensionDueDateNull() && !row.IsExtensionGrantedDateNull())
							deadline.GrantExtension(row.ExtensionDueDate.Date, row.ExtensionGrantedDate, null);
						visit.ResponseDeadline = deadline;
						c.ResponseDeadlines.Add(deadline);
					}
					c.Visits.Add(visit);
				}
				return c;
			}
		}
	}
}
