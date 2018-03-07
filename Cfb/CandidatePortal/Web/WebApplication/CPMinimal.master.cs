using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public partial class CPMinimal : System.Web.UI.MasterPage
    {
        protected string _applicationName;
        protected string _agencyName;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _applicationName = CPProviders.SettingsProvider.ApplicationName;
                _agencyName = CPProviders.SettingsProvider.AgencyName;
            }
            catch
            {
                _applicationName = CPResources.application_name;
                _agencyName = CPResources.agency_name;
            }
        }
    }
}