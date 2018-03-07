namespace Cfb.Camp.Cmo
{
	partial class CandidateMessagesReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.CmoMessageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.CmoMessageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            reportDataSource1.Name = "Cfb_CandidatePortal_Cmo_CmoMessage";
            reportDataSource1.Value = this.CmoMessageBindingSource;
            this.ReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewer.LocalReport.ReportEmbeddedResource = "Cfb.Camp.Cmo.Reports.CandidateMessagesReport.rdlc";
            this.ReportViewer.Size = new System.Drawing.Size(754, 348);
            this.ReportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // CandidateMessagesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(754, 348);
            this.MinimumSize = new System.Drawing.Size(402, 386);
            this.Name = "CandidateMessagesReport";
            this.Text = "Candidate Message Details";
            ((System.ComponentModel.ISupportInitialize)(this.CmoMessageBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.BindingSource CmoMessageBindingSource;

	}
}