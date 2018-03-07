using System;
using System.Windows.Forms;

namespace Cfb.Camp
{
	partial class CampForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openedStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCandidateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.allAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.messagesToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newMessageToolStripDropDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMessageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.findMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.accountExplorerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.findAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAccountToolStripDropDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.reportsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.messagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.messageStatusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.byCandidateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCampaignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.toolsMenu,
            this.windowsMenu,
            this.securityToolStripMenuItem,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1066, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.findToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.toolStripSeparator3,
            this.printSetupToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.MergeIndex = 0;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            this.fileMenu.DropDownOpening += new System.EventHandler(this.fileMenu_DropDownOpening);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMessageToolStripMenuItem,
            this.newAccountToolStripMenuItem});
            this.newToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.DefaultDocument_16x16;
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.MergeIndex = 0;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // newMessageToolStripMenuItem
            // 
            this.newMessageToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.NewEmail_16x16;
            this.newMessageToolStripMenuItem.Name = "newMessageToolStripMenuItem";
            this.newMessageToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.newMessageToolStripMenuItem.Text = "&Message";
            this.newMessageToolStripMenuItem.Click += new System.EventHandler(this.CreateNewMessage);
            // 
            // newAccountToolStripMenuItem
            // 
            this.newAccountToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.User_16x16;
            this.newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
            this.newAccountToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.newAccountToolStripMenuItem.Text = "&Account";
            this.newAccountToolStripMenuItem.Click += new System.EventHandler(this.newAccountToolStripDropDownMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMessageToolStripMenuItem,
            this.accountExplorerToolStripMenuItem});
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.MergeIndex = 1;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // openMessageToolStripMenuItem
            // 
            this.openMessageToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Email_16x16;
            this.openMessageToolStripMenuItem.Name = "openMessageToolStripMenuItem";
            this.openMessageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openMessageToolStripMenuItem.Text = "&Message";
            this.openMessageToolStripMenuItem.Click += new System.EventHandler(this.OpenMessage);
            // 
            // accountExplorerToolStripMenuItem
            // 
            this.accountExplorerToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Directory_16x16;
            this.accountExplorerToolStripMenuItem.Name = "accountExplorerToolStripMenuItem";
            this.accountExplorerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.accountExplorerToolStripMenuItem.Text = "&Account Explorer";
            this.accountExplorerToolStripMenuItem.Click += new System.EventHandler(this.ShowAccountExplorer);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messagesToolStripMenuItem2,
            this.accountToolStripMenuItem1});
            this.findToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Search_16x16;
            this.findToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findToolStripMenuItem.MergeIndex = 2;
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.findToolStripMenuItem.Text = "&Find";
            // 
            // messagesToolStripMenuItem2
            // 
            this.messagesToolStripMenuItem2.Image = global::Cfb.Camp.Properties.Resources.SearchDocs_16x16;
            this.messagesToolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.messagesToolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.messagesToolStripMenuItem2.Name = "messagesToolStripMenuItem2";
            this.messagesToolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.messagesToolStripMenuItem2.Text = "&Messages";
            this.messagesToolStripMenuItem2.Click += new System.EventHandler(this.FindMessages);
            // 
            // accountToolStripMenuItem1
            // 
            this.accountToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.SearchUsers_16x16;
            this.accountToolStripMenuItem1.Name = "accountToolStripMenuItem1";
            this.accountToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.accountToolStripMenuItem1.Text = "&Accounts";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messagesToolStripMenuItem,
            this.accountsToolStripMenuItem1});
            this.reportsToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.MSReport_16x16;
            this.reportsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reportsToolStripMenuItem.MergeIndex = 3;
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.reportsToolStripMenuItem.Text = "&Reports";
            // 
            // messagesToolStripMenuItem
            // 
            this.messagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openedStatusToolStripMenuItem,
            this.byCandidateToolStripMenuItem});
            this.messagesToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Emails_16x16;
            this.messagesToolStripMenuItem.Name = "messagesToolStripMenuItem";
            this.messagesToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.messagesToolStripMenuItem.Text = "Posted &Messages";
            // 
            // openedStatusToolStripMenuItem
            // 
            this.openedStatusToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.openedStatusToolStripMenuItem.Name = "openedStatusToolStripMenuItem";
            this.openedStatusToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.openedStatusToolStripMenuItem.Text = "&Status Summary";
            this.openedStatusToolStripMenuItem.Click += new System.EventHandler(this.ReportMessageStatusSummary);
            // 
            // byCandidateToolStripMenuItem
            // 
            this.byCandidateToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.byCandidateToolStripMenuItem.Name = "byCandidateToolStripMenuItem";
            this.byCandidateToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.byCandidateToolStripMenuItem.Text = "Message &Details - Single Candidate";
            this.byCandidateToolStripMenuItem.Click += new System.EventHandler(this.ReportMessageDetailByCandidate);
            // 
            // accountsToolStripMenuItem1
            // 
            this.accountsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allAccountsToolStripMenuItem});
            this.accountsToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Groups_16x16;
            this.accountsToolStripMenuItem1.Name = "accountsToolStripMenuItem1";
            this.accountsToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.accountsToolStripMenuItem1.Text = "&Accounts";
            // 
            // allAccountsToolStripMenuItem
            // 
            this.allAccountsToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.allAccountsToolStripMenuItem.Name = "allAccountsToolStripMenuItem";
            this.allAccountsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.allAccountsToolStripMenuItem.Text = "&All Accounts";
            this.allAccountsToolStripMenuItem.Click += new System.EventHandler(this.ReportAllAccounts);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.MergeIndex = 4;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // printSetupToolStripMenuItem
            // 
            this.printSetupToolStripMenuItem.MergeIndex = 5;
            this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
            this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.printSetupToolStripMenuItem.Text = "Print Setup";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.MergeIndex = 6;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.MergeIndex = 7;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem});
            this.viewMenu.MergeIndex = 1;
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "&View";
            this.viewMenu.DropDownOpening += new System.EventHandler(this.viewMenu_DropDownOpening);
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsMenu.MergeIndex = 2;
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(48, 20);
            this.toolsMenu.Text = "&Tools";
            this.toolsMenu.DropDownOpening += new System.EventHandler(this.toolsMenu_DropDownOpening);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Options;
            this.optionsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.MergeIndex = 3;
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(68, 20);
            this.windowsMenu.Text = "&Windows";
            this.windowsMenu.DropDownOpening += new System.EventHandler(this.windowsMenu_DropDownOpening);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.closeAllToolStripMenuItem.Text = "C&lose All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.arrangeIconsToolStripMenuItem.Text = "&Arrange Icons";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupsToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.securityToolStripMenuItem.Text = "&Security";
            // 
            // groupsToolStripMenuItem
            // 
            this.groupsToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Groups_16x16;
            this.groupsToolStripMenuItem.Name = "groupsToolStripMenuItem";
            this.groupsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.groupsToolStripMenuItem.Text = "&Groups";
            this.groupsToolStripMenuItem.Click += new System.EventHandler(this.groupsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.MergeIndex = 4;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            this.helpMenu.DropDownOpening += new System.EventHandler(this.helpMenu_DropDownOpening);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.aboutToolStripMenuItem.Text = "&About CAMP";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // messagesToolStripDropDownButton
            // 
            this.messagesToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMessageToolStripDropDownMenuItem,
            this.openMessageToolStripMenuItem1,
            this.findMessagesToolStripMenuItem});
            this.messagesToolStripDropDownButton.Image = global::Cfb.Camp.Properties.Resources.Emails_16x16;
            this.messagesToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.messagesToolStripDropDownButton.Name = "messagesToolStripDropDownButton";
            this.messagesToolStripDropDownButton.Size = new System.Drawing.Size(87, 22);
            this.messagesToolStripDropDownButton.Text = "&Messages";
            // 
            // newMessageToolStripDropDownMenuItem
            // 
            this.newMessageToolStripDropDownMenuItem.Image = global::Cfb.Camp.Properties.Resources.NewEmail_16x16;
            this.newMessageToolStripDropDownMenuItem.Name = "newMessageToolStripDropDownMenuItem";
            this.newMessageToolStripDropDownMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newMessageToolStripDropDownMenuItem.Text = "&New Message";
            this.newMessageToolStripDropDownMenuItem.Click += new System.EventHandler(this.CreateNewMessage);
            // 
            // openMessageToolStripMenuItem1
            // 
            this.openMessageToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Email_16x16;
            this.openMessageToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Gray;
            this.openMessageToolStripMenuItem1.Name = "openMessageToolStripMenuItem1";
            this.openMessageToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.openMessageToolStripMenuItem1.Text = "&Open Message";
            this.openMessageToolStripMenuItem1.Click += new System.EventHandler(this.OpenMessage);
            // 
            // findMessagesToolStripMenuItem
            // 
            this.findMessagesToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.SearchDocs_16x16;
            this.findMessagesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.findMessagesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findMessagesToolStripMenuItem.Name = "findMessagesToolStripMenuItem";
            this.findMessagesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.findMessagesToolStripMenuItem.Text = "&Find Messages";
            this.findMessagesToolStripMenuItem.Click += new System.EventHandler(this.FindMessages);
            // 
            // accountsToolStripDropDownButton
            // 
            this.accountsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountExplorerToolStripMenuItem1,
            this.newAccountToolStripDropDownMenuItem,
            this.findAccountToolStripMenuItem});
            this.accountsToolStripDropDownButton.Image = global::Cfb.Camp.Properties.Resources.Groups_16x16;
            this.accountsToolStripDropDownButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.accountsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.accountsToolStripDropDownButton.Name = "accountsToolStripDropDownButton";
            this.accountsToolStripDropDownButton.Size = new System.Drawing.Size(86, 22);
            this.accountsToolStripDropDownButton.Text = "&Accounts";
            // 
            // accountExplorerToolStripMenuItem1
            // 
            this.accountExplorerToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Directory_16x16;
            this.accountExplorerToolStripMenuItem1.Name = "accountExplorerToolStripMenuItem1";
            this.accountExplorerToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.accountExplorerToolStripMenuItem1.Text = "Account &Explorer";
            this.accountExplorerToolStripMenuItem1.Click += new System.EventHandler(this.ShowAccountExplorer);
            // 
            // findAccountToolStripMenuItem
            // 
            this.findAccountToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.SearchUsers_16x16;
            this.findAccountToolStripMenuItem.Name = "findAccountToolStripMenuItem";
            this.findAccountToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.findAccountToolStripMenuItem.Text = "&Find Account";
            this.findAccountToolStripMenuItem.Click += new System.EventHandler(this.findAccountToolStripMenuItem_Click);
            // 
            // newAccountToolStripDropDownMenuItem
            // 
            this.newAccountToolStripDropDownMenuItem.Image = global::Cfb.Camp.Properties.Resources.User_16x16;
            this.newAccountToolStripDropDownMenuItem.Name = "newAccountToolStripDropDownMenuItem";
            this.newAccountToolStripDropDownMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newAccountToolStripDropDownMenuItem.Text = "&New Account";
            this.newAccountToolStripDropDownMenuItem.Visible = false;
            this.newAccountToolStripDropDownMenuItem.Click += new System.EventHandler(this.newAccountToolStripDropDownMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messagesToolStripDropDownButton,
            this.accountsToolStripDropDownButton,
            this.reportsToolStripDropDownButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1066, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // reportsToolStripDropDownButton
            // 
            this.reportsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messagesToolStripMenuItem1,
            this.accountsToolStripMenuItem});
            this.reportsToolStripDropDownButton.Image = global::Cfb.Camp.Properties.Resources.MSReport_16x16;
            this.reportsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reportsToolStripDropDownButton.Name = "reportsToolStripDropDownButton";
            this.reportsToolStripDropDownButton.Size = new System.Drawing.Size(76, 22);
            this.reportsToolStripDropDownButton.Text = "&Reports";
            this.reportsToolStripDropDownButton.ToolTipText = "Reports";
            // 
            // messagesToolStripMenuItem1
            // 
            this.messagesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageStatusToolStripMenuItem1,
            this.byCandidateToolStripMenuItem1});
            this.messagesToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Emails_16x16;
            this.messagesToolStripMenuItem1.Name = "messagesToolStripMenuItem1";
            this.messagesToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.messagesToolStripMenuItem1.Text = "Posted &Messages";
            // 
            // messageStatusToolStripMenuItem1
            // 
            this.messageStatusToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.messageStatusToolStripMenuItem1.Name = "messageStatusToolStripMenuItem1";
            this.messageStatusToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.messageStatusToolStripMenuItem1.Text = "&Status Summary";
            this.messageStatusToolStripMenuItem1.Click += new System.EventHandler(this.ReportMessageStatusSummary);
            // 
            // byCandidateToolStripMenuItem1
            // 
            this.byCandidateToolStripMenuItem1.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.byCandidateToolStripMenuItem1.Name = "byCandidateToolStripMenuItem1";
            this.byCandidateToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.byCandidateToolStripMenuItem1.Text = "&Details By Candidate";
            this.byCandidateToolStripMenuItem1.Click += new System.EventHandler(this.ReportMessageDetailByCandidate);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byCampaignToolStripMenuItem});
            this.accountsToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Groups_16x16;
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.accountsToolStripMenuItem.Text = "&Accounts";
            // 
            // byCampaignToolStripMenuItem
            // 
            this.byCampaignToolStripMenuItem.Image = global::Cfb.Camp.Properties.Resources.Report_16x16;
            this.byCampaignToolStripMenuItem.Name = "byCampaignToolStripMenuItem";
            this.byCampaignToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.byCampaignToolStripMenuItem.Text = "&All Accounts";
            this.byCampaignToolStripMenuItem.Click += new System.EventHandler(this.ReportAllAccounts);
            // 
            // CampForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 764);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "CampForm";
            this.Text = global::Cfb.Camp.Properties.Settings.Default.CampTitle;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CampMdiParent_FormClosing);
            this.Shown += new System.EventHandler(this.CampForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.CampForm_ResizeEnd);
            this.Move += new System.EventHandler(this.CampForm_Move);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewMenu;
		private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsMenu;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsMenu;
		private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpMenu;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ToolStripMenuItem newMessageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openMessageToolStripMenuItem;
		private ToolStripDropDownButton messagesToolStripDropDownButton;
		private ToolStripMenuItem newMessageToolStripDropDownMenuItem;
        private ToolStripDropDownButton accountsToolStripDropDownButton;
		protected ToolStrip toolStrip;
		private ToolStripMenuItem reportsToolStripMenuItem;
		private ToolStripMenuItem messagesToolStripMenuItem;
		private ToolStripMenuItem byCandidateToolStripMenuItem;
		private ToolStripDropDownButton reportsToolStripDropDownButton;
		private ToolStripMenuItem messagesToolStripMenuItem1;
		private ToolStripMenuItem byCandidateToolStripMenuItem1;
		private ToolStripMenuItem openedStatusToolStripMenuItem;
		private ToolStripMenuItem messageStatusToolStripMenuItem1;
		private ToolStripMenuItem securityToolStripMenuItem;
		private ToolStripMenuItem groupsToolStripMenuItem;
		private ToolStripMenuItem openMessageToolStripMenuItem1;
		private ToolStripMenuItem findMessagesToolStripMenuItem;
		private ToolStripMenuItem findToolStripMenuItem;
		private ToolStripMenuItem messagesToolStripMenuItem2;
		private ToolStripMenuItem findAccountToolStripMenuItem;
		private ToolStripMenuItem accountToolStripMenuItem1;
		private ToolStripMenuItem newAccountToolStripMenuItem;
		private ToolStripMenuItem accountExplorerToolStripMenuItem;
		private ToolStripMenuItem accountExplorerToolStripMenuItem1;
		private ToolStripMenuItem accountsToolStripMenuItem;
		private ToolStripMenuItem byCampaignToolStripMenuItem;
		private ToolStripMenuItem accountsToolStripMenuItem1;
		private ToolStripMenuItem allAccountsToolStripMenuItem;
        private ToolStripMenuItem newAccountToolStripDropDownMenuItem;
	}
}



