using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class Submissions : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Gets the target URL of the default tab page.
        /// </summary>
        public string DefaultTabUrl
        {
            get { return this.SubmissionTabs != null ? this.SubmissionTabs.DefaultTabTarget : null; }
        }
    }
}