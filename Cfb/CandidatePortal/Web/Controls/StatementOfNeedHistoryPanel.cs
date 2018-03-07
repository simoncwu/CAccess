using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying statement of need history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:StatementOfNeedHistoryPanel runat=\"server\" />")]
    public class StatementOfNeedHistoryPanel : Panel
    {
        /// <summary>
        /// Primary election statement of need history.
        /// </summary>
        private SubmissionHistoryGrid _primary;

        /// <summary>
        /// General election statement of need history.
        /// </summary>
        private SubmissionHistoryGrid _general;

        /// <summary>
        /// Raises the <see mref="Init"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs args)
        {
            base.OnInit(args);
            this.EnsureChildControls();
        }

        /// <summary>
        /// Raises the <see mref="Load"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            if (!Page.IsPostBack)
            {
                StatementOfNeedHistory sonh = CPProfile.StatementOfNeedHistory;
                this.EnsureChildControls();
                if (sonh.Primary.Documents.Count > 0)
                {
                    _primary.DataSource = sonh.Primary.Documents;
                    _primary.DataBind();
                }
                else
                {
                    _primary.Visible = false;
                }
                if (sonh.General.Documents.Count > 0)
                {
                    _general.DataSource = sonh.General.Documents;
                    _general.DataBind();
                }
                else
                {
                    _general.Visible = false;
                }
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!Page.IsPostBack)
            {
                this.CssClass = "statementOfNeedHistory";
                _primary = new SubmissionHistoryGrid();
                _primary.Title = "Primary Election";
                _general = new SubmissionHistoryGrid();
                _general.Title = "General Election";
                this.Controls.Add(_primary);
                this.Controls.Add(_general);
            }
        }
    }
}
