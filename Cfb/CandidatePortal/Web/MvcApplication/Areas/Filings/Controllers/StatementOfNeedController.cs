using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    public class StatementOfNeedController : Controller
    {
        public const string ControllerName = "StatementOfNeed";

        // GET: Filings/StatementOfNeed
        public ActionResult Index()
        {
            var rawData = CPProfile.StatementOfNeedHistory ?? new SubmissionDocuments.StatementOfNeedHistory();
            return View(FilingsViewModelFactory.PrimaryGeneralStatusListFrom(rawData.General, rawData.Primary));
        }
    }
}