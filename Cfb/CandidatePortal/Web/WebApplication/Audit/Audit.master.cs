using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class Audit : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Gets the target URL of the default tab page.
        /// </summary>
        public string DefaultTabUrl
        {
            get { return this.AuditTabs != null ? this.AuditTabs.DefaultTabTarget : null; }
        }
    }
}