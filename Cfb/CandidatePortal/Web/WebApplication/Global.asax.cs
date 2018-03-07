using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;
using System.Web.Configuration;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Name of the session variable for storing the candidate's CFIS ID.
        /// </summary>
        private const string CfisIDSessionVariable = @"CfbCPCandidateCfisID";

        /// <summary>
        /// Name of the session variable for storing the candidate's currently selected election cycle context.
        /// </summary>
        private const string ECSessionVariable = @"CfbCPCurrentElectionCycle";

        void Application_Start(object sender, EventArgs e)
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var section = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section != null)
            {
                foreach (ConnectionStringSettings css in section.ConnectionStrings)
                {
                    var overrideSetting = configuration.AppSettings.Settings[css.Name];
                    if (overrideSetting != null)
                    {
                        string cs = Environment.GetEnvironmentVariable(overrideSetting.Value, EnvironmentVariableTarget.Machine);
                        if (!string.IsNullOrEmpty(cs))
                            css.ConnectionString = cs;
                    }
                }
            }

            // Code that runs on application startup
            CPApplication.Initialize(this.Application);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            CPApplication.Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // check for SQL connection problems and show offline message as appropriate
            Exception ex = Server.GetLastError();
            // detect web transfer
            if (CPProviders.DataProvider.IsOfflineDataException(ex.GetBaseException()) || CPSecurity.Provider.IsOfflineDataException(ex.GetBaseException()))
            {
                Session.Clear();
                Session.Abandon();
                Server.Transfer("~/Offline.aspx");
            }
            else if (Request.Url.AbsolutePath.StartsWith("/_", StringComparison.Ordinal))
            {
                Server.RedirectPageNotFound();
            }
            else
            {
                try
                {
                    Cfb.ExceptionHandling.CfbExceptionPolicy.LogException(ex);
                    CPMailMessage email = new CPMailMessage();
                    email.Recipient = new MailAddress(CPApplication.GeneralEmail);
                    email.Subject = "C-Access Server Error Report";
                    email.Body = ex.ToString();
                    HttpException he = ex as HttpException;
                    if (he != null)
                    {
                        int httpCode = he.GetHttpCode();
                        if (httpCode < 500)
                            return;
                        email.Body += "\n\nHTTP Error Code: " + httpCode;
                    }
                    email.Send();
                }
                catch { }
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Session.Clear();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}