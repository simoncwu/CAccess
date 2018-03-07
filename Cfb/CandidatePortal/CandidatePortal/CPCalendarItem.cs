using System;
using System.Runtime.Serialization;
using Cfb.CandidatePortal.Properties;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Represents an item that can be placed on a Candidate Portal calendar.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CPCalendarItem : ICalendarItem, IComparable
	{
		#region ICalendarItem Members

		/// <summary>
		/// Gets or sets the string that represents the description of the calendar item.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the date time value that represents the end date of the calendar item.
		/// </summary>
		[DataMember]
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Gets or sets the Boolean value that indicates whether the calendar item has an end date.
		/// </summary>
		[DataMember]
		public bool HasEndDate { get; set; }

		/// <summary>
		/// Gets or sets the Boolean value that indicates whether the calendar item is an all-day event.
		/// </summary>
		[DataMember]
		public bool IsAllDayEvent { get; set; }

		/// <summary>
		/// Gets or sets the Boolean value that indicates the recurrence of the calendar item.
		/// </summary>
		[DataMember]
		public bool IsRecurrence { get; set; }

		/// <summary>
		/// Gets or sets the location where the calendar item takes place.
		/// </summary>
		[DataMember]
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the date/time value that represents the start date of the calendar item.
		/// </summary>
		[DataMember]
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the string value that indicates the title of the calendar item.
		/// </summary>
		[DataMember]
		public string Title { get; set; }

		#endregion

		/// <summary>
		/// Gets the date of the calendar item.
		/// </summary>
		public virtual DateTime Date
		{
			get { return this.StartDate; }
		}

		/// <summary>
		/// Gets or sets the date when the item was last updated in CFIS.
		/// </summary>
		[DataMember]
		public DateTime LastUpdated { get; set; }

		/// <summary>
		/// The type of calendar item.
		/// </summary>
		[DataMember(Name = "CalendarItemType")]
		private readonly CPCalendarItemType _type;

		/// <summary>
		/// The type of calendar item.
		/// </summary>
		public CPCalendarItemType CalendarItemType
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets whether or not the calendar item is upcoming.
		/// </summary>
		public bool IsUpcoming
		{
			get
			{
				TimeSpan ts = this.StartDate - DateTime.Today;
				Settings settings = Properties.Settings.Default;
				return ts.Days >= settings.UpcomingPastCutoffDays && ts.Days <= settings.UpcomingCutoffDays;
			}
		}

		/// <summary>
		/// Gets whether or not the calendar item is upcoming based on condensed election year timeframes.
		/// </summary>
		public bool IsElectionYearUpcoming
		{
			get
			{
				TimeSpan ts = this.StartDate - DateTime.Today;
				Settings settings = Properties.Settings.Default;
				return ts.Days >= settings.UpcomingPastCutoffDays && ts.Days <= settings.UpcomingElectionYearCutoffDays;
			}
		}

		/// <summary>
		/// Gets whether or not the calendar item is immediate.
		/// </summary>
		public bool IsImmediate
		{
			get
			{
				TimeSpan t = this.StartDate.Date.Subtract(DateTime.Today);
				return t.Days <= 3 || (t.Days <= 7 && this.CalendarItemType == CPCalendarItemType.FilingDeadline);
			}
		}

		/// <summary>
		/// Creates a new, generic calendar item for the current date and time.
		/// </summary>
		public CPCalendarItem()
			: this(DateTime.Now) { }

		/// <summary>
		/// Creates a new, generic calendar item with a start and end date/time.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		public CPCalendarItem(DateTime start, DateTime end)
			: this(start, end, CPCalendarItemType.GenericItem) { }

		/// <summary>
		/// Creates a new, generic calendar item on a specific day.
		/// </summary>
		/// <param name="dt">The day of the event.</param>
		public CPCalendarItem(DateTime dt)
			: this(dt, CPCalendarItemType.GenericItem) { }

		/// <summary>
		/// Creates a new calendar item of a specific type on a specific day.
		/// </summary>
		/// <param name="dt">The day of the event.</param>
		/// <param name="type">The type of calendar event.</param>
		public CPCalendarItem(DateTime dt, CPCalendarItemType type)
			: this(dt, dt, type)
		{
		}

		/// <summary>
		/// Creates a new calendar item of a specific type with a start and end date/time.
		/// </summary>
		/// <param name="start">The starting date and time of the event.</param>
		/// <param name="end">The ending date and time of the event.</param>
		/// <param name="type">The type of calendar event.</param>
		public CPCalendarItem(DateTime start, DateTime end, CPCalendarItemType type)
			: this(type)
		{
			this.HasEndDate = true;
			this.IsAllDayEvent = start.CompareTo(end) == 0;
			this.StartDate = start;
			this.EndDate = end;
		}

		/// <summary>
		/// Creates a new calendar item of a specific type.
		/// </summary>
		/// <param name="type">The type of calendar event.</param>
		public CPCalendarItem(CPCalendarItemType type)
		{
			_type = type;
		}

		/// <summary>
		/// Sets the calendar item to occur on a specific day as an all-day event.
		/// </summary>
		/// <param name="dt">The new day of the event.</param>
		protected void SetDate(DateTime dt)
		{
			this.StartDate = this.EndDate = dt.Date;
			this.IsAllDayEvent = true;
		}

		#region IComparable Members

		/// <summary>
		/// Compares the value of this instance to a specified <see cref="ICalendarItem"/> object, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified object.
		/// </summary>
		/// <param name="obj">An <see cref="ICalendarItem"/> to compare, or null.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="obj"/>.
		/// <list type="bullet">
		/// <item>Less than zero if this instance is earlier than <paramref name="obj"/>.</item>
		/// <item>Zero if this instance coincides with <paramref name="obj"/>.</item>
		/// <item>Greater than zero if this instance is later than <paramref name="obj"/>, or <paramref name="obj"/> is null.</item>
		/// </list>
		/// </returns>
		public int CompareTo(object obj)
		{
			ICalendarItem item = obj as ICalendarItem;
			return item != null ? this.StartDate.CompareTo(item.StartDate) : 1;
		}

		#endregion
	}
}
