using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Controllers
{
    [Authorize]
    public class PostElectionController : Controller
    {
        public const string ControllerName = "PostElection";

        public const string ActionName_Index = "Index";

        public const string ActionName_StageSummary = "StageSummary";

        public const string ActionName_TollingEvents = "TollingEvents";

        // GET: Audit/PostElection
        public ActionResult Index()
        {
            var idr = GetReport(AuditReportType.InitialDocumentationRequest);
            var dar = GetReport(AuditReportType.DraftAuditReport);
            var far = GetReport(AuditReportType.FinalAuditReport);
            var cid = CPProfile.Cid;
            var ec = CPProfile.ElectionCycle;
            var training = CPProfile.TrainingStatus;
            bool trainingComplete = training != null && training.AuditComplianceAchieved;
            var model = AuditViewModelFactory.PostElectionFrom(
                idr: idr,
                dar: dar,
                far: far,
                agencyPhone: CPProviders.SettingsProvider.AgencyPhoneNumber,
                darTollingDays: idr != null ? idr.TollingDaysIncurred : InitialDocumentationRequest.CountDarTollingDaysIncurred(cid, ec),
                farTollingDays: dar != null ? dar.TollingDaysIncurred : DraftAuditReport.CountFarTollingDaysIncurred(cid, ec),
                darIssueDate: DraftAuditReport.GetOriginalIssuanceDate(cid, ec),
                farIssueDate: FinalAuditReport.GetOriginalIssuanceDate(cid, ec),
                farOffsetMonths: trainingComplete ? FinalAuditReport.TrainingCompliantFarTargetOffsetMonths : 0,
                suspended: CPProfile.PESuspension != null);
            return View(model);
        }

        public ActionResult StageSummary(AuditReportType id)
        {
            var stage = GetReport(id);
            var model = AuditViewModelFactory.PostElectionSummaryFrom(stage, CmoAuditReview.GetAuditReportMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle));
            if (stage == null)
            {
                // check for no findings condition
                if (CPProfile.HasFinalAuditReport)
                {
                    model.ReportResponse = AuditViewModelFactory.NoFindingsResponse();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return PartialView(Mvc.Strings.Views.PostElectionSummary, model);
        }

        private AuditReportBase GetReport(AuditReportType type)
        {
            string key = string.Join(type.GetType().FullName, "_", type.ToString());
            AuditReportBase report = null;
            if (ViewData.ContainsKey(key))
                report = ViewData[key] as AuditReportBase;
            if (report == null)
            {
                switch (type)
                {
                    case AuditReportType.InitialDocumentationRequest:
                        report = CPProfile.InitialDocumentationRequest;
                        break;
                    case AuditReportType.DraftAuditReport:
                        report = CPProfile.DraftAuditReport;
                        break;
                    case AuditReportType.FinalAuditReport:
                        report = CPProfile.FinalAuditReport;
                        break;
                }
                if (report != null)
                    ViewData[key] = report;
            }
            return report;
        }

        public ActionResult TollingEvents(AuditReportType id = AuditReportType.FinalAuditReport)
        {
            AuditReportBase source = null,
                secondSource = null;
            switch (id)
            {
                case AuditReportType.InitialDocumentationRequest:
                    source = GetReport(AuditReportType.InitialDocumentationRequest);
                    break;
                case AuditReportType.DraftAuditReport:
                    source = GetReport(AuditReportType.DraftAuditReport);
                    break;
                case AuditReportType.FinalAuditReport:
                    source = GetReport(AuditReportType.InitialDocumentationRequest);
                    secondSource = GetReport(AuditReportType.DraftAuditReport);
                    break;
            }
            return PartialView(Mvc.Strings.Views.TollingEvents, AuditViewModelFactory.TollingEventsListFrom(source, CmoAuditReview.GetTollingMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle, false), secondSource));
        }
    }
}