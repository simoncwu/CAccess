using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings
{
    public class FilingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Filings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Filings_default",
                url: "Filings/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { this.GetType().Namespace + ".Controllers" }
            );
        }
    }
}