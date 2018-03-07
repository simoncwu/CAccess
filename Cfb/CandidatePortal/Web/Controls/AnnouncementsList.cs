using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// Control for displaying a list of broadcast announcements.
	/// </summary>
	[ToolboxData("<{0}:AnnouncementsList runat=server></{0}:AnnouncementsList>")]
	public class AnnouncementsList : WebControl
	{
		// Default property values
		private const string DefaultTitle = "Announcements";
		private const string DefaultEmptyDataText = "There are currently no announcements available.";

		/// <summary>
		/// Gets or sets the text to display as the title of the control.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultTitle)]
		[Description("The text to display as the title of the control.")]
		[Localizable(true)]
		public string Title
		{
			get { return this.ViewState["Title"] as string ?? string.Empty; }
			set { this.ViewState["Title"] = value; }
		}

		/// <summary>
		/// Gets or sets the text to display when the control does not contain any records.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(DefaultEmptyDataText)]
		[Description("The text to display when the control does not contain any records.")]
		[Localizable(true)]
		public string EmptyDataText
		{
			get { return this.ViewState["EmptyDataText"] as string ?? string.Empty; }
			set { this.ViewState["EmptyDataText"] = value; }
		}

		/// <summary>
		/// Gets or sets the announcements data source.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		public IEnumerable<Announcement> DataSource { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AnnouncementsList"/> control.
		/// </summary>
		public AnnouncementsList()
		{
			this.Title = DefaultTitle;
			this.EmptyDataText = DefaultEmptyDataText;
		}

		/// <summary>
		/// Raises the <see cref="Control.PreRender"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.DataSource == null)
				this.DataSource = CPProviders.DataProvider.GetAnnouncements(CPProfile.ElectionCycle);
		}

		/// <summary>
		/// Renders the opening HTML tag of the control into the specified <see cref="HtmlTextWriter"/> object.
		/// </summary>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> that receives the rendered content.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-" + this.GetType().Name);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			// table header
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "caption");
			writer.RenderBeginTag(HtmlTextWriterTag.H3);
			writer.Write(this.Title);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-boundingBox cp-table cp-Announcements");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);

			// loop through announcements and render each
			if (this.DataSource != null && this.DataSource.Count() > 0)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				foreach (Announcement item in this.DataSource)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Li);
					// show indicator if new
					int daysOld = DateTime.Today.Subtract(item.Posted.Value).Days;
					if (daysOld >= 0 && daysOld <= Properties.Settings.Default.NewAnnouncementCutoffDays)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Src, "/images/new.gif");
						writer.AddAttribute(HtmlTextWriterAttribute.Width, "22");
						writer.AddAttribute(HtmlTextWriterAttribute.Height, "10");
						writer.AddAttribute(HtmlTextWriterAttribute.Alt, "New");
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "newAlert");
						writer.RenderBeginTag(HtmlTextWriterTag.Img);
						writer.RenderEndTag();
						writer.Write(" ");
					}
					// render link
					writer.AddAttribute(HtmlTextWriterAttribute.Title, item.Title);
					writer.AddAttribute(HtmlTextWriterAttribute.Href, PageUrlManager.GetAnnouncementUrl(item));
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.Write(item.Title);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			else
			{
				writer.Write(this.EmptyDataText);
			}

			writer.RenderEndTag();
		}

		/// <summary>
		/// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}
	}
}
