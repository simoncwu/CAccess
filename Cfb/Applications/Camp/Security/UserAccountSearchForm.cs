using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Security;
using Cfb.DirectoryServices;
using Cfb.Extensions;
using Microsoft.Reporting.WinForms;
using Cfb.Camp.Reports;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// A form for searching Campaign Messages Online messages.
    /// </summary>
    public partial class UserAccountSearchForm : SearchResultsFormBase
    {
        /// <summary>
        /// The number of results to buffer before repainting the results list.
        /// </summary>
        private const byte RefreshInterval = 7;

        /// <summary>
        /// Account search parameters structure.
        /// </summary>
        struct SearchParameters
        {
            public string Name;
            public string Email;
            public string CandidateID;
            public string ElectionCycle;
            public byte? GroupID;
            public bool? Disabled;
            public bool? Locked;
            public bool? Used;
            public DateTime? CreationStartDate;
            public DateTime? CreationEndDate;
        }

        /// <summary>
        /// Image list image indices.
        /// </summary>
        private const int EnabledUserImageIndex = 0;
        private const int DisabledUserImageIndex = 1;

        /// <summary>
        /// The original size of the form.
        /// </summary>
        private Size _originalSize;

        /// <summary>
        /// Gets whether or not the form is currently conducting a search.
        /// </summary>
        public bool IsSearching { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountSearchForm"/> form.
        /// </summary>
        public UserAccountSearchForm()
        {
            InitializeComponent();
            this.IsSearching = false;
            _createStartDatePicker.MaxDate = _createEndDatePicker.MaxDate = DateTime.Today;
            this.toolStripStatusLabel.Text = string.Empty;
        }

        /// <summary>
        /// Attempts to display a C-Access user in a new <see cref="UserAccountForm"/>.
        /// </summary>
        /// <param name="username">The username of the account to display.</param>
        private void ShowUserAccount(string username)
        {
            this.Cursor = Cursors.WaitCursor;
            new MethodInvoker(() =>
            {
                CPUser user = CPSecurity.Provider.GetUser(username);
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (user == null)
                            MessageBox.Show(this, string.Format("User \"{0}\" could not be found.", username), "User Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            UserAccountForm.ShowExistingAccountForm(user, this.MdiParent);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }));
            }).BeginInvoke(null, null);
        }

        /// <summary>
        /// Displays the accounts selected in the search results list.
        /// </summary>
        private void ShowSelectedUsers()
        {
            ListView.SelectedListViewItemCollection selection = this.ResultsListView.SelectedItems;
            if (!this.ConfirmMultipleAction("Open", selection.Count))
                return;
            foreach (ListViewItem item in selection)
            {
                ShowUserAccount(item.Name);
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
                ShowSelectedUsers();
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
                        Name = _displayNameTextBox.Text,
                        Email = _emailTextBox.Text,
                        CandidateID = _candidatePicker.SelectedCid,
                        ElectionCycle = _electionPicker.SelectedElection == null ? null : _electionPicker.SelectedElection.Cycle,
                        GroupID = _groupComboBox.SelectedGroup,
                        Disabled = _disabledCheckBox.CheckState == CheckState.Indeterminate ? null : (bool?)(_disabledCheckBox.Checked),
                        Locked = _lockedCheckBox.CheckState == CheckState.Indeterminate ? null : (bool?)(_lockedCheckBox.Checked),
                        Used = _loggedInCheckBox.CheckState == CheckState.Indeterminate ? null : (bool?)(_loggedInCheckBox.Checked),
                        CreationStartDate = _createStartDatePicker.Enabled ? (DateTime?)_createStartDatePicker.Value.Date : null,
                        CreationEndDate = _createEndDatePicker.Enabled ? (DateTime?)_createEndDatePicker.Value.Date : null,
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
                var results = CPSecurity.Provider.FindUsers(param.Name, param.Email, param.CandidateID, param.ElectionCycle, param.GroupID, param.Disabled, param.Locked, param.Used, param.CreationStartDate, param.CreationEndDate);
                if (results != null)
                {
                    int total = results.Count;
                    var queue = new Queue<ListViewItem>();
                    if (total > 0)
                    {
                        int processed = 0;
                        foreach (var user in results)
                        {
                            if (this.SearchBackgroundWorker.CancellationPending) // check for asynchronous cancel
                            {
                                e.Cancel = true;
                                break;
                            }
                            queue.Enqueue(new ListViewItem(new string[] {
                                user.UserName,
                                user.DisplayName,
                                user.Email,
                                Cfb.CandidatePortal.Cfis.GetCandidateName(user.Cid, true),
                                user.CreationDate.ToShortDateString(),
                                user.LastLoginDate.HasValue ? user.LastLoginDate.Value.ToShortDateString() : "(never)",
                                user.Enabled ? null : "✔",
                                user.LockedOut ? "✔" : null
                            }, user.Enabled ? EnabledUserImageIndex : DisabledUserImageIndex)
                            {
                                Name = user.UserName,
                                Tag = user
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
            var users = e.UserState as IEnumerable<ListViewItem>;
            if (users != null)
            {
                this.ResultsListView.Items.AddRange(users.ToArray());
                int count = this.ResultsListView.Items.Count;
                this.toolStripStatusLabel.Text = string.Format("Searching... ({0} users{1} found)", count, count == 1 ? "" : "s");
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
            this.toolStripStatusLabel.Text = string.Format("{0} user{1} found.", count, count == 1 ? "" : "s");
            this.IsSearching = false;
            this.SearchButton.Text = "Search";
        }

        private void UserAccountSearchForm_Load(object sender, EventArgs e)
        {
            _originalSize = this.Size;
            // populate search lists
            if (!this.DesignMode)
            {
                _electionPicker.SetCandidateID(null);
                _candidatePicker.SetElectionCycle(null);
                _groupComboBox.LoadGroups();
            }
        }

        private void ResultsListView_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectedUsers();
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
            string ec = SettingsManager.DefaultElectionCycle;
            if (string.IsNullOrWhiteSpace(ec))
                _electionPicker.SelectedIndex = -1;
            else
                _electionPicker.SelectedValue = SettingsManager.DefaultElectionCycle;
            _displayNameTextBox.Text = _emailTextBox.Text = null;
            _postedCheckBox.Checked = false;
            _createStartDatePicker.Value = _createEndDatePicker.Value = DateTime.Today;
            _electionPicker.Focus();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedUsers();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selection = this.ResultsListView.SelectedItems;
            if (!this.ConfirmMultipleAction("Printing", selection.Count))
                return;
            foreach (ListViewItem item in selection)
            {
                Application.DoEvents();
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
            List<CPUser> users = new List<CPUser>();
            foreach (ListViewItem item in this.ResultsListView.CheckedItems)
            {
                users.Add(item.Tag as CPUser);
            }
            UserAccountsReport viewer = new UserAccountsReport()
            {
                CampaignSelectionEnabled = false,
                BeginGetReportData = (BackgroundWorker worker) =>
                {
                    var candidates = CPProviders.DataProvider.GetCandidates();
                    return from u in users
                           select ReportableUser.CreateReportableUser(u, candidates.ContainsKey(u.Cid) ? candidates[u.Cid].GetFullName(true) : string.Empty);
                },
                Title = "C-Access User Account Search Results"
            };
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Cfb_CandidatePortal_Security", this.ReportableUserBindingSource));
            viewer.LocalReport.ReportEmbeddedResource = "Cfb.Camp.Security.Reports.GenericAccountsReport.rdlc";
            viewer.ShowAsMdiSiblingOf(this);
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
            _createStartDatePicker.Enabled = _createEndDatePicker.Enabled = _postedCheckBox.Checked;
        }

        private void _postedStartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_createStartDatePicker.Value > _createEndDatePicker.Value)
                _createEndDatePicker.Value = _createStartDatePicker.Value;
        }

        private void _postedEndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_createEndDatePicker.Value < _createStartDatePicker.Value)
                _createStartDatePicker.Value = _createEndDatePicker.Value;
        }

        #endregion
    }
}
