using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers
{
    [Authorize]
    public class FinancialSummaryController : Controller
    {
        public const string ControllerName = "FinancialSummary";

        public const string ActionName_Index = "Index";

        // GET: Profile/FinancialSummary
        public ActionResult Index()
        {
            var summary = FinancialSummary.GetFinancialSummary(CPProfile.Cid, CPProfile.ElectionCycle);
            var candidate = CPProfile.ActiveCandidate;
            var election = CPProfile.Election;
            return View(ProfileViewModelFactory.FinancialSummaryFrom(summary, candidate, election));
        }
    }
}