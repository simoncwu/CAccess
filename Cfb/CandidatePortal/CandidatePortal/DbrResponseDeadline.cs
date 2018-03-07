using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a deadline for a response to a Doing Business review.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DbrResponseDeadline : ResponseDeadlineBase
	{
		/// <summary>
		/// Grants an extension for a response to a Doing Business review.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted, if available.</param>
		/// <param name="number">The extension number, if available.</param>
		/// <remarks>Extensions to Doing Business review responses are not allowed, and therefore this method has no effect.</remarks>
		internal override void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number)
		{
		}

		/// <summary>
		/// Creates a due date for a response to a Doing Buiness review.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding Doing Business review source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public DbrResponseDeadline(DateTime deadline, DoingBusinessReview source)
			: base(deadline, CPCalendarItemType.DbrResponseDeadline)
		{
			if (object.Equals(source, null) || source.Statement == null)
				throw new ArgumentNullException("source", "Source Doing Business Review cannot be null.");
			if (!source.SentDate.HasValue)
				throw new ArgumentException("source", "Cannot establish a deadline for a Doing Business Review that has not been sent.");
			this.ReviewSentDate = source.SentDate.Value;
			byte stnum = source.Statement.Number;
			this.CommitteeID = source.CommitteeID;
			this.ReviewNumber = stnum;
			this.Title = string.Format(Properties.CPCalendarItemResources.DbrRDTitle, stnum);
			this.Description = string.Format(Properties.CPCalendarItemResources.DbrRDDescription, stnum);
		}
	}
}
