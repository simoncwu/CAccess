using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Represents a grid view that looks and behaves like a tree view.
	/// </summary>
	[SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ToolboxData("<{0}:GroupGridView runat=server></{0}:GroupGridView>")]
	public class GroupGridView : SPGridView, ICallbackEventHandler, IPostBackEventHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupGridView"/> control.
		/// </summary>
		public GroupGridView()
			: base()
		{
			this.AlternatingRowStyle.CssClass = null;
			this.GridLines = GridLines.None;
			this.AutoGenerateColumns = false;
			this.AllowSorting = true;
			this.AllowGrouping = false;
			this.EnableViewState = true;
			this.EnableTheming = true;
		}

		/// <summary>
		/// Raises the <see cref="GridView.OnInit"/> event.
		/// </summary>
		/// <param name="args">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnInit(EventArgs args)
		{
			base.OnInit(args);
			this.SelectedRowStyle.Font.Bold = false;
			this.Width = Unit.Empty;
		}

		/// <summary>
		/// Raises the <see cref="GridView.RowDataBound"/> event.
		/// </summary>
		/// <param name="args">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnDataBound(EventArgs args)
		{
			base.OnDataBound(args);
			SelectHttpRequestedItem();
		}

		/// <summary>
		/// Raises the <see cref="Control.DataBinding"/> event.
		/// </summary>
		/// <param name="arguments">An <see cref="EventArgs"/> object that contains the event data.</param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		protected override void OnDataBinding(EventArgs arguments)
		{
			base.OnDataBinding(arguments);
			// don't know why, but must replace first column with SPMenuField when grouping
			if (this.AllowGrouping)
			{
				this.Columns.RemoveAt(0);
				this.Columns.Insert(0, new CPMenuField());
			}
		}

		/// <summary>
		/// Finds the item specified by the current <see cref="HttpRequest"/> context and, if found, selects it.
		/// </summary>
		protected virtual void SelectHttpRequestedItem() { }

		/// <summary>
		/// Groups data in the grid.
		/// </summary>
		/// <param name="displayName">The display name of the grouping field.</param>
		/// <param name="field">The field by which to group.</param>
		/// <param name="descriptionField">The description of the grouping field.</param>
		public void GroupBy(string displayName, string field, string descriptionField)
		{
			this.AllowGrouping = true;
			this.AllowGroupCollapse = false;
			this.GroupFieldDisplayName = displayName;
			this.GroupField = field;
			this.GroupDescriptionField = descriptionField;
		}
	}
}
