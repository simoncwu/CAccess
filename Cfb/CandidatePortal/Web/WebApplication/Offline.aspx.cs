using System;
using System.Web.Security;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public partial class Offline : System.Web.UI.Page
    {
        protected string _applicationName;

        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                _applicationName = CPProviders.SettingsProvider.ApplicationName;
            }
            catch
            {
                _applicationName = Resources.CPResources.application_name;
            }
            try
            {
                refreshButton_Click(this, e);
            }
            catch (Exception ex)
            {
                if (!CPProviders.DataProvider.IsOfflineDataException(ex) && !CPSecurity.Provider.IsOfflineDataException(ex))
                    throw;
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
            }
        }

        protected void refreshButton_Click(object sender, EventArgs e)
        {
            CPProviders.DataProvider.PingCfis();
            Response.Redirect("~");
        }
    }
}