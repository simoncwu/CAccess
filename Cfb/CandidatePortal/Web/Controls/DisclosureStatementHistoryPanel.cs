using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.SubmissionDocuments;
using System.ComponentModel;

namespace Cfb.CandidatePortal.Web.Controls
{
    /// <summary>
    /// A control for displaying candidate disclosure statement history in a grid format.
    /// </summary>
    [ToolboxData("<{0}:DisclosureStatementHistoryGrid runat=\"server\" />")]
    public class DisclosureStatementHistoryPanel : Panel
    {
        /// <summary>
        /// Gets or sets the CSS class assigned to tooltip items.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string TooltipCssClass
        {
            get { return _gridTemplate.TooltipCssClass; }
            set { _gridTemplate.TooltipCssClass = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class assigned to regular submission tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string RegularTooltipCssClass
        {
            get { return _gridTemplate.RegularTooltipCssClass; }
            set { _gridTemplate.RegularTooltipCssClass = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class assigned to amendment tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string AmendmentTooltipCssClass
        {
            get { return _gridTemplate.AmendmentTooltipCssClass; }
            set { _gridTemplate.AmendmentTooltipCssClass = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class assigned to resubmission tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string ResubmissionTooltipCssClass
        {
            get { return _gridTemplate.ResubmissionTooltipCssClass; }
            set { _gridTemplate.ResubmissionTooltipCssClass = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class assigned to internal amendment tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string IAmendmentTooltipCssClass
        {
            get { return _gridTemplate.IAmendmentTooltipCssClass; }
            set { _gridTemplate.IAmendmentTooltipCssClass = value; }
        }

        /// <summary>
        /// Gets or sets the CSS class assigned to documentation tooltips.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("tooltip")]
        [Description("The CSS class assigned to tooltip items.")]
        [Localizable(true)]
        public string DocumentationTooltipCssClass
        {
            get { return _gridTemplate.DocumentationTooltipCssClass; }
            set { _gridTemplate.DocumentationTooltipCssClass = value; }
        }

        #region Child Controls

        /// <summary>
        /// A container for drop-down selection lists.
        /// </summary>
        Panel _selectPanel;

        /// <summary>
        /// A container for showing disclosure statement history data from partial-page postbacks.
        /// </summary>
        UpdatePanel _historyUP;

        /// <summary>
        /// The drop-down list for selecting a committee.
        /// </summary>
        DropDownList _committeeList;

        /// <summary>
        /// The drop-down list for selectin a statement number.
        /// </summary>
        DropDownList _statementList;

        /// <summary>
        /// The repeater displaying disclosure statement data.
        /// </summary>
        Repeater _repeater;

        #endregion

        /// <summary>
        /// Displays error messages when encountered.
        /// </summary>
        Label _errorMessage;

        /// <summary>
        /// The underlying disclosure statement histories data source.
        /// </summary>
        DisclosureStatementHistories _dsh;

        /// <summary>
        /// An empty submission history grid to use as a template for rendering the disclosure statement history.
        /// </summary>
        SubmissionHistoryGrid _gridTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisclosureStatementHistoryPanel"/> class.
        /// </summary>
        public DisclosureStatementHistoryPanel()
        {
            this.EnableViewState = false;
            _gridTemplate = new SubmissionHistoryGrid();
        }

        #region Control Life Cycle Overrides

        /// <summary>
        /// Raises the <see mref="Init"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs args)
        {
            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (object.Equals(sm, null))
            {
                sm = new ScriptManager();
                sm.SupportsPartialRendering = true;
                this.Controls.Add(sm);
            }
            base.OnInit(args);
            this.CssClass = "submissionGrid disclosureStatementHistory";
            _dsh = CPProfile.DisclosureStatementHistories;
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.EnsureChildControls();
            // set up committee list
            _committeeList.Items.Clear();
            foreach (char id in _dsh.CommitteeNames.Keys)
            {
                _committeeList.Items.Add(new ListItem(string.Format("{0} ({1})", _dsh.CommitteeNames[id], id), id.ToString()));
            }
            // show default committee, else show first committee
            char? primaryID = AuthorizedCommittee.GetPrimaryCommitteeID(CPProfile.Cid, CPProfile.ElectionCycle);
            if (primaryID.HasValue)
            {
                ListItem item = _committeeList.Items.FindByValue(primaryID.Value.ToString());
                if (!object.Equals(item, null))
                {
                    item.Selected = true;
                    FilterByCommittee(null, null);
                }
            }
            else
            {
                _committeeList.SelectedIndex = 0;
                FilterByCommittee(null, null);
            }
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the control content</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (_dsh.Count > 0)
            {
                base.Render(writer);
            }
            else
            {
                writer.Write(string.Format(Properties.Resources.EmptyDataTextTemplate, "disclosure statements", CPProfile.ElectionCycle));
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            // set up selection bar
            _selectPanel = new Panel();
            _selectPanel.CssClass = "cp-SelectionBar";

            // committee list
            _committeeList = new DropDownList();
            _committeeList.ID = "CommitteeList";
            _committeeList.CssClass += " ajaxAutoPostBack";
            _committeeList.AutoPostBack = true;
            _committeeList.SelectedIndexChanged += new EventHandler(FilterByCommittee);
            Literal committeeLabelBeginTag = new Literal();
            committeeLabelBeginTag.Text = "<label class=\"ui-front\"><span>Committee:</span>";
            Literal committeeLabelEndTag = new Literal();
            committeeLabelEndTag.Text = "</label>";
            _selectPanel.Controls.Add(committeeLabelBeginTag);
            _selectPanel.Controls.Add(_committeeList);
            _selectPanel.Controls.Add(committeeLabelEndTag);

            // statement number list
            _statementList = new DropDownList();
            _statementList.ID = "StatementsList";
            _statementList.CssClass += " ajaxAutoPostBack";
            _statementList.AutoPostBack = true;
            _statementList.SelectedIndexChanged += new EventHandler(FilterByStatement);
            Literal statementLabelBeginTag = new Literal();
            statementLabelBeginTag.Text = "<label class=\"ui-front\"><span>Statement #:</span>";
            Literal statementLabelEndTag = new Literal();
            statementLabelEndTag.Text = "</label>";
            _selectPanel.Controls.Add(statementLabelBeginTag);
            _selectPanel.Controls.Add(_statementList);
            _selectPanel.Controls.Add(statementLabelEndTag);

            // expand/collapse all buttons
            Literal expandCollapse = new Literal();
            expandCollapse.Text = "<span class=\"CollapsiblePanelButtons\"><input type=\"button\" value=\"Expand All\" onclick=\"cpg.openAllPanels();\" /><input type=\"button\" value=\"Collapse All\" onclick=\"cpg.closeAllPanels();\" /></span>";
            _selectPanel.Controls.Add(expandCollapse);

            // set up repeater list
            _historyUP = new UpdatePanel();
            _historyUP.UpdateMode = UpdatePanelUpdateMode.Conditional;
            _historyUP.Triggers.Add(new AsyncPostBackTrigger()
            {
                ControlID = _committeeList.ID,
                EventName = "SelectedIndexChanged"
            });
            _historyUP.Triggers.Add(new AsyncPostBackTrigger()
            {
                ControlID = _statementList.ID,
                EventName = "SelectedIndexChanged"
            });
            _errorMessage = new Label();
            _errorMessage.Visible = false;
            _repeater = new Repeater();
            _repeater.HeaderTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildHeaderTemplate));
            _repeater.ItemTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildItemTemplate));
            _repeater.FooterTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildFooterTemplate));

            // AJAX progress indicator
            UpdateProgress progress = new UpdateProgress();
            progress.AssociatedUpdatePanelID = _historyUP.ID;

            // refresh collapsible panels on refresh
            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes.Add("type", "text/javascript");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("function {0}_initWidgets(){{$('#{0} .cp-DisclosureStatements').accordion({{collapsible:true,heightStyle:'content',icons:false,active:0,multiActive:true}});}}Sys.WebForms.PageRequestManager.getInstance().add_endRequest({0}_initWidgets);", this.ClientID);
            js.InnerHtml = sb.ToString();

            // add child controls
            _historyUP.ContentTemplateContainer.Controls.Add(_selectPanel);
            _historyUP.ContentTemplateContainer.Controls.Add(_errorMessage);
            _historyUP.ContentTemplateContainer.Controls.Add(_repeater);
            this.Controls.Add(_historyUP);
            //Page.Header.Controls.Add(js);
        }

        #endregion

        #region Templates

        /// <summary>
        /// Generates the header for a list of certification submissions in a repeater control.
        /// </summary>
        /// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
        void BuildHeaderTemplate(Control container)
        {
            Table t = new Table()
            {
                BorderWidth = 0,
                CellPadding = 0,
                CellSpacing = 0,
                CssClass = "ms-listviewtable"
            };
            container.Controls.Add(t);
            TableHeaderRow tr = new TableHeaderRow() { CssClass = "ms-viewheadertr" };
            t.Controls.Add(tr);
            tr.Controls.Add(new TableHeaderCell() { CssClass = "expander" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-ReceivedDate", Text = "<span>Received</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-SubmissionType", Text = "<span>Document Type</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-Status", Text = "<span>Status</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-StatusAsOf", Text = "<span>Status Date</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-PageCount", Text = "<span>Pages</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-DeliveryType", Text = "<span>Delivery Type</span>" });
            tr.Controls.Add(new TableHeaderCell() { CssClass = "cp-SmallDeferred" });
            container.Controls.Add(new Literal() { Text = "<div class=\"cp-DisclosureStatements ms-listviewtable\">" });
        }

        /// <summary>
        /// Generates an item in a list of certification submissions in a repeater control.
        /// </summary>
        /// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
        void BuildItemTemplate(Control container)
        {
            Table docSummary = new Table() { BorderWidth = 0, CellPadding = 0, CellSpacing = 0 };
            Table format_Backup = new Table() { BorderWidth = 0, CellPadding = 0, CellSpacing = 0 };
            Panel docPanel = new Panel() { CssClass = "docSummary" };
            docPanel.Controls.Add(docSummary);
            container.Controls.Add(docPanel);
            Panel formatPanel = new Panel() { CssClass = "format-backup" };
            formatPanel.Controls.Add(format_Backup);
            container.Controls.Add(formatPanel);

            // basic properties
            docSummary.DataBinding += (sender, e) =>
            {
                DisclosureStatement item = (container as RepeaterItem).DataItem as DisclosureStatement;
                if (item == null) return;
                TableRow tr = new TableRow();
                docSummary.Controls.Add(tr);
                tr.Controls.Add(new TableCell() { CssClass = "expander" });
                tr.Controls.Add(new TableCell() { CssClass = "cp-ReceivedDate", Text = string.Format(Properties.Resources.DateFormat, item.ReceivedDate) });
                tr.Controls.Add(new TableCell() { CssClass = "cp-SubmissionType", Text = _gridTemplate.GetSubmissionTypeText(item) });
                tr.Controls.Add(new TableCell() { CssClass = "cp-Status", Text = CPConvert.ToString(item.Status) });
                tr.Controls.Add(new TableCell() { CssClass = "cp-StatusAsOf", Text = string.Format(Properties.Resources.DateFormat, item.StatusDate) });
                tr.Controls.Add(new TableCell() { CssClass = "cp-PageCount", Text = item.PageCount == 0 && item.SubmittedOnline ? "(n/a)" : item.PageCount.ToString() });
                tr.Controls.Add(new TableCell() { CssClass = "cp-DeliveryType", Text = CPConvert.ToString(item.DeliveryType) });
                tr.Controls.Add(new TableCell() { CssClass = "cp-SmallDeferred", Text = item.SmallCampaign ? "* Small Campaign" : item.DeferredFiling ? "* Deferred Filing" : null });
            };

            // additional properties
            format_Backup.DataBinding += (sender, e) =>
            {
                // data format
                DisclosureStatement item = (container as RepeaterItem).DataItem as DisclosureStatement;
                if (item == null) return;
                StringBuilder sb = new StringBuilder();
                bool multiFormat = false;
                sb.Append("<span class=\"label\">Submission Format:</span> ");
                foreach (SubmissionFormat f in Enum.GetValues(typeof(SubmissionFormat)))
                {
                    if (f == SubmissionFormat.None)
                        continue;
                    if (item.SubmissionFormats.HasFlag(f))
                    {
                        if (multiFormat)
                            sb.Append(", ");
                        sb.AppendFormat(f.ToString<SubmissionFormat>());
                        multiFormat = true;
                    }
                }
                TableRow tr = new TableRow();
                format_Backup.Controls.Add(tr);
                tr.Controls.Add(new TableCell() { CssClass = "cp-ReceivedDate" });
                tr.Controls.Add(new TableCell() { CssClass = "cp-DataFormat", Text = sb.ToString(), ColumnSpan = 2 });
                tr.Controls.Add(new TableCell() { CssClass = "cp-PostmarkDate", Text = item.PostmarkDate.HasValue ? "<span class=\"label\">Postmarked:</span> " + string.Format(Properties.Resources.DateFormat, item.PostmarkDate) : null });

                // backup documentation
                if (item.BackupReceived)
                {
                    tr = new TableRow() { CssClass = "cp-BackupDetails" };
                    format_Backup.Controls.Add(tr);
                    tr.Controls.Add(new TableCell() { CssClass = "caption", Text = "Backup Documentation" });
                    tr.Controls.Add(new TableCell() { CssClass = "cp-ReceivedDate", Text = "<span class=\"label\"Received:</span> " + string.Format(Properties.Resources.DateFormat, item.BackupReceivedDate) });
                    tr.Controls.Add(new TableCell() { CssClass = "cp-DeliveryType", Text = "<span class=\"label\">Delivery Method:</span> " + CPConvert.ToString(item.BackupDeliveryType) });
                    tr.Controls.Add(new TableCell() { CssClass = "cp-PostmarkDate", Text = item.BackupPostmarkDate.HasValue ? "<span class=\"label\">Postmarked:</span> " + string.Format(Properties.Resources.DateFormat, item.BackupPostmarkDate) : null });
                }
            };
        }

        /// <summary>
        /// Generates the footer for a list of certification submissions in a repeater control.
        /// </summary>
        /// <param name="container">A reference to the containing <see cref="RepeaterItem"/> control.</param>
        void BuildFooterTemplate(Control container)
        {
            container.Controls.Add(new Literal()
            {
                Text = string.Format("</div>\n<script type=\"text/javascript\">function {0}_initWidgets(){{$('.cp-DisclosureStatements').accordion({{collapsible:true,heightStyle:'content',icons:false,active:0,multiActive:true}});}}{0}_initWidgets();Sys.WebForms.PageRequestManager.getInstance().add_endRequest({0}_initWidgets);</script>", this.ClientID)
            });
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Filters the data source by statement number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FilterByStatement(object sender, EventArgs e)
        {
            _errorMessage.Visible = false;
            string idValue = _committeeList.SelectedValue;
            if (string.IsNullOrEmpty(idValue))
            {
                ShowRetrieveError();
                return;
            }
            byte number;
            if (byte.TryParse(_statementList.SelectedValue, out number))
            {
                string key = DisclosureStatementHistories.ToKey(idValue.ToCharArray()[0], number);
                if (_dsh.Submissions.ContainsKey(key))
                {
                    _statementList.Items.FindByValue(_statementList.SelectedValue).Selected = true;
                    SetDataSource(_dsh.Submissions[key]);
                }
                else
                {
                    ShowRetrieveError();
                }
            }
            else
            {
                ShowRetrieveError();
            }
        }

        /// <summary>
        /// Occurs when the committee selection changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        void FilterByCommittee(object sender, EventArgs e)
        {
            _errorMessage.Visible = false;
            string id = _committeeList.SelectedValue;
            if (string.IsNullOrEmpty(id) || (id.Length > 1))
            {
                ShowRetrieveError();
                return;
            }
            char idChar = id.ToCharArray()[0];
            _statementList.Items.Clear();
            if (_dsh.CommitteeStatements.ContainsKey(idChar))
            {
                foreach (Statement statement in _dsh.CommitteeStatements[idChar].Values)
                {
                    _statementList.Items.Add(new ListItem(string.Format("{0} ({1:M/d/yyyy})", statement.Number.ToString(), statement.DueDate), statement.Number.ToString()));
                }
            }
            if (_statementList.Items.Count > 0)
            {
                _statementList.SelectedIndex = 0;
                FilterByStatement(null, null);
            }
            else
            {
                ShowRetrieveError();
            }
            _historyUP.Update();
        }

        #endregion

        /// <summary>
        /// Shows an error message indicating a record retrieval failure.
        /// </summary>
        private void ShowRetrieveError()
        {
            _errorMessage.Visible = true;
            _errorMessage.Text = "Sorry, the disclosure statements you have requested could not be retrieved.";
            _repeater.Visible = false;
            _historyUP.Update();
        }

        /// <summary>
        /// Sets the data source for the underlying history grid and invokes data binding.
        /// </summary>
        /// <param name="dsh">The data source for the underlying history grid.</param>
        private void SetDataSource(DisclosureStatementHistory dsh)
        {
            this.EnsureChildControls();
            _repeater.DataSource = dsh.Documents;
            _repeater.DataBind();
            _historyUP.Update();
        }
    }
}
