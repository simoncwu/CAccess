namespace Cfb.Camp.Security
{
	partial class GroupAdministrationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupAdministrationForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._groupListBox = new System.Windows.Forms.ListBox();
			this._membersButton = new System.Windows.Forms.ToolStripButton();
			this._refreshButton = new System.Windows.Forms.ToolStripButton();
			this._newButton = new System.Windows.Forms.ToolStripButton();
			this._renameButton = new System.Windows.Forms.ToolStripButton();
			this._deleteButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._membersButton,
            this._refreshButton,
            this.toolStripSeparator1,
            this._newButton,
            this._renameButton,
            this._deleteButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(344, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// _groupListBox
			// 
			this._groupListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._groupListBox.FormattingEnabled = true;
			this._groupListBox.HorizontalScrollbar = true;
			this._groupListBox.IntegralHeight = false;
			this._groupListBox.ItemHeight = 15;
			this._groupListBox.Location = new System.Drawing.Point(12, 28);
			this._groupListBox.MinimumSize = new System.Drawing.Size(150, 0);
			this._groupListBox.Name = "_groupListBox";
			this._groupListBox.Size = new System.Drawing.Size(320, 172);
			this._groupListBox.Sorted = true;
			this._groupListBox.TabIndex = 2;
			this._groupListBox.SelectedIndexChanged += new System.EventHandler(this._groupListBox_SelectedIndexChanged);
			this._groupListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._groupListBox_KeyDown);
			// 
			// _membersButton
			// 
			this._membersButton.Image = global::Cfb.Camp.Security.Properties.Resources.User_16x16;
			this._membersButton.Name = "_membersButton";
			this._membersButton.Size = new System.Drawing.Size(77, 22);
			this._membersButton.Text = "&Members";
			this._membersButton.Click += new System.EventHandler(this._membersButton_Click);
			// 
			// _refreshButton
			// 
			this._refreshButton.Image = global::Cfb.Camp.Security.Properties.Resources.Refresh_16x16;
			this._refreshButton.Name = "_refreshButton";
			this._refreshButton.Size = new System.Drawing.Size(66, 22);
			this._refreshButton.Text = "&Refresh";
			this._refreshButton.Click += new System.EventHandler(this._refreshButton_Click);
			// 
			// _newButton
			// 
			this._newButton.Image = global::Cfb.Camp.Security.Properties.Resources.Groups_16x16;
			this._newButton.Name = "_newButton";
			this._newButton.Size = new System.Drawing.Size(51, 22);
			this._newButton.Text = "&New";
			this._newButton.Click += new System.EventHandler(this._newButton_Click);
			// 
			// _renameButton
			// 
			this._renameButton.Image = global::Cfb.Camp.Security.Properties.Resources.Select_16x16;
			this._renameButton.Name = "_renameButton";
			this._renameButton.Size = new System.Drawing.Size(70, 22);
			this._renameButton.Text = "Re&name";
			this._renameButton.Click += new System.EventHandler(this._renameButton_Click);
			// 
			// _deleteButton
			// 
			this._deleteButton.Image = global::Cfb.Camp.Security.Properties.Resources.Delete_16x16;
			this._deleteButton.Name = "_deleteButton";
			this._deleteButton.Size = new System.Drawing.Size(60, 22);
			this._deleteButton.Text = "&Delete";
			this._deleteButton.Click += new System.EventHandler(this._deleteButton_Click);
			// 
			// GroupAdministrationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.ClientSize = new System.Drawing.Size(344, 212);
			this.Controls.Add(this._groupListBox);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(360, 250);
			this.Name = "GroupAdministrationForm";
			this.Text = "Security Groups";
			this.Load += new System.EventHandler(this.GroupAdministrationForm_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton _newButton;
		private System.Windows.Forms.ToolStripButton _deleteButton;
		private System.Windows.Forms.ToolStripButton _renameButton;
		private System.Windows.Forms.ListBox _groupListBox;
		private System.Windows.Forms.ToolStripButton _membersButton;
		private System.Windows.Forms.ToolStripButton _refreshButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
