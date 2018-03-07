using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Profile/Home
        public ActionResult Index()
        {
            return RedirectToAction(CandidateController.ActionName_Index, CandidateController.ControllerName);
        }
    }
}