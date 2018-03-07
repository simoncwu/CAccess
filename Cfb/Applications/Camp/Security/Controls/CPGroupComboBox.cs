using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.DirectoryServices;
using Cfb.Extensions;
using Cfb.CandidatePortal.Security;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// A custom control for selecting a C-Access security group from a dropdown combobox.
    /// </summary>
    public class CPGroupComboBox : CfbComboBox
    {
        private const string DisplayMemberValue = "Name";
        private const string ValueMemberValue = "ID";

        /// <summary>
        /// Initializes a new instance of the <see cref="CPGroupComboBox"/> custom control.
        /// </summary>
        public CPGroupComboBox()
        {
            this.SuspendLayout();
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.MaxDropDownItems = 16;
            this.ResumeLayout();
        }

        /// <summary>
        /// Gets or sets the currently selected group.
        /// </summary>
        [Category("Data")]
        [Description("The currently selected group.")]
        public byte? SelectedGroup
        {
            get
            {
                return this.SelectedValue as byte?;
            }
            set
            {
                if (value == null)
                    this.SelectedIndex = -1;
                else
                    this.SelectedValue = value;
            }
        }

        /// <summary>
        /// Loads the category selection list with categories.
        /// </summary>
        public void LoadGroups()
        {
            object oldSelection = this.SelectedValue;
            this.ClearDataSourceItems();
            this.DisplayMember = DisplayMemberValue;
            this.ValueMember = ValueMemberValue;
            this.DataSource = CPSecurity.Provider.GetGroups().OrderBy(g => g.Name).ToList();
            if (oldSelection != null)
                this.SelectedValue = oldSelection;
            else
                this.SelectedIndex = -1;
        }
    }
}
