namespace Cfb.Camp.Security
{
    partial class AccountExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Election Cycles", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Candidates", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Security Groups", 2, 2, new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountExplorer));
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this._listView = new Cfb.Camp.Forms.CfbListView();
            this._nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._usernameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._emailColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._listContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._listContextMenuStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._createAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._propertiesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._createAccountToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._listToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._newAccountToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._listContextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._treeView);
            this._splitContainer.Panel1MinSize = 175;
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._listView);
            this._splitContainer.Size = new System.Drawing.Size(808, 424);
            this._splitContainer.SplitterDistance = 220;
            this._splitContainer.TabIndex = 0;
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.HideSelection = false;
            this._treeView.ImageIndex = 0;
            this._treeView.ImageList = this.imageList;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Name = "_treeView";
            treeNode1.Name = "Node0";
            treeNode1.Text = "";
            treeNode2.ImageIndex = 0;
            treeNode2.Name = "Election Cycles";
            treeNode2.SelectedImageIndex = 0;
            treeNode2.Text = "Election Cycles";
            treeNode2.ToolTipText = "Browse Accounts by Election Cycle";
            treeNode3.Name = "Node1";
            treeNode3.Text = "";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Candidates";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "Candidates";
            treeNode4.ToolTipText = "Browse Accounts by Candidate";
            treeNode5.Name = "Node2";
            treeNode5.Text = "";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "Security Groups";
            treeNode6.SelectedImageIndex = 2;
            treeNode6.Text = "Security Groups";
            treeNode6.ToolTipText = "Browse Accounts by Security Group Membership";
            this._treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4,
            treeNode6});
            this._treeView.SelectedImageIndex = 0;
            this._treeView.Size = new System.Drawing.Size(220, 424);
            this._treeView.TabIndex = 0;
            this._treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this._treeView_BeforeExpand);
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Calendar_16x16.png");
            this.imageList.Images.SetKeyName(1, "Candidate_16x16.png");
            this.imageList.Images.SetKeyName(2, "Groups_16x16.png");
            this.imageList.Images.SetKeyName(3, "User_16x16.png");
            this.imageList.Images.SetKeyName(4, "UserDisabled_16x16.png");
            this.imageList.Images.SetKeyName(5, "Block_16x16.png");
            this.imageList.Images.SetKeyName(6, "UserInactive_16x16.png");
            // 
            // _listView
            // 
            this._listView.AllowColumnReorder = true;
            this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._nameColumnHeader,
            this._typeColumnHeader,
            this._usernameColumnHeader,
            this._idColumnHeader,
            this._emailColumnHeader});
            this._listView.ContextMenuStrip = this._listContextMenuStrip;
            this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listView.FullRowSelect = true;
            this._listView.HideSelection = false;
            this._listView.Location = new System.Drawing.Point(0, 0);
            this._listView.Margin = new System.Windows.Forms.Padding(0);
            this._listView.MultiSelect = false;
            this._listView.Name = "_listView";
            this._listView.Size = new System.Drawing.Size(584, 424);
            this._listView.SmallImageList = this.imageList;
            this._listView.SortColumn = 0;
            this._listView.SortOrder = System.Windows.Forms.SortOrder.None;
            this._listView.TabIndex = 0;
            this._listView.UseCompatibleStateImageBehavior = false;
            this._listView.View = System.Windows.Forms.View.Details;
            this._listView.ItemActivate += new System.EventHandler(this._listView_ItemActivate);
            this._listView.SelectedIndexChanged += new System.EventHandler(this._listView_SelectedIndexChanged);
            // 
            // _nameColumnHeader
            // 
            this._nameColumnHeader.Text = "Name";
            this._nameColumnHeader.Width = 170;
            // 
            // _typeColumnHeader
            // 
            this._typeColumnHeader.Text = "Type";
            this._typeColumnHeader.Width = 90;
            // 
            // _usernameColumnHeader
            // 
            this._usernameColumnHeader.Text = "Username";
            this._usernameColumnHeader.Width = 110;
            // 
            // _idColumnHeader
            // 
            this._idColumnHeader.Text = "ID";
            this._idColumnHeader.Width = 50;
            // 
            // _emailColumnHeader
            // 
            this._emailColumnHeader.Text = "Email";
            this._emailColumnHeader.Width = 200;
            // 
            // _listContextMenuStrip
            // 
            this._listContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newAccountToolStripMenuItem,
            this._deleteToolStripMenuItem,
            this._listContextMenuStripSeparator,
            this._createAccountToolStripMenuItem,
            this._propertiesToolStripMenuItem,
            this._refreshToolStripMenuItem});
            this._listContextMenuStrip.Name = "_listContextMenuStrip";
            this._listContextMenuStrip.Size = new System.Drawing.Size(161, 120);
            this._listContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this._listContextMenuStrip_Opening);
            // 
            // _newAccountToolStripMenuItem
            // 
            this._newAccountToolStripMenuItem.Image = global::Cfb.Camp.Security.Properties.Resources.CAccessUser_16x16;
            this._newAccountToolStripMenuItem.Name = "_newAccountToolStripMenuItem";
            this._newAccountToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._newAccountToolStripMenuItem.Text = "&New Account";
            this._newAccountToolStripMenuItem.Click += new System.EventHandler(this._newAccountToolStripMenuItem_Click);
            // 
            // _deleteToolStripMenuItem
            // 
            this._deleteToolStripMenuItem.Image = global::Cfb.Camp.Security.Properties.Resources.Delete_16x16;
            this._deleteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._deleteToolStripMenuItem.Name = "_deleteToolStripMenuItem";
            this._deleteToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._deleteToolStripMenuItem.Text = "&Delete";
            this._deleteToolStripMenuItem.Click += new System.EventHandler(this._deleteToolStripMenuItem_Click);
            // 
            // _listContextMenuStripSeparator
            // 
            this._listContextMenuStripSeparator.Name = "_listContextMenuStripSeparator";
            this._listContextMenuStripSeparator.Size = new System.Drawing.Size(157, 6);
            // 
            // _createAccountToolStripMenuItem
            // 
            this._createAccountToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._createAccountToolStripMenuItem.Image = global::Cfb.Camp.Security.Properties.Resources.User_16x16;
            this._createAccountToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._createAccountToolStripMenuItem.Name = "_createAccountToolStripMenuItem";
            this._createAccountToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._createAccountToolStripMenuItem.Text = "&Create Account";
            this._createAccountToolStripMenuItem.Click += new System.EventHandler(this._listView_ItemActivate);
            // 
            // _propertiesToolStripMenuItem
            // 
            this._propertiesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._propertiesToolStripMenuItem.Image = global::Cfb.Camp.Security.Properties.Resources.Contact_16x16;
            this._propertiesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._propertiesToolStripMenuItem.Name = "_propertiesToolStripMenuItem";
            this._propertiesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._propertiesToolStripMenuItem.Text = "&Properties";
            this._propertiesToolStripMenuItem.Click += new System.EventHandler(this._listView_ItemActivate);
            // 
            // _refreshToolStripMenuItem
            // 
            this._refreshToolStripMenuItem.Image = global::Cfb.Camp.Security.Properties.Resources.Refresh_16x16;
            this._refreshToolStripMenuItem.Name = "_refreshToolStripMenuItem";
            this._refreshToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this._refreshToolStripMenuItem.Text = "&Refresh";
            this._refreshToolStripMenuItem.Click += new System.EventHandler(this._refreshToolStripButton_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this._refreshToolStripButton,
            this._propertiesToolStripButton,
            this._createAccountToolStripButton,
            this._listToolStripSeparator,
            this._deleteToolStripButton,
            this._newAccountToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(808, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 4;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _refreshToolStripButton
            // 
            this._refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._refreshToolStripButton.Image = global::Cfb.Camp.Security.Properties.Resources.Refresh_16x16;
            this._refreshToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._refreshToolStripButton.MergeIndex = 4;
            this._refreshToolStripButton.Name = "_refreshToolStripButton";
            this._refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._refreshToolStripButton.Text = "Refresh";
            this._refreshToolStripButton.Click += new System.EventHandler(this._refreshToolStripButton_Click);
            // 
            // _propertiesToolStripButton
            // 
            this._propertiesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._propertiesToolStripButton.Image = global::Cfb.Camp.Security.Properties.Resources.Contact_16x16;
            this._propertiesToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._propertiesToolStripButton.MergeIndex = 4;
            this._propertiesToolStripButton.Name = "_propertiesToolStripButton";
            this._propertiesToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._propertiesToolStripButton.Text = "Properties";
            this._propertiesToolStripButton.Click += new System.EventHandler(this._listView_ItemActivate);
            // 
            // _createAccountToolStripButton
            // 
            this._createAccountToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._createAccountToolStripButton.Image = global::Cfb.Camp.Security.Properties.Resources.User_16x16;
            this._createAccountToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._createAccountToolStripButton.MergeIndex = 4;
            this._createAccountToolStripButton.Name = "_createAccountToolStripButton";
            this._createAccountToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._createAccountToolStripButton.Text = "Create Account";
            this._createAccountToolStripButton.Click += new System.EventHandler(this._listView_ItemActivate);
            // 
            // _listToolStripSeparator
            // 
            this._listToolStripSeparator.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._listToolStripSeparator.MergeIndex = 4;
            this._listToolStripSeparator.Name = "_listToolStripSeparator";
            this._listToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // _deleteToolStripButton
            // 
            this._deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._deleteToolStripButton.Image = global::Cfb.Camp.Security.Properties.Resources.Delete_16x16;
            this._deleteToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._deleteToolStripButton.MergeIndex = 4;
            this._deleteToolStripButton.Name = "_deleteToolStripButton";
            this._deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._deleteToolStripButton.Text = "Delete";
            this._deleteToolStripButton.Click += new System.EventHandler(this._deleteToolStripMenuItem_Click);
            // 
            // _newAccountToolStripButton
            // 
            this._newAccountToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._newAccountToolStripButton.Image = global::Cfb.Camp.Security.Properties.Resources.CAccessUser_16x16;
            this._newAccountToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._newAccountToolStripButton.MergeIndex = 4;
            this._newAccountToolStripButton.Name = "_newAccountToolStripButton";
            this._newAccountToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._newAccountToolStripButton.Text = "New Account";
            this._newAccountToolStripButton.Click += new System.EventHandler(this._newAccountToolStripMenuItem_Click);
            // 
            // AccountExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 424);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "AccountExplorer";
            this.Text = "C-Access Account Explorer";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._listContextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader _nameColumnHeader;
        private System.Windows.Forms.ColumnHeader _typeColumnHeader;
        private System.Windows.Forms.ColumnHeader _idColumnHeader;
        private System.Windows.Forms.ColumnHeader _emailColumnHeader;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton _refreshToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip _listContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _createAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator _listContextMenuStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem _newAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton _propertiesToolStripButton;
        private System.Windows.Forms.ToolStripButton _createAccountToolStripButton;
        private System.Windows.Forms.ToolStripButton _deleteToolStripButton;
        private System.Windows.Forms.ToolStripButton _newAccountToolStripButton;
        private System.Windows.Forms.ToolStripSeparator _listToolStripSeparator;
        private System.Windows.Forms.ColumnHeader _usernameColumnHeader;
        private Forms.CfbListView _listView;

    }
}