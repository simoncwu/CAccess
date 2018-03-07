using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Web.Controls.Properties;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A link to search for press releases mentioning a candidate.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PressReleasesLink runat=server/>")]
	public class PressReleasesLink : WebControl
	{
		/// <summary>
		/// The key for persisting the first name in view-state.
		/// </summary>
		private const string FirstNameViewStateKey = "FirstName";

		/// <summary>
		/// The key for persisting the last name in view-state.
		/// </summary>
		private const string LastNameViewStateKey = "LastName";

		/// <summary>
		/// The key for persisting the middle initial in view-state.
		/// </summary>
		private const string MiddleInitialViewStateKey = "MiddleInitial";

		/// <summary>
		/// The key for persisting the link text in view-state.
		/// </summary>
		private const string TextViewStateKey = "Text";

		/// <summary>
		/// Gets or sets the candidate's first name to use in the search.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Localizable(true)]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the candidate's last name to use in the search.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Localizable(true)]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the candidate's middle initial to use in the search.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Localizable(true)]
		public string MiddleInitial { get; set; }

		/// <summary>
		/// Gets or sets the text to display for the link.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("View My Press Release References")]
		[Localizable(true)]
		public string Text { get; set; }

		/// <summary>
		/// Restores view-state information from a previous request that was saved with the <see cref="WebControl.SaveViewState"/> method.
		/// </summary>
		/// <param name="savedState">An object that represents the state to be restored.</param>
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			this.FirstName = ViewState[FirstNameViewStateKey] as string;
			this.LastName = ViewState[LastNameViewStateKey] as string;
			this.MiddleInitial = ViewState[MiddleInitialViewStateKey] as string;
			this.Text = ViewState[TextViewStateKey] as string;
		}

		/// <summary>
		/// Saves any state that was modified after the <see cref="Style.TrackViewState"/> method was invoked.
		/// </summary>
		/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, a null reference.</returns>
		protected override object SaveViewState()
		{
			ViewState[FirstNameViewStateKey] = this.FirstName;
			ViewState[LastNameViewStateKey] = this.LastName;
			ViewState[MiddleInitialViewStateKey] = this.MiddleInitial;
			ViewState[TextViewStateKey] = this.Text;
			return base.SaveViewState();
		}

		/// <summary>
		/// Renders the HTML opening tag of the control into the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			Settings settings = Properties.Settings.Default;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "popup");
			writer.AddAttribute(HtmlTextWriterAttribute.Target, "_blank");
			writer.AddAttribute(HtmlTextWriterAttribute.Href, string.IsNullOrEmpty(this.MiddleInitial) ?
				string.Format(settings.PressReleasesSearchUrlFormat, this.FirstName, this.LastName) :
				string.Format(settings.PressReleasesSearchUrlMIFormat, this.FirstName, this.MiddleInitial, this.LastName));
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		/// <summary>
		/// Renders the HTML closing tag of the control into the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}
	}
}
