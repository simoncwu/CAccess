using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying the current election cycle and allowing switching to other (active) election cycles via a menu bar interface.
    /// </summary>
    [ToolboxData("<{0}:ElectionCycleChooser runat=\"server\" />")]
    public class ElectionCycleChooser : WebControl
    {
        /// <summary>
        /// The inner dropdown list. 
        /// </summary>
        DropDownList _dropDownList;

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.EnsureChildControls();
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(_dropDownList = new DropDownList()
            {
                ID = "ElectionCycleChooserList",
                AutoPostBack = true
            });
            _dropDownList.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                ChangeElectionCycle(_dropDownList.SelectedValue);
            };
            if (!Page.IsPostBack || !Page.EnableViewState || !this.EnableViewState)
            {
                _dropDownList.Items.AddRange(CPProfile.Elections.Values.OrderByDescending(e => e.ElectionDate).Select(e => new ListItem(e.Cycle)).ToArray());
                _dropDownList.SelectedValue = CPProfile.ElectionCycle;
            }
        }

        /// <summary>
        /// Renders the HTML opening tag of the control into the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "ElectionCycleChooser");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ui-front");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Renders the contents of the <see cref="ElectionCycleChooser"/> into the specified writer.
        /// </summary>
        /// <param name="writer">The output stream that renders HTML content to the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // label
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "label");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("Election Cycle");
            writer.RenderEndTag();

            // menu
            if (_dropDownList.Items.Count == 1)
            {
                // only one EC, so render single box instead of dropdown
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ui-selectmenu-button");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ui-selectmenu-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(_dropDownList.Items[0].Text);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            else
            {
                _dropDownList.RenderControl(writer);
            }
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        /// <summary>
        /// Changes the election cycle context for the session.
        /// </summary>
        /// <param name="electionCycle">The desired election cycle to change to.</param>
        private void ChangeElectionCycle(string electionCycle)
        {
            Election e = CPProfile.Elections[electionCycle];
            if (e != null)
            {
                Page.Session.Clear();
                CPProfile.ElectionCycle = electionCycle;
                Page.Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }
    }
}