using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication.Profile
{
    public partial class Default : CPPage, ISecurePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string defaultUrl = this.Master.DefaultTabUrl;
            if (defaultUrl != null)
                Response.Redirect(defaultUrl, true);
        }
    }
}