using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.SubmissionDocuments;
using System.ComponentModel;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying pre-election disclosure statement historues in grids.
    /// </summary>
    [ToolboxData("<{0}:PreElectionHistoryPanel runat=\"server\" />")]
    public class PreElectionHistoryPanel : Panel
    {
        /// <summary>
        /// Primary pre-election submission history.
        /// </summary>
        private SubmissionHistoryGrid _primary;

        /// <summary>
        /// General pre-election submission history.
        /// </summary>
        private SubmissionHistoryGrid _general;

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
        /// Raises the <see mref="Init"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs args)
        {
            base.OnInit(args);
            this.EnsureChildControls();
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreElectionDisclosureHistory pedh = CPProfile.PreElectionDisclosureHistory;
                this.EnsureChildControls();
                if (pedh.Primary.Documents.Count > 0)
                {
                    _primary.DataSource = pedh.Primary.Documents;
                    _primary.DataBind();
                }
                else
                {
                    _primary.Visible = false;
                }
                if (pedh.General.Documents.Count > 0)
                {
                    _general.DataSource = pedh.General.Documents;
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
                this.CssClass = "preElectionHistory";
                _primary = new SubmissionHistoryGrid();
                _primary.Title = "Primary Election";
                _general = new SubmissionHistoryGrid();
                _general.Title = "General Election";
                foreach (var grid in new[] { _primary, _general })
                {
                    grid.TooltipCssClass = this.TooltipCssClass;
                    grid.RegularTooltipCssClass = this.RegularTooltipCssClass;
                    grid.AmendmentTooltipCssClass = this.AmendmentTooltipCssClass;
                    grid.ResubmissionTooltipCssClass = this.ResubmissionTooltipCssClass;
                    grid.IAmendmentTooltipCssClass = this.IAmendmentTooltipCssClass;
                    grid.DocumentationTooltipCssClass = this.DocumentationTooltipCssClass;
                }
                this.Controls.Add(_primary);
                this.Controls.Add(_general);
            }
        }
    }
}
