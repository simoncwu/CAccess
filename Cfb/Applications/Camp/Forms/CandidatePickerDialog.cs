using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.CandidatePortal;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// A dialog form for selecting a candidate.
    /// </summary>
    public partial class CandidatePickerDialog : Form
    {
        /// <summary>
        /// The ID of the candidate selected.
        /// </summary>
        private string _candidateId;

        /// <summary>
        /// Gets the ID of the candidate selected.
        /// </summary>
        public string CandidateId
        {
            get { return _candidateId; }
        }

        /// <summary>
        /// The election cycle selected.
        /// </summary>
        private Election _election;

        /// <summary>
        /// Gets the election cycle selected.
        /// </summary>
        public Election Election
        {
            get { return _election; }
        }

        /// <summary>
        /// Whether or not to show an election cycle picker.
        /// </summary>
        private bool _showElections;

        /// <summary>
        /// Gets or sets whether or not to show an election cycle picker.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether or not to show an election cycle picker.")]
        public bool ShowElections
        {
            get
            {
                return _showElections;
            }
            set
            {
                if (!(this.electionPanel.Visible = this.shrinkPanel.AutoSize = _showElections = value))
                {
                    // manually size box to hide election panel
                    this.shrinkPanel.Height -= this.electionPanel.Height + this.electionPanel.Margin.Top;
                }
                else
                {
                    this.shrinkPanel.Height += this.electionPanel.Height + this.electionPanel.Margin.Top;
                }
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets whether or not an option for selecting all candidates will be available.
        /// </summary>
        public bool AllCandidatesOption
        {
            get { return this.candidatePicker.AllCandidatesOption; }
            set { this.candidatePicker.AllCandidatesOption = value; }
        }

        /// <summary>
        /// Gets or sets whether or not an option for selecting all elections will be available.
        /// </summary>
        public bool AllElectionsOption
        {
            get { return this.electionPicker.AllElectionsOption; }
            set { this.electionPicker.AllElectionsOption = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatePickerDialog"/> form.
        /// </summary>
        public CandidatePickerDialog()
        {
            InitializeComponent();
        }

        #region Control Event Handlers

        private void CandidatePickerForm_Load(object sender, EventArgs e)
        {
            this.candidatePicker.SelectedCid = _candidateId;
            if (this.candidatePicker.Items.Count == 0)
                this.candidatePicker.SetElectionCycle(null);
            this.electionPicker.SelectedElection = _election;
            if (this.electionPicker.Items.Count == 0)
                this.electionPicker.SetCandidateID(null);
        }

        private void candidatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.okButton.Enabled = (this.candidatePicker.AllCandidatesOption || !string.IsNullOrEmpty(candidatePicker.SelectedCid)) && (this.electionPicker.AllElectionsOption || electionPicker.SelectedElection != null);
        }

        private void CandidatePickerDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.candidatePicker.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _candidateId = this.candidatePicker.AllCandidatesSelected ? null : this.candidatePicker.SelectedCid;
            _election = this.electionPicker.AllElectionsSelected ? null : this.electionPicker.SelectedElection;
        }

        #endregion

    }
}
