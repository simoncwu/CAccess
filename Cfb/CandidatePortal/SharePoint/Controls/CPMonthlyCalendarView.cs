using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Represents a monthly calendar control.
	/// </summary>
	[DefaultProperty(null), ToolboxData("<{0}:CPMonthlyCalendarView runat=server></{0}:CPMonthlyCalendarView>"),
	SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true),
	AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
	SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true),
	AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CPMonthlyCalendarView : MonthlyCalendarView
	{
		/// <summary>
		/// Raises the <see cref="Control.OnInit"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			if (this.HasEvents())
			{
				object EventInit = typeof(Control).InvokeMember("EventInit", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField, null, null, null);
				EventHandler handler = this.Events[EventInit] as EventHandler;
				if (handler != null)
				{
					handler(this, e);
				}
			}
			if (this.Page != null)
			{
				this.Page.PreLoad += new EventHandler(this.OnPagePreLoad);
				if (!base.IsViewStateEnabled && this.Page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		/// <param name="output">
		/// Output
		/// </param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void Render(HtmlTextWriter output)
		{
			this.EnsureChildControls();
			if (!this.DownLevelRendering)
			{
				this.RenderCalendar(output);
			}
			else
			{
				StringBuilder st = new StringBuilder();
				output.Write(this.BuildHtmlForDownlLevelBrowsers(st));
			}
		}
	}
}