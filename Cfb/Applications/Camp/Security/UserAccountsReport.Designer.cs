namespace Cfb.Camp.Security
{
    partial class UserAccountsReport
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.showDisabledButton = new System.Windows.Forms.ToolStripButton();
            this.ReportableUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportableUserBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            reportDataSource1.Name = "ReportableUserDataSource";
            reportDataSource1.Value = this.ReportableUserBindingSource;
            this.ReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewer.LocalReport.ReportEmbeddedResource = "Cfb.Camp.Security.Reports.CampaignAccountsReport.rdlc";
            this.ReportViewer.Location = new System.Drawing.Point(0, 25);
            this.ReportViewer.Size = new System.Drawing.Size(754, 323);
            this.ReportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDisabledButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(754, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Visible = false;
            // 
            // showDisabledButton
            // 
            this.showDisabledButton.CheckOnClick = true;
            this.showDisabledButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showDisabledButton.Name = "showDisabledButton";
            this.showDisabledButton.Size = new System.Drawing.Size(103, 22);
            this.showDisabledButton.Text = "Include Disabled?";
            this.showDisabledButton.ToolTipText = "Click to include disabled users";
            // 
            // ReportableUserBindingSource
            // 
            this.ReportableUserBindingSource.DataSource = typeof(Cfb.Camp.Security.ReportableUser);
            // 
            // CampaignAccountsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(754, 348);
            this.Controls.Add(this.toolStrip);
            this.MinimumSize = new System.Drawing.Size(402, 386);
            this.Name = "CampaignAccountsReport";
            this.Text = "";
            this.Load += new System.EventHandler(this.CampaignAccountsReport_Load);
            this.Controls.SetChildIndex(this.toolStrip, 0);
            this.Controls.SetChildIndex(this.ReportViewer, 0);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportableUserBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton showDisabledButton;
        private System.Windows.Forms.BindingSource ReportableUserBindingSource;

    }
}
