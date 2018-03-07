using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Cfb.Camp.Forms.Properties;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CPConfiguration;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// A custom control for selecting an election cycle from a dropdown combobox.
    /// </summary>
    public class ElectionPicker : CfbComboBox
    {
        /// <summary>
        /// The text to display for the option representing all items.
        /// </summary>
        private const string AllOptionText = "(All)";

        /// <summary>
        /// Gets or sets the currently selected election.
        /// </summary>
        public Election SelectedElection
        {
            get
            {
                return this.SelectedItem as Election;
            }
            set
            {
                if (value == null)
                    this.SelectedIndex = this.AllElectionsOption ? this.FindStringExact(AllOptionText) : -1;
                else
                    this.SelectedIndex = this.FindStringExact(value.Cycle);
            }
        }

        /// <summary>
        /// Gets whether or not the all-elections option is currently selected.
        /// </summary>
        public bool AllElectionsSelected
        {
            get { return AllOptionText.Equals(this.SelectedValue); }
        }

        /// <summary>
        /// The candidate ID used to filter the list of election cycles.
        /// </summary>
        private string _candidateID;

        /// <summary>
        /// Gets the candidate ID used to filter the list of election cycles.
        /// </summary>
        [Category("Data")]
        [Description("The candidate ID used to filter the list of election cycles.")]
        public string CandidateID
        {
            get { return _candidateID; }
        }

        /// <summary>
        /// Gets or sets whether or not an option for selecting all elections will be available.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether or not an option for selecting all elections will be available.")]
        public bool AllElectionsOption { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionPicker"/> custom control.
        /// </summary>
        public ElectionPicker()
            : base()
        {
            this.SuspendLayout();
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.MaxDropDownItems = 16;
            this.MinimumSize = new System.Drawing.Size(60, 21);
            this.ResumeLayout();
        }

        /// <summary>
        /// Sets the candidate ID filter and refreshes the list of election cycles.
        /// </summary>
        /// <param name="id">The candidate ID to filter the list of election cycles by.</param>
        public void SetCandidateID(string id)
        {
            _candidateID = id;
            LoadElections(id);
        }

        /// <summary>
        /// Loads all elections in which a candidate is active into the election picker control.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to filter by.</param>
        private void LoadElections(string candidateID)
        {
            Election oldElection = this.SelectedElection;
            // clear ValueMember property temporarily while repopulating elections
            this.ValueMember = null;
            this.UseWaitCursor = true;
            string cutoff = CPProviders.SettingsProvider.MinimumElectionCycle;
            if (string.IsNullOrEmpty(cutoff))
                cutoff = DateTime.Now.Year.ToString(); // default cutoff
            Elections elections = string.IsNullOrEmpty(candidateID) ? CPProviders.DataProvider.GetElections(cutoff) : CPProviders.DataProvider.GetActiveElections(cutoff, candidateID);
            List<Election> ds = elections.Values.ToList();
            if (this.AllElectionsOption)
                ds.Insert(0, new Election(AllOptionText));
            this.DataSource = ds;
            this.UseWaitCursor = false;
            this.DisplayMember = this.ValueMember = "Cycle";
            // reselect old selection if found
            if (oldElection != null)
                this.SelectedValue = oldElection.Cycle;
            else if (this.AllElectionsOption)
                this.SelectedIndex = 0;
            else if (!string.IsNullOrEmpty(SettingsManager.DefaultElectionCycle))
                this.SelectedValue = SettingsManager.DefaultElectionCycle;
            else
                this.SelectedIndex = -1;
        }
    }
}
