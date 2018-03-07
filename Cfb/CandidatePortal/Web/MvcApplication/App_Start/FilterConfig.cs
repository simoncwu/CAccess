using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DisableResponseCacheAttribute());
        }
    }
}
