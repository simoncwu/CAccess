using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Tracking details for a Post Election Audit Draft Audit Report for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(DarResponseDeadline))]
	public class DraftAuditReport : AuditReportBase
	{
		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public override AuditReportType AuditReportType
		{
			get { return AuditReportType.DraftAuditReport; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DraftAuditReport"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		public DraftAuditReport(DateTime date)
			: base(date, CPCalendarItemType.DraftAuditReport)
		{
			this.Title = Properties.CPCalendarItemResources.DarTitle;
			this.Description = Properties.CPCalendarItemResources.DarDescription;
		}


		/// <summary>
		/// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle)
		{
			return GetDraftAuditReport(candidateID, electionCycle, true);
		}

		/// <summary>
		/// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle, bool published)
		{
			return CPProviders.DataProvider.GetDraftAuditReport(candidateID, electionCycle, published);
		}

		/// <summary>
		/// Retrieves the original Draft Audit Report issuance date for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The original issuance date if the election cycle is valid and the candidate is active in that election cycle; otherwise, null.</returns>
		public static DateTime? GetOriginalIssuanceDate(string candidateID, string electionCycle)
		{
			ActiveCandidate c = ActiveCandidate.GetActiveCandidate(candidateID, electionCycle);
			if (c != null)
			{
				DateTime? startDate = CPProviders.DataProvider.GetTollingStartDate(electionCycle);
				if (startDate.HasValue)
				{
					return startDate.Value.AddMonths(c.Office.IsCitywide ? Properties.Settings.Default.CitywideDarIntervalMonths : Properties.Settings.Default.DarFarIntervalMonths);
				}
			}
			return null;
		}

		/// <summary>
		/// Retrieves a collection of tolling events that affect the Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of tolling events that affect the specified candidate's Draft Audit Report for the specified election cycle.</returns>
		public static TollingEventCollection GetTollingEvents(string candidateID, string electionCycle)
		{
			return new TollingEventCollection(CPProviders.DataProvider.GetTollingEvents(candidateID, electionCycle, false));
		}

		/// <summary>
		/// Counts the number of Final Audit Report tolling days incurred by a candidate for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The number of FAR tolling days incurred that match the criteria specified.</returns>
		public static int CountFarTollingDaysIncurred(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.CountTollingDaysIncurred(candidateID, electionCycle, true);
		}
	}
}