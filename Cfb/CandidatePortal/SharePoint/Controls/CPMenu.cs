using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Permissions;
using Microsoft.SharePoint.Security;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Provides a data-bound drop-down menu to be used in place of BoundField, TemplateField, or similar controls.
	/// </summary>
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true),
	AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
	AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
	SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
	[ToolboxData("<{0:CPMenu runat=server></{0}:CPMenu>")]
	public class CPMenu : Microsoft.SharePoint.WebControls.Menu
	{
		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="eventArgs">An <see cref="EventArgs"/> object that contains the event data.</param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnLoad(EventArgs eventArgs)
		{
			// try/catch to gracefully handle error resulting from base class's OnLoad implementation when used in a site not registered with SharePoint
			try
			{
				base.OnLoad(eventArgs);
			}
			catch (NullReferenceException)
			{
				this.InitializeControlIds(this);
			}
		}
	}
}
