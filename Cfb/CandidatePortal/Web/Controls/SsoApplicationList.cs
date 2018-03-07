using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// Control for displaying a list of single sign-on applications.
    /// </summary>
    [ToolboxData("<{0}:SsoApplicationList runat=server></{0}:SsoApplicationList>")]
    public class SsoApplicationList : BulletedList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SsoApplicationList"/> class.
        /// </summary>
        public SsoApplicationList()
        {
            this.EnableViewState = false;
            this.DisplayMode = BulletedListDisplayMode.HyperLink;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            var apps = CPProfile.Applications;
            foreach (var app in apps)
            {
                ListItem li = new ListItem(app.Name, app.Url);

                // auto-generate css class
                if (!string.IsNullOrWhiteSpace(app.Name))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in app.Name.ToCharArray())
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            sb.Append(c);
                        }
                    }
                    li.Attributes.Add("class", sb.ToString().ToLower());
                    li.Attributes.Add("title", app.Description);
                }
                this.Items.Add(li);
            }
        }
    }
}
