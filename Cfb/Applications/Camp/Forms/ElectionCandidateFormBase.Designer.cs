namespace Cfb.Camp.Forms
{
	partial class ElectionCandidateFormBase
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
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.ActiveCandidatePicker = new Cfb.Camp.Forms.ActiveCandidatePicker();
			this.StatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel,
            this.ToolStripProgressBar});
			this.StatusStrip.Location = new System.Drawing.Point(0, 417);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.StatusStrip.Size = new System.Drawing.Size(729, 24);
			this.StatusStrip.TabIndex = 1;
			// 
			// ToolStripStatusLabel
			// 
			this.ToolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
			this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
			this.ToolStripStatusLabel.Size = new System.Drawing.Size(601, 19);
			this.ToolStripStatusLabel.Spring = true;
			this.ToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ToolStripProgressBar
			// 
			this.ToolStripProgressBar.Name = "ToolStripProgressBar";
			this.ToolStripProgressBar.Size = new System.Drawing.Size(117, 18);
			// 
			// ActiveCandidatePicker
			// 
			this.ActiveCandidatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ActiveCandidatePicker.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.ActiveCandidatePicker.Location = new System.Drawing.Point(12, 12);
			this.ActiveCandidatePicker.MinimumSize = new System.Drawing.Size(400, 23);
			this.ActiveCandidatePicker.Name = "ActiveCandidatePicker";
			this.ActiveCandidatePicker.ReadOnly = false;
			this.ActiveCandidatePicker.Size = new System.Drawing.Size(705, 23);
			this.ActiveCandidatePicker.TabIndex = 2;
			// 
			// ElectionCandidateFormBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(729, 441);
			this.Controls.Add(this.ActiveCandidatePicker);
			this.Controls.Add(this.StatusStrip);
			this.MinimumSize = new System.Drawing.Size(745, 479);
			this.Name = "ElectionCandidateFormBase";
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
		protected System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar;
		protected System.Windows.Forms.StatusStrip StatusStrip;
		public ActiveCandidatePicker ActiveCandidatePicker;
	}
}