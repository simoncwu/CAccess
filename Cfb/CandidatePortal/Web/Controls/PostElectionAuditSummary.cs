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

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a post election audit status summary.
	/// </summary>
	[ToolboxData("<{0}:PostElectionAuditSummary runat=server></{0}:PostElectionAuditSummary>")]
	public class PostElectionAuditSummary : DataBoundControlBase
	{
		#region Default Property Values

		private const string DefaultTitle = "";
		private const string DefaultSentTitle = "Report Issued";
		private const string DefaultSecondSentTitle = "Second Report Issued";
		private const string DefaultNotYetIssuedText = "Not Yet Issued";
		private const string DefaultResponseNotReceivedText = "Not Received";
		private const string DefaultNoReportText = "A report was not issued for the current election cycle.";
		private const bool DefaultNoFindingsOverride = false;

		#endregion

		/// <summary>
		/// Gets or sets the text to display as the title of the control.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultTitle)]
		[Description("The text to display as the title of the control.")]
		[Localizable(true)]
		public string Title
		{
			get { return this.ViewState["Title"] as string ?? string.Empty; }
			set { this.ViewState["Title"] = value; }
		}

		/// <summary>
		/// Gets or sets the text to display when a report has not yet been issued.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultNotYetIssuedText)]
		[Description("The text to display when a report has not yet been issued.")]
		[Localizable(true)]
		public string NotYetIssuedText
		{
			get { return this.ViewState["NotYetIssuedText"] as string ?? string.Empty; }
			set { this.ViewState["NotYetIssuedText"] = value; }
		}

		/// <summary>
		/// Gets or sets the text to display when a response has not yet been received.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultResponseNotReceivedText)]
		[Description("The text to display when a response has not yet been received.")]
		[Localizable(true)]
		public string ResponseNotReceivedText
		{
			get { return this.ViewState["ResponseNotReceivedText"] as string ?? string.Empty; }
			set { this.ViewState["ResponseNotReceivedText"] = value; }
		}

		/// <summary>
		/// The text to display when no report was issued.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultNoReportText)]
		[Description("The text to display when no report was issued.")]
		[Localizable(true)]
		public string NoReportText
		{
			get { return this.ViewState["NoReportText"] as string ?? string.Empty; }
			set { this.ViewState["NoReportText"] = value; }
		}

		/// <summary>
		/// Gets or sets the text to display as the title of the sent date entry.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultSentTitle)]
		[Description("The text to display as the title of the sent date entry.")]
		[Localizable(true)]
		public string SentTitle
		{
			get { return this.ViewState["SentTitle"] as string ?? string.Empty; }
			set { this.ViewState["SentTitle"] = value; }
		}

		/// <summary>
		/// Gets or sets the report sent date.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report sent date.")]
		[Localizable(true)]
		public DateTime? SentDate
		{
			get { return this.ViewState["SentDate"] as DateTime?; }
			set { this.ViewState["SentDate"] = value; }
		}

		/// <summary>
		/// Gets or sets the text to display as the title of the second sent date entry.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultSecondSentTitle)]
		[Description("The text to display as the title of the second sent date entry.")]
		[Localizable(true)]
		public string SecondSentTitle
		{
			get { return this.ViewState["SecondSentTitle"] as string ?? string.Empty; }
			set { this.ViewState["SecondSentTitle"] = value; }
		}

		/// <summary>
		/// Gets or sets the report second sent date.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report second sent date.")]
		[Localizable(true)]
		public DateTime? SecondSentDate
		{
			get { return this.ViewState["SecondSentDate"] as DateTime?; }
			set { this.ViewState["SecondSentDate"] = value; }
		}

		/// <summary>
		/// Gets or sets the report due date.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report due date.")]
		[Localizable(true)]
		public DateTime? DueDate
		{
			get { return this.ViewState["DueDate"] as DateTime?; }
			set { this.ViewState["DueDate"] = value; }
		}

		/// <summary>
		/// Gets or sets the report response date.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report response date.")]
		[Localizable(true)]
		public DateTime? ResponseDate
		{
			get { return this.ViewState["ResponseDate"] as DateTime?; }
			set { this.ViewState["ResponseDate"] = value; }
		}

		/// <summary>
		/// Gets or sets the report postmark date.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report postmark date.")]
		[Localizable(true)]
		public DateTime? PostmarkDate
		{
			get { return this.ViewState["PostmarkDate"] as DateTime?; }
			set { this.ViewState["PostmarkDate"] = value; }
		}

		/// <summary>
		/// Gets or sets the report CMO message ID.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The report CMO message ID.")]
		public string MessageID
		{
			get { return this.ViewState["MessageID"] as string ?? string.Empty; }
			set { this.ViewState["MessageID"] = value == null ? null : value.Trim(); }
		}

		/// <summary>
		/// Gets or sets whether or not a no-findings scenario is in effect.
		/// </summary>
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(DefaultNoFindingsOverride)]
		[Description("Whether or not a no-findings scenario is in effect.")]
		[Localizable(true)]
		public bool NoFindingsOverride
		{
			get
			{
				object obj = this.ViewState["EmptyVisible"];
				return obj is bool ? (bool)obj : DefaultNoFindingsOverride;
			}
			set
			{
				this.ViewState["EmptyVisible"] = value;
			}
		}

		/// <summary>
		/// Gets whether or not the second sent entry is visible.
		/// </summary>
		public bool SecondSentVisible
		{
			get { return this.SecondSentDate.HasValue; }
		}

		/// <summary>
		/// Ges whether or not the response date entries are visible.
		/// </summary>
		public bool ResponseVisible
		{
			get { return this.DueDate.HasValue; }
		}

		/// <summary>
		/// Gets whether or not the postmark date entry is visible.
		/// </summary>
		public bool PostmarkVisible
		{
			get { return this.PostmarkDate.HasValue; }
		}

		/// <summary>
		/// Gets or sets a data source for audit report messages.
		/// </summary>
		[Bindable(true)]
		[Description("A data source for audit report messages.")]
		[Category("Data")]
		public Dictionary<AuditReportType, string> MessagesDataSource { get; set; }

		/// <summary>
		/// Gets or sets the post election audit report data source.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		public new AuditReportBase DataSource { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PostElectionAuditSummary"/> control.
		/// </summary>
		public PostElectionAuditSummary()
		{
			this.CssClass = "column";
			this.Title = DefaultTitle;
			this.SentTitle = DefaultSentTitle;
			this.SecondSentTitle = DefaultSecondSentTitle;
			this.NotYetIssuedText = DefaultNotYetIssuedText;
			this.NoReportText = DefaultNoReportText;
			this.ResponseNotReceivedText = DefaultResponseNotReceivedText;
		}

		/// <summary>
		/// Renders the opening HTML tag of the control into the specified <see cref="HtmlTextWriter"/> object.
		/// </summary>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> that receives the rendered content.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", this.GetType().Name, this.CssClass));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			// opening <table> tag
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);

			// <caption>
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(this.Title);
			writer.RenderEndTag();

			// issuance date row
			bool hasMessage = !string.IsNullOrEmpty(this.MessageID);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.Write(this.SentTitle);
			writer.RenderEndTag();
			if (hasMessage && this.SentDate.HasValue)
			{
				// render date with CMO message link
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-DownloadLink");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "pdf-file");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, PageUrlManager.GetMessageUrl(this.MessageID));
				writer.AddAttribute(HtmlTextWriterAttribute.Title, "View " + this.Title);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(this.SentDate.Value.ToShortDateString());
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write(this.NoFindingsOverride ? this.NoReportText : this.SentDate.HasValue ? this.SentDate.Value.ToShortDateString() : this.NotYetIssuedText);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();

			if (this.SecondSentVisible)
			{
				// second issuance date row
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write(this.SecondSentTitle);
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write("{0:d}", this.SecondSentDate);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			if (this.ResponseVisible)
			{
				// response due date row
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write("Response Due");
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (!this.ResponseDate.HasValue && DateTime.Today > this.DueDate)
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "alert");
				if (!this.ResponseDate.HasValue)
					writer.RenderBeginTag(HtmlTextWriterTag.Strong);
				writer.Write("{0:d}", this.DueDate);
				if (!this.ResponseDate.HasValue)
					writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();

				// response received date row
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write("Response Received");
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (this.ResponseDate.HasValue)
				{
					writer.Write("{0:d}", this.ResponseDate);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "alert");
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write(this.ResponseNotReceivedText);
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// postmark date row
			if (this.PostmarkVisible)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Th);
				writer.Write("Response Postmarked");
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write("{0:d}", this.PostmarkDate);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// closing </table> tag
			writer.RenderEndTag();
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
		/// Controls how data is retrieved and bound to the control.
		/// </summary>
		protected override void PerformSelect()
		{
			AuditReportBase ds = this.DataSource;
			if (ds != null)
			{
				// alternate titles for IDR
				switch (ds.AuditReportType)
				{
					case AuditReportType.InitialDocumentationRequest:
						this.SentTitle = "Request Sent";
						this.SecondSentTitle = "Second Request Sent";
						break;
					case AuditReportType.IdrInadequateResponse:
					case AuditReportType.IdrAdditionalInadequateResponse:
					case AuditReportType.DarInadequateResponse:
					case AuditReportType.DarAdditionalInadequateResponse:
						this.SentTitle = "Notice Sent";
						this.SecondSentTitle = "Second Notice Sent";
						break;
				}
				this.Title = ds.IsInadequateNotice ? "Inadequate Response Notice" : CPConvert.ToString(ds.AuditReportType);
				this.SentDate = ds.SentDate;
				this.SecondSentDate = ds.SecondSentDate;
				this.DueDate = ds.DueDate;
				this.ResponseDate = ds.ResponseReceivedDate;
				this.PostmarkDate = ds.ResponseSubmittedDate;
				string messageID;
				this.MessageID = this.MessagesDataSource != null && this.MessagesDataSource.TryGetValue(ds.AuditReportType, out messageID) ? messageID : null;
			}
			else
			{
				this.Title = DefaultTitle;
				this.SentTitle = DefaultSentTitle;
				this.SecondSentTitle = DefaultSecondSentTitle;
				this.SentDate = this.SecondSentDate = this.DueDate = this.ResponseDate = this.PostmarkDate = null;
				this.MessageID = null;
			}
			this.MarkAsDataBound();
		}

		/// <summary>
		/// Verifies that the object a data-bound control binds to is one it can work with.
		/// </summary>
		/// <param name="dataSource">The object to verify as an instance of <see cref="AuditReportBase"/>.</param>
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource != null && !(dataSource is AuditReportBase))
				throw new InvalidOperationException(string.Format("Data source must be of type {0}.", typeof(AuditReportBase).FullName));
		}
	}
}
