using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a deadline for a response to a statement review.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class SRResponseDeadline : ResponseDeadlineBase
	{
		/// <summary>
		/// Grants an extension for a response to a statement review.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted, if available.</param>
		/// <param name="number">The extension number.</param>
		internal override void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number)
		{
			if (number.HasValue && number.Value > 0)
			{
				this.ExtensionNumber = number.Value;
				this.SetDate(extension);
			}
		}

		/// <summary>
		/// Creates a due date for a response to a statement review.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding statement review source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null or has a null statement.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public SRResponseDeadline(DateTime deadline, StatementReview source)
			: base(deadline, CPCalendarItemType.SRResponseDeadline)
		{
			if (object.Equals(source, null) || source.Statement == null)
				throw new ArgumentNullException("source", "Source Statement Review cannot be null.");
			if (!source.SentDate.HasValue)
				throw new ArgumentException("source", "Cannot establish a deadline for a Doing Business Review that has not been sent.");
			this.ReviewSentDate = source.SentDate.Value;
			byte stnum = source.Number;
			this.CommitteeID = source.CommitteeID;
			this.ReviewNumber = stnum;
			this.Title = string.Format(Properties.CPCalendarItemResources.SRRDTitle, stnum);
			this.Description = string.Format(Properties.CPCalendarItemResources.SRRDDescription, stnum);
		}
	}
}
