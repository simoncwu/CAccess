using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Cfb.CandidatePortal.Cmo;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A custom control for selecting a CFB employee from a dropdown combobox.
	/// </summary>
	public class CfbEmployeePicker : CfbComboBox
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CfbEmployeePicker"/> custom control.
		/// </summary>
		public CfbEmployeePicker()
		{
			this.SuspendLayout();
			this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.MaxDropDownItems = 16;
			this.MinimumSize = new System.Drawing.Size(82, 21);
			this.ResumeLayout();
		}

		/// <summary>
		/// Gets or sets the user name of the currently selected CFB employee.
		/// </summary>
		public string SelectedEmployee
		{
			get
			{
				return this.SelectedValue as string;
			}
			set
			{
				this.SelectedValue = value;
				if (this.SelectedIndex < 0)
				{
					User user = CfbDirectorySearcher.GetUser(value);
					this.Text = user.IsAdHoc ? string.Format("({0})", value) : user.DisplayName;
				}
			}
		}

		/// <summary>
		/// Loads all CFB employees.
		/// </summary>
		public void LoadCfbEmployees()
		{
			LoadUsers(CfbDirectorySearcher.GetCfbStaff().OrderBy(u => u.DisplayName).ToDictionary(u => u.Username, u => u.DisplayName));
		}

		/// <summary>
		/// Loads users from an existing data source.
		/// </summary>
		/// <param name="data">The data source collection of users, mapping usernames to display names.</param>
		private void LoadUsers(IEnumerable<KeyValuePair<string, string>> data)
		{
			object oldSelection = this.SelectedValue;
			this.DataSource = data != null ? data.ToList() : null;
			if (oldSelection != null)
				this.SelectedValue = oldSelection;
			else
				this.SelectedIndex = -1;
			this.DisplayMember = "Value";
			this.ValueMember = "Key";
		}

		/// <summary>
		/// Loads all existing CMO message creators.
		/// </summary>
		public void LoadMessageCreators()
		{
			LoadUsers(CmoProviders.DataProvider.GetCmoMessageCreators());
		}
	}
}
