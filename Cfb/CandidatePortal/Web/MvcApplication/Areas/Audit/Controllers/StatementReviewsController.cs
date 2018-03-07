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
    public class StatementReviewsController : Controller
    {
        public const string ControllerName = "StatementReviews";

        // GET: Audit/StatementReviews
        public ActionResult Index()
        {
            return View(AuditViewModelFactory.StatementReviewsFrom(CPProfile.StatementReviews, CmoAuditReview.GetStatementReviewMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle)));
        }
    }
}