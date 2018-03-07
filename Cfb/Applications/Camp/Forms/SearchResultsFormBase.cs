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
	/// An inheritable base form for conducting searches.
	/// </summary>
	public partial class SearchResultsFormBase : CampMdiChild
	{
		/// <summary>
		/// Gets the item count threshold for performing an action on multiple items before warning the user.
		/// </summary>
		public virtual ushort MultipleItemActionThreshold
		{
			get { return Properties.Settings.Default.MultipleItemActionThreshold; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SearchResultsFormBase"/> base form.
		/// </summary>
		public SearchResultsFormBase()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Requests confirmation from the user whether or not to continue an operation on multiple items.
		/// </summary>
		/// <param name="action">The name of the action being performed.</param>
		/// <param name="count">The number of items acted upon.</param>
		/// <returns>true if the user chose to continue with the operation; otherwise, false.</returns>
		public bool ConfirmMultipleAction(string action, int count)
		{
			if (this.ResultsListView.SelectedItems.Count > this.MultipleItemActionThreshold)
				return MessageBox.Show(this, string.Format("Warning—{0} {1} items at once may take a long time and slow down your computer significantly.\n\nDo you wish to continue?", action, count), string.Format("Confirm {0} {1} Items", action, count), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
			return true;
		}

		private void SearchPanel_SizeChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}
	}
}
