using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.Web;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication.Login
{
    public partial class Login : MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                // check for logged-in status
                if (Page.User.Identity.IsAuthenticated)
                {
                    string username = Page.User.Identity.Name;
                    // attempt single sign-on application redirect
                    SsoRedirect(username);
                    // BUG #13 - prevent multiple logins
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                }
                else
                {
                    // check for timeout/logout status
                    if (this.Session != null && this.Session.IsNewSession && !Request.HasLoggedOut())
                    {
                        string cookie = Request.Headers["Cookie"];
                        if (!string.IsNullOrEmpty(cookie) && cookie.Contains("ASP.NET_SessionId"))
                        {
                            _loggedOutMessage.Visible = true;
                            _loggedOutMessage.Text = string.Format("Your user session has expired. Please sign in to continue.");
                        }
                    }
                }
            }
            else
            {
                _loggedOutMessage.Visible = false;
            }
        }

        protected void _loginButton_Click(object sender, EventArgs e)
        {
            // show error highlighting for login errors
            string errorFieldName = null;
            if (!Page.IsValid)
            {
                errorFieldName = "UserName";
            }
            else
            {
                _login.FindControl("FailureText").Visible = true;
                errorFieldName = "Password";

                // check if account is disabled -- DISABLED PENDING MANUAL PASSWORD CHECK
                //CPUser user = CPSecurity.Provider.GetUser(_login.UserName);
                //if ((user != null) && !user.Enabled)
                //{
                //    _errorMessage.Visible = true;
                //    _errorMessage.Text = string.Format("Your {0} account has been deactivated. Please contact our Candidate Services Unit at {1} to regain access.", CPProviders.SettingsProvider.ApplicationName, CPProviders.SettingsProvider.AgencyPhoneNumber);
                //    _login.Visible = this.ForgottenPasswordLink.Visible = false;
                //}
            }
            if (errorFieldName != null)
            {
                WebControl errorField = _login.FindControl(errorFieldName) as WebControl;
                if (errorField != null)
                {
                    errorField.CssClass += " error";
                }
            }
        }

        protected void _login_LoggedIn(object sender, EventArgs e)
        {
            Context.CheckPitStops(_login.UserName);
            SsoRedirect(_login.UserName);
        }

        /// <summary>
        /// Generates a single sign-on token for enterprise level cross-application authentication and redirects to a requesting application.
        /// </summary>
        /// <param name="userName">The login name of the user accessing a single sign-on enabled application.</param>
        private void SsoRedirect(string userName)
        {
            byte? appID = Request.GetSsoAppID();
            if (appID.HasValue)
            {
                Token t = CPSecurity.Provider.CreateToken(userName, appID.Value);
                if (t != null)
                {
                    Application app = t.Application;
                    string target = app.RedirectUrl ?? app.Url;
                    if (!string.IsNullOrWhiteSpace(target))
                    {
                        NameValueCollection qs = new NameValueCollection(Request.QueryString);
                        qs[app.TokenParameter ?? "token"] = t.ID.ToString();
                        qs[QueryStringManager.ElectionCycleParameter] = CPProfile.ElectionCycle;
                        qs.Remove(CPSecurity.QueryStringSsoAppIDParameter);
                        Response.Redirect(target, qs);
                    }
                    else
                    {
                        CPSecurity.Provider.ValidateToken(t.ID);
                    }
                }
                else
                {
                    Response.Redirect(PageUrlManager.UnauthorizedPageUrl, Request.QueryString);
                }
            }
        }
    }
}