using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a collection of filing dates for a candidate.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class FilingDeadlines
	{
		/// <summary>
		/// The inner collection of filing dates.
		/// </summary>
		[DataMember(Name = "Deadlines")]
		private readonly CPCalendarItemCollection<FilingDeadline> _deadlines;

		/// <summary>
		/// Gets the inner collection of filing dates.
		/// </summary>
		public CPCalendarItemCollection<FilingDeadline> Deadlines
		{
			get { return _deadlines; }
		}

		/// <summary>
		/// A collection of upcoming required filing dates.
		/// </summary>
		[DataMember(Name = "Upcoming")]
		private readonly CPCalendarItemCollection<FilingDeadline> _upcoming;

		/// <summary>
		/// A collection of required filing dates.
		/// </summary>
		[DataMember(Name = "Required")]
		private CPCalendarItemCollection<FilingDeadline> _required;

		/// <summary>
		/// A collection of late filings.
		/// </summary>
		[DataMember(Name = "Late")]
		private readonly CPCalendarItemCollection<FilingDeadline> _late;

		/// <summary>
		/// Gets a collection of late filings.
		/// </summary>
		public CPCalendarItemCollection<FilingDeadline> Late
		{
			get { return _late; }
		}

		/// <summary>
		/// A collection of missing filings.
		/// </summary>
		[DataMember(Name = "Missing")]
		private readonly CPCalendarItemCollection<FilingDeadline> _missing;

		/// <summary>
		/// Gets a collection of missing filings.
		/// </summary>
		public CPCalendarItemCollection<FilingDeadline> Missing
		{
			get { return _missing; }
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="FilingDeadline"/> objects.
		/// </summary>
		internal FilingDeadlines()
		{
			int capacity = Properties.Settings.Default.MaxStatementsPerCycle;
			_deadlines = new CPCalendarItemCollection<FilingDeadline>(capacity);
			_late = new CPCalendarItemCollection<FilingDeadline>(capacity);
			_missing = new CPCalendarItemCollection<FilingDeadline>(capacity);
			_required = new CPCalendarItemCollection<FilingDeadline>(capacity);
			_upcoming = new CPCalendarItemCollection<FilingDeadline>(capacity);
		}

		/// <summary>
		/// Retrieves a collection of upcoming required filing dates.
		/// </summary>
		public IEnumerable<FilingDeadline> GetUpcomingDeadlines()
		{
			return from d in _deadlines where d.IsUpcoming select d;
		}

		/// <summary>
		/// Retrieves a collection of required filing dates.
		/// </summary>
		public IEnumerable<FilingDeadline> GetRequiredDeadlines()
		{
			return from d in _deadlines where d.IsRequired select d;
		}
	}
}
