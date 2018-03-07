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
    /// A control for displaying a list of post election audit tolling events.
    /// </summary>
    [ToolboxData("<{0}:TollingEventsInnerList runat=server></{0}:TollingEventsInnerList>")]
    internal class TollingEventsInnerList : DataBoundControlBase
    {
        #region Default Property Values

        internal const bool DefaultEmptyVisible = true;
        internal const string DefaultEmptyDataText = "No tolling events have occurred.";
        internal const string DefaultOngoingEndDateText = "(Ongoing)";

        #endregion

        /// <summary>
        /// The view state of the currently bound-to data.
        /// </summary>
        private TollingEventsListViewState _viewState;

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
        /// Gets or sets the text to display as the end date for a tolling event that is still ongoing.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultOngoingEndDateText)]
        [Description("The text to display as the end date for a tolling event that is still ongoing.")]
        [Localizable(true)]
        public string OngoingEndDateText
        {
            get { return this.ViewState["OngoingEndDateText"] as string ?? string.Empty; }
            set { this.ViewState["OngoingEndDateText"] = value; }
        }

        /// <summary>
        /// Gets or sets whether or not the control should be visible when the data source does not contain any records.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(DefaultEmptyVisible)]
        [Description("Whether or not the control should be visible when the data source does not contain any records.")]
        public bool EmptyVisible
        {
            get
            {
                object obj = this.ViewState["EmptyVisible"];
                return obj is bool ? (bool)obj : DefaultEmptyVisible;
            }
            set
            {
                this.ViewState["EmptyVisible"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a data source for tolling event messages.
        /// </summary>
        [Bindable(true)]
        [Description("A data source for tolling event messages.")]
        [Category("Data")]
        public Dictionary<int, string> MessagesDataSource
        {
            get { return this.ViewState["MessagesDataSource"] as Dictionary<int, string>; }
            set { this.ViewState["MessagesDataSource"] = value; }
        }

        /// <summary>
        /// Gets or sets the post election audit tolling event collection data source.
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        public new List<ITollingEvent> DataSource { get; set; }

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
                base.EnableViewState = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TollingEventsInnerList"/> control.
        /// </summary>
        public TollingEventsInnerList()
        {
            this.EmptyDataText = DefaultEmptyDataText;
            this.EmptyVisible = DefaultEmptyVisible;
            this.OngoingEndDateText = DefaultOngoingEndDateText;
        }

        /// <summary>
        /// Saves any state that was modified after the <see cref="WebControl.TrackViewState"/> method was invoked.
        /// </summary>
        /// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.</returns>
        protected override object SaveViewState()
        {
            return new Pair(base.SaveViewState(), _viewState);
        }

        /// <summary>
        /// Restores view-state information from a previous page request that was saved by the SaveViewState method.
        /// </summary>
        /// <param name="savedState">An <see cref="Object"/> that represents the control state to be restored.</param>
        protected override void LoadViewState(object savedState)
        {
            Pair state = savedState as Pair;
            if (state != null)
            {
                _viewState = state.Second as TollingEventsListViewState;
                base.LoadViewState(state.First);
            }
            else
            {
                _viewState = new TollingEventsListViewState();
            }
        }

        /// <summary>
        /// Renders the opening HTML tag of the control into the specified <see cref="HtmlTextWriter"/> object.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> that receives the rendered content.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // table start
            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "cp-TollingEventList ms-listviewtable");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // header row
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ms-viewheadertr");
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            // description
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEvent");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Event Description");
            writer.RenderEndTag(); // Th
            // start date
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingStartDate");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Start Date");
            writer.RenderEndTag(); // Th
            // end date
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEndDate");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("End Date");
            writer.RenderEndTag(); // Th
            // end reason
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEndReason");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("End Reason");
            writer.RenderEndTag(); // Th
            // tolling days
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingDays");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("# of Days");
            writer.RenderEndTag(); // Th
            // due date
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "dueDate");
            writer.RenderBeginTag(HtmlTextWriterTag.Th);
            writer.Write("Due Date");
            writer.RenderEndTag(); // Th
            writer.RenderEndTag(); // Tr

            if (_viewState == null || _viewState.Events.Count == 0)
            {
                // empty data message
                if (this.Visible = this.EmptyVisible)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "7");
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "emptydata");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(this.EmptyDataText);
                    writer.RenderEndTag(); // Td
                    writer.RenderEndTag(); // Tr
                }
            }
            else
            {
                foreach (var e in _viewState.Events)
                {
                    string description = Page.Server.HtmlEncode(e.Description);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    // description
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEvent cp-DownloadLink");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (string.IsNullOrEmpty(e.MessageID))
                        writer.Write("<span class=\"nolink\">{0}</span>", description);
                    else
                        writer.Write("<a class=\"pdf-file\" href=\"{0}\" title=\"{1}\">{1}</a>", PageUrlManager.GetMessageUrl(e.MessageID), description);
                    writer.RenderEndTag(); // Td
                    // start date
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingStartDate");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(e.StartDate.ToString("d"));
                    writer.RenderEndTag(); // Td
                    // end date
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEndDate");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(e.EndDate.HasValue ? e.EndDate.Value.ToString("d") : this.OngoingEndDateText);
                    writer.RenderEndTag(); // Td
                    // end reason
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingEndReason");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(e.EndReason);
                    writer.RenderEndTag(); // Td
                    // tolling days
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tollingDays");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(e.TollingDays);
                    writer.RenderEndTag(); // Td
                    // due date
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "dueDate");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (e.Deadline.HasValue)
                        writer.Write(e.Deadline.Value.ToString("d"));
                    writer.RenderEndTag(); // Td
                    writer.RenderEndTag(); // Tr
                }
            }
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // table end
            writer.RenderEndTag(); // Table
        }

        /// <summary>
        /// Controls how data is retrieved and bound to the control.
        /// </summary>
        protected override void PerformSelect()
        {
            if (this.DataSourceID.Length == 0)
            {
                this.OnDataBinding(EventArgs.Empty);
            }
            var ds = this.DataSource;
            if (ds != null)
            {
                _viewState = new TollingEventsListViewState();
                Dictionary<int, string> mds = this.MessagesDataSource == null ? null : this.MessagesDataSource;
                foreach (var t in ds.OrderByDescending(t => t.TollingStartDate))
                {
                    string messageID;
                    _viewState.Events.Add(new TollingEventsListViewState.TollingEventListItem()
                    {
                        Description = t.Description,
                        StartDate = t.StartDate,
                        EndDate = t.TollingEndDate,
                        TollingDays = t.GetTollingDays(),
                        Deadline = t.TollingDueDate,
                        EndReason = CPConvert.ToString(t.TollingEndReason),
                        MessageID = mds != null && mds.TryGetValue(t.IsInadequateResponse ? -t.TollingEventNumber : t.TollingEventNumber, out messageID) ? messageID : null
                    });
                }
            }
            this.MarkAsDataBound();
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

        /// <summary>
        /// Storage for <see cref="TollingEventsList"/> state information to allow saving and restoring of view state information across multiple requests.
        /// </summary>
        [Serializable]
        private class TollingEventsListViewState
        {
            /// <summary>
            /// The inner collection of tolling event list view state items.
            /// </summary>
            private readonly List<TollingEventListItem> _events;

            /// <summary>
            /// Gets the inner collection of tolling event list view state items.
            /// </summary>
            public List<TollingEventListItem> Events
            {
                get { return _events; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TollingEventsListViewState"/> class.
            /// </summary>
            public TollingEventsListViewState()
            {
                _events = new List<TollingEventListItem>();
            }

            /// <summary>
            /// Storage for individual tolling event item state information to allow saving and restoring of view state information across multiple requests.
            /// </summary>
            [Serializable]
            public class TollingEventListItem
            {
                /// <summary>
                /// Gets or sets description data.
                /// </summary>
                public string Description { get; set; }

                /// <summary>
                /// Gets or sets start date data.
                /// </summary>
                public DateTime StartDate { get; set; }

                /// <summary>
                /// Gets or sets end date data.
                /// </summary>
                public DateTime? EndDate { get; set; }

                /// <summary>
                /// Gets or sets tolling days data.
                /// </summary>
                public int TollingDays { get; set; }

                /// <summary>
                /// Gets or sets deadline data.
                /// </summary>
                public DateTime? Deadline { get; set; }

                /// <summary>
                /// Gets or sets tolling end reason data.
                /// </summary>
                public string EndReason { get; set; }

                /// <summary>
                /// Gets or sets CMO message ID data.
                /// </summary>
                public string MessageID { get; set; }
            }
        }
    }
}
