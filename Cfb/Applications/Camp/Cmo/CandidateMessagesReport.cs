using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Cmo;
using Cfb.Cfis.CampaignContacts;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Cmo
{
    /// <summary>
    /// A form for performing reports on all messages sent to a single candidate.
    /// </summary>
    public partial class CandidateMessagesReport : Cfb.Camp.Reports.ReportViewerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateMessagesReport"/> form.
        /// </summary>
        public CandidateMessagesReport()
        {
            InitializeComponent();
            this.BindingSource = this.CmoMessageBindingSource;
            this.BeginGetReportData = GetMessages;
        }

        /// <summary>
        /// Retrieves a collection of messages to disply in the report.
        /// </summary>
        /// <param name="worker">The <see cref="BackgroundWorker"/> that will asynchronously execute the method.</param>
        /// <returns>A collection of <see cref="CmoMessage"/> objects that match the user's selections.</returns>
        private IEnumerable<CmoMessage> GetMessages(BackgroundWorker worker)
        {
            Candidate c = this.Candidate;
            Election e = this.Election;
            string cycle = e == null ? null : e.Cycle;
            return c == null ? null : CmoProviders.DataProvider.GetCmoPostedMessages(c.ID, cycle);
        }

        /// <summary>
        /// Sets up properties for the report prior to execution.
        /// </summary>
        /// <param name="report">The report to be run.</param>
        protected override void SetReportProperties(LocalReport report)
        {
            report.SetParameters(new ReportParameter("Title", this.Title));
            report.SetParameters(new ReportParameter("CampaignContext", this.SelectedCampaign));
        }
    }
}
