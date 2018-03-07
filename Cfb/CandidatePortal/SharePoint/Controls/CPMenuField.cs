using System;
using System.Collections.Generic;
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
	/// Provides a data-bound drop-down menu to be used in place of BoundField, TemplateField, or similar controls.
	/// </summary>
	[SharePointPermission(SecurityAction.InheritanceDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CPMenuField : SPMenuField
	{
		/// <summary>
		/// When overridden in a derived class, creates an empty <see cref="DataControlField"/>-derived object.
		/// </summary>
		/// <returns>An empty <see cref="DataControlField"/>-derived object.</returns>
		protected override DataControlField CreateField()
		{
			return new CPMenuField();
		}

		/// <summary>
		/// Adds text or controls to a cell's controls collection.
		/// </summary>
		/// <param name="cell">A DataControlFieldCell that contains the text or controls of the DataControlField.</param>
		/// <param name="cellType">One of the <see cref="DataControlCellType"/> values.</param>
		/// <param name="rowState">One of the <see cref="DataControlRowState"/> values, specifying the state of the row that contains the <see cref="DataControlFieldCell"/>.</param>
		/// <param name="rowIndex">The index of the row that the <see cref="DataControlFieldCell"/> is contained in.</param>
		[SharePointPermission(SecurityAction.Demand, ObjectModel = true)]
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				cell.CssClass = "ms-vb-title";
				CPMenu child = new CPMenu
				{
					UseMaximumWidth = this.UseMaximumWidth,
					MenuFormat = this.MenuFormat
				};
				if (!string.IsNullOrEmpty(this.MenuTemplateId))
				{
					child.TemplateId = this.MenuTemplateId;
				}
				if (!string.IsNullOrEmpty(this.TextCssClass))
				{
					child.TextCssClass = this.TextCssClass;
				}
				if (!string.IsNullOrEmpty(this.ImageUrlFormat) && string.IsNullOrEmpty(this.ImageUrlFields))
				{
					child.ImageUrl = this.ImageUrlFormat;
				}
				if (!string.IsNullOrEmpty(this.NavigateUrlFormat) && string.IsNullOrEmpty(this.NavigateUrlFields))
				{
					child.NavigateUrl = this.NavigateUrlFormat;
				}
				if (!string.IsNullOrEmpty(this.TextFormat) && string.IsNullOrEmpty(this.TextFields))
				{
					child.Text = this.TextFormat;
				}
				if (!string.IsNullOrEmpty(this.ToolTipFormat) && string.IsNullOrEmpty(this.ToolTipFields))
				{
					child.ToolTip = this.ToolTipFormat;
				}
				if (cell.HasControls())
				{
					// replace Microsoft.SharePoint.WebControls.Menu that was added by base method
					cell.Controls.Clear();
				}
				cell.Controls.Add(child);
				if (((rowState & DataControlRowState.Insert) == DataControlRowState.Normal) && ((!string.IsNullOrEmpty(this.TextFields) || !string.IsNullOrEmpty(this.ImageUrlFields)) || ((!string.IsNullOrEmpty(this.ToolTipFields) || !string.IsNullOrEmpty(this.NavigateUrlFields)) || !string.IsNullOrEmpty(this.TokenNameAndValueFields))))
				{
					// set DataBinding event handler to execute the handler for Microsoft.SharePoint.WebControls.Menu
					child.DataBinding += new EventHandler(delegate(object sender, EventArgs e)
					{
						Type baseType = typeof(SPMenuField);
						baseType.InvokeMember("DataBindingEventHandler", BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, this, new[] { sender, e });
					});
				}
			}
		}
	}
}
