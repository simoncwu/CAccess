using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A deadline for a response to a post election audit draft audit report.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DarResponseDeadline : ReportResponseDeadline
	{
		/// <summary>
		/// Creates a due date for a response to an Draft Audit Report.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding draft audit report source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public DarResponseDeadline(DateTime deadline, AuditReportBase source)
			: base(deadline, CPCalendarItemType.DarResponseDeadline)
		{
			if (source == null)
				throw new ArgumentNullException("source", "Source Initial Documentation Request cannot be null.");
			this.ReviewSentDate = source.SentDate;
			this.Title = Properties.CPCalendarItemResources.DarRDTitle;
			this.Description = Properties.CPCalendarItemResources.DarRDDescription;
		}
	}
}
