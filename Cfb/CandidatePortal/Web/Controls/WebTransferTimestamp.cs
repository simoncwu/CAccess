using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a web transfer date timestamp for a particular election cycle.
	/// </summary>
	[DefaultProperty("ElectionCycle")]
	[ToolboxData("<{0}:WebTransferTimestamp runat=\"server\" />")]
	public class WebTransferTimestamp : WebControl
	{
		/// <summary>
		/// The election cycle whose last web transfer timestamp is to be displayed.
		/// </summary>
		string _electionCycle;

		/// <summary>
		/// Gets or sets the election cycle whose last web transfer timestamp is to be displayed.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The election cycle whose last web transfer timestamp is to be displayed.")]
		public string ElectionCycle
		{
			get
			{
				return _electionCycle;
			}

			set
			{
				_electionCycle = value;
			}
		}

		/// <summary>
		/// The timestamp date format.
		/// </summary>
		string _dateFormat;

		/// <summary>
		/// Gets or sets the timestamp date format.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[Description("The format string for the web transfer timestamp.")]
		public string DateFormat
		{
			get { return _dateFormat; }
			set { _dateFormat = value; }
		}

		/// <summary>
		/// The <see cref="Control.ViewState"/> key containing the time stamp value.
		/// </summary>
		readonly string _timeStampViewStateKey;

		/// <summary>
		/// The last web transfer timestamp for an election cycle.
		/// </summary>
		DateTime? _timeStamp;

		/// <summary>
		/// Initializes a new instance of a <see cref="WebTransferTimestamp"/> control.
		/// </summary>
		public WebTransferTimestamp()
		{
			_timeStampViewStateKey = this.UniqueID + "_TimeStamp";
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnInit(e);
			_timeStamp = ViewState[_timeStampViewStateKey] as DateTime?;
			if (!_timeStamp.HasValue)
				ViewState[_timeStampViewStateKey] = _timeStamp = CPProviders.DataProvider.GetWebTransferDate(CPProfile.ElectionCycle);
		}

		/// <summary>
		/// Renders the HTML opening tag of the control into the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "transferTimestamp");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		/// <summary>
		/// Renders the contents of the <see cref="WebTransferTimestamp"/> into the specified writer.
		/// </summary>
		/// <param name="output">The output stream that renders HTML content to the client.</param>
		protected override void RenderContents(HtmlTextWriter output)
		{
			if (_timeStamp.HasValue)
				output.Write(string.Format(string.IsNullOrEmpty(_dateFormat) ? Properties.Resources.WebTransferTimeFormat : _dateFormat, _timeStamp));
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
