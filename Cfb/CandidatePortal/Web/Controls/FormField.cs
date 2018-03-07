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
	/// A control for displaying a form field with a name and value.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:AmendableField runat=server></{0}:AmendableField>")]
	public class FormField : WebControl
	{
		/// <summary>
		/// A label for displaying the field name.
		/// </summary>
		Label _label;

		/// <summary>
		/// A label for displaying the field value.
		/// </summary>
		Label _data;

		/// <summary>
		/// Gets or sets the field name.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string LabelText
		{
			get { return _label.Text; }
			set { _label.Text = value; }
		}

		/// <summary>
		/// Gets or sets the field value.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Value
		{
			get { return _data.Text; }
			set { _data.Text = value; }
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			_label.RenderControl(writer);
			_data.RenderControl(writer);
		}

		/// <summary>
		/// Renders the HTML opening tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("label");
			writer.WriteAttribute("class", string.Format("{0} field", this.CssClass).Trim());
			writer.Write(HtmlTextWriter.TagRightChar);
		}

		/// <summary>
		/// Renders the HTML closing tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag("label");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormField"/> control.
		/// </summary>
		public FormField()
		{
			_label = new Label();
			_label.CssClass = "fieldLabel";
			_data = new Label();
			_data.CssClass = "fieldData";
		}
	}
}
