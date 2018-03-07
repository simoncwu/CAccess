using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Defines base properties of a CFB response deadline.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public abstract class ResponseDeadlineBase : CPCalendarItem
	{
		/// <summary>
		/// The original response due date.
		/// </summary>
		[DataMember(Name = "OriginalDueDate")]
		private readonly DateTime _originalDueDate;

		/// <summary>
		/// Gets or sets the response due date.
		/// </summary>
		public DateTime OriginalDueDate
		{
			get { return _originalDueDate; }
		}

		/// <summary>
		/// Gets or sets the date the response was received.
		/// </summary>
		[DataMember]
		public DateTime? ResponseReceivedDate { get; set; }

		/// <summary>
		/// Gets whether or not the response has been received.
		/// </summary>
		public bool ResponseReceived
		{
			get { return this.ResponseReceivedDate.HasValue; }
		}

		/// <summary>
		/// Gets or sets the ID of the committee responsible for the response.
		/// </summary>
		[DataMember]
		public char CommitteeID { get; set; }

		/// <summary>
		/// Gets or sets the number of the associated review requiring a response.
		/// </summary>
		[DataMember]
		public byte ReviewNumber { get; set; }

		/// <summary>
		/// Gets or sets the date when the review was sent.
		/// </summary>
		[DataMember]
		public DateTime ReviewSentDate { get; set; }

		/// <summary>
		/// Gets or sets the number of extensions granted for the response.
		/// </summary>
		[DataMember]
		public ushort ExtensionNumber { get; set; }

		/// <summary>
		/// Gets the date an extension was last granted, if applicable.
		/// </summary>
		[DataMember]
		public DateTime? ExtensionDate { get; set; }

		/// <summary>
		/// Gets whether or not an extension was granted for the response deadline.
		/// </summary>
		public bool ExtensionGranted
		{
			get { return _originalDueDate < this.Date; }
		}

		/// <summary>
		/// Grants an extension for a response to an audit review.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted, if available.</param>
		/// <param name="number">The extension number, if available.</param>
		internal abstract void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number);

		/// <summary>
		/// Creates a new review response deadline of a specific type on a specific day.
		/// </summary>
		/// <param name="date">The date of the event.</param>
		/// <param name="type">The type of calendar event.</param>
		protected ResponseDeadlineBase(DateTime date, CPCalendarItemType type)
			: base(date, type)
		{
			_originalDueDate = date;
			this.ExtensionNumber = 0;
		}
	}
}
