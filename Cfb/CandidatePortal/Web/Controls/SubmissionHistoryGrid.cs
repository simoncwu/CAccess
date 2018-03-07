using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.SharePoint.Controls;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.CandidatePortal.Web;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// An control for displaying submission document history in a grid format.
    /// </summary>
    public class SubmissionHistoryGrid : GroupGridView
    {
        /// <summary>
        /// Data format string to wrap cell contents for CSS formatting.
        /// </summary>
        internal const string DataFormatString = "<span>{0}</span>";

        /// <summary>
        /// Gets or sets the CSS class assigned to tooltip items.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string TooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class assigned to regular submission tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string RegularTooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class assigned to amendment tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string AmendmentTooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class assigned to resubmission tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string ResubmissionTooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class assigned to internal amendment tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string IAmendmentTooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class assigned to documentation tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string DocumentationTooltipCssClass { get; set; }

        /// <summary>
        /// Gets or sets the title for the grid.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("The title for the grid.")]
        [Localizable(true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
        /// </summary>
        public override string CssClass
        {
            get { return base.CssClass; }
            set { base.CssClass = "submissionGrid ms-listviewtable " + value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmissionHistoryGrid"/> class.
        /// </summary>
        public SubmissionHistoryGrid()
        {
            this.CssClass = null;
            if (string.IsNullOrWhiteSpace(this.TooltipCssClass))
                this.TooltipCssClass = "tooltip";
            this.EmptyDataText = string.Format(Properties.Resources.EmptyDataTextTemplate, "candidate certifications", CPProfile.ElectionCycle);
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "subsection");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.WriteLine(this.Title);
                writer.RenderEndTag();
            }
            base.RenderContents(writer);
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // set up field binding
            BoundField bf;
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Received");
            bf.DataField = "ReceivedDate";
            bf.HeaderStyle.CssClass += " cp-ReceivedDate";
            bf.ItemStyle.CssClass += " cp-ReceivedDate";
            bf.DataFormatString = string.Format(SubmissionHistoryGrid.DataFormatString, Properties.Resources.DateFormat);
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Document Type");
            bf.DataField = "SubmissionType";
            bf.HeaderStyle.CssClass += " cp-SubmissionType";
            bf.ItemStyle.CssClass += " cp-SubmissionType";
            bf.DataFormatString = SubmissionHistoryGrid.DataFormatString;
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Status");
            bf.DataField = "StatusString";
            bf.HeaderStyle.CssClass += " cp-Status";
            bf.ItemStyle.CssClass += " cp-Status";
            bf.DataFormatString = SubmissionHistoryGrid.DataFormatString;
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Status Date");
            bf.DataField = "StatusDate";
            bf.HeaderStyle.CssClass += " cp-StatusAsOf";
            bf.ItemStyle.CssClass += " cp-StatusAsOf";
            bf.DataFormatString = string.Format(SubmissionHistoryGrid.DataFormatString, Properties.Resources.DateFormat);
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Pages");
            bf.DataField = "PageCount";
            bf.HeaderStyle.CssClass += " cp-PageCount";
            bf.ItemStyle.CssClass += " cp-PageCount";
            bf.DataFormatString = SubmissionHistoryGrid.DataFormatString;
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Delivery Type");
            bf.DataField = "DeliveryType";
            bf.HeaderStyle.CssClass += " cp-DeliveryType";
            bf.ItemStyle.CssClass += " cp-DeliveryType";
            bf.DataFormatString = SubmissionHistoryGrid.DataFormatString;
            bf.HtmlEncode = false;
            this.Columns.Add(bf);
            bf = new BoundField();
            bf.HeaderText = string.Format(SubmissionHistoryGrid.DataFormatString, "Postmarked");
            bf.DataField = "PostmarkDate";
            bf.HeaderStyle.CssClass += " cp-PostmarkDate";
            bf.ItemStyle.CssClass += " cp-PostmarkDate";
            bf.DataFormatString = string.Format(SubmissionHistoryGrid.DataFormatString, Properties.Resources.DateFormat);
            bf.HtmlEncode = false;
            bf.NullDisplayText = "(n/a)";
            this.Columns.Add(bf);
        }

        /// <summary>
        /// Rebinds the <see cref="SubmissionHistoryGrid"/> control to its data after the <see cref="SubmissionHistoryGrid.DataMember"/>, <see cref="SubmissionHistoryGrid.DataSource"/>, or <see cref="SubmissionHistoryGrid.DataSourceID"/> property is changed.
        /// </summary>
        protected override void OnDataPropertyChanged()
        {
            if (this.DataSource is IEnumerable<PreElectionDisclosure> && ((IEnumerable<PreElectionDisclosure>)this.DataSource).GroupBy(d => d.CommitteeID).Count() > 1)
                this.GroupBy("Committee", "CommitteeID", "CommitteeName");
            base.OnDataPropertyChanged();
        }

        /// <summary>
        /// Raises the <see cref="GridView.RowDataBound"/> event.
        /// </summary>
        /// <param name="e">A <see cref="GridViewRowEventArgs"/> that contains event data.</param>
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            // additional databinding because data values are not accessible until databinding events
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SubmissionDocument document = e.Row.DataItem as SubmissionDocument;
                if (document != null)
                {
                    // skip a column when grouping (first column is group-by column)
                    e.Row.Cells[this.AllowGrouping ? 2 : 1].Text = GetSubmissionTypeText(document);
                    // BUGFIX #58: Corrected typo in group condition detection when populating delivery type column
                    e.Row.Cells[this.AllowGrouping ? 6 : 5].Text = CPConvert.ToString(document.DeliveryType);
                }
            }
        }

        /// <summary>
        /// Gets the submission type of a document as an HTML-formatted tooltip-enabled string.
        /// </summary>
        /// <param name="document">The <see cref="SubmissionDocument"/> to get a tooltip for.</param>
        internal string GetSubmissionTypeText(SubmissionDocument document)
        {
            SubmissionType type = document.SubmissionType;
            string cssClass = string.Empty;
            switch (type)
            {
                case SubmissionType.Regular:
                    cssClass = this.RegularTooltipCssClass;
                    break;
                case SubmissionType.Amendment:
                    cssClass = this.AmendmentTooltipCssClass;
                    break;
                case SubmissionType.Resubmission:
                    cssClass = this.ResubmissionTooltipCssClass;
                    break;
                case SubmissionType.InternalAmendment:
                    cssClass = this.IAmendmentTooltipCssClass;
                    break;
                case SubmissionType.Documentation:
                    cssClass = this.DocumentationTooltipCssClass;
                    break;
            }
            return string.Format("{0} {1}", CPConvert.ToString(type), string.Format(Properties.Settings.Default.TooltipFormat, string.Format("{0} {1}", this.TooltipCssClass, cssClass)));
        }
    }
}
