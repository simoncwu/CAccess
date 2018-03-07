using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Defines common properties for post election audit tolling events.
	/// </summary>
	public interface ITollingEvent : ICalendarItem
	{
		/// <summary>
		/// Gets the type of tolling letter associated with the event.
		/// </summary>
		TollingLetter TollingLetter { get; }

		/// <summary>
		/// Gets the type of tolling event.
		/// </summary>
		TollingEventType TollingEventType { get; }

		/// <summary>
		/// Gets the start of the tolling period.
		/// </summary>
		DateTime TollingStartDate { get; }

		/// <summary>
		/// Gets the end of the tolling period.
		/// </summary>
		DateTime? TollingEndDate { get; }

		/// <summary>
		/// Gets the deadline for the response to the tolling event.
		/// </summary>
		DateTime? TollingDueDate { get; }

		/// <summary>
		/// Gets the reason the tolling event ended.
		/// </summary>
		TollingEndReason TollingEndReason { get; }

		/// <summary>
		/// Gets the tolling event number.
		/// </summary>
		int TollingEventNumber { get; }

		/// <summary>
		/// Gets whether or not the tolling event is an extension event.
		/// </summary>
		bool IsExtension { get; }

		/// <summary>
		/// Gets whether or not the tolling event is an inadequate response event.
		/// </summary>
		bool IsInadequateResponse { get; }

		/// <summary>
		/// Gets the number of the reference tolling event to which this event applies.
		/// </summary>
		int ReferenceEventNumber { get; }
	}
}
