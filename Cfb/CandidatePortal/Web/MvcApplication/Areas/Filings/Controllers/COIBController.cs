using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    public class COIBController : Controller
    {
        public const string ControllerName = "COIB";

        // GET: Filings/COIB
        public ActionResult Index()
        {
            return View(FilingsViewModelFactory.DocumentStatusListFrom(CPProfile.CoibReceiptHistory));
        }
    }
}