using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A deadline for a response to a post election audit initial documentation request.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class IdrResponseDeadline : ReportResponseDeadline
	{
		/// <summary>
		/// Creates a due date for a response to an Initial Documentation Request.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding initial documentation request source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public IdrResponseDeadline(DateTime deadline, AuditReportBase source)
			: base(deadline, CPCalendarItemType.IdrResponseDeadline)
		{
			if (source == null)
				throw new ArgumentNullException("source", "Source Initial Documentation Request cannot be null.");
			this.ReviewSentDate = source.SentDate;
			this.Title = Properties.CPCalendarItemResources.IdrRDTitle;
			this.Description = Properties.CPCalendarItemResources.IdrRDDescription;
		}
	}
}
