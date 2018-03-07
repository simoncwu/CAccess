using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A deadline for a response to a post election audit report.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ReportResponseDeadline : ResponseDeadlineBase
	{
		/// <summary>
		/// Grants an extension for a response to a post election audit report.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted, if available.</param>
		/// <param name="number">The extension number, if available.</param>
		internal override void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number)
		{
			this.SetDate(extension);
		}

		/// <summary>
		/// The due date for the response to the first audit report.
		/// </summary>
		[DataMember]
		public DateTime FirstDueDate { get; set; }

		/// <summary>
		/// The due date for the response to the second audit report, if one was sent.
		/// </summary>
		[DataMember(Name = "SecondDueDate")]
		private DateTime? _secondDueDate;

		/// <summary>
		/// Gets or sets the due date for the response to the second audit report, if one was sent.
		/// </summary>
		public DateTime? SecondDueDate
		{
			get { return _secondDueDate; }
			set { if (value.HasValue) this.SetDate((_secondDueDate = value.Value).Value); }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ReportResponseDeadline"/> class.
		/// </summary>
		/// <param name="date">The date of the event.</param>
		/// <param name="type">The type of calendar event.</param>
		protected ReportResponseDeadline(DateTime date, CPCalendarItemType type)
			: base(date, type)
		{
		}
	}
}
