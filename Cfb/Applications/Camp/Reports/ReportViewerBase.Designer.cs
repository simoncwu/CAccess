namespace Cfb.Camp.Reports
{
	partial class ReportViewerBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewerBase));
            this.ReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportViewerToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.campaignToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ReportViewerToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            this.ReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.LocalReport.ReportEmbeddedResource = "Cfb.Camp.Reports.Empty.rdlc";
            this.ReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.Name = "ReportViewer";
            this.ReportViewer.Size = new System.Drawing.Size(754, 330);
            this.ReportViewer.TabIndex = 1;
            // 
            // ReportViewerToolStrip
            // 
            this.ReportViewerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.runToolStripButton,
            this.campaignToolStripButton});
            this.ReportViewerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerToolStrip.Name = "ReportViewerToolStrip";
            this.ReportViewerToolStrip.Size = new System.Drawing.Size(754, 25);
            this.ReportViewerToolStrip.TabIndex = 1;
            this.ReportViewerToolStrip.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // runToolStripButton
            // 
            this.runToolStripButton.Image = global::Cfb.Camp.Reports.Properties.Resources.Run_16x16;
            this.runToolStripButton.Name = "runToolStripButton";
            this.runToolStripButton.Size = new System.Drawing.Size(86, 22);
            this.runToolStripButton.Text = "R&un Report";
            this.runToolStripButton.ToolTipText = "Run Report";
            this.runToolStripButton.Click += new System.EventHandler(this.runToolStripButton_Click);
            // 
            // campaignToolStripButton
            // 
            this.campaignToolStripButton.Image = global::Cfb.Camp.Reports.Properties.Resources.Candidate_16x16;
            this.campaignToolStripButton.Name = "campaignToolStripButton";
            this.campaignToolStripButton.Size = new System.Drawing.Size(82, 22);
            this.campaignToolStripButton.Text = "&Campaign";
            this.campaignToolStripButton.ToolTipText = "Select Candidate and Election Cycle";
            this.campaignToolStripButton.Click += new System.EventHandler(this.campaignToolStripButton_Click);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // ReportViewerBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(754, 330);
            this.Controls.Add(this.ReportViewerToolStrip);
            this.Controls.Add(this.ReportViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(347, 340);
            this.Name = "ReportViewerBase";
            this.Shown += new System.EventHandler(this.ReportViewerBase_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportViewerBase_KeyDown);
            this.ReportViewerToolStrip.ResumeLayout(false);
            this.ReportViewerToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        protected Microsoft.Reporting.WinForms.ReportViewer ReportViewer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        protected System.Windows.Forms.ToolStripButton runToolStripButton;
        protected System.Windows.Forms.ToolStripButton campaignToolStripButton;
        protected System.Windows.Forms.ToolStrip ReportViewerToolStrip;

	}
}
