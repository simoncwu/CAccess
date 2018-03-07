namespace Cfb.Camp.Security
{
    partial class UserAccountForm
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
            this._mainTabControl = new System.Windows.Forms.TabControl();
            this._generalTabPage = new System.Windows.Forms.TabPage();
            this._syncButton = new System.Windows.Forms.Button();
            this._unlockButton = new System.Windows.Forms.Button();
            this._lockoutTextBox = new System.Windows.Forms.TextBox();
            this._pwSetTextBox = new System.Windows.Forms.TextBox();
            this._resetButton = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this._createdTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this._userNameTextBox = new System.Windows.Forms.TextBox();
            this._loginTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this._emailTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._enableButton = new System.Windows.Forms.Button();
            this._securityTabPage = new System.Windows.Forms.TabPage();
            this._removeGroupButton = new System.Windows.Forms.Button();
            this._addGroupButton = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this._permissionsPanel = new System.Windows.Forms.Panel();
            this._permissionPictureBox = new System.Windows.Forms.PictureBox();
            this._permissionLabel = new System.Windows.Forms.Label();
            this._groupsListBox = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this._electionsTabPage = new System.Windows.Forms.TabPage();
            this._allCyclesCheckBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this._electionCandidateInfoPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._electionsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label15 = new System.Windows.Forms.Label();
            this._cfisTabPage = new System.Windows.Forms.TabPage();
            this._usernameButton = new System.Windows.Forms.Button();
            this._newUsernameTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this._miTextBox = new System.Windows.Forms.TextBox();
            this._newUsernamePlaceholder = new System.Windows.Forms.Label();
            this._newUsernameLabel = new System.Windows.Forms.Label();
            this._passwordLabel = new System.Windows.Forms.Label();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._cfisEmailTextBox = new System.Windows.Forms.TextBox();
            this._cfisDisplayNameTextBox = new System.Windows.Forms.TextBox();
            this._lastNameTextBox = new System.Windows.Forms.TextBox();
            this._committeeTextBox = new System.Windows.Forms.TextBox();
            this._ecTextBox = new System.Windows.Forms.TextBox();
            this._typeTextBox = new System.Windows.Forms.TextBox();
            this._firstNameTextBox = new System.Windows.Forms.TextBox();
            this._cfisEmailLabel = new System.Windows.Forms.Label();
            this._cfisDisplayNameLabel = new System.Windows.Forms.Label();
            this._lastNameLabel = new System.Windows.Forms.Label();
            this._miLabel = new System.Windows.Forms.Label();
            this._committeeLabel = new System.Windows.Forms.Label();
            this._ecLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._firstNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._applyButton = new System.Windows.Forms.Button();
            this._userImage = new System.Windows.Forms.PictureBox();
            this._passwordErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._emailErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._cfisEmailErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._usernameErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._electionsCandidateToolTip = new System.Windows.Forms.ToolTip(this.components);
            this._displayNameTextBox = new System.Windows.Forms.TextBox();
            this._candidatePicker = new Cfb.Camp.Forms.CandidatePicker();
            this._candidateTextBox = new System.Windows.Forms.TextBox();
            this._displaynamePlaceholder = new System.Windows.Forms.Label();
            this._mainTabControl.SuspendLayout();
            this._generalTabPage.SuspendLayout();
            this._securityTabPage.SuspendLayout();
            this._permissionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._permissionPictureBox)).BeginInit();
            this._electionsTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this._electionCandidateInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this._cfisTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._userImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._passwordErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._emailErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._cfisEmailErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._usernameErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainTabControl
            // 
            this._mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._mainTabControl.Controls.Add(this._generalTabPage);
            this._mainTabControl.Controls.Add(this._securityTabPage);
            this._mainTabControl.Controls.Add(this._electionsTabPage);
            this._mainTabControl.Controls.Add(this._cfisTabPage);
            this._mainTabControl.Location = new System.Drawing.Point(9, 76);
            this._mainTabControl.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._mainTabControl.MinimumSize = new System.Drawing.Size(316, 244);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            this._mainTabControl.Size = new System.Drawing.Size(326, 270);
            this._mainTabControl.TabIndex = 0;
            this._mainTabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this._mainTabControl_KeyDown);
            this._mainTabControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this._mainTabControl_KeyUp);
            // 
            // _generalTabPage
            // 
            this._generalTabPage.Controls.Add(this._syncButton);
            this._generalTabPage.Controls.Add(this._unlockButton);
            this._generalTabPage.Controls.Add(this._lockoutTextBox);
            this._generalTabPage.Controls.Add(this._pwSetTextBox);
            this._generalTabPage.Controls.Add(this._resetButton);
            this._generalTabPage.Controls.Add(this.label27);
            this._generalTabPage.Controls.Add(this.label17);
            this._generalTabPage.Controls.Add(this._createdTextBox);
            this._generalTabPage.Controls.Add(this.label28);
            this._generalTabPage.Controls.Add(this._userNameTextBox);
            this._generalTabPage.Controls.Add(this._loginTextBox);
            this._generalTabPage.Controls.Add(this.label9);
            this._generalTabPage.Controls.Add(this.label13);
            this._generalTabPage.Controls.Add(this._emailTextBox);
            this._generalTabPage.Controls.Add(this.label7);
            this._generalTabPage.Controls.Add(this._enableButton);
            this._generalTabPage.Location = new System.Drawing.Point(4, 24);
            this._generalTabPage.Name = "_generalTabPage";
            this._generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._generalTabPage.Size = new System.Drawing.Size(318, 242);
            this._generalTabPage.TabIndex = 1;
            this._generalTabPage.Text = "C-Access";
            this._generalTabPage.UseVisualStyleBackColor = true;
            // 
            // _syncButton
            // 
            this._syncButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._syncButton.Image = global::Cfb.Camp.Security.Properties.Resources.Sync_16x16;
            this._syncButton.Location = new System.Drawing.Point(42, 211);
            this._syncButton.Name = "_syncButton";
            this._syncButton.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this._syncButton.Size = new System.Drawing.Size(114, 25);
            this._syncButton.TabIndex = 8;
            this._syncButton.Text = "&Sync with CFIS";
            this._syncButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._syncButton.UseVisualStyleBackColor = true;
            this._syncButton.Visible = false;
            this._syncButton.Click += new System.EventHandler(this._syncButton_Click);
            // 
            // _unlockButton
            // 
            this._unlockButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._unlockButton.Image = global::Cfb.Camp.Security.Properties.Resources.Lock_16x16;
            this._unlockButton.Location = new System.Drawing.Point(42, 180);
            this._unlockButton.Name = "_unlockButton";
            this._unlockButton.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this._unlockButton.Size = new System.Drawing.Size(114, 25);
            this._unlockButton.TabIndex = 6;
            this._unlockButton.Text = "&Unlock User";
            this._unlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._unlockButton.UseVisualStyleBackColor = true;
            this._unlockButton.Click += new System.EventHandler(this._unlockButton_Click);
            // 
            // _lockoutTextBox
            // 
            this._lockoutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lockoutTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lockoutTextBox.Location = new System.Drawing.Point(87, 93);
            this._lockoutTextBox.Name = "_lockoutTextBox";
            this._lockoutTextBox.ReadOnly = true;
            this._lockoutTextBox.Size = new System.Drawing.Size(225, 23);
            this._lockoutTextBox.TabIndex = 3;
            this._lockoutTextBox.TabStop = false;
            // 
            // _pwSetTextBox
            // 
            this._pwSetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pwSetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pwSetTextBox.Location = new System.Drawing.Point(87, 122);
            this._pwSetTextBox.Name = "_pwSetTextBox";
            this._pwSetTextBox.ReadOnly = true;
            this._pwSetTextBox.Size = new System.Drawing.Size(225, 23);
            this._pwSetTextBox.TabIndex = 4;
            this._pwSetTextBox.TabStop = false;
            // 
            // _resetButton
            // 
            this._resetButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._resetButton.Image = global::Cfb.Camp.Security.Properties.Resources.Key_16x16;
            this._resetButton.Location = new System.Drawing.Point(162, 211);
            this._resetButton.Name = "_resetButton";
            this._resetButton.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this._resetButton.Size = new System.Drawing.Size(114, 25);
            this._resetButton.TabIndex = 9;
            this._resetButton.Text = "&Reset Password";
            this._resetButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._resetButton.UseVisualStyleBackColor = true;
            this._resetButton.Click += new System.EventHandler(this._resetButton_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 95);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 15);
            this.label27.TabIndex = 11;
            this.label27.Text = "Last lockout";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 124);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 15);
            this.label17.TabIndex = 11;
            this.label17.Text = "PW last set";
            // 
            // _createdTextBox
            // 
            this._createdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._createdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._createdTextBox.Location = new System.Drawing.Point(87, 151);
            this._createdTextBox.Name = "_createdTextBox";
            this._createdTextBox.ReadOnly = true;
            this._createdTextBox.Size = new System.Drawing.Size(225, 23);
            this._createdTextBox.TabIndex = 5;
            this._createdTextBox.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 153);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 15);
            this.label28.TabIndex = 11;
            this.label28.Text = "Created on";
            // 
            // _userNameTextBox
            // 
            this._userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._userNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._userNameTextBox.Location = new System.Drawing.Point(87, 6);
            this._userNameTextBox.Name = "_userNameTextBox";
            this._userNameTextBox.ReadOnly = true;
            this._userNameTextBox.Size = new System.Drawing.Size(225, 23);
            this._userNameTextBox.TabIndex = 0;
            this._userNameTextBox.TabStop = false;
            // 
            // _loginTextBox
            // 
            this._loginTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._loginTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._loginTextBox.Location = new System.Drawing.Point(87, 64);
            this._loginTextBox.Name = "_loginTextBox";
            this._loginTextBox.ReadOnly = true;
            this._loginTextBox.Size = new System.Drawing.Size(225, 23);
            this._loginTextBox.TabIndex = 2;
            this._loginTextBox.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "Last login";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 15);
            this.label13.TabIndex = 11;
            this.label13.Text = "E-mail";
            // 
            // _emailTextBox
            // 
            this._emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._emailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._emailTextBox.Location = new System.Drawing.Point(87, 35);
            this._emailTextBox.Name = "_emailTextBox";
            this._emailTextBox.Size = new System.Drawing.Size(225, 23);
            this._emailTextBox.TabIndex = 1;
            this._emailTextBox.TextChanged += new System.EventHandler(this._emailTextBox_TextChanged);
            this._emailTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.EmailTextBox_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "Username";
            // 
            // _enableButton
            // 
            this._enableButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._enableButton.Image = global::Cfb.Camp.Security.Properties.Resources.ApprovedUser_16x16;
            this._enableButton.Location = new System.Drawing.Point(162, 180);
            this._enableButton.Name = "_enableButton";
            this._enableButton.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this._enableButton.Size = new System.Drawing.Size(114, 25);
            this._enableButton.TabIndex = 1;
            this._enableButton.Text = "&Enable User";
            this._enableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._enableButton.UseVisualStyleBackColor = true;
            this._enableButton.Click += new System.EventHandler(this._enableButton_Click);
            // 
            // _securityTabPage
            // 
            this._securityTabPage.Controls.Add(this._removeGroupButton);
            this._securityTabPage.Controls.Add(this._addGroupButton);
            this._securityTabPage.Controls.Add(this.label19);
            this._securityTabPage.Controls.Add(this.label18);
            this._securityTabPage.Controls.Add(this._permissionsPanel);
            this._securityTabPage.Controls.Add(this._groupsListBox);
            this._securityTabPage.Controls.Add(this.label11);
            this._securityTabPage.Location = new System.Drawing.Point(4, 24);
            this._securityTabPage.Name = "_securityTabPage";
            this._securityTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._securityTabPage.Size = new System.Drawing.Size(318, 242);
            this._securityTabPage.TabIndex = 2;
            this._securityTabPage.Text = "Security";
            this._securityTabPage.UseVisualStyleBackColor = true;
            // 
            // _removeGroupButton
            // 
            this._removeGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._removeGroupButton.Location = new System.Drawing.Point(237, 50);
            this._removeGroupButton.Name = "_removeGroupButton";
            this._removeGroupButton.Size = new System.Drawing.Size(75, 23);
            this._removeGroupButton.TabIndex = 2;
            this._removeGroupButton.Text = "&Remove";
            this._removeGroupButton.UseVisualStyleBackColor = true;
            this._removeGroupButton.Click += new System.EventHandler(this._removeGroupButton_Click);
            // 
            // _addGroupButton
            // 
            this._addGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._addGroupButton.Location = new System.Drawing.Point(237, 21);
            this._addGroupButton.Name = "_addGroupButton";
            this._addGroupButton.Size = new System.Drawing.Size(75, 23);
            this._addGroupButton.TabIndex = 1;
            this._addGroupButton.Text = "A&dd";
            this._addGroupButton.UseVisualStyleBackColor = true;
            this._addGroupButton.Click += new System.EventHandler(this._addGroupButton_Click);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(256, 88);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 15);
            this.label19.TabIndex = 7;
            this.label19.Text = "Allow";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 88);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 15);
            this.label18.TabIndex = 6;
            this.label18.Text = "Permissions";
            // 
            // _permissionsPanel
            // 
            this._permissionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._permissionsPanel.AutoScroll = true;
            this._permissionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._permissionsPanel.Controls.Add(this._permissionPictureBox);
            this._permissionsPanel.Controls.Add(this._permissionLabel);
            this._permissionsPanel.Location = new System.Drawing.Point(6, 106);
            this._permissionsPanel.Name = "_permissionsPanel";
            this._permissionsPanel.Padding = new System.Windows.Forms.Padding(3);
            this._permissionsPanel.Size = new System.Drawing.Size(306, 130);
            this._permissionsPanel.TabIndex = 3;
            // 
            // _permissionPictureBox
            // 
            this._permissionPictureBox.Image = global::Cfb.Camp.Security.Properties.Resources.Check_16x16;
            this._permissionPictureBox.Location = new System.Drawing.Point(250, 3);
            this._permissionPictureBox.Name = "_permissionPictureBox";
            this._permissionPictureBox.Size = new System.Drawing.Size(34, 16);
            this._permissionPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._permissionPictureBox.TabIndex = 2;
            this._permissionPictureBox.TabStop = false;
            this._permissionPictureBox.Tag = "CAccess";
            this._permissionPictureBox.Visible = false;
            // 
            // _permissionLabel
            // 
            this._permissionLabel.AutoSize = true;
            this._permissionLabel.Location = new System.Drawing.Point(6, 4);
            this._permissionLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._permissionLabel.Name = "_permissionLabel";
            this._permissionLabel.Size = new System.Drawing.Size(56, 15);
            this._permissionLabel.TabIndex = 1;
            this._permissionLabel.Text = "C-Access";
            // 
            // _groupsListBox
            // 
            this._groupsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._groupsListBox.DisplayMember = "Name";
            this._groupsListBox.ItemHeight = 15;
            this._groupsListBox.Location = new System.Drawing.Point(6, 21);
            this._groupsListBox.Name = "_groupsListBox";
            this._groupsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._groupsListBox.Size = new System.Drawing.Size(225, 64);
            this._groupsListBox.Sorted = true;
            this._groupsListBox.TabIndex = 0;
            this._groupsListBox.ValueMember = "ID";
            this._groupsListBox.SelectedIndexChanged += new System.EventHandler(this._groupsListBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 15);
            this.label11.TabIndex = 1;
            this.label11.Text = "Group Membership:";
            // 
            // _electionsTabPage
            // 
            this._electionsTabPage.Controls.Add(this._allCyclesCheckBox);
            this._electionsTabPage.Controls.Add(this.panel2);
            this._electionsTabPage.Controls.Add(this.label15);
            this._electionsTabPage.Location = new System.Drawing.Point(4, 24);
            this._electionsTabPage.Name = "_electionsTabPage";
            this._electionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._electionsTabPage.Size = new System.Drawing.Size(318, 242);
            this._electionsTabPage.TabIndex = 3;
            this._electionsTabPage.Text = "Elections";
            this._electionsTabPage.UseVisualStyleBackColor = true;
            // 
            // _allCyclesCheckBox
            // 
            this._allCyclesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._allCyclesCheckBox.AutoSize = true;
            this._allCyclesCheckBox.Location = new System.Drawing.Point(242, 2);
            this._allCyclesCheckBox.Name = "_allCyclesCheckBox";
            this._allCyclesCheckBox.Size = new System.Drawing.Size(77, 19);
            this._allCyclesCheckBox.TabIndex = 7;
            this._allCyclesCheckBox.Text = "All Cycles";
            this._allCyclesCheckBox.UseVisualStyleBackColor = true;
            this._allCyclesCheckBox.CheckedChanged += new System.EventHandler(this._allCyclesCheckBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._electionCandidateInfoPanel);
            this.panel2.Controls.Add(this._electionsCheckedListBox);
            this.panel2.Location = new System.Drawing.Point(6, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 215);
            this.panel2.TabIndex = 6;
            // 
            // _electionCandidateInfoPanel
            // 
            this._electionCandidateInfoPanel.BackColor = System.Drawing.SystemColors.Info;
            this._electionCandidateInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._electionCandidateInfoPanel.Controls.Add(this.label2);
            this._electionCandidateInfoPanel.Controls.Add(this.pictureBox1);
            this._electionCandidateInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._electionCandidateInfoPanel.Location = new System.Drawing.Point(0, 0);
            this._electionCandidateInfoPanel.Name = "_electionCandidateInfoPanel";
            this._electionCandidateInfoPanel.Size = new System.Drawing.Size(306, 32);
            this._electionCandidateInfoPanel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(25, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(279, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Election cycle authorization for candidates cannot be modified.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Cfb.Camp.Security.Properties.Resources.Info_16x16;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _electionsCheckedListBox
            // 
            this._electionsCheckedListBox.CheckOnClick = true;
            this._electionsCheckedListBox.IntegralHeight = false;
            this._electionsCheckedListBox.Location = new System.Drawing.Point(0, 38);
            this._electionsCheckedListBox.Name = "_electionsCheckedListBox";
            this._electionsCheckedListBox.Size = new System.Drawing.Size(306, 177);
            this._electionsCheckedListBox.Sorted = true;
            this._electionsCheckedListBox.TabIndex = 3;
            this._electionsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this._electionsCheckedListBox_ItemCheck);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 15);
            this.label15.TabIndex = 5;
            this.label15.Text = "Authorized Election Cycles:";
            // 
            // _cfisTabPage
            // 
            this._cfisTabPage.Controls.Add(this._usernameButton);
            this._cfisTabPage.Controls.Add(this._newUsernameTextBox);
            this._cfisTabPage.Controls.Add(this.panel1);
            this._cfisTabPage.Controls.Add(this._miTextBox);
            this._cfisTabPage.Controls.Add(this._newUsernamePlaceholder);
            this._cfisTabPage.Controls.Add(this._newUsernameLabel);
            this._cfisTabPage.Controls.Add(this._passwordLabel);
            this._cfisTabPage.Controls.Add(this._passwordTextBox);
            this._cfisTabPage.Controls.Add(this._cfisEmailTextBox);
            this._cfisTabPage.Controls.Add(this._cfisDisplayNameTextBox);
            this._cfisTabPage.Controls.Add(this._lastNameTextBox);
            this._cfisTabPage.Controls.Add(this._committeeTextBox);
            this._cfisTabPage.Controls.Add(this._ecTextBox);
            this._cfisTabPage.Controls.Add(this._typeTextBox);
            this._cfisTabPage.Controls.Add(this._firstNameTextBox);
            this._cfisTabPage.Controls.Add(this._cfisEmailLabel);
            this._cfisTabPage.Controls.Add(this._cfisDisplayNameLabel);
            this._cfisTabPage.Controls.Add(this._lastNameLabel);
            this._cfisTabPage.Controls.Add(this._miLabel);
            this._cfisTabPage.Controls.Add(this._committeeLabel);
            this._cfisTabPage.Controls.Add(this._ecLabel);
            this._cfisTabPage.Controls.Add(this.label3);
            this._cfisTabPage.Controls.Add(this._firstNameLabel);
            this._cfisTabPage.Location = new System.Drawing.Point(4, 24);
            this._cfisTabPage.Name = "_cfisTabPage";
            this._cfisTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._cfisTabPage.Size = new System.Drawing.Size(318, 242);
            this._cfisTabPage.TabIndex = 0;
            this._cfisTabPage.Text = "CFIS";
            this._cfisTabPage.UseVisualStyleBackColor = true;
            // 
            // _usernameButton
            // 
            this._usernameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._usernameButton.Location = new System.Drawing.Point(244, 214);
            this._usernameButton.Name = "_usernameButton";
            this._usernameButton.Size = new System.Drawing.Size(68, 23);
            this._usernameButton.TabIndex = 9;
            this._usernameButton.Text = "Override";
            this._usernameButton.UseVisualStyleBackColor = true;
            this._usernameButton.Click += new System.EventHandler(this._usernameButton_Click);
            // 
            // _newUsernameTextBox
            // 
            this._newUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._newUsernameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._newUsernameTextBox.CausesValidation = false;
            this._newUsernameTextBox.Location = new System.Drawing.Point(87, 216);
            this._newUsernameTextBox.Name = "_newUsernameTextBox";
            this._newUsernameTextBox.Size = new System.Drawing.Size(225, 23);
            this._newUsernameTextBox.TabIndex = 10;
            this._newUsernameTextBox.Visible = false;
            this._newUsernameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._newUsernameTextBox_Validating);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(6, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 1);
            this.panel1.TabIndex = 9;
            // 
            // _miTextBox
            // 
            this._miTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._miTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._miTextBox.Location = new System.Drawing.Point(291, 71);
            this._miTextBox.Name = "_miTextBox";
            this._miTextBox.Size = new System.Drawing.Size(21, 23);
            this._miTextBox.TabIndex = 4;
            // 
            // _newUsernamePlaceholder
            // 
            this._newUsernamePlaceholder.AutoSize = true;
            this._newUsernamePlaceholder.Location = new System.Drawing.Point(84, 218);
            this._newUsernamePlaceholder.Name = "_newUsernamePlaceholder";
            this._newUsernamePlaceholder.Size = new System.Drawing.Size(143, 15);
            this._newUsernamePlaceholder.TabIndex = 1;
            this._newUsernamePlaceholder.Text = "(automatically generated)";
            // 
            // _newUsernameLabel
            // 
            this._newUsernameLabel.AutoSize = true;
            this._newUsernameLabel.Location = new System.Drawing.Point(3, 218);
            this._newUsernameLabel.Name = "_newUsernameLabel";
            this._newUsernameLabel.Size = new System.Drawing.Size(60, 15);
            this._newUsernameLabel.TabIndex = 1;
            this._newUsernameLabel.Text = "Username";
            // 
            // _passwordLabel
            // 
            this._passwordLabel.AutoSize = true;
            this._passwordLabel.Location = new System.Drawing.Point(3, 189);
            this._passwordLabel.Name = "_passwordLabel";
            this._passwordLabel.Size = new System.Drawing.Size(57, 15);
            this._passwordLabel.TabIndex = 1;
            this._passwordLabel.Text = "Password";
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._passwordTextBox.Location = new System.Drawing.Point(87, 187);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.Size = new System.Drawing.Size(225, 23);
            this._passwordTextBox.TabIndex = 8;
            this._passwordTextBox.Text = "password";
            this._passwordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._passwordTextBox_Validating);
            // 
            // _cfisEmailTextBox
            // 
            this._cfisEmailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cfisEmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._cfisEmailTextBox.Location = new System.Drawing.Point(87, 158);
            this._cfisEmailTextBox.Name = "_cfisEmailTextBox";
            this._cfisEmailTextBox.Size = new System.Drawing.Size(225, 23);
            this._cfisEmailTextBox.TabIndex = 7;
            this._cfisEmailTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.EmailTextBox_Validating);
            // 
            // _cfisDisplayNameTextBox
            // 
            this._cfisDisplayNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cfisDisplayNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._cfisDisplayNameTextBox.Location = new System.Drawing.Point(87, 129);
            this._cfisDisplayNameTextBox.Name = "_cfisDisplayNameTextBox";
            this._cfisDisplayNameTextBox.Size = new System.Drawing.Size(225, 23);
            this._cfisDisplayNameTextBox.TabIndex = 6;
            // 
            // _lastNameTextBox
            // 
            this._lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lastNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lastNameTextBox.Location = new System.Drawing.Point(87, 100);
            this._lastNameTextBox.Name = "_lastNameTextBox";
            this._lastNameTextBox.Size = new System.Drawing.Size(225, 23);
            this._lastNameTextBox.TabIndex = 5;
            // 
            // _committeeTextBox
            // 
            this._committeeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._committeeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._committeeTextBox.Location = new System.Drawing.Point(87, 35);
            this._committeeTextBox.Name = "_committeeTextBox";
            this._committeeTextBox.ReadOnly = true;
            this._committeeTextBox.Size = new System.Drawing.Size(225, 23);
            this._committeeTextBox.TabIndex = 2;
            this._committeeTextBox.TabStop = false;
            // 
            // _ecTextBox
            // 
            this._ecTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ecTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._ecTextBox.Location = new System.Drawing.Point(272, 6);
            this._ecTextBox.Name = "_ecTextBox";
            this._ecTextBox.ReadOnly = true;
            this._ecTextBox.Size = new System.Drawing.Size(40, 23);
            this._ecTextBox.TabIndex = 1;
            this._ecTextBox.TabStop = false;
            // 
            // _typeTextBox
            // 
            this._typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._typeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._typeTextBox.Location = new System.Drawing.Point(87, 6);
            this._typeTextBox.Name = "_typeTextBox";
            this._typeTextBox.ReadOnly = true;
            this._typeTextBox.Size = new System.Drawing.Size(115, 23);
            this._typeTextBox.TabIndex = 0;
            this._typeTextBox.TabStop = false;
            // 
            // _firstNameTextBox
            // 
            this._firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._firstNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._firstNameTextBox.Location = new System.Drawing.Point(87, 71);
            this._firstNameTextBox.Name = "_firstNameTextBox";
            this._firstNameTextBox.Size = new System.Drawing.Size(171, 23);
            this._firstNameTextBox.TabIndex = 3;
            // 
            // _cfisEmailLabel
            // 
            this._cfisEmailLabel.AutoSize = true;
            this._cfisEmailLabel.Location = new System.Drawing.Point(3, 160);
            this._cfisEmailLabel.Name = "_cfisEmailLabel";
            this._cfisEmailLabel.Size = new System.Drawing.Size(41, 15);
            this._cfisEmailLabel.TabIndex = 1;
            this._cfisEmailLabel.Text = "E-mail";
            // 
            // _cfisDisplayNameLabel
            // 
            this._cfisDisplayNameLabel.AutoSize = true;
            this._cfisDisplayNameLabel.Location = new System.Drawing.Point(3, 131);
            this._cfisDisplayNameLabel.Name = "_cfisDisplayNameLabel";
            this._cfisDisplayNameLabel.Size = new System.Drawing.Size(78, 15);
            this._cfisDisplayNameLabel.TabIndex = 1;
            this._cfisDisplayNameLabel.Text = "Display name";
            // 
            // _lastNameLabel
            // 
            this._lastNameLabel.AutoSize = true;
            this._lastNameLabel.Location = new System.Drawing.Point(3, 102);
            this._lastNameLabel.Name = "_lastNameLabel";
            this._lastNameLabel.Size = new System.Drawing.Size(61, 15);
            this._lastNameLabel.TabIndex = 1;
            this._lastNameLabel.Text = "Last name";
            // 
            // _miLabel
            // 
            this._miLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._miLabel.AutoSize = true;
            this._miLabel.Location = new System.Drawing.Point(264, 73);
            this._miLabel.Name = "_miLabel";
            this._miLabel.Size = new System.Drawing.Size(21, 15);
            this._miLabel.TabIndex = 1;
            this._miLabel.Text = "MI";
            // 
            // _committeeLabel
            // 
            this._committeeLabel.AutoSize = true;
            this._committeeLabel.Location = new System.Drawing.Point(3, 37);
            this._committeeLabel.Name = "_committeeLabel";
            this._committeeLabel.Size = new System.Drawing.Size(67, 15);
            this._committeeLabel.TabIndex = 1;
            this._committeeLabel.Text = "Committee";
            // 
            // _ecLabel
            // 
            this._ecLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ecLabel.Location = new System.Drawing.Point(208, 8);
            this._ecLabel.Name = "_ecLabel";
            this._ecLabel.Size = new System.Drawing.Size(58, 15);
            this._ecLabel.TabIndex = 1;
            this._ecLabel.Text = "Election";
            this._ecLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Contact type";
            // 
            // _firstNameLabel
            // 
            this._firstNameLabel.AutoSize = true;
            this._firstNameLabel.Location = new System.Drawing.Point(3, 73);
            this._firstNameLabel.Name = "_firstNameLabel";
            this._firstNameLabel.Size = new System.Drawing.Size(62, 15);
            this._firstNameLabel.TabIndex = 1;
            this._firstNameLabel.Text = "First name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Candidate";
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.AutoSize = true;
            this._cancelButton.CausesValidation = false;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(179, 350);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 25);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.AutoSize = true;
            this._okButton.Location = new System.Drawing.Point(100, 350);
            this._okButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 25);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _applyButton
            // 
            this._applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._applyButton.AutoSize = true;
            this._applyButton.Enabled = false;
            this._applyButton.Location = new System.Drawing.Point(257, 350);
            this._applyButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._applyButton.Name = "_applyButton";
            this._applyButton.Size = new System.Drawing.Size(75, 25);
            this._applyButton.TabIndex = 3;
            this._applyButton.Text = "&Apply";
            this._applyButton.UseVisualStyleBackColor = true;
            this._applyButton.Click += new System.EventHandler(this._applyButton_Click);
            // 
            // _userImage
            // 
            this._userImage.BackColor = System.Drawing.Color.Transparent;
            this._userImage.BackgroundImage = global::Cfb.Camp.Security.Properties.Resources.User_32x32;
            this._userImage.Image = global::Cfb.Camp.Security.Properties.Resources.DisabledOverlay_32x32;
            this._userImage.Location = new System.Drawing.Point(12, 12);
            this._userImage.Name = "_userImage";
            this._userImage.Size = new System.Drawing.Size(32, 32);
            this._userImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._userImage.TabIndex = 13;
            this._userImage.TabStop = false;
            // 
            // _passwordErrorProvider
            // 
            this._passwordErrorProvider.ContainerControl = this;
            // 
            // _emailErrorProvider
            // 
            this._emailErrorProvider.ContainerControl = this;
            // 
            // _cfisEmailErrorProvider
            // 
            this._cfisEmailErrorProvider.ContainerControl = this;
            // 
            // _usernameErrorProvider
            // 
            this._usernameErrorProvider.ContainerControl = this;
            // 
            // _electionsCandidateToolTip
            // 
            this._electionsCandidateToolTip.Active = false;
            this._electionsCandidateToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._electionsCandidateToolTip.ToolTipTitle = "Election cycle authorization for candidates cannot be modified.";
            // 
            // _displayNameTextBox
            // 
            this._displayNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._displayNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._displayNameTextBox.Location = new System.Drawing.Point(50, 18);
            this._displayNameTextBox.MaxLength = 256;
            this._displayNameTextBox.Name = "_displayNameTextBox";
            this._displayNameTextBox.Size = new System.Drawing.Size(282, 23);
            this._displayNameTextBox.TabIndex = 14;
            this._displayNameTextBox.Tag = "";
            this._displayNameTextBox.TextChanged += new System.EventHandler(this._displayNameTextBox_TextChanged);
            // 
            // _candidatePicker
            // 
            this._candidatePicker.AllCandidatesOption = false;
            this._candidatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._candidatePicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._candidatePicker.DisplayMember = "ID";
            this._candidatePicker.EmptySelectionText = null;
            this._candidatePicker.FormatString = "{0} ({1})";
            this._candidatePicker.FormattingEnabled = true;
            this._candidatePicker.Location = new System.Drawing.Point(76, 46);
            this._candidatePicker.MaxDropDownItems = 16;
            this._candidatePicker.Name = "_candidatePicker";
            this._candidatePicker.ReadOnly = true;
            this._candidatePicker.SelectedCid = null;
            this._candidatePicker.Size = new System.Drawing.Size(256, 23);
            this._candidatePicker.TabIndex = 15;
            this._candidatePicker.ValueMember = "ID";
            this._candidatePicker.Visible = false;
            this._candidatePicker.SelectedValueChanged += new System.EventHandler(this._candidatePicker_SelectedValueChanged);
            // 
            // _candidateTextBox
            // 
            this._candidateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._candidateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._candidateTextBox.Location = new System.Drawing.Point(76, 47);
            this._candidateTextBox.Name = "_candidateTextBox";
            this._candidateTextBox.ReadOnly = true;
            this._candidateTextBox.Size = new System.Drawing.Size(256, 23);
            this._candidateTextBox.TabIndex = 5;
            this._candidateTextBox.TabStop = false;
            // 
            // _displaynamePlaceholder
            // 
            this._displaynamePlaceholder.AutoSize = true;
            this._displaynamePlaceholder.Location = new System.Drawing.Point(50, 20);
            this._displaynamePlaceholder.Name = "_displaynamePlaceholder";
            this._displaynamePlaceholder.Size = new System.Drawing.Size(113, 15);
            this._displaynamePlaceholder.TabIndex = 1;
            this._displaynamePlaceholder.Text = "(New User Account)";
            // 
            // UserAccountForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(344, 384);
            this.Controls.Add(this._candidatePicker);
            this.Controls.Add(this._displayNameTextBox);
            this.Controls.Add(this._userImage);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._applyButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._mainTabControl);
            this.Controls.Add(this._candidateTextBox);
            this.Controls.Add(this._displaynamePlaceholder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 412);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 412);
            this.Name = "UserAccountForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Account Properties";
            this._mainTabControl.ResumeLayout(false);
            this._generalTabPage.ResumeLayout(false);
            this._generalTabPage.PerformLayout();
            this._securityTabPage.ResumeLayout(false);
            this._securityTabPage.PerformLayout();
            this._permissionsPanel.ResumeLayout(false);
            this._permissionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._permissionPictureBox)).EndInit();
            this._electionsTabPage.ResumeLayout(false);
            this._electionsTabPage.PerformLayout();
            this.panel2.ResumeLayout(false);
            this._electionCandidateInfoPanel.ResumeLayout(false);
            this._electionCandidateInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this._cfisTabPage.ResumeLayout(false);
            this._cfisTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._userImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._passwordErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._emailErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._cfisEmailErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._usernameErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl _mainTabControl;
        private System.Windows.Forms.TabPage _cfisTabPage;
        private System.Windows.Forms.TabPage _generalTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage _securityTabPage;
        private System.Windows.Forms.Label _passwordLabel;
        private System.Windows.Forms.Label _cfisEmailLabel;
        private System.Windows.Forms.Label _cfisDisplayNameLabel;
        private System.Windows.Forms.Label _miLabel;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.TextBox _firstNameTextBox;
        private System.Windows.Forms.TextBox _miTextBox;
        private System.Windows.Forms.TextBox _cfisEmailTextBox;
        private System.Windows.Forms.TextBox _cfisDisplayNameTextBox;
        private System.Windows.Forms.TextBox _lastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.TextBox _committeeTextBox;
        private System.Windows.Forms.TextBox _ecTextBox;
        private System.Windows.Forms.Label _committeeLabel;
        private System.Windows.Forms.Label _ecLabel;
        private System.Windows.Forms.Label _lastNameLabel;
        private System.Windows.Forms.Label _firstNameLabel;
        private System.Windows.Forms.TextBox _pwSetTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox _loginTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox _emailTextBox;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox _userImage;
        private System.Windows.Forms.Button _syncButton;
        private System.Windows.Forms.Button _unlockButton;
        private System.Windows.Forms.Button _resetButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox _groupsListBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel _permissionsPanel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage _electionsTabPage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button _removeGroupButton;
        private System.Windows.Forms.Button _addGroupButton;
        private System.Windows.Forms.Button _applyButton;
        private System.Windows.Forms.PictureBox _permissionPictureBox;
        private System.Windows.Forms.TextBox _lockoutTextBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox _createdTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button _enableButton;
        private System.Windows.Forms.Label _newUsernameLabel;
        private System.Windows.Forms.TextBox _newUsernameTextBox;
        private System.Windows.Forms.Button _usernameButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider _passwordErrorProvider;
        private System.Windows.Forms.TextBox _userNameTextBox;
        private System.Windows.Forms.ErrorProvider _emailErrorProvider;
        private System.Windows.Forms.ErrorProvider _cfisEmailErrorProvider;
        private System.Windows.Forms.ErrorProvider _usernameErrorProvider;
        private System.Windows.Forms.ToolTip _electionsCandidateToolTip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox _electionsCheckedListBox;
        private System.Windows.Forms.Panel _electionCandidateInfoPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox _typeTextBox;
        private System.Windows.Forms.TextBox _displayNameTextBox;
        private System.Windows.Forms.Label _permissionLabel;
        private Forms.CandidatePicker _candidatePicker;
        private System.Windows.Forms.TextBox _candidateTextBox;
        private System.Windows.Forms.Label _newUsernamePlaceholder;
        private System.Windows.Forms.Label _displaynamePlaceholder;
        private System.Windows.Forms.CheckBox _allCyclesCheckBox;
    }
}