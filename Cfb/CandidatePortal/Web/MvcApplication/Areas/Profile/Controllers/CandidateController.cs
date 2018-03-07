using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers
{
    [Authorize]
    public class CandidateController : Controller
    {
        public const string ControllerName = "Candidate";

        public const string ActionName_Index = Mvc.Strings.Actions.Default;

        public const string ActionName_Summary = "Summary";

        // GET: Profile/Candidate
        public ActionResult Index()
        {
            return View(ProfileViewModelFactory.CandidateProfileFrom(CPProfile.ActiveCandidate, CPProfile.AuthorizedCommittees.Committees.Values));
        }

        [ChildActionOnly]
        public ActionResult Summary()
        {
            return PartialView(ProfileViewModelFactory.CandidateProfileFrom(CPProfile.ActiveCandidate));
        }
    }
}