using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication.Errors
{
    public partial class ServerError : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _agencyPhoneNumber.Text = CPProviders.SettingsProvider.AgencyPhoneNumber;
            }
            catch
            {
                _agencyPhoneNumber.Text = CPResources.agency_phone;
            }
        }
    }
}