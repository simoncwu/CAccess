using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        public const string ControllerName = "Training";

        public const string ActionName_Index = "Index";

        // GET: Profile/Training
        public ActionResult Index()
        {
            DateTime cutoff = (CPProviders.DataProvider.GetWebTransferDate(CPProfile.ElectionCycle) ?? DateTime.Today).AddDays(-1);
            var model = ProfileViewModelFactory.TrainingTrackingFrom(CPProfile.TrainingStatus, cutoff);
            model.AgencyPhoneNumber = CPProviders.SettingsProvider.AgencyPhoneNumber;
            return View(model);
        }
    }
}