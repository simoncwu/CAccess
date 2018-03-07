using System;
using System.Web.UI;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying candidate certification history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:CertificationHistoryGrid runat=\"server\" />")]
    public class CertificationHistoryPanel : SubmissionHistoryGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificationHistoryGrid"/> class.
        /// </summary>
        public CertificationHistoryPanel()
        {
            this.CssClass = "certificationHistory";
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
                CertificationHistory ch = CPProfile.CertificationHistory;
                if (ch != null)
                {
                    this.EnsureChildControls();
                    this.DataSource = ch.Documents;
                    this.DataBind();
                }
            }
        }
    }
}
