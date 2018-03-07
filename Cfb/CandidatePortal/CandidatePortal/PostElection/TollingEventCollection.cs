using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// A collection of post election audit tolling events.
	/// </summary>
	public class TollingEventCollection : List<ITollingEvent>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TollingEventCollection"/> class that is empty.
		/// </summary>
		public TollingEventCollection()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TollingEventCollection"/> class that is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">The number of elements that the new list can initially store.</param>
		public TollingEventCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TollingEventCollection"/> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the new list.</param>
		public TollingEventCollection(IEnumerable<ITollingEvent> collection)
			: base(collection)
		{
		}

		/// <summary>
		/// Retrieves a collection of post election audit events for display in a calendar.
		/// </summary>
		/// <returns>A collection of post election audit events for display in a calendar.</returns>
		public IEnumerable<CPCalendarItem> GetCalendarEvents()
		{
			List<CPCalendarItem> items = new List<CPCalendarItem>();
			foreach (var evt in this)
			{
				InadequateEventBase inadequate = evt as InadequateEventBase;
				if (inadequate != null)
				{
					items.Add(inadequate);
					ResponseDeadlineBase deadline = inadequate.ResponseDeadline;
					if (deadline != null)
						items.Add(deadline);
				}
			}
			return items;
		}
	}
}
