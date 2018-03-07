using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Cfb.CandidatePortal.Cmo;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Cmo
{
    /// <summary>
    /// A form for performing reports on the opened status of all posted messages.
    /// </summary>
    public partial class PostedMessagesStatusReport : Cfb.Camp.Reports.ReportViewerBase
    {
        private readonly string _baseTitle;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostedMessagesStatusReport"/> form.
        /// </summary>
        public PostedMessagesStatusReport()
        {
            InitializeComponent();
            this.BindingSource = this.CmoMessageBindingSource;
            this.BeginGetReportData = GetMessages;
            this.AllCandidatesEnabled = true;
            _baseTitle = this.Title = this.Text;
        }

        /// <summary>
        /// Retrieves a collection of messages to disply in the report.
        /// </summary>
        /// <param name="worker">The <see cref="BackgroundWorker"/> that will asynchronously execute the method.</param>
        /// <returns>A collection of <see cref="CmoMessage"/> objects that match the user's selections.</returns>
        private IEnumerable<CmoMessage> GetMessages(BackgroundWorker worker)
        {
            string title = _baseTitle;
            try
            {
                if (this.AllCandidates)
                    return CmoProviders.DataProvider.GetCmoPostedMessages(false);
                if (this.Candidate != null)
                {
                    title += " – " + this.SelectedCampaign;
                    return CmoProviders.DataProvider.GetCmoPostedMessages(this.Candidate.ID, this.Election == null ? null : this.Election.Cycle);
                }
            }
            finally
            {
                this.Title = title;
            }
            return null;
        }

        /// <summary>
        /// Sets up properties for the report prior to execution.
        /// </summary>
        /// <param name="report">The report to be run.</param>
        protected override void SetReportProperties(LocalReport report)
        {
            report.SetParameters(new ReportParameter("Title", this.Title));
        }
    }
}
