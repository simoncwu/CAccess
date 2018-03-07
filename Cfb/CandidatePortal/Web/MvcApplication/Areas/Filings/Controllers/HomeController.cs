using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Filings/Home
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            foreach (var c in model.Controllers)
            {
                return RedirectToAction(Actions.Default, c.Value);
            }
            return View(model);
        }
    }
}