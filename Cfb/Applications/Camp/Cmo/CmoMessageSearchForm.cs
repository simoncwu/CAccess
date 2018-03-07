using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.Camp.Cmo
{
    /// <summary>
    /// A form for searching Campaign Messages Online messages.
    /// </summary>
    public partial class CmoMessageSearchForm : SearchResultsFormBase
    {
        /// <summary>
        /// The number of results to buffer before repainting the results list.
        /// </summary>
        private const byte RefreshInterval = 7;

        /// <summary>
        /// CMO message search parameters structure.
        /// </summary>
        struct SearchParameters
        {
            public string CandidateID;
            public string ElectionCycle;
            public string Creator;
            public byte? Category;
            public string Title;
            public string Body;
            public DateTime? PostedStartDate;
            public DateTime? PostedEndDate;
            public bool? Opened;
            public bool? HasAttachments;
        }

        private Size _originalSize;

        /// <summary>
        /// Gets whether or not the form is currently conducting a search.
        /// </summary>
        public bool IsSearching { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmoMessageSearchForm"/> form.
        /// </summary>
        public CmoMessageSearchForm()
        {
            InitializeComponent();
            this.IsSearching = false;
            _postedStartDatePicker.MaxDate = _postedEndDatePicker.MaxDate = DateTime.Today;
            this.toolStripStatusLabel.Text = string.Empty;
        }

        /// <summary>
        /// Attempts to display a CMO message in a new <see cref="CmoMessageForm"/>.
        /// </summary>
        /// <param name="uniqueId">The unique identifer of the CMO message to display.</param>
        private void ShowMessage(string uniqueId)
        {
            this.Cursor = Cursors.WaitCursor;
            new MethodInvoker(() =>
            {
                CmoMessage message = CmoMessage.GetMessage(uniqueId);
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (message == null)
                        {
                            MessageBox.Show(this, string.Format("Unable to retrieve campaign message with ID \"{0}\". Please check the message ID and retry your request.", uniqueId), "Message Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            CmoMessageForm form = new CmoMessageForm();
                            form.SetMessage(message);
                            form.ShowAsMdiSiblingOf(this);
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }));
            }).BeginInvoke(null, null);
        }

        /// <summary>
        /// Displays the messages selected in the search results list.
        /// </summary>
        private void ShowSelectedMessages()
        {
            ListView.SelectedListViewItemCollection selection = this.ResultsListView.SelectedItems;
            if (!this.ConfirmMultipleAction("Printing", selection.Count))
                return;
            foreach (ListViewItem item in selection)
            {
                ShowMessage(item.Name);
            }
        }

        /// <summary>
        /// Sets the checkbox value for all selected messages.
        /// </summary>
        /// <param name="isChecked">true if all selected messages are to be checked; otherwise, false to clear their checkboxes.</param>
        private void CheckSelected(bool isChecked)
        {
            foreach (ListViewItem item in this.ResultsListView.SelectedItems)
            {
                item.Checked = isChecked;
            }
        }

        /// <summary>
        /// Sets the checkbox value for all messages.
        /// </summary>
        /// <param name="isChecked">true if all messages are to be checked; otherwise, false to clear their checkboxes.</param>
        private void CheckAll(bool isChecked)
        {
            foreach (ListViewItem item in this.ResultsListView.Items)
            {
                item.Checked = isChecked;
            }
        }

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public override IEnumerable<ToolStrip> GetMergingToolStrips()
        {
            return new[] { this.toolStrip };
        }

        #region Control Event Handlers

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (ResultsListView.Focused)
            {
                // submit will show messages if results view has focus
                ShowSelectedMessages();
            }
            else if (this.IsSearching)
            {
                // if searching, stop the search
                this.IsSearching = false;
                SearchBackgroundWorker.CancelAsync();
            }
            else
            {
                // perform search
                this.IsSearching = true;
                this.SearchButton.Text = "Stop";
                this.statusStrip.Visible = true;
                int statusHeight = this.statusStrip.Height;
                this.MinimumSize = new Size(_originalSize.Width, _originalSize.Height + statusHeight);
                int preferredHeight = this.MinimumSize.Height * 2 + statusHeight;
                if (this.Height < preferredHeight)
                    this.Size = new Size(this.Width, preferredHeight);
                if (!SearchBackgroundWorker.IsBusy) // BUGFIX #36: check bg worker status before invoking
                {
                    // search by properties
                    this.ResultsListView.Items.Clear();
                    this.ResultsListView.Cursor = Cursors.WaitCursor;
                    this.toolStripStatusLabel.Text = "Searching...";
                    Application.DoEvents();
                    this.ResultsListView.BeginUpdate();
                    SearchBackgroundWorker.RunWorkerAsync(new SearchParameters()
                    {
                        CandidateID = _candidatePicker.SelectedCid,
                        ElectionCycle = _electionPicker.SelectedElection == null ? null : _electionPicker.SelectedElection.Cycle,
                        Creator = _cfbEmployeePicker.SelectedEmployee,
                        Category = _categoryComboBox.SelectedCategory,
                        Title = string.IsNullOrEmpty(_subjectTextBox.Text) ? null : _subjectTextBox.Text,
                        Body = string.IsNullOrEmpty(_bodyTextBox.Text) ? null : _bodyTextBox.Text,
                        PostedStartDate = _postedStartDatePicker.Enabled ? (DateTime?)_postedStartDatePicker.Value.Date : null,
                        PostedEndDate = _postedEndDatePicker.Enabled ? (DateTime?)_postedEndDatePicker.Value.Date : null,
                        Opened = _openedCheckBox.CheckState == CheckState.Indeterminate ? null : (bool?)_openedCheckBox.Checked,
                        HasAttachments = _attachmentsCheckBox.CheckState == CheckState.Indeterminate ? null : (bool?)_attachmentsCheckBox.Checked,
                    });
                }
            }
        }

        private void SearchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.SearchBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                if (e.Argument.GetType() != typeof(SearchParameters))
                    return;
                SearchParameters param = (SearchParameters)e.Argument;
                var results = CmoProviders.DataProvider.FindCmoMessages(param.CandidateID, param.ElectionCycle, param.Creator, param.Category, param.Title, param.Body, param.PostedStartDate, param.PostedEndDate, param.Opened, param.HasAttachments);
                if (results != null)
                {
                    int total = results.Count;
                    var queue = new Queue<ListViewItem>();
                    if (total > 0)
                    {
                        int processed = 0;
                        foreach (CmoMessage m in results)
                        {
                            if (this.SearchBackgroundWorker.CancellationPending) // check for asynchronous cancel
                            {
                                e.Cancel = true;
                                break;
                            }
                            queue.Enqueue(new ListViewItem(new string[] {
                                m.UniqueID, 					
                                m.PostDate.HasValue ? string.Format("{0:d} {0:t}", m.PostDate.Value) : null, 				
                                m.ElectionCycle, 		
                                Cfb.CandidatePortal.Cfis.GetCandidateName(m.CandidateID, true), 			
                                m.Title, 		
                                m.Creator,				
                                m.OpenDate.HasValue ? string.Format("{0:d} {0:t}", m.OpenDate) : null, 		
                                m.HasAttachment ? "Y" : null, 
                            }, m.OpenDate.HasValue ? "ReadEmail_16x16.png" : "Unread_16x16.png")
                            {
                                Name = m.UniqueID,
                                Tag = m
                            });
                            if (++processed % RefreshInterval == 0)
                            {
                                this.SearchBackgroundWorker.ReportProgress(processed * 100 / total, queue.ToList());
                                queue.Clear();
                            }
                        }
                    }
                    e.Result = queue;
                }
            }
        }

        private void SearchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var messages = e.UserState as IEnumerable<ListViewItem>;
            if (messages != null)
            {
                this.ResultsListView.Items.AddRange(messages.ToArray());
                int count = this.ResultsListView.Items.Count;
                this.toolStripStatusLabel.Text = string.Format("Searching... ({0} message{1} found)", count, count == 1 ? "" : "s");
                this.statusStrip.Refresh();
            }
            Application.DoEvents();
        }

        private void SearchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                IEnumerable<ListViewItem> queue = e.Result as IEnumerable<ListViewItem>;
                if (queue != null)
                    this.ResultsListView.Items.AddRange(queue.ToArray()); // process the tail end of the queue
            }
            this.ResultsListView.EndUpdate();
            this.ResultsListView.Cursor = Cursors.Default;
            int count = this.ResultsListView.Items.Count;
            this.toolStripStatusLabel.Text = string.Format("{0} message{1} found.", count, count == 1 ? "" : "s");
            this.IsSearching = false;
            this.SearchButton.Text = "Search";
        }

        private void CmoMessageSearchForm_Load(object sender, EventArgs e)
        {
            _originalSize = this.Size;
            // populate search lists
            _electionPicker.SetCandidateID(null);
            // not sure why, but this has to be done twice or else first item gets selected
            _electionPicker.SelectedElection = null;
            _electionPicker.SelectedElection = null;
            _candidatePicker.SetElectionCycle(null);
            if (_cfbEmployeePicker.Items.Count == 0)
                _cfbEmployeePicker.LoadMessageCreators();
            _categoryComboBox.LoadCategories();
        }

        private void ResultsListView_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectedMessages();
        }

        private void _electionPicker_Leave(object sender, EventArgs e)
        {
            _electionPicker.AutoSelect();
        }

        private void _candidatePicker_Leave(object sender, EventArgs e)
        {
            _candidatePicker.AutoSelect();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (this.SearchBackgroundWorker.IsBusy)
                this.SearchBackgroundWorker.CancelAsync();
            this.ResultsListView.Items.Clear();
            this.Size = this.MinimumSize = _originalSize;
            this.statusStrip.Visible = false;
            this.toolStripStatusLabel.Text = string.Empty;
            _candidatePicker.SelectedIndex = _cfbEmployeePicker.SelectedIndex = _categoryComboBox.SelectedIndex = -1;
            _openedCheckBox.CheckState = _attachmentsCheckBox.CheckState = CheckState.Indeterminate;
            string ec = SettingsManager.DefaultElectionCycle;
            if (string.IsNullOrWhiteSpace(ec))
                _electionPicker.SelectedIndex = -1;
            else
                _electionPicker.SelectedValue = SettingsManager.DefaultElectionCycle;
            _subjectTextBox.Text = _bodyTextBox.Text = null;
            _postedCheckBox.Checked = false;
            _postedStartDatePicker.Value = _postedEndDatePicker.Value = DateTime.Today;
            _electionPicker.Focus();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedMessages();
        }


        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selection = this.ResultsListView.SelectedItems;
            if (!this.ConfirmMultipleAction("Printing", selection.Count))
                return;
            foreach (ListViewItem item in selection)
            {
                CmoMessage message = CmoMessage.GetMessage(item.Name);
                if (message != null)
                {
                    using (CmoMessageForm form = new CmoMessageForm())
                    {
                        form.Message = message;
                        form.PrintMessage(false);
                    }
                }
            }
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelected(true);
        }

        private void uncheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSelected(false);
        }

        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll(true);
        }

        private void uncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll(false);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.ResultsListView.Items)
            {
                item.Selected = true;
            }
        }

        private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.ResultsListView.Items)
            {
                item.Selected = !item.Selected;
            }
        }

        private void resultsContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            bool hasSelection = this.ResultsListView.SelectedItems.Count > 0;
            this.openToolStripMenuItem.Visible = this.printToolStripMenuItem.Visible = this.checkToolStripMenuItem.Visible = this.uncheckToolStripMenuItem.Visible = hasSelection;
            this.checkAllToolStripMenuItem1.Visible = this.uncheckAllToolStripMenuItem1.Visible = this.selectAllToolStripMenuItem1.Visible = !hasSelection;
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.checkSelectedToolStripMenuItem.Enabled = this.uncheckSelectedToolStripMenuItem.Enabled = this.ResultsListView.SelectedItems.Count > 0;
        }

        private void reportToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GenericMessagesReport report = new GenericMessagesReport();
            foreach (ListViewItem item in this.ResultsListView.CheckedItems)
            {
                report.Messages.Add(item.Tag as CmoMessage);
            }
            report.ShowAsMdiSiblingOf(this);
            this.Cursor = Cursors.Default;
        }

        private void ResultsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this._reportToolStripButton.Enabled = this.ResultsListView.CheckedItems.Count > 0;
        }

        private void ResultsListView_VisibleChanged(object sender, EventArgs e)
        {
            this._reportToolStripButton.Enabled = this.ResultsListView.Visible && this.ResultsListView.CheckedItems.Count > 0;
        }

        /// <summary>
        /// Occurs as the "File" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public override void FileMenuDropDownOpening(object sender, EventArgs e)
        {
            this.openMessageToolStripMenuItem.Enabled = this.printMessageToolStripMenuItem.Enabled = this.ResultsListView.SelectedItems.Count > 0 && this.ResultsListView.Visible;
        }

        private void _postedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _postedStartDatePicker.Enabled = _postedEndDatePicker.Enabled = _postedCheckBox.Checked;
        }

        private void _postedStartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_postedStartDatePicker.Value > _postedEndDatePicker.Value)
                _postedEndDatePicker.Value = _postedStartDatePicker.Value;
        }

        private void _postedEndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_postedEndDatePicker.Value < _postedStartDatePicker.Value)
                _postedStartDatePicker.Value = _postedEndDatePicker.Value;
        }

        #endregion
    }
}
