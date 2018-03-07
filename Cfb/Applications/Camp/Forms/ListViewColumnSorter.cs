using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// Compares two <see cref="ListViewItem"/> objects for equivalence, ignoring the case of strings.
	/// </summary>
	public class ListViewColumnComparer : IComparer
	{
		/// <summary>
		/// Gets or sets the index of the sort column.
		/// </summary>
		public int SortColumn { get; set; }

		/// <summary>
		/// Gets or sets the sort order for items.
		/// </summary>
		public SortOrder SortOrder { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ListViewColumnComparer"/> class.
		/// </summary>
		public ListViewColumnComparer()
		{
			this.SortColumn = 0;
			this.SortOrder = SortOrder.None;
		}

		#region IComparer Members

		/// <summary>
		/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
		/// </summary>
		/// <param name="x">First object to be compared</param>
		/// <param name="y">Second object to be compared</param>
		/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
		public int Compare(object x, object y)
		{
			int compareResult;
			ListViewItem listviewX = x as ListViewItem;
			ListViewItem listviewY = y as ListViewItem;
			if (listviewX == null && listviewY == null)
				return 0;
			else if (listviewX == null)
				return -1;
			else if (listviewY == null)
				return 1;
			string xVal = listviewX.SubItems[SortColumn].Text;
			string yVal = listviewY.SubItems[SortColumn].Text;
			// BUGFIX #42: sorting of date columns is by default alphabetical, not chronological
			double xDbl, yDbl;
			DateTime xDt, yDt;
			if (double.TryParse(xVal, out xDbl) && double.TryParse(yVal, out yDbl))
			{
				compareResult = xDbl.CompareTo(yDbl);
			}
			else if (DateTime.TryParse(xVal, out xDt) && DateTime.TryParse(yVal, out yDt))
			{
				compareResult = xDt.CompareTo(yDt);
			}
			else
			{
				compareResult = xVal.CompareTo(yVal);
			}
			return SortOrder == SortOrder.Ascending ? compareResult : SortOrder == SortOrder.Descending ? -compareResult : 0;
		}

		#endregion
	}
}
