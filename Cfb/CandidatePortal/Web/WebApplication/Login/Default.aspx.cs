using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Web.WebApplication.Login
{
    public partial class Default : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            byte? appID = Request.GetSsoAppID();
            if (appID.HasValue)
            {
                Cfb.CandidatePortal.Security.Sso.Application app = CPSecurity.Provider.GetApplication(appID.Value);
                if (app != null && !string.IsNullOrWhiteSpace(app.LoginPagePath))
                {
                    try
                    {
                        Server.Transfer(app.LoginPagePath, true);
                    }
                    catch (HttpException ex)
                    {
                    }
                }
            }
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                CPProviders.DataProvider.PingCfis(); // try handshake with CFISPUB
            }
        }
    }
}