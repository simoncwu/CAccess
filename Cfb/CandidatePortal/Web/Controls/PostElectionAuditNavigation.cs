using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.TrainingTracking;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for navigating between post election audit report types.
    /// </summary>
    [ToolboxData("<{0}:PostElectionNavigationControl runat=server></{0}:PostElectionNavigationControl>")]
    public class PostElectionAuditNavigation : WebControl
    {
        #region Default Values

        private const bool DefaultDarFarPresent = false;
        private const string DefaultQueryStringParameterName = "peart";
        private const string DefaultIdrTitle = "Request for Documentation";
        private const string DefaultDarTitle = "Draft Audit Report";
        private const string DefaultFarTitle = "Final Audit Report";

        #endregion

        /// <summary>
        /// Gets the <see cref="HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        /// <summary>
        /// Gets or sets the title to display for the control.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("The title to display for the control.")]
        public string Title
        {
            get { return this.ViewState["Title"] as string ?? string.Empty; }
            set { this.ViewState["Title"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Initial Documentation Request when it is selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Initial Documentation Request when it is selected.")]
        [UrlProperty("*.gif;*.png")]
        public string IdrOnImageUrl
        {
            get { return this.ViewState["IdrOnImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["IdrOnImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Initial Documentation Request when it is not selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Initial Documentation Request when it is not selected.")]
        [UrlProperty("*.gif;*.png")]
        public string IdrOffImageUrl
        {
            get { return this.ViewState["IdrOffImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["IdrOffImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the title to display for Initial Documentation Request.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultIdrTitle)]
        [Description("The title to display for Initial Documentation Request.")]
        public string IdrTitle
        {
            get { return this.ViewState["IdrTitle"] as string ?? string.Empty; }
            set { this.ViewState["IdrTitle"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Draft Audit Report when it is selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Draft Audit Report when it is selected.")]
        [UrlProperty("*.gif;*.png")]
        public string DarOnImageUrl
        {
            get { return this.ViewState["DarOnImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["DarOnImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Draft Audit Report when it is not selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Draft Audit Report when it is not selected.")]
        [UrlProperty("*.gif;*.png")]
        public string DarOffImageUrl
        {
            get { return this.ViewState["DarOffImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["DarOffImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Draft Audit Report when it is disabled.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Draft Audit Report when it is disabled.")]
        [UrlProperty("*.gif;*.png")]
        public string DarDisabledImageUrl
        {
            get { return this.ViewState["DarDisabledImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["DarDisabledImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the title to display for Draft Audit Report.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultDarTitle)]
        [Description("The title to display for Draft Audit Report.")]
        public string DarTitle
        {
            get { return this.ViewState["DarTitle"] as string ?? string.Empty; }
            set { this.ViewState["DarTitle"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Final Audit Report when it is selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Final Audit Report when it is selected.")]
        [UrlProperty("*.gif;*.png")]
        public string FarOnImageUrl
        {
            get { return this.ViewState["FarOnImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["FarOnImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Final Audit Report when it is not selected.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Final Audit Report when it is not selected.")]
        [UrlProperty("*.gif;*.png")]
        public string FarOffImageUrl
        {
            get { return this.ViewState["FarOffImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["FarOffImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the path to an image to display for the Final Audit Report when it is disabled.
        /// </summary>
        [Category("Appearance")]
        [Description("The path to an image to display for the Final Audit Report when it is disabled.")]
        [UrlProperty("*.gif;*.png")]
        public string FarDisabledImageUrl
        {
            get { return this.ViewState["FarDisabledImageUrl"] as string ?? string.Empty; }
            set { this.ViewState["FarDisabledImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the title to display for Final Audit Report.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultFarTitle)]
        [Description("The title to display for Final Audit Report.")]
        public string FarTitle
        {
            get { return this.ViewState["FarTitle"] as string ?? string.Empty; }
            set { this.ViewState["FarTitle"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not a Draft Audit Report is present.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultDarFarPresent)]
        [Description("Whether or not a Draft Audit Report is present.")]
        public bool DarPresent
        {
            get
            {
                object obj = this.ViewState["DarPresent"];
                return obj is bool ? (bool)obj : DefaultDarFarPresent;
            }
            set
            {
                this.ViewState["DarPresent"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not Draft Audit Report tolling events are present.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultDarFarPresent)]
        [Description("Whether or not Draft Audit Report tolling events are present.")]
        public bool DarTollingPresent
        {
            get
            {
                object obj = this.ViewState["DarTollingPresent"];
                return obj is bool ? (bool)obj : DefaultDarFarPresent;
            }
            set
            {
                this.ViewState["DarTollingPresent"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not a Final Audit Report is present.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultDarFarPresent)]
        [Description("Whether or not a Final Audit Report is present.")]
        public bool FarPresent
        {
            get
            {
                object obj = this.ViewState["FarPresent"];
                return obj is bool ? (bool)obj : DefaultDarFarPresent;
            }
            set
            {
                this.ViewState["FarPresent"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not Final Audit Report tolling events are present.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultDarFarPresent)]
        [Description("Whether or not Final Audit Report tolling events are present.")]
        public bool FarTollingPresent
        {
            get
            {
                object obj = this.ViewState["FarTollingPresent"];
                return obj is bool ? (bool)obj : DefaultDarFarPresent;
            }
            set
            {
                this.ViewState["FarTollingPresent"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the query string parameter used for passing navigation information.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(DefaultQueryStringParameterName)]
        [Description("The name of the query string parameter used for passing navigation information.")]
        public string QueryStringParameterName
        {
            get { return this.ViewState["QueryStringParameterName"] as string ?? string.Empty; }
            set { this.ViewState["QueryStringParameterName"] = value; }
        }

        /// <summary>
        /// Gets the currently selected report type.
        /// </summary>
        public AuditReportType SelectedReportType
        {
            get
            {
                string name = Page.Request.QueryString[this.QueryStringParameterName];
                if (!string.IsNullOrEmpty(name) && Enum.IsDefined(typeof(AuditReportType), name))
                    return CPConvert.ParseEnum<AuditReportType>(name);
                else if (this.FarPresent)
                    return AuditReportType.FinalAuditReport;
                else if (this.DarPresent)
                    return AuditReportType.DraftAuditReport;
                else
                    return AuditReportType.InitialDocumentationRequest;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostElectionAuditNavigation"/> control.
        /// </summary>
        public PostElectionAuditNavigation()
        {
            this.CssClass = "cp-" + this.GetType().Name;
            this.QueryStringParameterName = DefaultQueryStringParameterName;
            this.IdrTitle = DefaultIdrTitle;
            this.DarTitle = DefaultDarTitle;
            this.FarTitle = DefaultFarTitle;
        }

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page != null)
            {
                this.Page.RegisterRequiresViewStateEncryption();
                this.Page.RegisterRequiresControlState(this);
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // render title
            string title = this.Title;
            if (!string.IsNullOrEmpty(title))
            {
                writer.Write(title);
            }

            // render navigation container
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            // render IDR
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "documentationRequest");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, PageUrlManager.GetPostElectionAuditReportUrl(AuditReportType.InitialDocumentationRequest));
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.IdrTitle);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ResolveUrl(this.SelectedReportType == AuditReportType.InitialDocumentationRequest ? this.IdrOnImageUrl : this.IdrOffImageUrl));
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.IdrTitle);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            // render DAR
            bool enableTab = this.DarPresent || this.DarTollingPresent;
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "draftAuditReport");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            if (enableTab)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, PageUrlManager.GetPostElectionAuditReportUrl(AuditReportType.DraftAuditReport));
                writer.AddAttribute(HtmlTextWriterAttribute.Title, this.DarTitle);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ResolveUrl(!enableTab ? this.DarDisabledImageUrl : this.SelectedReportType == AuditReportType.DraftAuditReport ? this.DarOnImageUrl : this.DarOffImageUrl));
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.DarTitle);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            if (enableTab)
            {
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            // render FAR
            enableTab = this.FarPresent || this.FarTollingPresent;
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "finalAuditReport");
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            if (enableTab)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, PageUrlManager.GetPostElectionAuditReportUrl(AuditReportType.FinalAuditReport));
                writer.AddAttribute(HtmlTextWriterAttribute.Title, this.FarTitle);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ResolveUrl(!enableTab ? this.FarDisabledImageUrl : this.SelectedReportType == AuditReportType.FinalAuditReport ? this.FarOnImageUrl : this.FarOffImageUrl));
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.FarTitle);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            if (enableTab)
            {
                writer.RenderEndTag();
            }
            writer.RenderEndTag();

            // end render navigation container
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
