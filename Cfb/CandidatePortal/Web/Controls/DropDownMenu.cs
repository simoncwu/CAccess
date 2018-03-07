using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Represents a vertical dropdown menu navigation control.
	/// </summary>
	[ToolboxData("<{0}:MenuControl runat=server />")]
	public class DropDownMenu : WebControl
	{
		/// <summary>
		/// Gets or sets the text to display in the menu by default.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The text to display in the menu by default.")]
		[Localizable(true)]
		public string Text { get; set; }

		/// <summary>
		/// A collection of list items to display in the menu.
		/// </summary>
		private ICollection<ListItem> _items;

		/// <summary>
		/// Gets a collection of list items to display in the menu.
		/// </summary>
		public ICollection<ListItem> Items
		{
			get { return _items; }
		}

		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		public string SelectedItem { get; set; }

		/// <summary>
		/// Gets or sets the menu activation behavior.
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(MenuActivationBehavior.Click)]
		[Description("The menu activation behavior.")]
		public MenuActivationBehavior Activation { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownMenu"/> control.
		/// </summary>
		public DropDownMenu()
		{
			_items = new List<ListItem>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			bool hover = this.Activation == MenuActivationBehavior.Hover;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "vmenu " + this.CssClass + (hover ? " hovermenu" : null));
			writer.RenderBeginTag(hover ? HtmlTextWriterTag.Ul : HtmlTextWriterTag.Div);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Activation == MenuActivationBehavior.Hover)
			{
				// <li>
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				// <a>
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(this.SelectedItem);
				// </a>
				writer.RenderEndTag();
				if (this.Items.Count > 1)
				{
					// <ul>
					writer.RenderBeginTag(HtmlTextWriterTag.Ul);
					// list item elements
					foreach (var i in this.Items)
					{
						// <li>
						if (i.Text == this.SelectedItem)
							continue;
						writer.RenderBeginTag(HtmlTextWriterTag.Li);
						// <a>
						writer.AddAttribute(HtmlTextWriterAttribute.Href, i.Value);
						writer.RenderBeginTag(HtmlTextWriterTag.A);
						writer.Write(i.Text);
						// </a>
						writer.RenderEndTag();
						// </li>
						writer.RenderEndTag();
					}
					// </ul>
					writer.RenderEndTag();
				}
				// </li>
				writer.RenderEndTag();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.Activation == MenuActivationBehavior.Click)
			{
				writer.RenderEndTag();
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
				writer.RenderBeginTag(HtmlTextWriterTag.Script);
				bool multi = this.Items.Count > 1;
				writer.Write("require([{0}],", multi ? "'dijit/DropDownMenu','dijit/MenuItem','dijit/form/DropDownButton'" : "'dijit/form/Button'");
				writer.WriteLine("function({0}){{", multi ? "DropDownMenu,MenuItem,DropDownButton" : "Button");
				if (this.Items.Count > 1)
				{
					writer.WriteLine("var menu=new DropDownMenu({style:'display:none;',class:'electionCycleMenu'});");
					foreach (var i in this.Items)
					{
						string text = i.Text == this.SelectedItem ? string.Format("<strong>{0}</strong>", i.Text) : i.Text;
						writer.Write("menu.addChild(new MenuItem({{label:'{0}'", text);
						if (!string.IsNullOrWhiteSpace(i.Value))
						{
							writer.Write(",onClick:function(){");
							if (i.Value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
								writer.Write(i.Value.Substring("javascript:".Length));
							else
								writer.Write("window.location.assign('" + i.Value + "');");
							writer.Write("}");
						}
						writer.WriteLine("}));");
					}
					writer.WriteLine("menu.startup();");
				}
				writer.Write("new {0}({{label:'", multi ? "DropDownButton" : "Button");
				writer.Write(this.SelectedItem == null ? this.Text : this.SelectedItem);
				writer.Write("',class:'vmenu',{0}}},'", multi ? "dropDown:menu" : "disabled:true");
				writer.Write(this.ClientID);
				writer.Write("').startup();");
				writer.Write("});");
				writer.RenderEndTag();
			}
		}

		/// <summary>
		/// Enumeration of the different ways possible to activate a menu control.
		/// </summary>
		public enum MenuActivationBehavior : byte
		{
			/// <summary>
			/// Activation/deactivation is achieved by clicking the menu.
			/// </summary>
			Click,
			/// <summary>
			/// Activation/deactivation is achieved by hovering in and out of the menu.
			/// </summary>
			Hover
		}
	}
}
