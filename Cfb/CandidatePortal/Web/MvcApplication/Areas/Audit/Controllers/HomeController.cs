using Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Audit/Home
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            foreach (var c in model.Controllers)
            {
                return RedirectToAction(Mvc.Strings.Actions.Default, c.Value);
            }
            return View(model);
        }
    }
}