using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A dialog box for selecting one or more items off of a list.
	/// </summary>
	public partial class PickListDialog : Form
	{
		/// <summary>
		/// Gets the currently selected item.
		/// </summary>
		public object SelectedItem
		{
			get { return _listBox.SelectedItem; }
		}

		/// <summary>
		/// Gets a collection containing the currently selected items.
		/// </summary>
		public ListBox.SelectedObjectCollection SelectedItems
		{
			get { return _listBox.SelectedItems; }
		}

		/// <summary>
		/// Gets or sets the data source for this <see cref="PickListDialog"/>.
		/// </summary>
		public object DataSource
		{
			get { return _listBox.DataSource; }
			set { _listBox.DataSource = value; }
		}

		/// <summary>
		/// Gets or sets the property to display for the list.
		/// </summary>
		public string DisplayMember
		{
			get { return _listBox.DisplayMember; }
			set { _listBox.DisplayMember = value; }
		}

		/// <summary>
		/// Gets or sets the property to use as the actual value for the items in the list.
		/// </summary>
		public string ValueMember
		{
			get { return _listBox.ValueMember; }
			set { _listBox.ValueMember = value; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="PickListDialog"/> class.
		/// </summary>
		public PickListDialog()
		{
			InitializeComponent();
		}

		private void _listBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			okButton.Enabled = _listBox.SelectedItems.Count > 0;
		}

		private void PickListDialog_Load(object sender, EventArgs e)
		{
			_listBox.ClearSelected();
		}

		private void _listBox_DoubleClick(object sender, EventArgs e)
		{
			this.AcceptButton.PerformClick();
		}
	}
}
