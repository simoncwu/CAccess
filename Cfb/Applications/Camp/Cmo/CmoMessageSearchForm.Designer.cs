namespace Cfb.Camp.Cmo
{
	partial class CmoMessageSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CmoMessageSearchForm));
            this._idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._electionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._candidateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._creatorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._subjectColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._postedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._openedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._attachmentColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._reportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._postedEndDatePicker = new System.Windows.Forms.DateTimePicker();
            this._postedStartDatePicker = new System.Windows.Forms.DateTimePicker();
            this._bodyTextBox = new System.Windows.Forms.TextBox();
            this._subjectTextBox = new System.Windows.Forms.TextBox();
            this._cfbEmployeePicker = new Cfb.Camp.Forms.CfbEmployeePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._electionLabel = new System.Windows.Forms.Label();
            this._creatorLabel = new System.Windows.Forms.Label();
            this._electionPicker = new Cfb.Camp.Forms.ElectionPicker();
            this._candidateIdLabel = new System.Windows.Forms.Label();
            this._postedCheckBox = new System.Windows.Forms.CheckBox();
            this._categoryComboBox = new Cfb.Camp.Forms.CmoCategoryComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this._candidatePicker = new Cfb.Camp.Forms.CandidatePicker();
            this._attachmentsCheckBox = new System.Windows.Forms.CheckBox();
            this._openedCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.resultsContextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.Image = global::Cfb.Camp.Cmo.Properties.Resources.Search_16x16;
            this.SearchButton.Location = new System.Drawing.Point(515, 126);
            this.SearchButton.TabIndex = 11;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this._openedCheckBox);
            this.SearchPanel.Controls.Add(this._attachmentsCheckBox);
            this.SearchPanel.Controls.Add(this._candidatePicker);
            this.SearchPanel.Controls.Add(this._categoryComboBox);
            this.SearchPanel.Controls.Add(this._postedCheckBox);
            this.SearchPanel.Controls.Add(this._postedStartDatePicker);
            this.SearchPanel.Controls.Add(this._postedEndDatePicker);
            this.SearchPanel.Controls.Add(this._bodyTextBox);
            this.SearchPanel.Controls.Add(this._subjectTextBox);
            this.SearchPanel.Controls.Add(this.label3);
            this.SearchPanel.Controls.Add(this.label2);
            this.SearchPanel.Controls.Add(this.label5);
            this.SearchPanel.Controls.Add(this.label1);
            this.SearchPanel.Controls.Add(this._electionLabel);
            this.SearchPanel.Controls.Add(this._creatorLabel);
            this.SearchPanel.Controls.Add(this._electionPicker);
            this.SearchPanel.Controls.Add(this._candidateIdLabel);
            this.SearchPanel.Controls.Add(this._cfbEmployeePicker);
            this.SearchPanel.Controls.Add(this.menuStrip);
            this.SearchPanel.Size = new System.Drawing.Size(604, 166);
            this.SearchPanel.Controls.SetChildIndex(this.menuStrip, 0);
            this.SearchPanel.Controls.SetChildIndex(this._cfbEmployeePicker, 0);
            this.SearchPanel.Controls.SetChildIndex(this._candidateIdLabel, 0);
            this.SearchPanel.Controls.SetChildIndex(this._electionPicker, 0);
            this.SearchPanel.Controls.SetChildIndex(this._creatorLabel, 0);
            this.SearchPanel.Controls.SetChildIndex(this._electionLabel, 0);
            this.SearchPanel.Controls.SetChildIndex(this.label1, 0);
            this.SearchPanel.Controls.SetChildIndex(this.label5, 0);
            this.SearchPanel.Controls.SetChildIndex(this.label2, 0);
            this.SearchPanel.Controls.SetChildIndex(this.label3, 0);
            this.SearchPanel.Controls.SetChildIndex(this._subjectTextBox, 0);
            this.SearchPanel.Controls.SetChildIndex(this._bodyTextBox, 0);
            this.SearchPanel.Controls.SetChildIndex(this._postedEndDatePicker, 0);
            this.SearchPanel.Controls.SetChildIndex(this._postedStartDatePicker, 0);
            this.SearchPanel.Controls.SetChildIndex(this._postedCheckBox, 0);
            this.SearchPanel.Controls.SetChildIndex(this._categoryComboBox, 0);
            this.SearchPanel.Controls.SetChildIndex(this._candidatePicker, 0);
            this.SearchPanel.Controls.SetChildIndex(this.SearchButton, 0);
            this.SearchPanel.Controls.SetChildIndex(this.ResetButton, 0);
            this.SearchPanel.Controls.SetChildIndex(this._attachmentsCheckBox, 0);
            this.SearchPanel.Controls.SetChildIndex(this._openedCheckBox, 0);
            // 
            // ResultsListView
            // 
            this.ResultsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._idColumnHeader,
            this._postedColumnHeader,
            this._electionColumnHeader,
            this._candidateColumnHeader,
            this._subjectColumnHeader,
            this._creatorColumnHeader,
            this._openedColumnHeader,
            this._attachmentColumnHeader});
            this.ResultsListView.ContextMenuStrip = this.resultsContextMenuStrip;
            this.ResultsListView.Location = new System.Drawing.Point(0, 166);
            this.ResultsListView.Size = new System.Drawing.Size(604, 0);
            this.ResultsListView.SmallImageList = this.imageList;
            this.ResultsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ResultsListView_ItemChecked);
            this.ResultsListView.VisibleChanged += new System.EventHandler(this.ResultsListView_VisibleChanged);
            this.ResultsListView.DoubleClick += new System.EventHandler(this.ResultsListView_DoubleClick);
            // 
            // SearchBackgroundWorker
            // 
            this.SearchBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SearchBackgroundWorker_DoWork);
            this.SearchBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SearchBackgroundWorker_ProgressChanged);
            this.SearchBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchBackgroundWorker_RunWorkerCompleted);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(448, 126);
            this.ResetButton.TabIndex = 12;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // _idColumnHeader
            // 
            this._idColumnHeader.Text = "         Message ID";
            this._idColumnHeader.Width = 120;
            // 
            // _electionColumnHeader
            // 
            this._electionColumnHeader.Text = "Election";
            // 
            // _candidateColumnHeader
            // 
            this._candidateColumnHeader.Text = "Candidate";
            this._candidateColumnHeader.Width = 150;
            // 
            // _creatorColumnHeader
            // 
            this._creatorColumnHeader.Text = "Created By";
            this._creatorColumnHeader.Width = 150;
            // 
            // _subjectColumnHeader
            // 
            this._subjectColumnHeader.Text = "Subject";
            this._subjectColumnHeader.Width = 180;
            // 
            // _postedColumnHeader
            // 
            this._postedColumnHeader.Text = "Posted On";
            this._postedColumnHeader.Width = 120;
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.checkToolStripMenuItem.Text = "&Check";
            this.checkToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
            // 
            // uncheckToolStripMenuItem
            // 
            this.uncheckToolStripMenuItem.Name = "uncheckToolStripMenuItem";
            this.uncheckToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.uncheckToolStripMenuItem.Text = "&Uncheck";
            this.uncheckToolStripMenuItem.Click += new System.EventHandler(this.uncheckToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(10, 10);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.Visible = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printMessageToolStripMenuItem,
            this.openMessageToolStripMenuItem,
            this.toolStripSeparator2});
            this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.FileMenuDropDownOpening);
            // 
            // printMessageToolStripMenuItem
            // 
            this.printMessageToolStripMenuItem.Image = global::Cfb.Camp.Cmo.Properties.Resources.Print_16x16;
            this.printMessageToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.printMessageToolStripMenuItem.MergeIndex = 4;
            this.printMessageToolStripMenuItem.Name = "printMessageToolStripMenuItem";
            this.printMessageToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.printMessageToolStripMenuItem.Text = "&Print Selected Messages";
            this.printMessageToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // openMessageToolStripMenuItem
            // 
            this.openMessageToolStripMenuItem.Image = global::Cfb.Camp.Cmo.Properties.Resources.Open;
            this.openMessageToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.openMessageToolStripMenuItem.MergeIndex = 4;
            this.openMessageToolStripMenuItem.Name = "openMessageToolStripMenuItem";
            this.openMessageToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.openMessageToolStripMenuItem.Text = "Open Selected &Messages";
            this.openMessageToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator2.MergeIndex = 4;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkSelectedToolStripMenuItem,
            this.uncheckSelectedToolStripMenuItem,
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.invertSelectionToolStripMenuItem});
            this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.editToolStripMenuItem.MergeIndex = 1;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
            // 
            // checkSelectedToolStripMenuItem
            // 
            this.checkSelectedToolStripMenuItem.Name = "checkSelectedToolStripMenuItem";
            this.checkSelectedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.checkSelectedToolStripMenuItem.Text = "&Check Selected";
            this.checkSelectedToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
            // 
            // uncheckSelectedToolStripMenuItem
            // 
            this.uncheckSelectedToolStripMenuItem.Name = "uncheckSelectedToolStripMenuItem";
            this.uncheckSelectedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.uncheckSelectedToolStripMenuItem.Text = "&Uncheck Selected";
            this.uncheckSelectedToolStripMenuItem.Click += new System.EventHandler(this.uncheckToolStripMenuItem_Click);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.checkAllToolStripMenuItem.Text = "Check &All";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.uncheckAllToolStripMenuItem.Text = "Uncheck A&ll";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.selectAllToolStripMenuItem.Text = "&Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // invertSelectionToolStripMenuItem
            // 
            this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.invertSelectionToolStripMenuItem.Text = "&Invert Selection";
            this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
            // 
            // checkAllToolStripMenuItem1
            // 
            this.checkAllToolStripMenuItem1.Name = "checkAllToolStripMenuItem1";
            this.checkAllToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.checkAllToolStripMenuItem1.Text = "&Check All";
            this.checkAllToolStripMenuItem1.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // uncheckAllToolStripMenuItem1
            // 
            this.uncheckAllToolStripMenuItem1.Name = "uncheckAllToolStripMenuItem1";
            this.uncheckAllToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.uncheckAllToolStripMenuItem1.Text = "&Uncheck All";
            this.uncheckAllToolStripMenuItem1.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // selectAllToolStripMenuItem1
            // 
            this.selectAllToolStripMenuItem1.Name = "selectAllToolStripMenuItem1";
            this.selectAllToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.selectAllToolStripMenuItem1.Text = "&Select All";
            this.selectAllToolStripMenuItem1.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Cfb.Camp.Cmo.Properties.Resources.Open;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::Cfb.Camp.Cmo.Properties.Resources.Print_16x16;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // resultsContextMenuStrip
            // 
            this.resultsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.printToolStripMenuItem,
            this.checkAllToolStripMenuItem1,
            this.uncheckAllToolStripMenuItem1,
            this.toolStripSeparator3,
            this.checkToolStripMenuItem,
            this.uncheckToolStripMenuItem,
            this.selectAllToolStripMenuItem1});
            this.resultsContextMenuStrip.Name = "resultsContextMenuStrip";
            this.resultsContextMenuStrip.Size = new System.Drawing.Size(138, 164);
            this.resultsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.resultsContextMenuStrip_Opening);
            // 
            // _openedColumnHeader
            // 
            this._openedColumnHeader.Text = "Opened On";
            this._openedColumnHeader.Width = 120;
            // 
            // _attachmentColumnHeader
            // 
            this._attachmentColumnHeader.Text = "";
            this._attachmentColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._attachmentColumnHeader.Width = 32;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Paperclip.png");
            this.imageList.Images.SetKeyName(1, "Unread_16x16.png");
            this.imageList.Images.SetKeyName(2, "ReadEmail_16x16.png");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator4.MergeIndex = 4;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // _reportToolStripButton
            // 
            this._reportToolStripButton.Image = global::Cfb.Camp.Cmo.Properties.Resources.Report_16x16;
            this._reportToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this._reportToolStripButton.MergeIndex = 4;
            this._reportToolStripButton.Name = "_reportToolStripButton";
            this._reportToolStripButton.Size = new System.Drawing.Size(86, 22);
            this._reportToolStripButton.Text = "&Run Report";
            this._reportToolStripButton.ToolTipText = "Generate Report for Checked Messages";
            this._reportToolStripButton.Click += new System.EventHandler(this.reportToolStripButton_Click);
            // 
            // _postedEndDatePicker
            // 
            this._postedEndDatePicker.Enabled = false;
            this._postedEndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._postedEndDatePicker.Location = new System.Drawing.Point(261, 126);
            this._postedEndDatePicker.MinDate = new System.DateTime(2009, 1, 1, 0, 0, 0, 0);
            this._postedEndDatePicker.Name = "_postedEndDatePicker";
            this._postedEndDatePicker.Size = new System.Drawing.Size(95, 23);
            this._postedEndDatePicker.TabIndex = 10;
            this._postedEndDatePicker.ValueChanged += new System.EventHandler(this._postedEndDatePicker_ValueChanged);
            // 
            // _postedStartDatePicker
            // 
            this._postedStartDatePicker.Enabled = false;
            this._postedStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._postedStartDatePicker.Location = new System.Drawing.Point(127, 126);
            this._postedStartDatePicker.MinDate = new System.DateTime(2009, 1, 1, 0, 0, 0, 0);
            this._postedStartDatePicker.Name = "_postedStartDatePicker";
            this._postedStartDatePicker.Size = new System.Drawing.Size(95, 23);
            this._postedStartDatePicker.TabIndex = 9;
            this._postedStartDatePicker.ValueChanged += new System.EventHandler(this._postedStartDatePicker_ValueChanged);
            // 
            // _bodyTextBox
            // 
            this._bodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._bodyTextBox.Location = new System.Drawing.Point(71, 97);
            this._bodyTextBox.Name = "_bodyTextBox";
            this._bodyTextBox.Size = new System.Drawing.Size(520, 23);
            this._bodyTextBox.TabIndex = 7;
            // 
            // _subjectTextBox
            // 
            this._subjectTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subjectTextBox.Location = new System.Drawing.Point(71, 68);
            this._subjectTextBox.Name = "_subjectTextBox";
            this._subjectTextBox.Size = new System.Drawing.Size(520, 23);
            this._subjectTextBox.TabIndex = 6;
            // 
            // _cfbEmployeePicker
            // 
            this._cfbEmployeePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cfbEmployeePicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._cfbEmployeePicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._cfbEmployeePicker.DisplayMember = "Value";
            this._cfbEmployeePicker.EmptySelectionText = "(any CFB staff)";
            this._cfbEmployeePicker.Location = new System.Drawing.Point(422, 10);
            this._cfbEmployeePicker.MaxDropDownItems = 16;
            this._cfbEmployeePicker.MinimumSize = new System.Drawing.Size(82, 0);
            this._cfbEmployeePicker.Name = "_cfbEmployeePicker";
            this._cfbEmployeePicker.SelectedEmployee = null;
            this._cfbEmployeePicker.Size = new System.Drawing.Size(169, 23);
            this._cfbEmployeePicker.TabIndex = 2;
            this._cfbEmployeePicker.Text = "(any CFB staff)";
            this._cfbEmployeePicker.ValueMember = "Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Body";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Subject";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "and";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Category";
            // 
            // _electionLabel
            // 
            this._electionLabel.AutoSize = true;
            this._electionLabel.Location = new System.Drawing.Point(11, 13);
            this._electionLabel.Name = "_electionLabel";
            this._electionLabel.Size = new System.Drawing.Size(49, 15);
            this._electionLabel.TabIndex = 22;
            this._electionLabel.Text = "Election";
            // 
            // _creatorLabel
            // 
            this._creatorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._creatorLabel.AutoSize = true;
            this._creatorLabel.Location = new System.Drawing.Point(381, 13);
            this._creatorLabel.Name = "_creatorLabel";
            this._creatorLabel.Size = new System.Drawing.Size(35, 15);
            this._creatorLabel.TabIndex = 28;
            this._creatorLabel.Text = "From";
            // 
            // _electionPicker
            // 
            this._electionPicker.AllElectionsOption = false;
            this._electionPicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._electionPicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._electionPicker.EmptySelectionText = "(any)";
            this._electionPicker.Location = new System.Drawing.Point(71, 10);
            this._electionPicker.MaxDropDownItems = 16;
            this._electionPicker.MinimumSize = new System.Drawing.Size(60, 0);
            this._electionPicker.Name = "_electionPicker";
            this._electionPicker.SelectedElection = null;
            this._electionPicker.Size = new System.Drawing.Size(60, 23);
            this._electionPicker.TabIndex = 0;
            // 
            // _candidateIdLabel
            // 
            this._candidateIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._candidateIdLabel.AutoSize = true;
            this._candidateIdLabel.Location = new System.Drawing.Point(137, 13);
            this._candidateIdLabel.Name = "_candidateIdLabel";
            this._candidateIdLabel.Size = new System.Drawing.Size(21, 15);
            this._candidateIdLabel.TabIndex = 25;
            this._candidateIdLabel.Text = "To";
            // 
            // _postedCheckBox
            // 
            this._postedCheckBox.AutoSize = true;
            this._postedCheckBox.Location = new System.Drawing.Point(14, 128);
            this._postedCheckBox.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this._postedCheckBox.Name = "_postedCheckBox";
            this._postedCheckBox.Size = new System.Drawing.Size(110, 19);
            this._postedCheckBox.TabIndex = 8;
            this._postedCheckBox.Text = "Posted Between";
            this._postedCheckBox.UseVisualStyleBackColor = true;
            this._postedCheckBox.CheckedChanged += new System.EventHandler(this._postedCheckBox_CheckedChanged);
            // 
            // _categoryComboBox
            // 
            this._categoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._categoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._categoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._categoryComboBox.DisplayMember = "Name";
            this._categoryComboBox.EmptySelectionText = "(any category)";
            this._categoryComboBox.Location = new System.Drawing.Point(71, 39);
            this._categoryComboBox.MaxDropDownItems = 16;
            this._categoryComboBox.MinimumSize = new System.Drawing.Size(212, 0);
            this._categoryComboBox.Name = "_categoryComboBox";
            this._categoryComboBox.SelectedCategory = null;
            this._categoryComboBox.ShowHidden = true;
            this._categoryComboBox.Size = new System.Drawing.Size(304, 23);
            this._categoryComboBox.TabIndex = 3;
            this._categoryComboBox.ValueMember = "ID";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this._reportToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(604, 25);
            this.toolStrip.TabIndex = 8;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Visible = false;
            // 
            // _candidatePicker
            // 
            this._candidatePicker.AllCandidatesOption = false;
            this._candidatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._candidatePicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._candidatePicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._candidatePicker.EmptySelectionText = "(any candidate)";
            this._candidatePicker.FormatString = "{0} ({1})";
            this._candidatePicker.FormattingEnabled = true;
            this._candidatePicker.Location = new System.Drawing.Point(164, 10);
            this._candidatePicker.MaxDropDownItems = 16;
            this._candidatePicker.Name = "_candidatePicker";
            this._candidatePicker.SelectedCid = null;
            this._candidatePicker.Size = new System.Drawing.Size(211, 23);
            this._candidatePicker.TabIndex = 1;
            // 
            // _attachmentsCheckBox
            // 
            this._attachmentsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._attachmentsCheckBox.AutoSize = true;
            this._attachmentsCheckBox.Checked = true;
            this._attachmentsCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this._attachmentsCheckBox.Location = new System.Drawing.Point(489, 41);
            this._attachmentsCheckBox.Name = "_attachmentsCheckBox";
            this._attachmentsCheckBox.Size = new System.Drawing.Size(112, 19);
            this._attachmentsCheckBox.TabIndex = 5;
            this._attachmentsCheckBox.Text = "Has Attachment";
            this._attachmentsCheckBox.ThreeState = true;
            this._attachmentsCheckBox.UseVisualStyleBackColor = true;
            // 
            // _openedCheckBox
            // 
            this._openedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._openedCheckBox.AutoSize = true;
            this._openedCheckBox.Checked = true;
            this._openedCheckBox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this._openedCheckBox.Location = new System.Drawing.Point(384, 41);
            this._openedCheckBox.Name = "_openedCheckBox";
            this._openedCheckBox.Size = new System.Drawing.Size(93, 19);
            this._openedCheckBox.TabIndex = 4;
            this._openedCheckBox.Text = "Was Opened";
            this._openedCheckBox.ThreeState = true;
            this._openedCheckBox.UseVisualStyleBackColor = true;
            // 
            // CmoMessageSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(604, 166);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 204);
            this.Name = "CmoMessageSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Find Messages";
            this.Load += new System.EventHandler(this.CmoMessageSearchForm_Load);
            this.Controls.SetChildIndex(this.toolStrip, 0);
            this.Controls.SetChildIndex(this.SearchPanel, 0);
            this.Controls.SetChildIndex(this.ResultsListView, 0);
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.resultsContextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ColumnHeader _idColumnHeader;
		private System.Windows.Forms.ColumnHeader _electionColumnHeader;
		private System.Windows.Forms.ColumnHeader _candidateColumnHeader;
		private System.Windows.Forms.ColumnHeader _creatorColumnHeader;
		private System.Windows.Forms.ColumnHeader _subjectColumnHeader;
		private System.Windows.Forms.ColumnHeader _postedColumnHeader;
		private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uncheckToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openMessageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printMessageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uncheckSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip resultsContextMenuStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ColumnHeader _openedColumnHeader;
		private System.Windows.Forms.ColumnHeader _attachmentColumnHeader;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.DateTimePicker _postedEndDatePicker;
		private System.Windows.Forms.DateTimePicker _postedStartDatePicker;
		private System.Windows.Forms.TextBox _bodyTextBox;
		private System.Windows.Forms.TextBox _subjectTextBox;
		private Forms.CfbEmployeePicker _cfbEmployeePicker;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label _electionLabel;
		private System.Windows.Forms.Label _creatorLabel;
        private Forms.ElectionPicker _electionPicker;
		private System.Windows.Forms.Label _candidateIdLabel;
		private System.Windows.Forms.CheckBox _postedCheckBox;
        private Forms.CmoCategoryComboBox _categoryComboBox;
		private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton _reportToolStripButton;
        private Forms.CandidatePicker _candidatePicker;
        private System.Windows.Forms.CheckBox _openedCheckBox;
        private System.Windows.Forms.CheckBox _attachmentsCheckBox;
	}
}
