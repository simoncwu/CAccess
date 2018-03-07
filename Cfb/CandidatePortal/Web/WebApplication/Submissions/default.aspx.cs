using System;
using Cfb.CandidatePortal.Web.Controls;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class Default : CPPage, ISecurePage
    {
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            string defaultUrl = this.Master.DefaultTabUrl;
            if (defaultUrl != null)
                Response.Redirect(defaultUrl, true);
        }
    }
}