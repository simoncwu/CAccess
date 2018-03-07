using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.SubmissionDocuments;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying COIB receipt history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:CoibReceiptHistoryGrid runat=\"server\" />")]
    public class CoibReceiptHistoryPanel : SubmissionHistoryGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoibReceiptHistoryGrid"/> class.
        /// </summary>
        public CoibReceiptHistoryPanel()
        {
            this.CssClass = "coibReceiptHistory";
        }

        /// <summary>
        /// Raises the <see mref="Init"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs args)
        {
            base.OnInit(args);
            if (!Page.IsPostBack)
            {
                this.EnsureChildControls();
                BoundField bf = new BoundField();
                bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "COIB Filing");
                bf.DataField = "CoibFilingDate";
                bf.HeaderStyle.CssClass += " cp-CoibDate";
                bf.ItemStyle.CssClass += " cp-CoibDate";
                bf.DataFormatString = string.Format(SubmissionHistoryGrid.DataFormatString, Properties.Resources.DateFormat);
                bf.HtmlEncode = false;
                this.Columns.Add(bf);
            }
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
                CoibReceiptHistory ch = CPProfile.CoibReceiptHistory;
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
