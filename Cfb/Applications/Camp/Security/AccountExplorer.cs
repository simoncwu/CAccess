using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;
using Cfb.Extensions;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// A form for browsing C-Access user accounts.
    /// </summary>
    public partial class AccountExplorer : CampMdiChild
    {
        private const string GroupNodeTag = "_GROUP_NODE_TAG";

        // Root node references
        private readonly TreeNode _candidatesNode;
        private readonly TreeNode _electionCyclesNode;
        private readonly TreeNode _groupsNode;

        // Node image indices
        private readonly int _candidatesImageIndex;
        private readonly int _electionCyclesImageIndex;
        private readonly int _groupsImageIndex;
        private readonly int _userImageIndex;
        private readonly int _userDisabledImageIndex;
        private readonly int _blockImageIndex;
        private readonly int _userInactiveImageIndex;

        // Root node load flags
        private bool _candidatesLoaded;
        private bool _electionsLoaded;
        private bool _groupsLoaded;

        // Flag to track if the listview was clicked.
        private bool _listViewClicked;

        // Locking object for synchronizing access to listview click tracking flag.
        private object _listViewClickedLock;

        /// <summary>
        /// Whether or not to display administrator-only options.
        /// </summary>
        private readonly bool _adminMode;

        /// <summary>
        /// Gets or sets whether or not the tree panel is collapsed.
        /// </summary>
        public bool TreePanelCollapsed
        {
            get { return _splitContainer.Panel1Collapsed; }
            set { _splitContainer.Panel1Collapsed = value; }
        }

        /// <summary>
        /// Gets the currently selected user account.
        /// </summary>
        private CPUser SelectedUser
        {
            get
            {
                ListViewItem i = this.SelectedItem;
                return i == null || !(i.Tag is CPUser) ? null : i.Tag as CPUser;
            }
        }

        /// <summary>
        /// Gets the currently selected CFIS entity.
        /// </summary>
        private Entity SelectedEntity
        {
            get
            {
                ListViewItem i = this.SelectedItem;
                if (i == null)
                    return null;
                return i.Tag as Entity;
            }
        }

        /// <summary>
        /// Gets the currently selected ineligibility status.
        /// </summary>
        private AccountEligibilityStatus SelectedIneligibilityStatus
        {
            get
            {
                ListViewItem i = this.SelectedItem;
                return i == null || !(i.Tag is AccountEligibilityStatus) ? null : i.Tag as AccountEligibilityStatus;
            }
        }

        /// <summary>
        /// Gets the currently selected list view item.
        /// </summary>
        private ListViewItem SelectedItem
        {
            get { return _listView.SelectedItems.Count == 0 ? null : _listView.SelectedItems[0]; }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AccountExplorer"/> form.
        /// </summary>
        public AccountExplorer()
        {
            InitializeComponent();
            _listViewClickedLock = new object();
            _candidatesNode = _treeView.Nodes["Candidates"];
            _electionCyclesNode = _treeView.Nodes["Election Cycles"];
            _groupsNode = _treeView.Nodes["Security Groups"];
            _candidatesImageIndex = _candidatesNode.ImageIndex;
            _electionCyclesImageIndex = _electionCyclesNode.ImageIndex;
            _groupsImageIndex = _groupsNode.ImageIndex;
            _userImageIndex = _groupsImageIndex + 1;
            _userDisabledImageIndex = _userImageIndex + 1;
            _blockImageIndex = _userDisabledImageIndex + 1;
            _userInactiveImageIndex = _blockImageIndex + 1;
            _candidatesLoaded = _electionsLoaded = _groupsLoaded = false;
            _adminMode = SettingsManager.AdministratorModeEnabled;
        }

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public override IEnumerable<ToolStrip> GetMergingToolStrips()
        {
            return new[] { this.toolStrip };
        }

        /// <summary>
        /// Refreshes the current view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        public void Refresh(object sender)
        {
            TreeNode node = _treeView.SelectedNode;
            if (node == _candidatesNode)
                _candidatesLoaded = false;
            else if (node == _electionCyclesNode)
                _electionsLoaded = false;
            else if (node == _groupsNode)
                _groupsLoaded = false;
            _treeView_AfterSelect(sender, new TreeViewEventArgs(node));
        }

        /// <summary>
        /// Creates a tree node to represent a candidate.
        /// </summary>
        /// <param name="candidate">The candidate represented by the tree node.</param>
        /// <returns>A new tree node that represents the specified candidate.</returns>
        private TreeNode CreateCandidateNode(Candidate candidate)
        {
            TreeNode node = null;
            if (candidate != null)
            {
                node = new TreeNode(string.Format("{0} [{1}]", candidate.FormalName, candidate.ID), _candidatesImageIndex, _candidatesImageIndex)
                {
                    Name = candidate.ID
                };
                ActiveCandidate ac = candidate as ActiveCandidate;
                if (ac == null)
                {
                    node.Tag = candidate;
                    node.Nodes.Add(new TreeNode());
                }
                else
                {
                    node.Tag = ac;
                }
            }
            return node;
        }

        /// <summary>
        /// Creates a tree node to represent an election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle represented by the tree node.</param>
        /// <param name="candidate">A candidate active in the cycle.</param>
        /// <returns>A new tree node that represents the specified election cyle.</returns>
        private TreeNode CreateElectionCycleNode(string electionCycle, Candidate candidate = null)
        {
            TreeNode node = null;
            if (!string.IsNullOrEmpty(electionCycle))
            {
                node = new TreeNode(electionCycle, _electionCyclesImageIndex, _electionCyclesImageIndex)
                {
                    Name = electionCycle,
                };
                if (candidate == null)
                {
                    node.Tag = electionCycle;
                    node.Nodes.Add(new TreeNode());
                }
                else
                {
                    node.Tag = ActiveCandidate.GetActiveCandidate(candidate.ID, electionCycle);
                }
            }
            return node;
        }

        /// <summary>
        /// Creates a tree node to represent a C-Access security group.
        /// </summary>
        /// <param name="group">The group represented by the tree node.</param>
        /// <returns>A new tree node that represents the specified group.</returns>
        private TreeNode CreateGroupNode(CPGroup group)
        {
            return group == null ? null : new TreeNode(group.Name, _groupsImageIndex, _groupsImageIndex)
            {
                Name = group.Name,
                Tag = GroupNodeTag
            };
        }

        /// <summary>
        /// Displays the child nodes for a single tree node in a <see cref="ListView"/> control.
        /// </summary>
        /// <param name="node">The node whose children are to be displayed.</param>
        private void ShowChildren(TreeNode node)
        {
            if (node == null)
                return;
            this.Cursor = Cursors.WaitCursor;
            Control focusedControl = _listView.Focused || _listViewClicked ? (Control)_listView : (Control)_treeView;
            _listView.Items.Clear();
            _listView.Columns.Clear();
            _listView.BeginUpdate();
            _treeView.Enabled = false;
            var caller = new Func<TreeNode, ListViewContents>(GetListItems);
            caller.BeginInvoke(node, (IAsyncResult ar) =>
            {
                var contents = (ar.AsyncState as Func<TreeNode, ListViewContents>).EndInvoke(ar);
                this.Invoke(new MethodInvoker(() =>
                {
                    _listView.Columns.AddRange(contents.Headers);
                    _listView.Items.AddRange(contents.Items);
                    _listView.EndUpdate();
                    _listView_SelectedIndexChanged(node, new EventArgs());
                    _treeView.Enabled = true;
                    focusedControl.Focus();
                    lock (_listViewClickedLock)
                    {
                        _listViewClicked = false;
                    }
                    this.Cursor = Cursors.Default;
                }));
            }, caller);
        }

        /// <summary>
        /// Retrieves the content to display in a <see cref="ListView"/> when a <see cref="TreeNode"/> is selected.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> that was selected.</param>
        /// <returns>A <see cref="ListViewContents"/> that defines the columns and items of the content.</returns>
        private ListViewContents GetListItems(TreeNode node)
        {
            ColumnHeader[] headers = null;
            IEnumerable<ListViewItem> items = null;
            if (node.Tag is ActiveCandidate)
            {
                // active candidate leaf node selected, show active candidate contacts/users
                headers = new[] { _nameColumnHeader, _typeColumnHeader, _usernameColumnHeader, _emailColumnHeader };
                ActiveCandidate ac = node.Tag as ActiveCandidate;
                AnalysisResults results = AccountAnalysis.Analyze(Cfb.CandidatePortal.Cfis.GetCandidate(ac.ID), ac.ElectionCycle);

                // users for current campaign
                var current = from u in results.CurrentUsers
                              where _adminMode || u.SourceType != EntityType.Generic // hide generic accounts from non-admins
                              select new ListViewItem(new[] { u.DisplayName, string.Format("User ({0})", u.SourceType), u.UserName, u.Email }, u.Enabled ? _userImageIndex : _userDisabledImageIndex)
                              {
                                  Tag = u
                              };
                // existing users from a different campaign
                var other = from s in results.OtherCampaignUsers
                            let u = s.MatchedUser
                            select new ListViewItem(new[] { u.DisplayName + "*", string.Format("EC{0} User", u.SourceElectionCycle), u.UserName, u.Email }, u.Enabled ? _userImageIndex : _userDisabledImageIndex)
                            {
                                Tag = s
                            };
                // eligible contacts
                var eligible = from e in results.EligibleContacts
                               let v = e.Value
                               select new ListViewItem(new[] { v.Name, AccountAnalysis.ParseEntityType(e.Key).ToString(), null, v.Email }, _userInactiveImageIndex)
                               {
                                   Name = e.Key,
                                   Tag = e.Value
                               };
                // ineligible contacts
                var ineligible = from s in results.IneligibleContacts
                                 select new ListViewItem(new[] { s.Entity.Name, "Ineligible Contact", null, s.Entity.Email }, _blockImageIndex)
                                 {
                                     Tag = s
                                 };
                items = eligible.Union(ineligible).Union(current).Union(other);
            }
            else if (node.Tag as string == GroupNodeTag)
            {
                // group leaf node selected, show groups
                headers = new[] { _nameColumnHeader, _usernameColumnHeader, _idColumnHeader, _emailColumnHeader };
                items = from u in CPSecurity.Provider.GetGroupMembers(node.Name)
                        let user = CPSecurity.Provider.GetUser(u)
                        where user != null && (_adminMode || user.SourceType != EntityType.Generic)
                        select new ListViewItem(new[] { user.DisplayName, user.UserName, user.Cid, user.Email }, user.Enabled ? _userImageIndex : _userDisabledImageIndex)
                        {
                            Tag = user
                        };
            }
            else
            {
                // non-leaf nodes
                if (node == _candidatesNode)
                    headers = new[] { _nameColumnHeader, _typeColumnHeader, _idColumnHeader };
                else if (node == _electionCyclesNode || node == _groupsNode || node.Tag is Candidate || node.ImageIndex == _electionCyclesImageIndex)
                    headers = new[] { _nameColumnHeader, _typeColumnHeader };
                TreeNodeCollection children = node.Nodes;
                List<ListViewItem> treeNodeItems = new List<ListViewItem>();
                foreach (TreeNode child in children)
                {
                    ListViewItem item = null;
                    if (child.ImageIndex == _candidatesImageIndex && child.Tag is Candidate)
                    {
                        Candidate cand = child.Tag as Candidate;
                        item = new ListViewItem(new[] { cand.FormalName, "Candidate", cand.ID }, _candidatesImageIndex)
                        {
                            Name = cand.ID,
                            Tag = child.Tag
                        };
                    }
                    else if (child.ImageIndex == _electionCyclesImageIndex)
                    {
                        item = new ListViewItem(new[] { child.Name, "Election Cycle" }, _electionCyclesImageIndex)
                        {
                            Name = child.Name,
                            Tag = child.Tag
                        };
                    }
                    else if (node.ImageIndex == _groupsImageIndex)
                    {
                        item = new ListViewItem(new[] { child.Name, "Security Group" }, _groupsImageIndex)
                        {
                            Name = child.Name
                        };
                    }
                    if (item != null)
                        treeNodeItems.Add(item);
                }
                items = treeNodeItems;
            }
            return new ListViewContents() { Headers = headers, Items = items.OrderBy(i => i.Text).ToArray() };
        }

        /// <summary>
        /// Loads the child nodes of a parent node.
        /// </summary>
        /// <param name="node">The parent node whose children are to be loaded.</param>
        private void LoadChildren(TreeNode node, Action onLoaded = null)
        {
            if (node != null && node.FirstNode != null && string.IsNullOrWhiteSpace(node.FirstNode.Text))
            {
                this.Cursor = Cursors.WaitCursor;
                _treeView.BeginUpdate();
                _listView.BeginUpdate();
                _treeView.Enabled = _listView.Enabled = false;
                var caller = new Func<TreeNode, TreeNode[]>(GetChildNodes);
                caller.BeginInvoke(node, (IAsyncResult ar) =>
                {
                    var childNodes = (ar.AsyncState as Func<TreeNode, TreeNode[]>).EndInvoke(ar);
                    this.Invoke(new MethodInvoker(() =>
                    {
                        if (childNodes != null)
                        {
                            node.Nodes.Clear();
                            node.Nodes.AddRange(childNodes);
                        }
                        _listView.EndUpdate();
                        _treeView.EndUpdate();
                        _treeView.Enabled = _listView.Enabled = true;
                        this.Cursor = Cursors.Default;
                        if (onLoaded != null)
                            onLoaded();
                    }));
                }, caller);
            }
            else if (onLoaded != null)
            {
                onLoaded();
            }
        }

        /// <summary>
        /// Retrieves the child nodes of a specific <see cref="TreeNode"/>.
        /// </summary>
        /// <param name="node">The parent <see cref="TreeNode"/>.</param>
        /// <returns>An array of <see cref="TreeNode"/> objects that are the children of <paramref name="node"/>.</returns>
        private TreeNode[] GetChildNodes(TreeNode node)
        {
            IEnumerable<TreeNode> childNodes = null;
            if (node == _candidatesNode && !_candidatesLoaded)
            {
                // candidates
                childNodes = from c in Cfb.CandidatePortal.Cfis.GetCandidates().Values
                             let cNode = CreateCandidateNode(c)
                             where cNode.Nodes.Count > 0
                             orderby c.FormalName
                             select cNode;
                _candidatesLoaded = true;
            }
            else if (node == _electionCyclesNode)
            {
                if (!_electionsLoaded)
                {
                    // election cycles
                    childNodes = from e in Elections.GetElections().Values
                                 orderby e.Cycle
                                 select CreateElectionCycleNode(e.Cycle);
                    _electionsLoaded = true;
                }
            }
            else if (node == _groupsNode && !_groupsLoaded)
            {
                // security groups
                childNodes = from g in CPSecurity.Provider.GetGroups()
                             select CreateGroupNode(g);
                _groupsLoaded = true;
            }
            else if (node.Tag is ActiveCandidate)
            {
                // active candidate -- do nothing
            }
            else if (node.Tag is Candidate)
            {
                // active election cycles
                childNodes = from e in Elections.GetActiveElectionCycles(node.Name)
                             orderby e
                             select CreateElectionCycleNode(e, node.Tag as Candidate);
            }
            else if (node.ImageIndex == _electionCyclesImageIndex)
            {
                // active candidates
                childNodes = from c in ActiveCandidate.GetActiveCandidates(node.Name).Values
                             orderby c.FormalName
                             select CreateCandidateNode(c);
            }
            return childNodes == null ? null : childNodes.ToArray();
        }

        private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadChildren(e.Node, () =>
            {
                ShowChildren(e.Node);
                _listView.Tag = e.Node.Tag;
            });
        }

        private void _refreshToolStripButton_Click(object sender, EventArgs e)
        {
            Refresh(sender);
        }

        private void _treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            LoadChildren(e.Node);
        }

        private void _listView_ItemActivate(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ListViewItem item = this.SelectedItem;
                if (item == null)
                    return;
                ActiveCandidate ac = null;
                if (_treeView.SelectedNode != null)
                    ac = _treeView.SelectedNode.Tag as ActiveCandidate;
                var user = this.SelectedUser;
                if (user != null || item.Text.EndsWith("*"))
                {
                    var status = this.SelectedIneligibilityStatus;
                    if (status != null)
                    {
                        // other cycle user, prompt for open or update
                        user = status.MatchedUser;
                        if (user != null)
                        {
                            if (MessageBox.Show(this, string.Format("{0} already has an account from EC{1}. Would you like to associate this account with the EC{2} data before opening it?", user.DisplayName, user.SourceElectionCycle, ac.ElectionCycle), "Update Account?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            {
                                // remap account
                                Entity entity = status.Entity;
                                string contactID = status.ContactID;
                                if (entity != null && contactID != null)
                                {
                                    char? sourceCommitteeID = AccountAnalysis.ParseCommitteeID(contactID);
                                    byte? sourceLiaisonID = AccountAnalysis.ParseLiaisonID(contactID);
                                    if (sourceCommitteeID.HasValue)
                                        user.SourceCommitteeID = sourceCommitteeID;
                                    if (sourceLiaisonID.HasValue)
                                        user.SourceLiaisonID = sourceLiaisonID;
                                    if (ac != null)
                                        user.SourceElectionCycle = ac.ElectionCycle;
                                    user.SourceType = entity.Type;
                                    user.ElectionCycles.Add(ac.ElectionCycle);
                                }
                                if (!user.Save())
                                {
                                    MessageBox.Show(this, string.Format("An error occurred attempting to update {0}'s account. Please try again, or contact an administrator for further assistance.", user.DisplayName), "Account Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    return;
                                }
                                _refreshToolStripButton.PerformClick();
                            }
                        }
                    }
                    if (user != null)
                    {
                        // show existing user account properties
                        UserAccountForm.ShowExistingAccountForm(user, this.MdiParent);
                    }
                }
                else
                {
                    // create new user
                    if (ac != null)
                    {
                        Entity entity = this.SelectedEntity;
                        if (entity != null)
                        {
                            UserAccountForm.CreateNewAccountForm(entity, ac.ID, ac.ElectionCycle, item.Name).ShowAsOwnedBy(this.MdiParent);
                            _refreshToolStripButton.PerformClick();
                        }
                        else
                        {
                            var status = this.SelectedIneligibilityStatus;
                            if (status != null)
                            {
                                entity = status.Entity;
                                MessageBox.Show(this, string.Format("A C-Access user account cannot be created for {0} for the following reason:\n\n{1}", entity != null ? entity.Name : this.SelectedItem.Name, status.Status.GetDescription()), "Ineligible Contact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        // does not match a leaf node, so expand tree
                        lock (_listViewClickedLock)
                        {
                            _listViewClicked = true;
                        }
                        if (!_treeView.SelectedNode.IsExpanded)
                            _treeView.SelectedNode.Expand();
                        var nodes = _treeView.SelectedNode.Nodes.Find(item.Name, false);
                        if (nodes.Length > 0 && nodes[0].TreeView != null)
                        {
                            nodes[0].EnsureVisible();
                            _treeView.SelectedNode = nodes[0];
                        }
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void _newAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveCandidate ac = _treeView.SelectedNode.Tag as ActiveCandidate;
            if (ac != null)
                UserAccountForm.CreateNewAccountForm(null, ac.ID, ac.ElectionCycle, null).ShowAsOwnedBy(this);
        }

        private void _listContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // selectively show context menu options
            bool isEntity = this.SelectedEntity != null;
            bool isUser = this.SelectedUser != null;
            bool isIneligible = this.SelectedIneligibilityStatus != null;
            bool hasSelection = _listView.SelectedItems.Count > 0;
            _createAccountToolStripMenuItem.Available = isEntity || isIneligible;
            _deleteToolStripMenuItem.Available = isUser && _adminMode;
            _propertiesToolStripMenuItem.Available = isUser;
            _newAccountToolStripMenuItem.Available = _newAccountToolStripButton.Available = _adminMode && !hasSelection && _listView.Tag is ActiveCandidate;
            _refreshToolStripMenuItem.Visible = !hasSelection;
            _listContextMenuStripSeparator.Available = _deleteToolStripMenuItem.Available || _newAccountToolStripMenuItem.Available;
            foreach (ToolStripItem i in _listContextMenuStrip.Items)
            {
                if (i.Available)
                {
                    e.Cancel = false;
                    return;
                }
            }
            e.Cancel = true; // cancel if no items are visible
        }

        private void _deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var user = this.SelectedUser;
            if (user != null)
            {
                if (MessageBox.Show(this, string.Format("Are you sure you want to delete the user {0}? This action cannot be undone.", user.DisplayName), "Confirm User Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes
                    && !CPSecurity.Provider.DeleteUser(user.UserName)
                    && MessageBox.Show(this, string.Format("Unable to delete user {0}. Retry?", user.UserName), "Deletion Failure", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    _deleteToolStripMenuItem_Click(sender, e);
                this._refreshToolStripButton_Click(sender, e);
            }
        }

        private void _listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // selectively show toolbar buttons
            bool isEntity = this.SelectedEntity != null;
            bool isUser = this.SelectedUser != null;
            bool isIneligible = this.SelectedIneligibilityStatus != null;
            _createAccountToolStripButton.Available = isEntity || isIneligible;
            _deleteToolStripButton.Available = isUser && _adminMode;
            _propertiesToolStripButton.Available = isUser;
            _newAccountToolStripButton.Visible = _adminMode;
            _listToolStripSeparator.Available = _deleteToolStripButton.Available || _newAccountToolStripButton.Available;
        }

        /// <summary>
        /// Represents a set of data that is to be used in defining the contents of a <see cref="ListView"/> control.
        /// </summary>
        private struct ListViewContents
        {
            /// <summary>
            /// Gets or sets the list headers.
            /// </summary>
            public ColumnHeader[] Headers;

            /// <summary>
            /// Gets or sets the list items.
            /// </summary>
            public ListViewItem[] Items;
        }
    }

    /// <summary>
    /// Account explorer related extensions.
    /// </summary>
    internal static class AccountExplorer_Extensions
    {
        /// <summary>
        /// Refreshes all open AccountExplorer instances within an MDI parent.
        /// </summary>
        /// <param name="parent">The MDI parent form to use.</param>
        internal static void RefreshAccountExplorers(this CampMdiParent parent)
        {
            if (parent != null)
            {
                foreach (var form in parent.MdiChildren)
                {
                    if (form is AccountExplorer)
                        ((AccountExplorer)form).Refresh(parent);
                }
            }
        }
    }
}
