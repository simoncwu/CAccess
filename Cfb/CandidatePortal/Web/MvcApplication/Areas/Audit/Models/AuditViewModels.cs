using Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Controllers;
using Cfb.CandidatePortal.Web.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public abstract class AuditViewModelBase : ITabPageViewModel
    {
        public const string ResponseNotRequiredText = "Response not required";

        public const string ComplianceVisitNumberName = "Visit";

        public string Area
        {
            get { return Mvc.Strings.Areas.Audit; }
        }

        public IDictionary<string, string> Controllers { get; set; }

        public string AreaName
        {
            get { return Mvc.Strings.AreaNames.Audit; }
        }

        public AuditViewModelBase()
        {
            var c = this.Controllers = new Dictionary<string, string>();
            if (CPProfile.HasStatementReviews)
                c.Add("Statement Reviews", StatementReviewsController.ControllerName);
            if (CPProfile.HasDoingBusinessReviews)
                c.Add("Doing Business", DoingBusinessReviewsController.ControllerName);
            if (CPProfile.HasComplianceVisits)
                c.Add("Compliance Visits", ComplianceVisitsController.ControllerName);
            if (CPProfile.HasThresholdHistory)
                c.Add("Threshold Status", ThresholdStatusController.ControllerName);
            if (CPProfile.HasPublicFundsHistory)
                c.Add("Public Funds", PublicFundsController.ControllerName);
            if (CPProfile.HasPostElectionAudit)
                c.Add("Post Election", PostElectionController.ControllerName);
        }
    }

    public class HomeViewModel : AuditViewModelBase
    {
    }
}