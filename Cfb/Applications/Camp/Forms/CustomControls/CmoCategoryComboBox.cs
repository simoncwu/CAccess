using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Cfb.CandidatePortal.Cmo;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.Camp.Forms
{
	/// <summary>
	/// A custom control for selecting a CMO category from a dropdown combobox.
	/// </summary>
	public class CmoCategoryComboBox : CfbComboBox
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CfbEmployeePicker"/> custom control.
		/// </summary>
		public CmoCategoryComboBox()
		{
			this.SuspendLayout();
			this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.DisplayMember = "Name";
			this.ValueMember = "ID";
			this.MaxDropDownItems = 16;
			this.MinimumSize = new System.Drawing.Size(212, 21);
			this.ResumeLayout();
		}

		/// <summary>
		/// Gets or sets whether or not to show hidden categories.
		/// </summary>
		[Category("Data")]
		[Description("Whether or not to show hidden categories.")]
		[DefaultValue("False")]
		public bool ShowHidden { get; set; }

		/// <summary>
		/// Gets or sets the currently selected category.
		/// </summary>
		[Category("Data")]
		[Description("The currently selected category.")]
		public byte? SelectedCategory
		{
			get
			{
				return this.SelectedValue as byte?;
			}
			set
			{
				if (value == null)
				{
					this.SelectedIndex = -1;
				}
				else
				{
					this.SelectedValue = value;

					// BUGFIX #50: for some reason, category ID 10 will not save properly the first time... but will if asked again.
					if (this.SelectedValue == null)
						this.SelectedValue = value;
				}
			}
		}

		/// <summary>
		/// Loads the category selection list with categories.
		/// </summary>
		public void LoadCategories()
		{
			object oldSelection = this.SelectedValue;
			string displayMember = this.DisplayMember;
			string valueMember = this.ValueMember;
			this.ClearDataSourceItems();
			var categores = CmoCategory.GetCmoCategories().Values.AsQueryable();
			if (!this.ShowHidden)
				categores = categores.Where(c => !c.Hidden);
			this.DisplayMember = displayMember;
			this.ValueMember = valueMember;
			this.DataSource = categores.OrderBy(c => c.Name).ToList();
			if (oldSelection != null)
				this.SelectedValue = oldSelection;
			else
				this.SelectedIndex = -1;
		}
	}
}
