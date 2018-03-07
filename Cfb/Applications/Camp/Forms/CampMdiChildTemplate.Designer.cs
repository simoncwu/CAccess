namespace Cfb.Camp.Forms
{
	partial class CampMdiChildTemplate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampMdiChildTemplate));
			this.PrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.PageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			this.PrintDialog = new System.Windows.Forms.PrintDialog();
			this.PrintDocument = new System.Drawing.Printing.PrintDocument();
			this.SuspendLayout();
			// 
			// PrintPreviewDialog
			// 
			this.PrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.PrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.PrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
			this.PrintPreviewDialog.Enabled = true;
			this.PrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewDialog.Icon")));
			this.PrintPreviewDialog.Name = "PrintPreviewDialog";
			this.PrintPreviewDialog.UseAntiAlias = true;
			this.PrintPreviewDialog.Visible = false;
			// 
			// PrintDialog
			// 
			this.PrintDialog.AllowPrintToFile = false;
			this.PrintDialog.Document = this.PrintDocument;
			this.PrintDialog.UseEXDialog = true;
			// 
			// PrintDocument
			// 
			this.PrintDocument.OriginAtMargins = true;
			this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
			// 
			// CampMdiChildTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(341, 307);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CampMdiChildTemplate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CampMdiChild";
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.PrintPreviewDialog PrintPreviewDialog;
		protected System.Windows.Forms.PageSetupDialog PageSetupDialog;
		protected System.Drawing.Printing.PrintDocument PrintDocument;
		protected System.Windows.Forms.PrintDialog PrintDialog;
	}
}