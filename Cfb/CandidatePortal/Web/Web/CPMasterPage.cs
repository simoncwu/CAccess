using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// Inherits from <see cref="MasterPage" /> to implement a C-Access master page.
	/// </summary>
	public abstract class CPMasterPage : MasterPage, IResultsPage
	{
		#region IResultsPage Members

		/// <summary>
		/// Gets whether or not the there are errors to be shown.
		/// </summary>
		public bool HasError
		{
			get { return Session[CacheKeys.PageError] != null; }
		}

		/// <summary>
		/// Gets or sets an error message to be displayed.
		/// </summary>
		public string PageError
		{
			get { return Session[CacheKeys.PageError] as string; }
			set { Session[CacheKeys.PageError] = value; }
		}

		/// <summary>
		/// Gets the web control used to display any errors.
		/// </summary>
		public abstract WebControl ErrorWebControl { get; }

		/// <summary>
		/// Gets whether or not the there are results to be shown.
		/// </summary>
		public bool HasResult
		{
			get { return Session[CacheKeys.PageResult] != null; }
		}

		/// <summary>
		/// Gets or sets a result message to be displayed.
		/// </summary>
		public string PageResult
		{
			get { return Session[CacheKeys.PageResult] as string; }
			set { Session[CacheKeys.PageResult] = value; }
		}

		/// <summary>
		/// Gets the web control used to display any results.
		/// </summary>
		public abstract WebControl ResultWebControl { get; }

		/// <summary>
		/// Discards all errors and results from the page.
		/// </summary>
		public void ClearMessages()
		{
			this.PageError = null;
			this.PageResult = null;
		}

		#endregion

		/// <summary>
		/// Displays any results or error messages.
		/// </summary>
		protected virtual void OnDisplayResults()
		{
			if (this.ErrorWebControl != null)
			{
				if (this.ErrorWebControl.Visible = this.HasError)
				{
					this.ErrorWebControl.Controls.Add(new LiteralControl(this.PageError));
				}
			}
			if (this.ResultWebControl != null)
			{
				if (this.ResultWebControl.Visible = this.HasResult)
				{
					this.ResultWebControl.Controls.Add(new LiteralControl(this.PageResult));
				}
			}
		}

		/// <summary>
		/// Sends server control content to a provided <see cref="HtmlTextWriter"/> object, which writes the content to be rendered on the client.
		/// </summary>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			this.OnDisplayResults();
			base.Render(writer);
			this.ClearMessages();
		}
	}
}
