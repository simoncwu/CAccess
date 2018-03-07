using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Tracking details for a Post Election Audit Final Audit Report for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(DarResponseDeadline))]
	public class FinalAuditReport : AuditReportBase
	{
		/// <summary>
		/// Gets the number of months to offset the FAR target issuance date when a campaign has completed audit training requirements.
		/// </summary>
		public static short TrainingCompliantFarTargetOffsetMonths
		{
			get { return Properties.Settings.Default.TrainingCompliantFarTargetOffsetMonths; }
		}

		/// <summary>
		/// Gets the type of post election audit report or letter.
		/// </summary>
		public override AuditReportType AuditReportType
		{
			get { return AuditReportType.FinalAuditReport; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FinalAuditReport"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		public FinalAuditReport(DateTime date)
			: base(date, CPCalendarItemType.FinalAuditReport)
		{
			this.Title = Properties.CPCalendarItemResources.DarTitle;
			this.Description = Properties.CPCalendarItemResources.DarDescription;
		}

		/// <summary>
		/// Retrieves the Post Election Audit Final Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Final Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public static FinalAuditReport GetFinalAuditReport(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetFinalAuditReport(candidateID, electionCycle);
		}

		/// <summary>
		/// Retrieves the original Final Audit Report issuance date for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The original issuance date if the election cycle is valid and the candidate is active in that election cycle; otherwise, null.</returns>
		public static DateTime? GetOriginalIssuanceDate(string candidateID, string electionCycle)
		{
			ActiveCandidate c = ActiveCandidate.GetActiveCandidate(candidateID, electionCycle);
			if (c != null)
			{
				DateTime? date = DraftAuditReport.GetOriginalIssuanceDate(candidateID, electionCycle);
				if (date.HasValue)
					return date.Value.AddMonths(Properties.Settings.Default.DarFarIntervalMonths);
			}
			return null;
		}

		/// <summary>
		/// Retrieves a collection of tolling events that affect the Final Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of tolling events that affect the specified candidate's Final Audit Report for the specified election cycle.</returns>
		public static TollingEventCollection GetTollingEvents(string candidateID, string electionCycle)
		{
			return new TollingEventCollection(CPProviders.DataProvider.GetTollingEvents(candidateID, electionCycle, true));
		}
	}
}