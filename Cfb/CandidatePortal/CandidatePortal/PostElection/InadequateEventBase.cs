using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Provides the abstract base class for tracking the details of a Post Election Audit Inadequate Response event for a single candidate and election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(IdrInadequateDeadline))]
	[KnownType(typeof(DarInadequateDeadline))]
	public abstract class InadequateEventBase : AuditReportBase, ITollingEvent
	{
		/// <summary>
		/// Whether or not the inadequate event is an additional one.
		/// </summary>
		[DataMember(Name = "IsAdditional")]
		private readonly bool _isAdditional;

		/// <summary>
		/// Gets whether or not the inadequate event is an additional one.
		/// </summary>
		public bool IsAdditional
		{
			get { return _isAdditional; }
		}

		#region ITollingEvent Members

		/// <summary>
		/// Gets the type of tolling letter associated with the event.
		/// </summary>
		[DataMember]
		public TollingLetter TollingLetter { get; set; }

		/// <summary>
		/// Gets the type of tolling event.
		/// </summary>
		public virtual TollingEventType TollingEventType
		{
			get { return _isAdditional ? TollingEventType.AdditionalInadequate : TollingEventType.InadequateResponse; }
		}

		/// <summary>
		/// Gets or sets the start of the tolling period.
		/// </summary>
		[DataMember]
		public DateTime TollingStartDate { get; set; }

		/// <summary>
		/// Gets or sets the end of the tolling period.
		/// </summary>
		[DataMember]
		public DateTime? TollingEndDate { get; set; }

		/// <summary>
		/// Gets the deadline for the response to the tolling event.
		/// </summary>
		public DateTime? TollingDueDate
		{
			get { return this.DueDate; }
		}

		/// <summary>
		/// Gets or sets the reason the tolling event ended.
		/// </summary>
		[DataMember]
		public TollingEndReason TollingEndReason { get; set; }

		/// <summary>
		/// Gets or sets the tolling event number.
		/// </summary>
		[DataMember]
		public int TollingEventNumber { get; set; }

		/// <summary>
		/// Gets whether or not the tolling event is an extension event.
		/// </summary>
		public bool IsExtension
		{
			get { return false; }
		}

		/// <summary>
		/// Gets whether or not the tolling event is an inadequate response event.
		/// </summary>
		public bool IsInadequateResponse
		{
			get { return true; }
		}

		/// <summary>
		/// Gets the number of the reference tolling event to which this event applies.
		/// </summary>
		public int ReferenceEventNumber { get { return 0; } }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="InadequateEventBase"/> class.
		/// </summary>
		/// <param name="date">The date the request was sent.</param>
		/// <param name="type">The type of calendar event.</param>
		public InadequateEventBase(DateTime date, CPCalendarItemType type)
			: base(date, type)
		{
			_isAdditional = type == CPCalendarItemType.IdrAdditionalInadequateEvent || type == CPCalendarItemType.DarAdditionalInadequateEvent;
		}
	}
}
