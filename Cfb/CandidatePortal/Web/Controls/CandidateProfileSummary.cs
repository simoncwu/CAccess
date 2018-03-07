using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying a candidate profile summary.
    /// </summary>
    [DefaultProperty("DataSource")]
    [ToolboxData("<{0}:CandidateProfileSummary runat=server></{0}:CandidateProfileSummary>")]
    public class CandidateProfileSummary : WebControl
    {
        // Default property values
        private const string DefaultEmptyValueText = "(not on file)";
        private const string DefaultEmptyLiaisonText = "(not assigned)";

        /// <summary>
        /// The format string for text with an expander link.
        /// </summary>
        private const string ExpanderLinkTextFormat = "<span><a href=\"{1}\" class=\"moreinfo\" title=\"{2}\"><img src=\"/images/expand.gif\" width=\"16\" height=\"16\" alt=\"More Info\" /></a>{0}</span>";

        /// <summary>
        /// Gets or sets the text to display for empty values.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultEmptyValueText)]
        [Description("The text to display for empty values.")]
        [Localizable(true)]
        public string EmptyValueText
        {
            get { return this.ViewState["EmptyValueText"] as string ?? DefaultEmptyValueText; }
            set { this.ViewState["EmptyValueText"] = value; }
        }

        /// <summary>
        /// Gets or sets the text to display for missing liaison names.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultEmptyLiaisonText)]
        [Description("The text to display for missing liaison names.")]
        [Localizable(true)]
        public string EmptyLiaisonText
        {
            get { return this.ViewState["EmptyLiaisonText"] as string ?? DefaultEmptyLiaisonText; }
            set { this.ViewState["EmptyLiaisonText"] = value; }
        }

        /// <summary>
        /// Gets or sets the active candidate whose profile summary is to be displayed.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [Description("The active candidate to display.")]
        public ActiveCandidate DataSource { get; set; }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "CandidateProfileSummary");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            ActiveCandidate candidate = this.DataSource;
            if (candidate == null)
            {
                writer.Write(this.EmptyValueText);
                return;
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "CandidateProfileSummary");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            // header
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "header");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            if (candidate.IsTerminated)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "terminated");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Candidate Information");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            // candidate ID
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "top");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>ID</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(ExpanderLinkTextFormat, candidate.ID, PageUrlManager.CandidateProfilePageUrl, "View Full Candidate Profile");
            writer.RenderEndTag();
            writer.RenderEndTag();

            // candidate name
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>Name</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", candidate.Name);
            writer.RenderEndTag();
            writer.RenderEndTag();

            // Cert/FR date
            if (candidate.HasCertified || candidate.HasRegistered)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write("<label>{0}</label>", candidate.HasCertified ? "Certification Date" : "Filer Registration Date");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("<span>{0:MMMM d, yyyy}</span>", candidate.HasCertified ? candidate.CertificationDate : candidate.FilerRegistrationDate);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            // termination date
            if (candidate.IsTerminated)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write("<label>Termination Date</label>");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("<span>{0:MMMM d, yyyy}</span>", candidate.TerminationDate);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            // office sought
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>Office Sought</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", Page.Server.HtmlEncode(candidate.Office.ToString()));
            writer.RenderEndTag();
            writer.RenderEndTag();

            // classification
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>Classification</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", CPConvert.ToString(candidate.Classification));
            writer.RenderEndTag();
            writer.RenderEndTag();

            // e-mail
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "bottom");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>E-mail Address</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", string.IsNullOrEmpty(candidate.Email) ? EmptyValueText : string.Format("<a href=\"mailto:{0}\">{0}</a>", candidate.Email));
            writer.RenderEndTag();
            writer.RenderEndTag();

            // principal committee
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "newSection top bottom");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>Principal Committee</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write(ExpanderLinkTextFormat, string.IsNullOrEmpty(candidate.PrincipalCommittee) ? EmptyValueText : candidate.PrincipalCommittee, PageUrlManager.CommitteeProfilePageUrl, "View Full Committee Profile");
            writer.RenderEndTag();
            writer.RenderEndTag();

            // CSU liaison
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "newSection top");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>CSU Liaison</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", candidate.CsuLiaisonName ?? this.EmptyLiaisonText);
            writer.RenderEndTag();
            writer.RenderEndTag();

            // audit liaison
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "bottom");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("<label>Audit Liaison</label>");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("<span>{0}</span>", candidate.AuditorName ?? this.EmptyLiaisonText);
            writer.RenderEndTag();
            writer.RenderEndTag();

            // closing table tag
            writer.RenderEndTag();
        }
    }
}
