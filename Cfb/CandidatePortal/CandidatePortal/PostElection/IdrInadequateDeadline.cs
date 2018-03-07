using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A deadline for a response to a post election audit initial documentation request inadequate response event.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class IdrInadequateDeadline : ReportResponseDeadline
	{
		/// <summary>
		/// Creates a due date for a response to an Initial Documentation Request Inadequate Response event.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding inadequate event source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public IdrInadequateDeadline(DateTime deadline, IdrInadequateEvent source)
			: this(deadline, source != null && source.IsAdditional)
		{
			if (source == null)
				throw new ArgumentNullException("source", "Source Initial Documentation Request Inadequate Event cannot be null.");
			this.ReviewSentDate = source.SentDate;
		}

		/// <summary>
		/// Creates a due date for a response to an Initial Documentation Request Inadequate Response event.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="isAdditional">true to create a response to an additional inadequate response event; otherwise, false to create a response to the initial one.</param>
		private IdrInadequateDeadline(DateTime deadline, bool isAdditional)
			: base(deadline, isAdditional ? CPCalendarItemType.IdrAdditionalInadequateDeadline : CPCalendarItemType.IdrInadequateDeadline)
		{
			this.Title = Properties.CPCalendarItemResources.IdrIRRDTitle;
			this.Description = Properties.CPCalendarItemResources.IdrIRRDDescription;
		}
	}
}
