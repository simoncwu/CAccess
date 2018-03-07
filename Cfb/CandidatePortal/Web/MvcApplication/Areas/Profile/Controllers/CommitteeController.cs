using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers
{
    [Authorize]
    public class CommitteeController : Controller
    {
        public const string ControllerName = "Committee";

        public const string ActionName_Principal = "Principal";

        public const string ActionName_Index = "Index";

        public const string ActionName_Details = "Details";

        public const string ActionName_CampaignStaff = "CampaignStaff";

        public const string ActionName_BankAccount = "BankAccount";

        // GET: Profile/Committee
        public ActionResult Index()
        {
            return RedirectToAction(ActionName_Principal);
        }

        // GET: Profile/Committee/Details/L
        public ActionResult Details(char? id)
        {
            if (id == null)
                return RedirectToAction(ActionName_Principal);
            AuthorizedCommittee comm = null;
            if (id.HasValue)
                comm = FindCommittee(id.Value);
            if (comm != null)
                return View(ProfileViewModelFactory.CommitteeProfileFrom(comm));
            return HttpNotFound();
        }

        // GET: Profile/Committee/Principal
        public ActionResult Principal()
        {
            return View(ActionName_Details, ProfileViewModelFactory.CommitteeProfileFrom(CPProfile.AuthorizedCommittees.PrincipalCommittee));
        }

        // GET: Profile/Committee/{committeeID}/CampaignStaff/{id}
        public ActionResult CampaignStaff(char committeeID, byte? id)
        {
            var comm = FindCommittee(committeeID);
            if (comm != null)
            {
                var liaisons = comm.Liaisons;

                // contacts listing
                if (!id.HasValue)
                    return PartialView(ActionName_CampaignStaff + "List", liaisons.Values.Select(l => ProfileViewModelFactory.CampaignContactSummaryFrom(l, committeeID)));

                // contact details
                if (liaisons != null && liaisons.ContainsKey(id.Value))
                {
                    var contact = ProfileViewModelFactory.CampaignStaffFrom(liaisons[id.Value]);
                    contact.CommitteeID = committeeID;
                    return PartialView(contact);
                }
            }
            return HttpNotFound();
        }

        // GET: Profile/Committee/{committeeID}/BankAccount/{id}
        public ActionResult BankAccount(char committeeID, byte? id)
        {
            var comm = FindCommittee(committeeID);
            if (comm != null)
            {
                var accounts = comm.BankAccounts;

                // accounts listing
                if (!id.HasValue)
                    return PartialView(ActionName_BankAccount + "List", accounts.Values.Select(a => ProfileViewModelFactory.BankAccountSummaryFrom(a, committeeID)));

                // account details
                if (accounts != null && accounts.ContainsKey(id.Value))
                    return PartialView(ProfileViewModelFactory.BankAccountFrom(accounts[id.Value], committeeID));
            }
            return HttpNotFound();
        }

        private static AuthorizedCommittee FindCommittee(char id)
        {
            return (from o in CPProfile.AuthorizedCommittees.Committees
                    where o.Key == id
                    select o.Value).SingleOrDefault();
        }
    }
}