using System;
using System.Web.UI;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying filer registration history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:FilerRegistrationHistoryGrid runat=\"server\" />")]
    public class FilerRegistrationHistoryPanel : SubmissionHistoryGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilerRegistrationHistoryGrid"/> class.
        /// </summary>
        public FilerRegistrationHistoryPanel()
        {
            this.CssClass = "filerRegistrationHistory";
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
                FilerRegistrationHistory frh = CPProfile.FilerRegistrationHistory;
                {
                    this.EnsureChildControls();
                    this.DataSource = frh.Documents;
                    this.DataBind();
                }
            }
        }
    }
}
