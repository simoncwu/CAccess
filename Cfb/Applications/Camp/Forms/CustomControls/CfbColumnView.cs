using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A Windows list view control, which displays a collection of items that can be displayed using one of four different views, with overriden behavior.
	/// </summary>
	public class CfbListView : ListView
	{
		/// <summary>
		/// Windows.h windows message constant representing the WM_LBUTTONDOWN notification, which occurs when the user releases the left mouse button while the cursor is in the client area of a window.
		/// </summary>
		private const int WM_LBUTTONDOWN = 0x0201;

		/// <summary>
		/// Windows.h windows message constant representing the WM_LBUTTONUP notification, which occurs when the user releases the left mouse button while the cursor is in the client area of a window.
		/// </summary>
		private const int WM_LBUTTONUP = 0x0202;

		/// <summary>
		/// Whether or not to suppress checking of items.
		/// </summary>
		private bool _suppressChecks;

		/// <summary>
		/// The comparer to use for comparing items.
		/// </summary>
		private ListViewColumnComparer _listViewColumnSorter;

		/// <summary>
		/// Gets or sets the index of the sort column.
		/// </summary>
		public int SortColumn
		{
			get { return _listViewColumnSorter.SortColumn; }
			set { _listViewColumnSorter.SortColumn = value; }
		}

		/// <summary>
		/// Gets or sets the sort order for items.
		/// </summary>
		public SortOrder SortOrder
		{
			get { return _listViewColumnSorter.SortOrder; }
			set { _listViewColumnSorter.SortOrder = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CfbListView"/> control.
		/// </summary>
		public CfbListView()
			: base()
		{
			_listViewColumnSorter = new ListViewColumnComparer();
			this.AllowColumnReorder = true;
			this.FullRowSelect = true;
			this.HideSelection = false;
			this.ListViewItemSorter = _listViewColumnSorter;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.UseCompatibleStateImageBehavior = false;
			this.View = View.Details;
		}

		/// <summary>
		/// Raises the <see cref="ListView.ColumnClick"/> event.
		/// </summary>
		/// <param name="e">A <see cref="ColumnClickEventArgs"/> that contains the event data.</param>
		protected override void OnColumnClick(ColumnClickEventArgs e)
		{
			if (e.Column == _listViewColumnSorter.SortColumn)
			{
				// if clicked column is already the sort column, reverse the sort direction
				_listViewColumnSorter.SortOrder = _listViewColumnSorter.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			}
			else
			{
				// remember the sort column and default to ascending.
				_listViewColumnSorter.SortColumn = e.Column;
				_listViewColumnSorter.SortOrder = SortOrder.Ascending;
			}
			this.Sort();
		}

		/// <summary>
		/// Raises the <see cref="ListView.ItemCheck"/> event.
		/// </summary>
		/// <param name="ice">An <see cref="ItemCheckEventArgs"/> that contains the event data.</param>
		protected override void OnItemCheck(ItemCheckEventArgs ice)
		{
			if (_suppressChecks)
				ice.NewValue = ice.CurrentValue;
			else
				base.OnItemCheck(ice);
		}

		/// <summary>
		/// Raises the <see cref="Control.MouseClick"/> event.
		/// </summary>
		/// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			_suppressChecks = false;
		}
		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="Message"/> to process.</param>
		protected override void WndProc(ref Message m)
		{
			Point pt;
			switch (m.Msg)
			{
				case WM_LBUTTONDOWN:
					pt = MakePoints(m.LParam);
					ColumnHeader c = this.Columns[0];
					_suppressChecks = pt == null || pt.X > 20 || pt.X < 5 ;
					break;
				//case WM_LBUTTONUP:
				//    pt = MakePoints(m.LParam);
				//    _suppressChecks = pt != null && pt.X <= this.Columns[0].Width && _suppressChecks;
				//    break;
			}
			base.WndProc(ref m);
		}

		/// <summary>
		/// Retrieves the signed x- and y-coordinates from the given LPARAM value.
		/// </summary>
		/// <param name="lParam">The LPARAM value to examine.</param>
		/// <returns>A <see cref="Point"/> representing the coordinates contained in <paramref name="lParam"/>.</returns>
		private Point MakePoints(IntPtr lParam)
		{
			if (lParam == null)
				throw new ArgumentNullException("lParam", "LPARAM cannot be null.");
			long longValue = lParam.ToInt64();
			return new Point((int)longValue & 0xFFFF, (int)(longValue >> 16) & 0xFFFF);
		}
	}
}
