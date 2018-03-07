using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a short, personalized greeting message.
	/// </summary>
	[ToolboxData("<{0}:UserHello runat=\"server\" />")]
	public class UserHello : Label
	{
		// Default property values
		private const string DefaultDisplayName = "Guest";

		/// <summary>
		/// Gets or sets the friendly display name of the logged-in user.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultDisplayName)]
		[Description("The friendly display name of the logged-in user.")]
		[Localizable(true)]
		public string DisplayName { get; set; }

		/// <summary>
		/// Raises the <see mref="Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CssClass = "userHello";
			this.Text = this.DisplayName;
		}
	}
}
