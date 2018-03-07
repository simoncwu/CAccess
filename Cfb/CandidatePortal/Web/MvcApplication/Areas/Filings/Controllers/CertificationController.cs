using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    public class CertificationController : Controller
    {
        public const string ControllerName = "Certification";

        // GET: Filings/Certification
        public ActionResult Index()
        {
            return View(FilingsViewModelFactory.DocumentStatusListFrom(CPProfile.CertificationHistory));
        }
    }
}