using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication.Controls
{
    public partial class NoScript : System.Web.UI.UserControl
    {
        protected string _applicationName;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _applicationName = CPProviders.SettingsProvider.ApplicationName;
            }
            catch
            {
                _applicationName = Resources.CPResources.application_name;
            }

        }
    }
}