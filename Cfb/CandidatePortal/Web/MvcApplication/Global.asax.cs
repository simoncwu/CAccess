using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cfb.CandidatePortal.Web.MvcApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var dataProvider = new ServiceModel.CPDataService.DataService();
            var securityProvider = new ServiceModel.CPSecurityService.SecurityService();
            CPApplication.Initialize(this.Application, dataProvider: dataProvider, cmoProvider: dataProvider, securityProvider: securityProvider);
        }
    }
}
