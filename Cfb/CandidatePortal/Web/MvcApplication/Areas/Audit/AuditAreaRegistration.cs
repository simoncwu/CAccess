using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit
{
    public class AuditAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return Mvc.Strings.Areas.Audit;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Audit_default",
                url: "Audit/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { this.GetType().Namespace + ".Controllers" }
            );
        }
    }
}