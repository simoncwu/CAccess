using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Security;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// A form for performing reports on C-Access user accounts.
    /// </summary>
    public partial class UserAccountsReport : Cfb.Camp.Reports.ReportViewerBase
    {
        /// <summary>
        /// Gets the local report in the <see cref="UserAccountsReport"/> control.
        /// </summary>
        public LocalReport LocalReport { get { return this.ReportViewer.LocalReport; } }

        /// <summary>
        /// Gets or sets whether or not campaign selection is enabled for the report.
        /// </summary>
        public bool CampaignSelectionEnabled
        {
            get { return this.campaignToolStripButton.Available; }
            set { this.campaignToolStripButton.Available = this.showDisabledButton.Available = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountsReport"/> form.
        /// </summary>
        public UserAccountsReport()
        {
            InitializeComponent();
            this.BindingSource = this.ReportableUserBindingSource;
            this.BeginGetReportData = GetCampaignUsers;
            this.AllCandidatesEnabled = true;
        }

        /// <summary>
        /// Retrieves a collection of C-Access users.
        /// </summary>
        /// <param name="worker">The <see cref="BackgroundWorker"/> that will asynchronously execute the method.</param>
        /// <returns>A collection of C-Access users in reportable format.</returns>
        private IEnumerable<ReportableUser> GetCampaignUsers(BackgroundWorker worker)
        {
            string cycle = this.Election == null ? null : this.Election.Cycle;
            // thread-safely get "Show Disabled" selection value
            bool includeDisabled = (bool)this.Invoke(new Func<bool>(() => { return this.showDisabledButton.Checked; }));
            // create collection of users based on campaign selection
            List<ReportableUser> users = new List<ReportableUser>();
            var candidates = this.AllCandidates ? CPProviders.DataProvider.GetCandidates() : new[] { this.Candidate }.ToDictionary(c => c.ID, c => c);
            foreach (var cid in candidates.Keys)
            {
                if (worker != null && worker.CancellationPending)
                    break;
                users.AddRange(
                    from u in CPSecurity.Provider.GetCampaignUsers(cid, cycle, includeDisabled)
                    select ReportableUser.CreateReportableUser(u, candidates[cid].GetFullName(true)));
            }
            return users;
        }

        /// <summary>
        /// Sets up properties for the report prior to execution.
        /// </summary>
        /// <param name="report">The report to be run.</param>
        protected override void SetReportProperties(LocalReport report)
        {
            if (this.CampaignSelectionEnabled)
            {
                string title = this.AllCandidates ? "All User Accounts (By Campaign)" : string.Format("User Accounts for {0}", this.Candidate.Name);
                if (this.Election != null)
                    title = string.Format("{0} {1}", this.Election.Cycle, title);
                this.Title = title;
            }
            report.SetParameters(new ReportParameter("Title", this.Title));
        }

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public override IEnumerable<ToolStrip> GetMergingToolStrips()
        {
            return (base.GetMergingToolStrips() ?? new List<ToolStrip>()).Concat(new[] { this.toolStrip });
        }

        private void CampaignAccountsReport_Load(object sender, EventArgs e)
        {
            if (!this.CampaignSelectionEnabled)
            {
                this.runToolStripButton.Enabled = true;
                this.runToolStripButton.PerformClick();
            }
        }
    }
}
