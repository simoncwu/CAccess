using System;
using System.Windows.Forms;
using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// A custom user control for selecting an active candidate by election.
    /// </summary>
    public partial class ActiveCandidatePicker : UserControl
    {
        /// <summary>
        /// Marks this control as read-only without disabling it.
        /// </summary>
        public bool ReadOnly
        {
            get { return this.ElectionPicker.ReadOnly && this.CandidatePicker.ReadOnly; }
            set { this.ElectionPicker.ReadOnly = this.CandidatePicker.ReadOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the control can respond to user interaction.
        /// </summary>
        public new bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = this.ElectionPicker.Enabled = this.CandidatePicker.Enabled = value; }
        }

        /// <summary>
        /// Gets the currently selected election.
        /// </summary>
        public Election SelectedElection
        {
            get { return this.ElectionPicker.SelectedElection; }
        }

        /// <summary>
        /// Gets the ID of the currently selected candidate.
        /// </summary>
        public string SelectedCid
        {
            get { return this.CandidatePicker.SelectedCid; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ActiveCandidatePicker"/> user control.
        /// </summary>
        public ActiveCandidatePicker()
        {
            InitializeComponent();
            this.CandidatePicker.DropDownStyle = ComboBoxStyle.DropDown;
        }

        /// <summary>
        /// Handles the event that occurs when the election cycle selection changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void ElectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CandidatePicker.SetElectionCycle(this.SelectedElection == null ? null : this.SelectedElection.Cycle);
        }

        /// <summary>
        /// Handles the event that occurs when the election combobox label is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void _electionLabel_Click(object sender, EventArgs e)
        {
            this.ElectionPicker.Focus();
        }

        /// <summary>
        /// Handles the event that occurs when the candidate combobox label is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void _candidateLabel_Click(object sender, EventArgs e)
        {
            this.CandidatePicker.Focus();
        }

        /// <summary>
        /// Sets the currently displayed active candidate.
        /// </summary>
        /// <param name="electionCycle">The election cycle in which the candidate is active.</param>
        /// <param name="candidateId">The ID of the active candidate.</param>
        public void SetActiveCandidate(string electionCycle, string candidateId)
        {
            this.ElectionPicker.SetCandidateID(candidateId);
            if (!this.ReadOnly)
            {
                this.ElectionPicker.SelectedValue = electionCycle;
                this.CandidatePicker.SelectCandidate(candidateId);
            }
            else
            {
                this.ElectionPicker.DataSource = new[] { new Election(electionCycle) };
                this.CandidatePicker.DataSource = new[] { Cfb.CandidatePortal.Cfis.GetCandidate(candidateId) };
            }
        }
    }
}
