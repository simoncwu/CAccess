using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public partial class RootLogin : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            Response.Redirect("~/Login/" + Request.Url.Query, true);
        }
    }
}