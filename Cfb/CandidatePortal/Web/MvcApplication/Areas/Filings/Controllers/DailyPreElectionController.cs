using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    public class DailyPreElectionController : Controller
    {
        public const string ControllerName = "DailyPreElection";

        // GET: Filings/DailyPreElection
        public ActionResult Index()
        {
            var rawData = CPProfile.PreElectionDisclosureHistory ?? new SubmissionDocuments.PreElectionDisclosureHistory();
            return View(FilingsViewModelFactory.PrimaryGeneralStatusListFrom(rawData.General, rawData.Primary));
        }
    }
}