using System;
using System.Web.UI;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying C-SMART/IDS password request history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:CsmartIdsHistoryGrid runat=\"server\" />")]
    public class CsmartIdsHistoryPanel : SubmissionHistoryGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsmartIdsHistoryGrid"/> class.
        /// </summary>
        public CsmartIdsHistoryPanel()
        {
            this.CssClass = "csmartIdsHistory";
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
                CsmartIdsRequestHistory cirh = CPProfile.CsmartIdsRequestHistory;
                if (cirh != null)
                {
                    this.EnsureChildControls();
                    this.DataSource = cirh.Documents;
                    this.DataBind();
                }
            }
        }
    }
}
