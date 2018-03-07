namespace Cfb.Camp.Cmo
{
    partial class GenericMessagesReport
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.CmoMessageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            reportDataSource1.Name = "Cfb_CandidatePortal_Cmo_CmoMessage";
            reportDataSource1.Value = this.CmoMessageBindingSource;
            this.ReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewer.LocalReport.ReportEmbeddedResource = "Cfb.Camp.Cmo.Reports.GenericMessagesReport.rdlc";
            this.ReportViewer.Location = new System.Drawing.Point(0, 25);
            this.ReportViewer.Size = new System.Drawing.Size(754, 323);
            this.ReportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // CmoMessageBindingSource
            // 
            this.CmoMessageBindingSource.DataSource = typeof(Cfb.CandidatePortal.Cmo.CmoMessage);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 4;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // GenericMessagesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(754, 348);
            this.MinimumSize = new System.Drawing.Size(402, 386);
            this.Name = "GenericMessagesReport";
            this.Text = "C-Access Report: Campaign Messages";
            this.Load += new System.EventHandler(this.GenericMessagesReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CmoMessageBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource CmoMessageBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}
