using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying a candidate's threshold status history.
    /// </summary>
    [ToolboxData("<{0}:ThresholdHistoryControl runat=server></{0}:ThresholdHistoryControl>")]
    public class ThresholdHistoryControl : WebControl
    {
        /// <summary>
        /// Renders a single threshold revision to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        /// <param name="entry">The threshold revision entry to render.</param>
        /// <param name="current">Whether or not the entry being rendered is the most current revision.</param>
        private void RenderThreshold(HtmlTextWriter writer, ThresholdRevision entry, bool current)
        {
            if (writer == null || entry == null) return;
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            new TableCell() { CssClass = "expander" }.RenderControl(writer);
            new TableCell() { CssClass = "statementNumber", Text = current ? entry.Statement.ToDetailString() : null }.RenderControl(writer);
            new TableCell() { CssClass = "numActual", Text = entry.IsUndetermined ? "(n/a)" : entry.Number.ToString("N0") }.RenderControl(writer);
            new TableCell() { CssClass = "numRequired", Text = entry.IsUndetermined ? "(n/a)" : entry.NumberRequired.ToString("N0") }.RenderControl(writer);
            new TableCell() { CssClass = "amtActual", Text = entry.Funds.ToString("C") }.RenderControl(writer);
            new TableCell() { CssClass = "amtRequired", Text = entry.IsUndetermined ? "(n/a)" : entry.FundsRequired.ToString("C") }.RenderControl(writer);
            new TableCell() { CssClass = "version", Text = CPConvert.ToString(entry.Type) }.RenderControl(writer);
            new TableCell() { CssClass = "office", Text = entry.Office.ToAbbrevString() }.RenderControl(writer);
            new TableCell() { CssClass = "date", Text = entry.Date.ToString("d") }.RenderControl(writer);
            writer.RenderEndTag();
        }

        /// <summary>
        /// Gets or sets the title of the threshold status history table.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Threshold History")]
        [Description("The title of the threshold status history table.")]
        [Localizable(true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the source containing the threshold status history to display.
        /// </summary>
        public ThresholdHistory DataSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdHistoryControl"/> control.
        /// </summary>
        public ThresholdHistoryControl()
        {
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-ThresholdHistoryControl");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.DataSource == null || this.DataSource.Current == null)
            {
                writer.Write("You do not have any threshold status history.");
                return;
            }

            // header
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-Threshold caption");
            writer.RenderBeginTag(HtmlTextWriterTag.H3);
            writer.Write(this.Title);
            writer.RenderEndTag();

            // table
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-Threshold cp-ThresholdHistory ms-listviewtable");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, GetAccordionID());
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-viewheadertr");
            writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            new TableHeaderCell() { CssClass = "expander" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "statementNumber", Text = "Statement #" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "numActual", Text = "Threshold #" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "numRequired", Text = "Required #" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "amtActual", Text = "Threshold $" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "amtRequired", Text = "Required $" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "version", Text = "Version" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "office", Text = "Office" }.RenderControl(writer);
            new TableHeaderCell() { CssClass = "date", Text = "Date" }.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            foreach (ThresholdStatus status in this.DataSource.History.Values)
            {
                // current revision
                if (status.Count > 1)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "multiRevision");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
                this.RenderThreshold(writer, status.Current, true);
                writer.RenderEndTag();

                // past revisions
                writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
                for (int i = 1; i < status.Count; i++)
                {
                    this.RenderThreshold(writer, status[i], false);
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            // initialize JS widgets
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            writer.WriteLine("$('#{0}').accordion({{collapsible:true,heightStyle:'content',icons:false,header:'tbody.multiRevision',active:false,multiActive:true}});", GetAccordionID());
            writer.RenderEndTag();
        }

        /// <summary>
        /// Gets an ID to identify the accordion container element.
        /// </summary>
        /// <returns></returns>
        private string GetAccordionID()
        {
            return "ThresholdHistoryPanel_" + this.ClientID;
        }
    }
}
