using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Control for displaying view tabs for a Campaign Messages Online mailbox.
	/// </summary>
	[ToolboxData("<{0}:CmoNavigationTabs runat=server></{0}:CmoNavigationTabs>")]
	public sealed class CmoNavigationTabs : ContentTabs
	{
		/// <summary>
		/// The mailbox view.
		/// </summary>
		private CmoMailboxView _view;

		/// <summary>
		/// The source Campaign Messages Online mailbox to display view tabs for.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The source Campaign Messages Online mailbox.")]
		public CmoMailbox DataSource { get; set; }

		/// <summary>
		/// Creates a new instance of the <see cref="CmoNavigationTabs"/> control.
		/// </summary>
		public CmoNavigationTabs()
			: base()
		{
		}

		/// <summary>
		/// Raises the <see cref="Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// check if a view is defined
			string viewName = Page.Request.QueryString[CmoMailbox.ViewParameter];
			_view = !string.IsNullOrEmpty(viewName) && Enum.IsDefined(typeof(CmoMailboxView), viewName) ? CPConvert.ParseEnum<CmoMailboxView>(viewName) : CmoMailbox.DefaultView;
		}

		/// <summary>
		/// Gets the URL that loads a specific view.
		/// </summary>
		/// <param name="view">The target view.</param>
		/// <returns>A root-relative URL for loading the view indicated by <paramref name="view"/>.</returns>
		private string GetViewUrl(CmoMailboxView view)
		{
			return string.Format("{0}?{1}={2}", PageUrlManager.CmoMailboxUrl, CmoMailbox.ViewParameter, view);
		}

		/// <summary>
		/// Determines whether or not the currently viewed tab matches a specific tab target.
		/// </summary>
		/// <param name="target">The target tab to compare against.</param>
		protected override bool CurrentTabMatches(string target)
		{
			return target != null && target.Equals(GetViewUrl(_view), StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Retrieves information regarding the submission tabs that are available for the current candidate context.
		/// </summary>
		/// <returns>A <see cref="NameValueCollection"/> containing mappings of tab names to destination page URLs for all submission types available within the current candidate context.</returns>
		public override NameValueCollection GetAvailableTabs()
		{
			NameValueCollection c = new NameValueCollection();
			c.Add("Current", GetViewUrl(CmoMailboxView.Current));
			string newTabLabel = "New";
			if (this.DataSource != null)
			{
				newTabLabel += string.Format(" ({0})", this.DataSource.CountUnopened());
			}
			c.Add(newTabLabel, GetViewUrl(CmoMailboxView.Unopened));
			c.Add("Follow-Up", GetViewUrl(CmoMailboxView.FollowUp));
			c.Add("Archived", GetViewUrl(CmoMailboxView.Archived));
			c.Add("All", GetViewUrl(CmoMailboxView.All));
			return c;
		}
	}
}
