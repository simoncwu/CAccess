using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers
{
    public class DisclosureStatementController : Controller
    {
        public const string ControllerName = "DisclosureStatement";

        public const string ActionName_Index = "Index";

        public const string ActionName_Statement = "Statement";

        // GET: Filings/DisclosureStatements
        public ActionResult Index(char? committeeID, byte? statementNumber)
        {
            bool idSpecified = committeeID.HasValue || statementNumber.HasValue;
            var data = GetDisclosureStatementData();
            if (!committeeID.HasValue)
            {
                committeeID = AuthorizedCommittee.GetPrimaryCommitteeID(CPProfile.Cid, CPProfile.ElectionCycle);
            }
            if (committeeID.HasValue && data != null && data.CommitteeNames.ContainsKey(committeeID.Value))
            {
                var statements = data.CommitteeStatements[committeeID.Value].Values;
                if (statements.Any())
                {
                    return Statement(committeeID.Value, statements.Max(s => s.Number));
                }
            }
            return View();
        }

        public ActionResult Statement(char committeeID, byte statementNumber)
        {
            var model = FilingsViewModelFactory.DisclosureStatementListFrom(GetDisclosureStatementData(), committeeID, statementNumber);
            if (Request.IsAjaxRequest())
                return PartialView(Views.DisclosureStatementTable, model.Documents);
            return View(model);
        }

        private DisclosureStatementHistories GetDisclosureStatementData()
        {
            string key = typeof(DisclosureStatementHistories).FullName;
            DisclosureStatementHistories data = ViewData[key] as DisclosureStatementHistories;
            if (data == null)
                ViewData[key] = data = CPProfile.DisclosureStatementHistories;
            return data;
        }
    }
}