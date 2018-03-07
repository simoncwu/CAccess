using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public partial class Logout : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            NameValueCollection logoutQS = new NameValueCollection { };
            if (User.Identity.IsAuthenticated)
            {
                logoutQS = QueryStringManager.MakeQueryString(loggedOut: true);
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }

            // check for preferred redirection URL
            string target = Request.GetReturnUrl();
            if (!string.IsNullOrWhiteSpace(target))
            {
                Response.Redirect(target);
            }

            // check SSO application properties
            byte? appID = Request.GetSsoAppID();
            Application app = null;
            if (appID.HasValue)
                app = CPSecurity.Provider.GetApplication(appID.Value);
            if (app != null)
            {
                if (!string.IsNullOrWhiteSpace(app.Url))
                    Response.Redirect(app.Url);
                else
                    Response.Redirect(FormsAuthentication.LoginUrl, new NameValueCollection { { CPSecurity.QueryStringSsoAppIDParameter, app.ID.ToString() }, logoutQS });
            }

            // default redirection
            Response.Redirect(FormsAuthentication.LoginUrl, logoutQS);
        }
    }
}