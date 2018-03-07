using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Controllers
{
    [Authorize]
    public class ComplianceVisitsController : Controller
    {
        public const string ControllerName = "ComplianceVisits";

        // GET: Audit/ComplianceVisits
        public ActionResult Index()
        {
            return View(AuditViewModelFactory.ComplianceVisitsFrom(CPProfile.ComplianceVisits, CmoAuditReview.GetComplianceVisitMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle)));
        }
    }
}