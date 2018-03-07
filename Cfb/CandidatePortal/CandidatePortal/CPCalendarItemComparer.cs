using System.Collections.Generic;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Compares two <see cref="CPCalendarItem"/> objects by date.
	/// </summary>
	public class CPCalendarItemComparer : IComparer<CPCalendarItem>
	{
		#region IComparer<CPCalendarItem> Members

		/// <summary>
		/// Compares two <see cref="CPCalendarItem"/> objects by date and returns a vlue indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <param name="x">The first item to compare.</param>
		/// <param name="y">The second item to compare.</param>
		/// <returns>
		/// <list type="bullet">
		/// <item>A negative integer if x precedes y.</item>
		/// <item>Zero if x is simulatenous with y.</item>
		/// <item>A positive integer if x is subsequent to y.</item>
		/// </list>
		/// </returns>
		public int Compare(CPCalendarItem x, CPCalendarItem y)
		{
			try
			{
				if (x == null)
				{
					return y == null ? 0 : -1;
				}
				else if (y == null)
				{
					return 1;
				}
				else
				{
					return x.CompareTo(y);
				}
			}
			catch
			{
				return 0;
			}
		}

		#endregion
	}
}
