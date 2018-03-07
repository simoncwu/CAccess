using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Defines base properties of an audit review.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public abstract class AuditReviewBase : CPCalendarItem
	{
		#region CalendarItem Properties

		/// <summary>
		/// Gets the date the review was completed.
		/// </summary>
		public override DateTime Date
		{
			get { return base.Date; }
		}

		#endregion

		/// <summary>
		/// Gets the number of the audit review.
		/// </summary>
		public abstract byte Number { get; }

		/// <summary>
		/// The ID of the committee under review.
		/// </summary>
		[DataMember(Name = "CommitteeID")]
		private readonly char _committeeID;

		/// <summary>
		/// Gets the ID of the committee under review.
		/// </summary>
		public char CommitteeID
		{
			get { return _committeeID; }
		}

		/// <summary>
		/// Gets or sets the name of the committee being reviewed.
		/// </summary>
		[DataMember]
		public string CommitteeName { get; set; }

		/// <summary>
		/// The election cycle context of the review.
		/// </summary>
		[DataMember(Name = "ElectionCycle")]
		private readonly string _electionCycle;

		/// <summary>
		/// Gets the election cycle context of the review.
		/// </summary>
		protected string ElectionCycle
		{
			get { return _electionCycle; }
		}

		/// <summary>
		/// Gets or sets the deadline for the response to the review.
		/// </summary>
		[DataMember]
		public ResponseDeadlineBase ResponseDeadline { get; set; }

		/// <summary>
		/// Gets or sets the date the statement review was sent to the campaign if a response was required.
		/// </summary>
		[DataMember]
		public DateTime? SentDate { get; set; }

		/// <summary>
		/// Gets whether or not the statement review was sent to the campaign.
		/// </summary>
		public bool Sent
		{
			get { return (this.SentDate != null); }
		}

		/// <summary>
		/// Gets whether or not a response is required for the review.
		/// </summary>
		public bool ResponseRequired
		{
			get { return !object.Equals(this.ResponseDeadline, null); }
		}

		/// <summary>
		/// Gets the date the review response was received.
		/// </summary>
		public DateTime? ResponseReceivedDate
		{
			get { return this.ResponseDeadline == null ? null : this.ResponseDeadline.ResponseReceivedDate; }
		}

		/// <summary>
		/// Gets whether or not the review response was received.
		/// </summary>
		public bool ResponseReceived
		{
			get { return this.ResponseReceivedDate.HasValue; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditReviewBase"/> class.
		/// </summary>
		/// <param name="type">The type of review.</param>
		/// <param name="electionCycle">The election cycle under review.</param>
		/// <param name="committeeID">The ID of the committee under review.</param>
		internal AuditReviewBase(CPCalendarItemType type, string electionCycle, char committeeID)
			: base(type)
		{
			_electionCycle = electionCycle;
			_committeeID = committeeID;
		}
	}
}
