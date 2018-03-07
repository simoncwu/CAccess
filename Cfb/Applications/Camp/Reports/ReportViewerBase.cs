using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Reports.Properties;
using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Reports
{
    /// <summary>
    /// Base template form for running reports.
    /// </summary>
    public partial class ReportViewerBase : CampMdiChild
    {
        /// <summary>
        /// Wether or not a report generation has been requested.
        /// </summary>
        private bool _runRequested;

        /// <summary>
        /// The image to display on the button for running the report.
        /// </summary>
        private readonly Image _runImage;

        /// <summary>
        /// The image to display on the button for stopping the report.
        /// </summary>
        private readonly Image _stopImage;

        /// <summary>
        /// The text to display on the button for running the report.
        /// </summary>
        private readonly string _runText;

        /// <summary>
        /// The text to display on the button for stopping the report.
        /// </summary>
        private readonly string _stopText;

        /// <summary>
        /// Gets or sets whether or not to allow running the report against all candidates.
        /// </summary>
        public bool AllCandidatesEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether or not the to run the report for all candidates.
        /// </summary>
        protected bool AllCandidates { get; set; }

        /// <summary>
        /// The title of the report.
        /// </summary>
        private string _title;

        // Gets or sets the title of the report.
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                Action method = () => { this.Text = "Report: " + value; };
                if (this.InvokeRequired)
                    this.Invoke(method);
                else
                    method();
            }
        }

        /// <summary>
        /// Gets or sets the candidate to report on.
        /// </summary>
        protected Candidate Candidate { get; set; }

        /// <summary>
        /// Gets or sets the election to report on.
        /// </summary>
        protected Election Election { get; set; }

        /// <summary>
        /// Gets or sets the report data binding source.
        /// </summary>
        public BindingSource BindingSource { get; set; }

        /// <summary>
        /// Gets or sets the method that will asynchronously retrieve data for the report.
        /// </summary>
        public Func<BackgroundWorker, object> BeginGetReportData { get; set; }

        /// <summary>
        /// Gets the currently selected campaign as a string.
        /// </summary>
        protected string SelectedCampaign
        {
            get
            {
                Candidate c = this.Candidate;
                Election e = this.Election;
                string candidate = this.AllCandidates ? "All Candidates" : c == null ? null : string.Format("{0} ({1})", c.GetFullName(false), c.ID);
                string ec = e == null ? null : string.Format(", EC{0}", e.Cycle);
                return candidate + ec;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportViewerBase"/> form.
        /// </summary>
        public ReportViewerBase()
        {
            InitializeComponent();
            _runImage = this.runToolStripButton.Image;
            _stopImage = Resources.Stop_16x16;
            _runText = this.runToolStripButton.Text;
            _stopText = "Cancel Report";
            this.runToolStripButton.Enabled = !this.campaignToolStripButton.Available;
        }

        /// <summary>
        /// Prompts for a candidate and election to report on and refreshes the report.
        /// </summary>
        protected void PickCandidate()
        {
            using (CandidatePickerDialog dialog = new CandidatePickerDialog()
            {
                ShowElections = true,
                AllCandidatesOption = this.AllCandidatesEnabled,
                AllElectionsOption = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Candidate c = CPProviders.DataProvider.GetCandidate(dialog.CandidateId);
                    if (!(this.AllCandidates = c == null))
                        this.Candidate = c;
                    this.Election = dialog.Election;
                    this.runToolStripButton.Enabled = this.Candidate != null || this.AllCandidates;
                    this.runToolStripButton.PerformClick();
                }
            }
        }

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public override IEnumerable<ToolStrip> GetMergingToolStrips()
        {
            return new[] { this.ReportViewerToolStrip };
        }

        /// <summary>
        /// Refreshes the report with new data.
        /// </summary>
        /// <param name="data">The data to refresh the report with.</param>
        private void RefreshReport(object data)
        {
            this.ReportViewer.Visible = true;
            this.Cursor = Cursors.Default;
            this.BindingSource.DataSource = data;
            this.SetReportProperties(this.ReportViewer.LocalReport);
            this.ReportViewer.RefreshReport();
        }

        /// <summary>
        /// Sets up properties for the report prior to execution.
        /// </summary>
        /// <param name="report">The report to be run.</param>
        protected virtual void SetReportProperties(LocalReport report) { }

        #region Event Handlers

        protected override void OnLoad(EventArgs e)
        {
            this.Bounds = this.MaximumBounds;
            base.OnLoad(e);
            this.ReportViewer.ZoomMode = ZoomMode.PageWidth;
            this.ReportViewer.ZoomPercent = 100;
        }

        private void runToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.runToolStripButton.Text == _runText)
            {
                this.Cursor = Cursors.WaitCursor;
                this.runToolStripButton.Image = _stopImage;
                this.runToolStripButton.Text = _stopText;
                this.ReportViewer.Visible = false;
                if (!this.BackgroundWorker.IsBusy)
                    this.BackgroundWorker.RunWorkerAsync();
            }
            else
            {
                this.BackgroundWorker.CancelAsync();
                this.ReportViewer.CancelRendering(0);
                this.ReportViewer.Reset();
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            e.Result = this.BeginGetReportData(worker);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
                RefreshReport(e.Result);
            this.runToolStripButton.Image = _runImage;
            this.runToolStripButton.Text = _runText;
        }

        private void campaignToolStripButton_Click(object sender, EventArgs e)
        {
            this.PickCandidate();
        }

        private void ReportViewerBase_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F5) || (e.KeyCode == Keys.Pause && e.Control))
                runToolStripButton.PerformClick();
        }

        private void ReportViewerBase_Shown(object sender, EventArgs e)
        {
            if (!this.DesignMode && this.campaignToolStripButton.Available)
                this.campaignToolStripButton.PerformClick();
        }

        #endregion
    }
}
