using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// A custom control for selecting a candidate from a dropdown combobox.
    /// </summary>
    public class CandidatePicker : CfbComboBox
    {
        /// <summary>
        /// The text to display for the option representing all items.
        /// </summary>
        private const string AllOptionText = "(All)";

        /// <summary>
        /// Gets the ID of the currently selected candidate.
        /// </summary>
        public string SelectedCid
        {
            get
            {
                return this.SelectedValue as string;
            }
            set
            {
                if (value == null)
                    this.SelectedIndex = this.AllCandidatesOption ? this.FindStringExact(AllOptionText) : -1;
                else
                    this.SelectedValue = value;
            }
        }

        /// <summary>
        /// The election cycle used to filter the list of candidates.
        /// </summary>
        private string _electionCycle;

        /// <summary>
        /// Gets or sets the election cycle used to filter the list of candidates.
        /// </summary>
        [Category("Data")]
        [Description("The election cycle used to filter the list of candidates.")]
        public string ElectionCycle
        {
            get { return _electionCycle; }
        }

        /// <summary>
        /// Gets or sets whether or not an option for selecting all candidates will be available.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether or not an option for selecting all candidates will be available.")]
        public bool AllCandidatesOption { get; set; }

        /// <summary>
        /// Gets whether or not the all-candidates option is currently selected.
        /// </summary>
        public bool AllCandidatesSelected
        {
            get { return AllOptionText.Equals(this.SelectedValue); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatePicker"/> custom control.
        /// </summary>
        public CandidatePicker()
            : base()
        {
            this.SuspendLayout();
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.FormatString = "{0} ({1})";
            this.FormattingEnabled = true;
            this.MaxDropDownItems = 16;
            this.ResumeLayout();
        }

        /// <summary>
        /// Sets the election cycle filter and refreshes the list of candidates.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter the list of candidates by.</param>
        public void SetElectionCycle(string electionCycle)
        {
            _electionCycle = electionCycle;
            LoadCandidatesBy(electionCycle);
        }

        /// <summary>
        /// Loads candidates into the candidate picker control.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <remarks>Loading a null or empty election cycle will retrieve all known candidates.</remarks>
        private void LoadCandidatesBy(string electionCycle)
        {
            string oldSelection = this.SelectedCid;
            // clear ValueMember property temporarily while repopulating candidates list
            this.ValueMember = null;
            this.UseWaitCursor = true;
            var candidates = (string.IsNullOrEmpty(electionCycle) ? CPProviders.DataProvider.GetCandidates().Values.OrderBy(c => c.FormalName) : CPProviders.DataProvider.GetActiveCandidates(electionCycle).Values.OrderBy(c => c.FormalName).Select(c => c as Candidate)).ToList();
            if (this.AllCandidatesOption)
                candidates.Insert(0, new Candidate(AllOptionText));
            this.DataSource = candidates.ToArray();
            this.UseWaitCursor = false;
            this.DisplayMember = this.ValueMember = "ID";
            // reselect old selection if found
            if (!string.IsNullOrEmpty(oldSelection))
                this.SelectedValue = oldSelection;
            else
                this.SelectedIndex = this.AllCandidatesOption ? 0 : -1;
        }

        /// <summary>
        /// Raises the <see cref="ListControl.Format"/> event.
        /// </summary>
        /// <param name="e">A <see cref="ListControlConvertEventArgs"/> that contains the event data.</param>
        protected override void OnFormat(ListControlConvertEventArgs e)
        {
            Candidate c = e.ListItem as Candidate;
            if (c != null)
            {
                e.Value = c.ID == AllOptionText ? AllOptionText : string.Format(this.FormatString, c.FormalName, c.ID);
            }
            base.OnFormat(e);
        }

        /// <summary>
        /// Selects a candidate in the list.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to select.</param>
        public void SelectCandidate(string candidateID)
        {
            if (string.Equals(this.SelectedValue as string, candidateID, StringComparison.InvariantCultureIgnoreCase))
                this.OnSelectedIndexChanged(new EventArgs());
            else
                this.SelectedValue = candidateID;
        }
    }
}
