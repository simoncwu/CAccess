using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication.Profile
{
    public partial class Profile : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Gets the target URL of the default tab page.
        /// </summary>
        public string DefaultTabUrl
        {
            get { return this.ProfileTabs != null ? this.ProfileTabs.DefaultTabTarget : null; }
        }
    }
}