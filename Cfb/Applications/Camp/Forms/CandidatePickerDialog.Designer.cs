namespace Cfb.Camp.Forms
{
	partial class CandidatePickerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CandidatePickerDialog));
            this.electionCycleLabel = new System.Windows.Forms.Label();
            this.electionPanel = new System.Windows.Forms.Panel();
            this.electionPicker = new Cfb.Camp.Forms.ElectionPicker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.candidatePanel = new System.Windows.Forms.Panel();
            this.shrinkPanel = new System.Windows.Forms.Panel();
            this.candidatePicker = new Cfb.Camp.Forms.CandidatePicker();
            this.electionPanel.SuspendLayout();
            this.shrinkPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // electionCycleLabel
            // 
            this.electionCycleLabel.AutoSize = true;
            this.electionCycleLabel.Location = new System.Drawing.Point(-3, 3);
            this.electionCycleLabel.Margin = new System.Windows.Forms.Padding(3);
            this.electionCycleLabel.Name = "electionCycleLabel";
            this.electionCycleLabel.Size = new System.Drawing.Size(81, 15);
            this.electionCycleLabel.TabIndex = 14;
            this.electionCycleLabel.Text = "Election Cycle";
            // 
            // electionPanel
            // 
            this.electionPanel.AutoSize = true;
            this.electionPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.electionPanel.Controls.Add(this.electionCycleLabel);
            this.electionPanel.Controls.Add(this.electionPicker);
            this.electionPanel.Location = new System.Drawing.Point(61, 41);
            this.electionPanel.Name = "electionPanel";
            this.electionPanel.Size = new System.Drawing.Size(171, 23);
            this.electionPanel.TabIndex = 1;
            // 
            // electionPicker
            // 
            this.electionPicker.AllElectionsOption = false;
            this.electionPicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.electionPicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.electionPicker.DisplayMember = "Cycle";
            this.electionPicker.EmptySelectionText = null;
            this.electionPicker.FormattingEnabled = true;
            this.electionPicker.Location = new System.Drawing.Point(84, 0);
            this.electionPicker.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.electionPicker.MaxDropDownItems = 16;
            this.electionPicker.MinimumSize = new System.Drawing.Size(87, 0);
            this.electionPicker.Name = "electionPicker";
            this.electionPicker.SelectedElection = null;
            this.electionPicker.Size = new System.Drawing.Size(87, 23);
            this.electionPicker.TabIndex = 0;
            this.electionPicker.ValueMember = "Cycle";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cancelButton.Location = new System.Drawing.Point(93, 0);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 27);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.okButton.Location = new System.Drawing.Point(0, 0);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 27);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // candidatePanel
            // 
            this.candidatePanel.AutoSize = true;
            this.candidatePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.candidatePanel.Location = new System.Drawing.Point(0, 0);
            this.candidatePanel.Name = "candidatePanel";
            this.candidatePanel.Size = new System.Drawing.Size(0, 0);
            this.candidatePanel.TabIndex = 0;
            // 
            // shrinkPanel
            // 
            this.shrinkPanel.AutoSize = true;
            this.shrinkPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shrinkPanel.Controls.Add(this.cancelButton);
            this.shrinkPanel.Controls.Add(this.okButton);
            this.shrinkPanel.Location = new System.Drawing.Point(56, 76);
            this.shrinkPanel.Name = "shrinkPanel";
            this.shrinkPanel.Size = new System.Drawing.Size(180, 27);
            this.shrinkPanel.TabIndex = 16;
            // 
            // candidatePicker
            // 
            this.candidatePicker.AllCandidatesOption = false;
            this.candidatePicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.candidatePicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.candidatePicker.DisplayMember = "ID";
            this.candidatePicker.EmptySelectionText = null;
            this.candidatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.candidatePicker.FormatString = "{0} ({1})";
            this.candidatePicker.FormattingEnabled = true;
            this.candidatePicker.Location = new System.Drawing.Point(21, 12);
            this.candidatePicker.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.candidatePicker.MaxDropDownItems = 16;
            this.candidatePicker.Name = "candidatePicker";
            this.candidatePicker.SelectedCid = null;
            this.candidatePicker.Size = new System.Drawing.Size(250, 23);
            this.candidatePicker.TabIndex = 0;
            this.candidatePicker.ValueMember = "ID";
            this.candidatePicker.SelectedIndexChanged += new System.EventHandler(this.candidatePicker_SelectedIndexChanged);
            // 
            // CandidatePickerDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(292, 112);
            this.Controls.Add(this.shrinkPanel);
            this.Controls.Add(this.candidatePicker);
            this.Controls.Add(this.electionPanel);
            this.Controls.Add(this.candidatePanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CandidatePickerDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Candidate Selection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CandidatePickerDialog_FormClosed);
            this.Load += new System.EventHandler(this.CandidatePickerForm_Load);
            this.electionPanel.ResumeLayout(false);
            this.electionPanel.PerformLayout();
            this.shrinkPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private CandidatePicker candidatePicker;
		private ElectionPicker electionPicker;
		private System.Windows.Forms.Label electionCycleLabel;
		private System.Windows.Forms.Panel electionPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Panel candidatePanel;
		private System.Windows.Forms.Panel shrinkPanel;
	}
}