namespace Cfb.Camp.Forms
{
	partial class ActiveCandidatePicker
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CandidateLabel = new System.Windows.Forms.Label();
			this.ElectionLabel = new System.Windows.Forms.Label();
			this.CandidatePicker = new Cfb.Camp.Forms.CandidatePicker();
			this.ElectionPicker = new Cfb.Camp.Forms.ElectionPicker();
			this.SuspendLayout();
			// 
			// CandidateLabel
			// 
			this.CandidateLabel.AutoSize = true;
			this.CandidateLabel.Location = new System.Drawing.Point(118, 3);
			this.CandidateLabel.Name = "CandidateLabel";
			this.CandidateLabel.Size = new System.Drawing.Size(61, 15);
			this.CandidateLabel.TabIndex = 9;
			this.CandidateLabel.Text = "Candidate";
			this.CandidateLabel.Click += new System.EventHandler(this._candidateLabel_Click);
			// 
			// ElectionLabel
			// 
			this.ElectionLabel.AutoSize = true;
			this.ElectionLabel.Location = new System.Drawing.Point(-3, 3);
			this.ElectionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.ElectionLabel.Name = "ElectionLabel";
			this.ElectionLabel.Size = new System.Drawing.Size(49, 15);
			this.ElectionLabel.TabIndex = 8;
			this.ElectionLabel.Text = "Election";
			this.ElectionLabel.Click += new System.EventHandler(this._electionLabel_Click);
			// 
			// CandidatePicker
			// 
			this.CandidatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CandidatePicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.CandidatePicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.CandidatePicker.BackColor = System.Drawing.SystemColors.Window;
			this.CandidatePicker.DisplayMember = "ID";
			this.CandidatePicker.EmptySelectionText = null;
			this.CandidatePicker.Enabled = false;
			this.CandidatePicker.FormatString = "{0} ({1})";
			this.CandidatePicker.FormattingEnabled = true;
			this.CandidatePicker.Location = new System.Drawing.Point(185, 0);
			this.CandidatePicker.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.CandidatePicker.MaxDropDownItems = 16;
			this.CandidatePicker.Name = "CandidatePicker";
			this.CandidatePicker.SelectedCid = null;
			this.CandidatePicker.Size = new System.Drawing.Size(215, 23);
			this.CandidatePicker.TabIndex = 1;
			this.CandidatePicker.ValueMember = "ID";
			// 
			// ElectionPicker
			// 
			this.ElectionPicker.AllElectionsOption = false;
			this.ElectionPicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.ElectionPicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.ElectionPicker.DisplayMember = "Cycle";
			this.ElectionPicker.EmptySelectionText = null;
			this.ElectionPicker.Location = new System.Drawing.Point(52, 0);
			this.ElectionPicker.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.ElectionPicker.MaxDropDownItems = 16;
			this.ElectionPicker.MinimumSize = new System.Drawing.Size(60, 0);
			this.ElectionPicker.Name = "ElectionPicker";
			this.ElectionPicker.SelectedElection = null;
			this.ElectionPicker.Size = new System.Drawing.Size(60, 23);
			this.ElectionPicker.TabIndex = 0;
			this.ElectionPicker.ValueMember = "Cycle";
			this.ElectionPicker.SelectedIndexChanged += new System.EventHandler(this.ElectionPicker_SelectedIndexChanged);
			// 
			// ActiveCandidatePicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CandidatePicker);
			this.Controls.Add(this.CandidateLabel);
			this.Controls.Add(this.ElectionLabel);
			this.Controls.Add(this.ElectionPicker);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.MinimumSize = new System.Drawing.Size(400, 23);
			this.Name = "ActiveCandidatePicker";
			this.Size = new System.Drawing.Size(400, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public ElectionPicker ElectionPicker;
		public CandidatePicker CandidatePicker;
		private System.Windows.Forms.Label ElectionLabel;
		private System.Windows.Forms.Label CandidateLabel;
	}
}
