using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// Inherits from <see cref="Page" /> to implement a C-Access secure page.
	/// </summary>
	public abstract class CPPage : Page, IResultsPage
	{
		/// <summary>
		/// Determines whether or not the page has valid data.
		/// </summary>
		/// <returns>true if there is any data to be shown on the page; otherwise, false.</returns>
		protected virtual bool HasData()
		{
			return true;
		}

		/// <summary>
		/// View state key for tracking data bound status.
		/// </summary>
		private const string DataBoundViewStateKey = "_!DataBound";

		/// <summary>
		/// Gets a value indicating whether or not data was previously bound.
		/// </summary>
		public bool IsDataBound
		{
			get { return this.ViewState[DataBoundViewStateKey] != null; }
		}

		/// <summary>
		/// Gets C-Access information about the user making the page request.
		/// </summary>
		public CPUser CPUser
		{
			get { return base.User.Identity.IsAuthenticated ? CPSecurity.Provider.GetUser(base.User.Identity.Name) : null; }
		}

		/// <summary>
		/// Raises the <see cref="Page.PreInit"/> event at the beginning of page initialization.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
		protected override void OnPreInit(EventArgs e)
		{
			base.OnPreInit(e);
			// prevent client-side caching
			Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
			Response.Cache.SetNoServerCaching();
			Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); // IE
			Response.Cache.SetNoStore(); // FF
			Context.CheckPitStops();

			// check for C-Access Security clearance
			if (Page is ISecurePage && !CPProfile.UserRights.HasRights(CPUserRights.CAccess))
				Response.RedirectUnauthorized();
		}

		/// <summary>
		/// Raises the <see cref="Control.Init"/> event to initialize the page.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			ViewStateUserKey = Session.SessionID;
			if (!HasData())
				Server.RedirectPageNotFound();
		}

		/// <summary>
		/// Marks the page as having been bound to data.
		/// </summary>
		protected void MarkAsDataBound()
		{
			this.ViewState[DataBoundViewStateKey] = true;
		}

		#region IResultsPage Members

		/// <summary>
		/// Gets whether or not the there are errors to be shown.
		/// </summary>
		public virtual bool HasError
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp != null && irp.HasError;
			}
		}

		/// <summary>
		/// Gets or sets an error message to be displayed.
		/// </summary>
		public virtual string PageError
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp == null ? null : irp.PageError;
			}
			set
			{
				IResultsPage irp = this.Master as IResultsPage;
				if (irp != null) irp.PageError = value;
			}
		}

		/// <summary>
		/// Gets the web control used to display any errors.
		/// </summary>
		public virtual WebControl ErrorWebControl
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp == null ? null : irp.ErrorWebControl;
			}
		}

		/// <summary>
		/// Gets whether or not the there are results to be shown.
		/// </summary>
		public virtual bool HasResult
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp != null && irp.HasResult;
			}
		}

		/// <summary>
		/// Gets or sets a result message to be displayed.
		/// </summary>
		public virtual string PageResult
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp == null ? null : irp.PageResult;
			}
			set
			{
				IResultsPage irp = this.Master as IResultsPage;
				if (irp != null) irp.PageResult = value;
			}
		}

		/// <summary>
		/// Gets the web control used to display any results.
		/// </summary>
		public virtual WebControl ResultWebControl
		{
			get
			{
				IResultsPage irp = this.Master as IResultsPage;
				return irp == null ? null : irp.ResultWebControl;
			}
		}

		/// <summary>
		/// Discards all errors and results from the page.
		/// </summary>
		public void ClearMessages()
		{
			IResultsPage irp = this.Master as IResultsPage;
			if (irp != null) irp.ClearMessages();
		}

		#endregion
	}
}
