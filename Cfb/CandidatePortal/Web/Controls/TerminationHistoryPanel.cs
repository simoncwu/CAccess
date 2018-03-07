using System;
using System.Web.UI;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying termination history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:TerminationHistoryPanel runat=\"server\" />")]
    public class TerminationHistoryPanel : SubmissionHistoryGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerminationHistoryGrid"/> class.
        /// </summary>
        public TerminationHistoryPanel()
        {
            this.CssClass = "terminationHistory";
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
                TerminationHistory th = CPProfile.TerminationHistory;
                if (th != null)
                {
                    this.EnsureChildControls();
                    this.DataSource = th.Documents;
                    this.DataBind();
                }
            }
        }
    }
}
