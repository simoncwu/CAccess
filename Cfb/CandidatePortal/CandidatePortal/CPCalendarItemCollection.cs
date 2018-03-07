using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A generic collection of objects for use with a Candidate Portal calendar.
	/// </summary>
	[CollectionDataContract(Namespace = "http://caccess.nyccfb.info/schema/data", Name = "CPCalendarItemCollection{0}")]
	public class CPCalendarItemCollection<T> : List<T> where T : CPCalendarItem
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="CPCalendarItem"/> objects.
		/// </summary>
		public CPCalendarItemCollection()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="CPCalendarItem"/> objects with a predefined initial capacity.
		/// </summary>
		/// <param name="capacity">The number of <see cref="CPCalendarItem"/> objects that the collection can initially store.</param>
		public CPCalendarItemCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Gets the date when the collection of items was last updated in CFIS.
		/// </summary>
		public virtual DateTime LastUpdated
		{
			get
			{
				DateTime dt = DateTime.MinValue;
				foreach (CPCalendarItem c in this)
				{
					if (dt.CompareTo(c.LastUpdated) < 0)
						dt = c.LastUpdated;
				}
				return dt;
			}
		}

		/// <summary>
		/// Adds the elements of the specified collection to the end of the <see cref="CPCalendarItemCollection{T}"/>.
		/// </summary>
		/// <param name="collection">The collection of items to add.</param>
		public void AddRange<U>(CPCalendarItemCollection<U> collection) where U : T
		{
			foreach (U item in collection)
			{
				this.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of the specified collection to the end of the <see cref="CPCalendarItemCollection{T}"/>.
		/// </summary>
		/// <param name="collection">The collection of items to add.</param>
		public void AddRange<U>(IEnumerable<U> collection) where U : T
		{
			foreach (U item in collection)
			{
				this.Add(item);
			}
		}
	}
}
