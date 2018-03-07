using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Web;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A web control for displaying a list of reminders for upcoming deadlines and scheduled events.
	/// </summary>
	public class RemindersControl : WebControl
	{
		/// <summary>
		/// The verbosity format for reminder dates.
		/// </summary>
		private string _dateFormat;

		/// <summary>
		/// The verbosity of the reminders' detail.
		/// </summary>
		private RemindersView _view;

		/// <summary>
		/// Gets or sets the verbosity of the reminders' detail as a custom web part property.
		/// </summary>
		[Browsable(true)]
		[DisplayName("Selected View")]
		[Description("Change the verbosity of the reminders list")]
		[Category("Configuration")]
		public RemindersView View
		{
			get
			{
				return _view;
			}
			set
			{
				_view = value;
				switch (value)
				{
					case RemindersView.Full:
						_dateFormat = "{0:d}";
						break;
					case RemindersView.Brief:
						_dateFormat = "{0:M/d}";
						break;
				}
			}
		}

		/// <summary>
		/// A control for displaying upcoming deadlines and scheduled events.
		/// </summary>
		Repeater _repeater;

		/// <summary>
		/// Gets or sets the upcoming events data source.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		public IEnumerable<CPCalendarItem> DataSource { get; set; }

		/// <summary>
		/// Initializes the web control.
		/// </summary>
		public RemindersControl()
		{
			_dateFormat = Properties.Resources.DateFormat;
			_view = RemindersView.Full;
			this.CssClass = "cp-" + this.GetType().Name;
		}

		#region WebPart Member Methods

		/// <summary>
		/// Sends server control content to a provided <see cref="HtmlTextWriter"/> object, which writes the content to be rendered on the client.
		/// </summary>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			writer.Write(BuildHeaderHtml());
			if (_repeater.Items.Count > 0)
				_repeater.RenderControl(writer);
			else
				writer.Write(string.Format("<tr><td class=\"emptyDataText\">{0}</td></tr>", string.Format(Properties.Resources.EmptyDataTextTemplate, "upcoming deadlines or events", CPProfile.ElectionCycle)));
			writer.Write(BuildFooterHtml());
		}

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			// set up repeater list
			_repeater = new Repeater()
			{
				HeaderTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildHeaderTemplate)),
				ItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildItemTemplate))
			};
			this.Controls.Add(_repeater);
		}

		/// <summary>
		/// Raises the <see cref="Control.DataBinding"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.EnsureChildControls();
			IEnumerable<CPCalendarItem> ds = this.DataSource ?? CPProfile.ReminderEvents;
			if (ds != null)
			{
				ds.OrderBy(i => i.Date);
			}
			_repeater.DataSource = ds;
			_repeater.DataBind();
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsPostBack)
			{
				this.DataBind();
			}
		}

		#endregion

		#region Templates

		/// <summary>
		/// Generates a header for a list of upcoming events in a repeater control.
		/// </summary>
		/// <returns>An HTML string representing the list header.</returns>
		string BuildHeaderHtml()
		{
			string cssClass = "cp-reminders " + _view.ToString().ToLower();
			return string.Format("<div class=\"{0}\"><h3 class=\"caption\">My Upcoming Events</h3><div class=\"cp-boundingBox\"><table class=\"cp-table\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">", cssClass);
		}

		/// <summary>
		/// Generates an header row for a list of upcoming events in a repeater control.
		/// </summary>
		/// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
		private void BuildHeaderTemplate(Control container)
		{
			container.Controls.Add(new Literal()
			{
				Text = _view == RemindersView.Full ? "<tr><th class=\"date\">Date</th><th class=\"event\">Event</th></tr>" : string.Empty
			});
		}

		/// <summary>
		/// Generates an item in a list of upcoming events in a repeater control.
		/// </summary>
		/// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
		private void BuildItemTemplate(Control container)
		{
			// event date
			Literal date = new Literal();
			date.DataBinding += new EventHandler(
				delegate
				{
					CPCalendarItem item = (container as RepeaterItem).DataItem as CPCalendarItem;
					if (item != null)
					{
						date.Text = string.Format("<tr{0}><td class=\"date\">{1}</td>", item.IsImmediate ? "class=\"alert\"" : null, string.Format(_dateFormat, item.Date));
					}
				});
			container.Controls.Add(date);
			// event title and link
			Literal html = new Literal()
			{
				Text = "<td class=\"event\">"
			};
			container.Controls.Add(html);
			HyperLink link = new HyperLink();
			link.DataBinding += new EventHandler(
				delegate
				{
					CPCalendarItem item = (container as RepeaterItem).DataItem as CPCalendarItem;
					if (item != null)
					{
						link.Text = HttpUtility.HtmlEncode((_view == RemindersView.Full) ? item.Description : item.Title);
						link.ToolTip = item.Description;
						string url = PageUrlManager.GetCalendarItemDisplayUrl(item);
						if (!string.IsNullOrEmpty(url))
							link.NavigateUrl = string.Format("{0}?ItemID={1}", url, PageUrlManager.GetCalendarItemID(item));
					}
				});
			container.Controls.Add(link);
			html = new Literal()
			{
				Text = "</td></tr>"
			};
			container.Controls.Add(html);
		}

		/// <summary>
		/// Generates a footer for a list of upcoming events in a repeater control.
		/// </summary>
		/// <returns>An HTML string representing the list footer.</returns>
		private string BuildFooterHtml()
		{
			return "</table></div></div>";
		}

		#endregion

		/// <summary>
		/// Enumeration for the different views supported by a RemindersWebPart.
		/// </summary>
		public enum RemindersView : byte
		{
			/// <summary>
			/// View that shows long descriptions and full dates for reminders.
			/// </summary>
			Full,
			/// <summary>
			/// View that shows abbreviated titles and concise dates for reminders.
			/// </summary>
			Brief
		}
	}
}
