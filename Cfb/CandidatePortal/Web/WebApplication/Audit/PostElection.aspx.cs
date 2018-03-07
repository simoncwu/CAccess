using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class PostElection : CPPage, ISecurePage
    {
        /// <summary>
        /// Query string key for specifying post election audit type.
        /// </summary>
        private const string PostElectionAuditTypeKey = "peatype";

        #region Current Status Text

        private const string FinalAuditCompleteText = "Final Audit Report issued&mdash;Post Election Audit complete";
        private const string UnderReviewText = "Campaign&rsquo;s response under CFB review";
        private const string DarInadequatePendingText = "Awaiting campaign&rsquo;s reply to the Draft Audit Report inadequate response notice";
        private const string DarPendingText = "Awaiting campaign&rsquo;s reply to the Draft Audit Report";
        private const string IdrInadequatePendingText = "Awaiting campaign&rsquo;s reply to the Request for Documentation inadequate response notice";
        private const string IdrPendingText = "Awaiting campaign&rsquo;s reply to the Request for Documentation";
        private readonly string SuspensionText = string.Format("Your audit review has been suspended. Please contact the CFB&rsquo;s Audit unit at {0} for more information.", CPProviders.SettingsProvider.AgencyPhoneNumber);

        #endregion

        /// <summary>
        /// The Initial Documentation Request to display.
        /// </summary>
        private InitialDocumentationRequest _idr;

        /// <summary>
        /// The Draft Audit Report to display.
        /// </summary>
        private DraftAuditReport _dar;

        /// <summary>
        /// The Final Audit Report to display.
        /// </summary>
        private FinalAuditReport _far;

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.EnsureChildControls();

            // detect DAR/FAR availability
            _idr = CPProfile.InitialDocumentationRequest;
            _dar = CPProfile.DraftAuditReport;
            _far = CPProfile.FinalAuditReport;

            // detect post election audit report navigation info
            if (!Page.IsPostBack || !this.IsDataBound || !_postElectionAuditNavigation.EnableViewState)
            {
                _postElectionAuditNavigation.DarPresent = _dar != null;
                _postElectionAuditNavigation.DarTollingPresent = _idr != null && _idr.TollingEvents.Count > 0;
                _postElectionAuditNavigation.FarPresent = _far != null;
                _postElectionAuditNavigation.FarTollingPresent = _dar != null && _dar.TollingEvents.Count > 0;
            }

            // populate immediate page data
            if (!Page.IsPostBack || !this.IsDataBound)
            {
                // DAR issue dates
                int darTollingDays = _idr != null ? _idr.TollingDaysIncurred : InitialDocumentationRequest.CountDarTollingDaysIncurred(CPProfile.Cid, CPProfile.ElectionCycle);
                _darTollingDaysLabel.Text = darTollingDays.ToString();
                DateTime? issueDate = DraftAuditReport.GetOriginalIssuanceDate(CPProfile.Cid, CPProfile.ElectionCycle);
                if (issueDate.HasValue)
                {
                    _darOriginalIssueDateLabel.Text = issueDate.Value.ToShortDateString();
                    _darCurrentIssueDateLabel.Text = issueDate.Value.AddDays(darTollingDays).ToShortDateString();
                }

                // FAR issue dates
                int farTollingDays = _dar != null ? _dar.TollingDaysIncurred : DraftAuditReport.CountFarTollingDaysIncurred(CPProfile.Cid, CPProfile.ElectionCycle);
                _farTollingDaysLabel.Text = farTollingDays.ToString();
                _totalTollingDaysLabel.Text = (darTollingDays + farTollingDays).ToString();
                issueDate = CPProfile.FarIssuanceDate;
                if (issueDate.HasValue)
                {
                    _farOriginalIssueDateLabel.Text = issueDate.Value.ToShortDateString();
                    // BUGFIX #54: FAR Original Date is wrong when Audit Training Compliance status is achieved
                    TrainingStatus training = CPProfile.TrainingStatus;
                    if (this.TrainingCompliance.Visible = this.TrainingComplianceReference.Visible = training != null && training.AuditComplianceAchieved)
                        issueDate = issueDate.Value.AddMonths(FinalAuditReport.TrainingCompliantFarTargetOffsetMonths);
                    _farCurrentIssueDateLabel.Text = issueDate.Value.AddDays(darTollingDays + farTollingDays).ToShortDateString();
                }

                // immediate report data
                switch (_postElectionAuditNavigation.SelectedReportType)
                {
                    case AuditReportType.DraftAuditReport:
                        DataBindToReport(_dar);
                        // check for "no findings" condition
                        _reportSummary.NoFindingsOverride = _dar == null && _far != null;
                        break;
                    case AuditReportType.FinalAuditReport:
                        DataBindToReport(_far);
                        break;
                    default:
                        _idr = CPProfile.InitialDocumentationRequest;
                        DataBindToReport(_idr);
                        break;
                }

                // current status/suspension check
                if (CPProfile.PESuspension != null)
                {
                    _currentStatus.Text = SuspensionText;
                    _currentStatus.CssClass = "campaign";
                    _darCurrentIssueDateLabel.Text = _farCurrentIssueDateLabel.Text = "(n/a)";
                }
                else if (_far != null)
                {
                    _currentStatus.Text = FinalAuditCompleteText;
                    _currentStatus.CssClass = null;
                }
                else
                {
                    bool responseReceived = false;
                    InadequateEventBase inadequate;
                    if (_dar != null)
                    {
                        inadequate = _dar.InadequateNotice;
                        if (inadequate != null)
                        {
                            responseReceived = inadequate.ResponseReceived;
                            _currentStatus.Text = responseReceived ? UnderReviewText : DarInadequatePendingText;
                        }
                        else
                        {
                            responseReceived = _dar.ResponseReceived;
                            _currentStatus.Text = responseReceived ? UnderReviewText : DarPendingText;
                        }
                    }
                    else if (_idr != null)
                    {
                        inadequate = _idr.InadequateNotice;
                        if (inadequate != null)
                        {
                            responseReceived = inadequate.ResponseReceived;
                            _currentStatus.Text = responseReceived ? UnderReviewText : IdrInadequatePendingText;
                        }
                        else
                        {
                            responseReceived = _idr.ResponseReceived;
                            _currentStatus.Text = responseReceived ? UnderReviewText : IdrPendingText;
                        }
                    }
                    if (!responseReceived)
                        _currentStatus.CssClass = "campaign";
                }
                _tollingEventsList.DataBind();

                this.MarkAsDataBound();
            }
        }

        /// <summary>
        /// Binds the page to data contained in a post election audit report.
        /// </summary>
        /// <param name="report">The report to bind to.</param>
        private void DataBindToReport(AuditReportBase report)
        {
            this.EnsureChildControls();

            // report dates
            _reportSummary.DataSource = report;
            if (report != null)
            {
                _reportSummary.MessagesDataSource = CmoAuditReview.GetAuditReportMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle);

                // inadequate response dates
                report = report.InadequateNotice;
                if (_inadequateSummary.Visible = report != null)
                {
                    _reportSummary.CssClass = "left column two-up";
                    _inadequateSummary.DataSource = report;
                    _inadequateSummary.MessagesDataSource = _reportSummary.MessagesDataSource;
                    _inadequateSummary.DataBind();
                }
            }
            _reportSummary.DataBind();

            // force display of title when report is null
            if (report == null)
                _reportSummary.Title = CPConvert.ToString(_postElectionAuditNavigation.SelectedReportType);
        }

        /// <summary>
        /// Retrieves the tolling events from a post election audit report.
        /// </summary>
        /// <param name="report">The report to analyze.</param>
        /// <returns>The tolling events contained in <paramref name="report"/> if not null; otherwise, null.</returns>
        private TollingEventCollection GetTollingEvents(AuditReportBase report)
        {
            return report == null ? null : report.TollingEvents;
        }

        /// <summary>
        /// Handles the <see cref="TollingEventsList.UpdateDataSources"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void _tollingEventsList_UpdateDataSources(object sender, EventArgs e)
        {
            // tolling events list
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (!Page.IsPostBack || !this.IsDataBound || !_tollingEventsList.EnableViewState || (sm != null && sm.IsInAsyncPostBack))
            {
                AuditReportType type = _postElectionAuditNavigation.SelectedReportType;
                _tollingEventsList.Title = string.Format("{0} Tolling Event Details", _tollingEventsList.DisplayAllEvents ? "All" : CPConvert.ToString(type));
                switch (type)
                {
                    case AuditReportType.FinalAuditReport:
                        _tollingEventsList.DataSource = GetTollingEvents(_dar);
                        _tollingEventsList.MessagesDataSource = CmoAuditReview.GetTollingMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle, true);
                        if (_tollingEventsList.DisplayAllEvents)
                        {
                            IncludeEvents(false);
                        }
                        break;
                    case AuditReportType.DraftAuditReport:
                        _tollingEventsList.DataSource = GetTollingEvents(_idr);
                        _tollingEventsList.MessagesDataSource = CmoAuditReview.GetTollingMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle, false);
                        if (_tollingEventsList.DisplayAllEvents)
                        {
                            IncludeEvents(true);
                        }
                        break;
                    default:
                        _tollingEventsList.DataSource = GetTollingEvents(_idr);
                        _tollingEventsList.MessagesDataSource = CmoAuditReview.GetTollingMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle, false);
                        if (_tollingEventsList.DisplayAllEvents)
                        {
                            IncludeEvents(true);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Includes a collection of tolling events in the tolling events list's data source.
        /// </summary>
        /// <param name="far">true to include Final Audit Report events; otherwise, false to include Draft Audit Report events.</param>
        private void IncludeEvents(bool far)
        {
            if (_tollingEventsList != null)
            {
                var events = GetTollingEvents(far ? (AuditReportBase)_dar : _idr);
                if (events != null)
                    _tollingEventsList.DataSource = _tollingEventsList.DataSource.Union(events).ToList();
                var messages = CmoAuditReview.GetTollingMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle, far);
                if (messages != null)
                    _tollingEventsList.MessagesDataSource = _tollingEventsList.MessagesDataSource.Union(messages).ToDictionary(k => k.Key, v => v.Value);
            }
        }

        protected override bool HasData()
        {
            return CPProfile.HasPostElectionAudit;
        }
    }
}