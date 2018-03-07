using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication.Errors
{
    public partial class Unauthorized : System.Web.UI.Page
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            byte? appID = Request.GetSsoAppID();
            if (appID.HasValue)
            {
                Cfb.CandidatePortal.Security.Sso.Application app = CPSecurity.Provider.GetApplication(appID.Value);
                if (app != null)
                {
                    _appNameLabel.Text = app.Name;
                    _signoutLink.HRef = string.Format("{0}?{1}", _signoutLink.HRef, QueryStringManager.MakeQueryString(returnUrl: CPSecurity.GetSsoLoginUrl(appID.Value)).ToQueryString());
                }
            }
            else
            {
                _appNameLabel.Text = CPProviders.SettingsProvider.ApplicationName;
            }
        }
    }
}