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
    public class ThresholdStatusController : Controller
    {
        public const string ControllerName = "ThresholdStatus";

        // GET: Audit/ThresholdStatus
        public ActionResult Index()
        {
            return View(AuditViewModelFactory.ThresholdStatusFrom(CPProfile.ThresholdHistory));
        }
    }
}