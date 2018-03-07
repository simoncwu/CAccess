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
    public class DoingBusinessReviewsController : Controller
    {
        public const string ControllerName = "DoingBusinessReviews";

        // GET: Audit/DoingBusinessReviews
        public ActionResult Index()
        {
            return View(AuditViewModelFactory.DoingBusinessReviewsFrom(CPProfile.DoingBusinessReviews, CmoAuditReview.GetDoingBusinessReviewMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle)));
        }
    }
}