using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a count of unopened (new) Campaign Messages Online messages.
	/// </summary>
	[ToolboxData("<{0}:CmoNewMessageCounter runat=\"server\" />")]
	public class CmoNewMessageCounter : WebControl
	{
		/// <summary>
		/// Gets or sets the new message count format when there is one unopened (new) message.
		/// </summary>
		[Bindable(true)]
		[Browsable(true)]
		[Category("Appearance")]
		[Description("The text to display when there is one unopened (new) message.")]
		[EditorBrowsable()]
		public string NewMessageText { get; set; }

		/// <summary>
		/// Gets or sets the new message count format when there are multiple or no unopened (new) messages.
		/// </summary>
		[Bindable(true)]
		[Browsable(true)]
		[Category("Appearance")]
		[Description("The format string for the new message count when there are multiple or no unopened (new) messages.")]
		[EditorBrowsable()]
		public string NewMessageCountFormat { get; set; }

		/// <summary>
		/// Gets or sets the Campaign Messages Online mailbox title.
		/// </summary>
		[Bindable(true)]
		[Browsable(true)]
		[Category("Appearance")]
		[Description("The Campaign Messages Online mailbox title.")]
		[EditorBrowsable()]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the election cycles to include in the count.
		/// </summary>
		[Bindable(true)]
		[Browsable(true)]
		[Category("Data")]
		[Description("The elections to include in the count.")]
		[EditorBrowsable()]
		public Elections Elections { get; set; }

		/// <summary>
		/// The number of unopened (new) messages.
		/// </summary>
		private uint _newMessageCount;

		/// <summary>
		/// Initializes a new instance of a <see cref="CmoNewMessageCounter"/> control.
		/// </summary>
		public CmoNewMessageCounter()
		{
			this._newMessageCount = 0;
			this.NewMessageText = Properties.Resources.NewMessageText;
			this.NewMessageCountFormat = Properties.Resources.NewMessageCountPluralFormat;
		}

		/// <summary>
		/// Raises the <see cref="Control.DataBind()"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnDataBinding(EventArgs e)
		{
			string cid = CPProfile.Cid;
			if (!string.IsNullOrEmpty(cid))
				this._newMessageCount = CmoMailbox.GetMailbox(cid, this.Elections).CountUnopened();
		}

		/// <summary>
		/// Renders the HTML opening tag of the control into the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, "CmoNewMessageCounter");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		/// <summary>
		/// Renders the contents of the <see cref="WebTransferTimestamp"/> into the specified writer.
		/// </summary>
		/// <param name="output">The output stream that renders HTML content to the client.</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			string status = _newMessageCount == 0 ? "open" : "new";
			output.AddAttribute(HtmlTextWriterAttribute.Href, Properties.Resources.CmoMessagesPath);
			output.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
			output.RenderBeginTag(HtmlTextWriterTag.A);
			output.AddAttribute(HtmlTextWriterAttribute.Src, string.Format("/images/msg-{0}.gif", status));
			output.AddAttribute(HtmlTextWriterAttribute.Width, "16");
			output.AddAttribute(HtmlTextWriterAttribute.Height, "16");
			output.AddAttribute(HtmlTextWriterAttribute.Alt, status + " message");
			output.RenderBeginTag(HtmlTextWriterTag.Img);
			output.Write(" ");
			output.AddAttribute(HtmlTextWriterAttribute.Class, status);
			output.RenderBeginTag(HtmlTextWriterTag.Span);
			output.Write(_newMessageCount == 1 ? this.NewMessageText : string.Format(this.NewMessageCountFormat, _newMessageCount));
			output.RenderEndTag();
			output.RenderEndTag();
			output.RenderEndTag();
		}

		/// <summary>
		/// Renders the HTML closing tag of the control into the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}
	}
}
