using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying the contents of a Campaign Messages Online mailbox in a table.
	/// </summary>
	[ToolboxData("<{0}:CmoMailboxMessageList runat=server />")]
	public class CmoMailboxMessageList : WebControl
	{
		/// <summary>
		/// The name for a checkbox that is used to select all messages.
		/// </summary>
		public const string CheckAllCheckboxId = "CheckAllMessagesCheckBox";

		/// <summary>
		/// The name for a checkbox that is used to select a message.
		/// </summary>
		public const string MessageCheckboxName = "messageselect";

		/// <summary>
		/// The CSS class used to differentiate unopened (new) messages from opened (old) messages.
		/// </summary>
		public const string NewMessageCssClass = "newMessage";

		/// <summary>
		/// The query string parameter name for passing a unique message identifier.
		/// </summary>
		public const string MessageIdParameterName = "id";

		/// <summary>
		/// The HTML for the follow-up flag image.
		/// </summary>
		private const string FlagImageHtml = @"<span class=""ui-icon ui-icon-flag""></span>";

		/// <summary>
		/// The HTML for the opened message image.
		/// </summary>
		private const string OpenImageHtml = @"<img src=""/images/msg-open.gif"" alt=""Open Message"" width=""16"" height=""16"" />";

		/// <summary>
		/// The HTML for the new message image.
		/// </summary>
		private const string NewImageHtml = @"<img src=""/images/msg-new.gif"" alt=""New Message"" width=""16"" height=""16"" />";

		/// <summary>
		/// The HTML for the attachment image.
		/// </summary>
		private const string AttachmentImageHtml = @"<img src=""/images/paperclip.gif"" alt=""Attachment"" width=""16"" height=""16"" />";

		/// <summary>
		/// HTML for the header of the mailbox messages table.
		/// </summary>
		private const string HeaderHtml = @"
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""cmoMailboxMessageList ms-listviewtable"">
	<tr class=""ms-viewheadertr"">
		<th class=""messageSelect"">
			<div><input type=""checkbox"" id=""" + CheckAllCheckboxId + @""" /></div>
		</th>
		<th class=""openStatus"">
			<div><img src=""/images/msg-gen.gif"" alt=""Status"" width=""16"" height=""16"" /></div>
		</th>
		<th class=""electionCycle"">
			<div>Election</div>
		</th>
		<th class=""title"">
			<div>Title</div>
		</th>
		<th class=""attachmentStatus"">
			<div><img src=""/images/paperclip_gray.gif"" alt=""Attachment"" width=""16"" height=""16"" /></div>
		</th>
		<th class=""receivedDate"">
			<div>Received</div>
		</th>
		<th class=""followUp"">
			<div class=""ui-state-hover""><span class=""ui-icon ui-icon-flag""></span></div>
		</th>
	</tr>";

		/// <summary>
		/// HTML format template for a single message.
		/// </summary>
		private const string MessageTemplate = @"
	<tr class=""cmoMessage {0}"" id=""{1}"">
		<td class=""messageSelect"">
			<div><input type=""checkbox"" name=""" + MessageCheckboxName + @""" value=""{1}""/></div>
		</td>
		<td class=""openStatus"">
			<div>{2}</div>
		</td>
		<td class=""electionCycle"">
			<div>{3}</div>
		</td>
		<td class=""title"">
			<div>{4}</div>
		</td>
		<td class=""attachmentStatus"">
			<div>{5}</div>
		</td>
		<td class=""receivedDate"">
			<div>{6:d}</div>
		</td>
		<td class=""followUp"">{7}</td>
	</tr>";

		/// <summary>
		/// HTML template for the footer of the mailbox messages table.
		/// </summary>
		private const string FooterHtmlTemplate = @"
</table>
<script language=""javascript"" type=""text/javascript"">
	//<![CDATA[
	$('.cmoMailboxMessageList td').not('.messageSelect').click(function() {{
		if (this.tagName && this.tagName == ""TD"") location.href = ""View.aspx?{1}&{2}="" + this.parentNode.id;
	}});
	$('.cmoMailboxMessageList .cmoMessage .messageSelect input:checkbox').change(function() {{
		$('#" + CheckAllCheckboxId + @"').prop('checked', $('.cmoMailboxMessageList .cmoMessage .messageSelect input:checkbox:not(:checked)').length==0);
	}});
	$('#" + CheckAllCheckboxId + @"').change(function() {{
		$('.cmoMailboxMessageList .cmoMessage .messageSelect input').prop('checked', this.checked);
	}});
	//]]>
</script>";

		/// <summary>
		/// The source Campaign Messages Online mailbox whose message contents are to be displayed.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The source Campaign Messages Online mailbox.")]
		public CmoMailbox DataSource { get; set; }

		/// <summary>
		/// Gets or sets the number of messages to display per page.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The number of messages to display per page.")]
		public int PageSize { get; set; }

		/// <summary>
		/// Gets or sets the index of the currently displayed page.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The index of the currently displayed page.")]
		public int PageIndex { get; set; }

		/// <summary>
		/// The control's output HTML.
		/// </summary>
		private StringBuilder _output;

		/// <summary>
		/// Raises the <see cref="System.Web.UI.Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			_output = new StringBuilder();
		}

		/// <summary>
		/// Raises the <see cref="Control.DataBinding"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnDataBinding(EventArgs e)
		{
			_output = new StringBuilder();
			// handle empty mailbox
			if ((this.DataSource == null) || (this.DataSource.Messages.Count == 0))
			{
				string viewLower;
				switch (this.DataSource.View)
				{
					case CmoMailboxView.Unopened:
						viewLower = "new messages";
						break;
					case CmoMailboxView.Archived:
						viewLower = "archived messages";
						break;
					case CmoMailboxView.FollowUp:
						viewLower = "messages flagged for follow-up";
						break;
					case CmoMailboxView.All:
						viewLower = "messages";
						break;
					case CmoMailboxView.Current:
					default:
						viewLower = "current messages";
						break;
				}
				_output.AppendFormat("<p>You do not have any {0}.</p>\n", viewLower);
				return;
			}
			// generate HTML for list of messages
			_output.AppendLine(HeaderHtml);
			foreach (CmoMessage msg in this.DataSource.Messages)
			{
				_output.AppendLine(ToHtml(msg));
			}
			_output.AppendLine(string.Format(FooterHtmlTemplate, this.DataSource.Messages.Count, this.DataSource.GetQueryString().ToQueryString(Context.Server), MessageIdParameterName));
		}

		/// <summary>
		/// Converts a <see cref="CmoMessage"/> object into its HTML equivalent.
		/// </summary>
		/// <param name="message">The <see cref="CmoMessage"/> to convert.</param>
		/// <returns>An HTML <see cref="String"/> for displaying the details of <paramref name="message"/> in a web browser.</returns>
		string ToHtml(CmoMessage message)
		{
			if (message == null)
				return string.Empty;
			else
			{
				string unopenedClass = message.IsOpened ? string.Empty : NewMessageCssClass;
				return string.Format(MessageTemplate,
					unopenedClass,
					message.UniqueID,
					message.IsOpened ? OpenImageHtml : NewImageHtml,
					message.ElectionCycle,
					message.Title,
					message.HasAttachment ? AttachmentImageHtml : null,
					message.PostDate,
					message.NeedsFollowUp ? FlagImageHtml : null);
			}
		}

		/// <summary>
		/// Renders the HTML opening tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("div");
			writer.WriteAttribute("class", "cp-CmoMailboxMessageListControl");
			writer.Write(HtmlTextWriter.TagRightChar);
		}

		/// <summary>
		/// Renders the HTML closing tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag("div");
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.WriteLine(_output.ToString());
		}
	}
}
