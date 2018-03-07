using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return Mvc.Strings.Areas.Profile;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Committee_CampaignContact",
                url: string.Format("{0}/{1}/{{committeeID}}/{2}/{{id}}", this.AreaName, CommitteeController.ControllerName, CommitteeController.ActionName_CampaignStaff),
                defaults: new { controller = CommitteeController.ControllerName, action = CommitteeController.ActionName_CampaignStaff, id = UrlParameter.Optional }
                );
            context.MapRoute(
                name: "Committee_BankAccount",
                url: string.Format("{0}/{1}/{{committeeID}}/{2}/{{id}}", this.AreaName, CommitteeController.ControllerName, CommitteeController.ActionName_BankAccount),
                defaults: new { controller = CommitteeController.ControllerName, action = CommitteeController.ActionName_BankAccount, id = UrlParameter.Optional }
                );
            context.MapRoute(
                name: "Profile_default",
                url: this.AreaName + "/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { this.GetType().Namespace + ".Controllers" }
                );
        }
    }
}