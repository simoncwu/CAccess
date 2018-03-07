using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a deadline for a response to a compliance deadline.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CVResponseDeadline : ResponseDeadlineBase
	{
		/// <summary>
		/// Grants an extension for a response to a compliance visit.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted.</param>
		/// <param name="number">The extension number, if available.</param>
		internal override void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number)
		{
			if (grantDate.HasValue)
			{
				this.ExtensionDate = grantDate;
				this.SetDate(extension);
				this.ExtensionNumber++;
			}
		}

		/// <summary>
		/// Creates a due date for a response to a compliance visit.
		/// </summary>
		/// <param name="deadline">The due date for the response.</param>
		/// <param name="source">The corresponding compliance deadline source.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="source"/> has not been sent.</exception>
		public CVResponseDeadline(DateTime deadline, ComplianceVisit source)
			: base(deadline, CPCalendarItemType.CVResponseDeadline)
		{
			if (object.Equals(source, null))
				throw new ArgumentNullException("source", "Source Compliance Visit cannot be null.");
			if (!source.SentDate.HasValue)
				throw new ArgumentException("source", "Cannot establish a deadline for a Doing Business Review that has not been sent.");
			this.ReviewSentDate = source.SentDate.Value;
			this.CommitteeID = source.CommitteeID;
			this.ReviewNumber = source.Number;
			this.Title = Properties.CPCalendarItemResources.CVRDTitle;
			this.Description = string.Format(Properties.CPCalendarItemResources.CVRDDescription, source.Date.ToString("MMM d"));
		}
	}
}
