using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Cfb.Camp.Forms
{
    /// <summary>
    /// A base MDI child form template for the C-Access Management Program.
    /// </summary>
    public partial class CampMdiChildTemplate : Form, ICampMdiParentMergeable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CampMdiChildTemplate"/> form.
        /// </summary>
        public CampMdiChildTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the <see cref="Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            CampMdiParent parent = this.MdiParent as CampMdiParent;
            if (parent != null && this.StartPosition == FormStartPosition.CenterParent)
            {
                // center the form in the MDI area
                Size clientArea = parent.AdjustedClientSize;
                if (this.Width <= clientArea.Width && this.Height <= clientArea.Height)
                    this.Location = new Point((clientArea.Width - this.Width) / 2, (clientArea.Height - this.Height) / 2);
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// Occurs once for each page to be printed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="PrintPageEventArgs"/> that contains the event data.</param>
        protected virtual void OnPrintPage(object sender, PrintPageEventArgs e)
        {
        }

        /// <summary>
        /// Raises the <see cref="Form.Activated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            CampMdiParent parent = this.MdiParent as CampMdiParent;
            IEnumerable<ToolStrip> toolstrips = this.GetMergingToolStrips();
            if (parent != null && toolstrips != null && toolstrips.Count() > 0)
            {
                foreach (var strip in toolstrips)
                {
                    ToolStripManager.Merge(strip, parent.MainToolStrip);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Form.Deactivate"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            CampMdiParent parent = this.MdiParent as CampMdiParent;
            IEnumerable<ToolStrip> toolstrips = this.GetMergingToolStrips();
            if (parent != null && toolstrips != null && toolstrips.Count() > 0)
            {
                foreach (var strip in toolstrips)
                {
                    ToolStripManager.RevertMerge(parent.MainToolStrip, strip);
                }
            }
        }

        /// <summary>
        /// Shows a message indicating a generic transaction error.
        /// </summary>
        /// <returns>The <see cref="DialogResult"/> response to the message selected by the user.</returns>
        protected DialogResult ShowTransactionError()
        {
            return MessageBox.Show(this, Properties.Resources.TransactionErrorMessage, string.Format("{0} ({1})", Properties.Resources.TransactionErrorTitle, this.Text), MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a message indicating a generic transaction error.
        /// </summary>
        /// <param name="title">The title to assign the error message.</param>
        /// <returns>The <see cref="DialogResult"/> response to the message selected by the user.</returns>
        protected DialogResult ShowTransactionError(string title)
        {
            return MessageBox.Show(this, Properties.Resources.TransactionErrorMessage, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows a message indicating a concurrency check error.
        /// </summary>
        /// <param name="title">The title to assign the error message.</param>
        /// <returns>The <see cref="DialogResult"/> response to the message selected by the user.</returns>
        protected DialogResult ShowConcurrencyError(string title)
        {
            return MessageBox.Show(this, Properties.Resources.ConcurrencyErrorMessage, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        #region ICampMdiParentMergeable Members

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public virtual IEnumerable<ToolStrip> GetMergingToolStrips() { return null; }

        /// <summary>
        /// Occurs as the "File" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public virtual void FileMenuDropDownOpening(object sender, EventArgs e) { }

        /// <summary>
        /// Occurs as the "View" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public virtual void ViewMenuDropDownOpening(object sender, EventArgs e) { }

        /// <summary>
        /// Occurs as the "Tools" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public virtual void ToolsMenuDropDownOpening(object sender, EventArgs e) { }

        /// <summary>
        /// Occurs as the "Windows" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public virtual void WindowsMenuDropDownOpening(object sender, EventArgs e) { }

        /// <summary>
        /// Occurs as the "Help" menu is opening.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        public virtual void HelpMenuDropDownOpening(object sender, EventArgs e) { }

        #endregion
    }
}
