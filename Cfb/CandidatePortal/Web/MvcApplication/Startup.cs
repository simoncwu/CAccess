using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cfb.CandidatePortal.Web.MvcApplication.Startup))]
namespace Cfb.CandidatePortal.Web.MvcApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
