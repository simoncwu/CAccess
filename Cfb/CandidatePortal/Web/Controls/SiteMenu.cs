using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a C-Access site menu.
	/// </summary>
	[ToolboxData("<{0}:SiteMenu runat=server />")]
	public class SiteMenu : Repeater
	{
		/// <summary>
		/// Raises the <see cref="Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.SeparatorTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildMenuSeparatorTemplate));
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.DataBind();
		}

		/// <summary>
		/// Raises the <see cref="Repeater.ItemDataBound"/> event.
		/// </summary>
		/// <param name="e">A <see cref="RepeaterItemEventArgs"/> that contains event data.</param>
		protected override void OnItemDataBound(RepeaterItemEventArgs e)
		{
			base.OnItemDataBound(e);
			// can only access item data upon DataBound event
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				SiteMapNode node = e.Item.DataItem as SiteMapNode;
				if (node != null)
				{
					Literal item = new Literal();
					item.Text = string.Format("<a href=\"{0}\" title=\"{1}\" class=\"{2}\">{3}</a>",
						node.Url, node.Description, HasParent(node.Url) ? "selected" : string.Empty, node.Title);
					e.Item.Controls.Add(item);
				}
			}
		}

		/// <summary>
		/// Generates a menu item separator for a site menu.
		/// </summary>
		/// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
		void BuildMenuSeparatorTemplate(Control container)
		{
			Literal separator = new Literal();
			separator.Text = " / ";
			container.Controls.Add(separator);
		}

		/// <summary>
		/// Determines whether or not the currently executing page has a particular URL as a parent.
		/// </summary>
		/// <param name="url">The parent URL to test against.</param>
		/// <returns>true if the current page has the path indicated by <paramref name="url"/> as a parent.</returns>
		bool HasParent(string url)
		{
			if (string.IsNullOrEmpty(url))
				return false;
			else
			{
				if (!url.EndsWith("/")) url = url + "/";
				return Page.Request.Url.AbsolutePath.StartsWith(url);
			}
		}
	}
}
