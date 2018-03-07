using System;
using System.Windows.Forms;
using Cfb.Camp.Forms;

namespace Cfb.Camp
{
	partial class CampOptionsDialog
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
			this.electionCycleLabel = new System.Windows.Forms.Label();
			this.electionPicker = new Cfb.Camp.Forms.ElectionPicker();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.okPanel = new System.Windows.Forms.Panel();
			this.optionsPanel = new System.Windows.Forms.Panel();
			this.okPanel.SuspendLayout();
			this.optionsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// electionCycleLabel
			// 
			this.electionCycleLabel.AutoSize = true;
			this.electionCycleLabel.Location = new System.Drawing.Point(12, 15);
			this.electionCycleLabel.Margin = new System.Windows.Forms.Padding(3);
			this.electionCycleLabel.Name = "electionCycleLabel";
			this.electionCycleLabel.Size = new System.Drawing.Size(122, 15);
			this.electionCycleLabel.TabIndex = 14;
			this.electionCycleLabel.Text = "Default Election Cycle";
			// 
			// electionPicker
			// 
			this.electionPicker.AllElectionsOption = false;
			this.electionPicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.electionPicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.electionPicker.DisplayMember = "Cycle";
			this.electionPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.electionPicker.FormattingEnabled = true;
			this.electionPicker.Location = new System.Drawing.Point(140, 12);
			this.electionPicker.MaxDropDownItems = 16;
			this.electionPicker.MinimumSize = new System.Drawing.Size(87, 0);
			this.electionPicker.Name = "electionPicker";
			this.electionPicker.ReadOnly = false;
			this.electionPicker.SelectedElection = null;
			this.electionPicker.Size = new System.Drawing.Size(87, 23);
			this.electionPicker.TabIndex = 0;
			this.electionPicker.ValueMember = "Cycle";
			this.electionPicker.SelectionChangeCommitted += new System.EventHandler(this.electionPicker_SelectionChangeCommitted);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.cancelButton.Location = new System.Drawing.Point(140, 13);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(87, 27);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.okButton.Location = new System.Drawing.Point(47, 13);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(87, 27);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// okPanel
			// 
			this.okPanel.Controls.Add(this.okButton);
			this.okPanel.Controls.Add(this.cancelButton);
			this.okPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.okPanel.Location = new System.Drawing.Point(0, 47);
			this.okPanel.Name = "okPanel";
			this.okPanel.Size = new System.Drawing.Size(239, 52);
			this.okPanel.TabIndex = 16;
			// 
			// optionsPanel
			// 
			this.optionsPanel.Controls.Add(this.electionCycleLabel);
			this.optionsPanel.Controls.Add(this.electionPicker);
			this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optionsPanel.Location = new System.Drawing.Point(0, 0);
			this.optionsPanel.Name = "optionsPanel";
			this.optionsPanel.Size = new System.Drawing.Size(239, 99);
			this.optionsPanel.TabIndex = 15;
			// 
			// CampOptionsDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(239, 99);
			this.Controls.Add(this.okPanel);
			this.Controls.Add(this.optionsPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CampOptionsDialog";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.CampOptionsDialog_Load);
			this.okPanel.ResumeLayout(false);
			this.optionsPanel.ResumeLayout(false);
			this.optionsPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private ElectionPicker electionPicker;
		private System.Windows.Forms.Label electionCycleLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private Panel okPanel;
		private Panel optionsPanel;
	}
}