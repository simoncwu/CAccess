using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Defines properties to mimick SharePoint ISPCalendarItem calendar items.
	/// </summary>
	public interface ICalendarItem
	{
		/// <summary>
		/// Gets the string that represents the description of the calendar item.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the date time value that represents the end date of the calendar item.
		/// </summary>
		DateTime EndDate { get; }

		/// <summary>
		/// Gets the Boolean value that indicates whether the calendar item has an end date.
		/// </summary>
		bool HasEndDate { get; }

		/// <summary>
		/// Gets the Boolean value that indicates whether the calendar item is an all-day event.
		/// </summary>
		bool IsAllDayEvent { get; }

		/// <summary>
		/// Gets the Boolean value that indicates the recurrence of the calendar item.
		/// </summary>
		bool IsRecurrence { get; }

		/// <summary>
		/// Gets the location where the calendar item takes place.
		/// </summary>
		string Location { get; }

		/// <summary>
		/// Gets the date/time value that represents the start date of the calendar item.
		/// </summary>
		DateTime StartDate { get; }

		/// <summary>
		/// Gets the string value that indicates the title of the calendar item.
		/// </summary>
		string Title { get; }
	}
}
