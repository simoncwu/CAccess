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
    public class PublicFundsController : Controller
    {
        public const string ControllerName = "PublicFunds";

        // GET: Audit/PublicFunds
        public ActionResult Index()
        {
            return View(AuditViewModelFactory.PublicFundsPaymentTrackingFrom(CPProfile.PublicFundsHistory));
        }
    }
}