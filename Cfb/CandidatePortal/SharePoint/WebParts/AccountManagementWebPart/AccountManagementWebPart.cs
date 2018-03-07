using System;
using System.Runtime.InteropServices;

using System.Web.UI.WebControls.WebParts;


namespace Cfb.CandidatePortal.SharePoint.WebParts
{
	/// <summary>
	/// 
	/// </summary>
	[Guid("3c14dd4e-ad62-474f-996b-3e722e54bc52")]
	public class AccountManagementWebPart : WebPart
	{

		/// <summary>
		/// Control for performing account management operations.
		/// </summary>
		private AccountManager _manager;

		/// <summary>
		/// Raises the <see cref="System.Web.UI.Control.Init"/> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			this.EnsureChildControls();
		}

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			_manager = new AccountManager();
			this.Controls.Add(_manager);
		}

		/// <summary>
		/// Initializes the web part.
		/// </summary>
		public AccountManagementWebPart()
		{
			this.ExportMode = WebPartExportMode.All;
		}
	}
}
