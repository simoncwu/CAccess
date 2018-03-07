using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A deadline for a response to a post election audit draft audit report inadequate response event.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DarInadequateDeadline : ReportResponseDeadline
	{
		/// <summary>
		/// Creates a due date for a response to an Draft Audit Report Inadequate Response event.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding inadequate event source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public DarInadequateDeadline(DateTime deadline, DarInadequateEvent source)
			: this(deadline, source != null && source.IsAdditional)
		{
			if (source == null)
				throw new ArgumentNullException("source", "Source Draft Audit Report Inadequate Event cannot be null.");
			this.ReviewSentDate = source.SentDate;
		}

		/// <summary>
		/// Creates a due date for a response to a Draft Audit Report Inadequate Response event.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="isAdditional">true to create a response to an additional inadequate response event; otherwise, false to create a response to the initial one.</param>
		private DarInadequateDeadline(DateTime deadline, bool isAdditional)
			: base(deadline, isAdditional ? CPCalendarItemType.DarAdditionalInadequateDeadline : CPCalendarItemType.DarInadequateDeadline)
		{
			this.Title = Properties.CPCalendarItemResources.DarIRRDTitle;
			this.Description = Properties.CPCalendarItemResources.DarIRRDDescription;
		}
	}
}
