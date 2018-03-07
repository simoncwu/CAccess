using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a list of post election audit tolling events with an option to switch between a filtered and an unfiltered view of events.
	/// </summary>
	[ToolboxData("<{0}:TollingEventsList runat=server></{0}:TollingEventsList>")]
	public class TollingEventsList : DataBoundControlBase
	{
		/// <summary>
		/// Represents the method that will handle retrieval of a <see cref="TollingEventsList"/> control's data sources.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		public delegate void UpdateDataSourcesEventHandler(object sender, EventArgs e);

		/// <summary>
		/// Occurs when the control retrieves its data sources.
		/// </summary>
		[Category("Action")]
		[Description("Fires when the control retrieves its data sources.")]
		public event UpdateDataSourcesEventHandler UpdateDataSources;

		#region Default Property Values

		private const string DefaultTitle = "Tolling Event Details";
		private const bool DefaultDisplayAllEvents = false;

		#endregion

		/// <summary>
		/// The inner control for displaying tolling events.
		/// </summary>
		private readonly TollingEventsInnerList _innerList;

		/// <summary>
		///  The inner control for displaying the control title.
		/// </summary>
		private Label _titleLabel;

		/// <summary>
		/// The inner control for toggling between views.
		/// </summary>
		private LinkButton _viewToggleLink;

		/// <summary>
		/// The inner update panel control for rendering tolling events.
		/// </summary>
		private UpdatePanel _tollingUpdatePanel;

		/// <summary>
		/// Gets or sets the text to display when the control does not contain any records.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(TollingEventsInnerList.DefaultEmptyDataText)]
		[Description("The text to display when the control does not contain any records.")]
		[Localizable(true)]
		public string EmptyDataText
		{
			get { return _innerList.EmptyDataText; }
			set { _innerList.EmptyDataText = value; }
		}

		/// <summary>
		/// Gets or sets the text to display as the end date for a tolling event that is still ongoing.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(TollingEventsInnerList.DefaultOngoingEndDateText)]
		[Description("The text to display as the end date for a tolling event that is still ongoing.")]
		[Localizable(true)]
		public string OngoingEndDateText
		{
			get { return _innerList.OngoingEndDateText; }
			set { _innerList.OngoingEndDateText = value; }
		}

		/// <summary>
		/// Gets or sets whether or not the control should be visible when the data source does not contain any records.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(TollingEventsInnerList.DefaultEmptyVisible)]
		[Description("Whether or not the control should be visible when the data source does not contain any records.")]
		public bool EmptyVisible
		{
			get { return _innerList.EmptyVisible; }
			set { _innerList.EmptyVisible = value; }
		}

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
			get { return _titleLabel.Text; }
			set { _titleLabel.Text = value; }
		}

		/// <summary>
		/// Gets or sets a data source for tolling event messages.
		/// </summary>
		[Bindable(true)]
		[Description("A data source for tolling event messages.")]
		[Category("Data")]
		public Dictionary<int, string> MessagesDataSource
		{
			get { return _innerList.MessagesDataSource; }
			set { _innerList.MessagesDataSource = value; }
		}

		/// <summary>
		/// Gets or sets the post election audit tolling event collection data source.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		public new List<ITollingEvent> DataSource
		{
			get { return _innerList.DataSource; }
			set { _innerList.DataSource = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the server control persists its view state, and the view state of any child controls it contains, to the requesting client.
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				this.EnsureChildControls();
				_innerList.EnableViewState = base.EnableViewState = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control should display all events.
		/// </summary>
		[Bindable(true)]
		[Description("A value indicating whether the control should display all events.")]
		[Category("Behavior")]
		[DefaultValue(DefaultDisplayAllEvents)]
		public bool DisplayAllEvents
		{
			get
			{
				object obj = this.ViewState["DisplayAllEvents"];
				return obj is bool ? (bool)obj : DefaultDisplayAllEvents;
			}
			set
			{
				this.ViewState["DisplayAllEvents"] = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TollingEventsList"/> control.
		/// </summary>
		public TollingEventsList()
		{
			_titleLabel = new Label();
			_innerList = new TollingEventsInnerList();
			this.CssClass = "cp-" + this.GetType().Name;
			this.Title = DefaultTitle;
			this.DisplayAllEvents = DefaultDisplayAllEvents;
		}

		/// <summary>
		/// Raises the <see cref="UpdateDataSources"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnUpdateDataSources(EventArgs e)
		{
			UpdateDataSourcesEventHandler handler = this.UpdateDataSources;
			if (handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Raises the <see cref="Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresViewStateEncryption();
				this.Page.RegisterRequiresControlState(this);
			}
		}

		/// <summary>
		/// Raises the <see cref="Control.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnLoad(e);
		}

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Clear();
			this.ClearChildState();

			// AJAX support
			// verify presence of a single ScriptManager
			if (ScriptManager.GetCurrent(Page) == null)
			{
				this.Controls.Add(new ScriptManager() { SupportsPartialRendering = true });
			}
			this.Controls.Add(_tollingUpdatePanel = new UpdatePanel() { ChildrenAsTriggers = false, UpdateMode = UpdatePanelUpdateMode.Conditional });

			// title and switch view link
			_tollingUpdatePanel.ContentTemplateContainer.Controls.Add(new Literal() { Text = "<h4>" });
			_tollingUpdatePanel.ContentTemplateContainer.Controls.Add(_titleLabel);
			_tollingUpdatePanel.ContentTemplateContainer.Controls.Add(_viewToggleLink = new LinkButton() { CssClass = "button", ID = "viewToggleLink" });
			_tollingUpdatePanel.ContentTemplateContainer.Controls.Add(new Literal() { Text = "</h4>" });

			// inner list
			_tollingUpdatePanel.ContentTemplateContainer.Controls.Add(_innerList);

			// triggers
			_tollingUpdatePanel.Triggers.Add(new AsyncPostBackTrigger() { ControlID = _viewToggleLink.ID, EventName = "Command" });
			_viewToggleLink.Click += ToggleView;

			// finalize control creation
			this.TrackViewState();
			this.ChildControlsCreated = true;
		}

		/// <summary>
		/// Occurs when the view toggles between showing all and a filtered subset of tolling events.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="EventArgs"/> object that contains the event data.</param>
		private void ToggleView(object sender, EventArgs e)
		{
			this.EnsureChildControls();
			this.DisplayAllEvents = !this.DisplayAllEvents;
			this.DataBind();
			_tollingUpdatePanel.Update();
		}

		/// <summary>
		/// Renders the opening HTML tag of the control into the specified <see cref="HtmlTextWriter"/> object.
		/// </summary>
		/// <param name="writer">The <see cref="HtmlTextWriter"/> that receives the rendered content.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetType().Name);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		/// <summary>
		/// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		/// <summary>
		/// Controls how data is retrieved and bound to the control.
		/// </summary>
		protected override void PerformSelect()
		{
			// update title and switch view link
			_viewToggleLink.Text = this.DisplayAllEvents ? "View Less" : "View All";
			this.MarkAsDataBound();
		}

		/// <summary>
		/// Binds a data source to the invoked server control and all its child controls.
		/// </summary>
		public override void DataBind()
		{
			this.EnsureChildControls();
			this.OnUpdateDataSources(EventArgs.Empty);
			_innerList.DataBind();
			base.DataBind();
		}

		/// <summary>
		/// Verifies that the object a data-bound control binds to is one it can work with.
		/// </summary>
		/// <param name="dataSource">The object to verify as an instance of <see cref="AuditReportBase"/>.</param>
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource != null && !(dataSource is TollingEventCollection))
				throw new InvalidOperationException(string.Format("Data source must be of type {0}.", typeof(TollingEventCollection).FullName));
		}
	}
}
