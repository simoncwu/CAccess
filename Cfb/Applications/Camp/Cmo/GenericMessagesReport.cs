using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Cfb.CandidatePortal.Cmo;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Cmo
{
    /// <summary>
    /// A form for performing a generic report on a set of messages.
    /// </summary>
    public partial class GenericMessagesReport : Cfb.Camp.Reports.ReportViewerBase
    {
        /// <summary>
        /// A collection of messages to be included in the report.
        /// </summary>
        private List<CmoMessage> _messages;

        /// <summary>
        /// Gets a collection of messages to be included in the report.
        /// </summary>
        public List<CmoMessage> Messages
        {
            get { return _messages; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMessagesReport"/> form.
        /// </summary>
        public GenericMessagesReport()
        {
            InitializeComponent();
            _messages = new List<CmoMessage>();
            this.BindingSource = this.CmoMessageBindingSource;
            this.BeginGetReportData = GetMessages;
            this.runToolStripButton.Enabled = true;
            this.campaignToolStripButton.Available = false;
        }

        /// <summary>
        /// Retrieves a collection of messages to disply in the report.
        /// </summary>
        /// <param name="worker">The <see cref="BackgroundWorker"/> that will asynchronously execute the method.</param>
        /// <returns>A collection of <see cref="CmoMessage"/> objects matching the IDs contained in <see cref="GenericMessagesReport.Messages"/>.</returns>
        private IEnumerable<CmoMessage> GetMessages(BackgroundWorker worker)
        {
            return _messages;
        }

        /// <summary>
        /// Sets up properties for the report prior to execution.
        /// </summary>
        /// <param name="report">The report to be run.</param>
        protected override void SetReportProperties(LocalReport report)
        {
            report.SetParameters(new ReportParameter("Title", this.Title));
        }

        private void GenericMessagesReport_Load(object sender, EventArgs e)
        {
            this.runToolStripButton.PerformClick();
        }
    }
}
