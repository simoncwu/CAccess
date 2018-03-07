using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using Cfb.Camp.Cmo;
using Cfb.Camp.Forms;
using Cfb.Camp.Security;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Cmo;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.Camp
{
    /// <summary>
    /// The main form workspace for the C-Access Management Program.
    /// </summary>
    public partial class CampForm : Cfb.Camp.Forms.CampMdiParent
    {
        /// <summary>
        /// Gets the main toolstrip for the form.
        /// </summary>
        public override ToolStrip MainToolStrip
        {
            get { return this.toolStrip; }
        }

        /// <summary>
        /// Gets the client size of the client area of the form, adjusted to not include any strips.
        /// </summary>
        public override Size AdjustedClientSize
        {
            get { return new Size(this.ClientSize.Width, this.ClientSize.Height - menuStrip.Height - toolStrip.Height); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CampMdiParent"/> form.
        /// </summary>
        public CampForm()
        {
            InitializeComponent();
            this.securityToolStripMenuItem.Visible = this.newAccountToolStripMenuItem.Visible = false;
            this.Location = Properties.Settings.Default.CampLocation;
            this.Width = Properties.Settings.Default.CampWidth;
            this.Height = Properties.Settings.Default.CampHeight;
            this.WindowState = Properties.Settings.Default.CampWindowState;
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// Handles the event that occurs when creating a new Campaign Messages Online message.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        private void CreateNewMessage(object sender, EventArgs e)
        {
            this.SpawnMdiChild<CmoMessageForm>();
        }

        /// <summary>
        /// Handles the event that occurs when searching for Campaign Messages Online messages.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void FindMessages(object sender, EventArgs e)
        {
            this.SpawnMdiChild<CmoMessageSearchForm>();
        }

        /// <summary>
        /// Handles the event that occurs when reporting messages by candidate.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void ReportMessageDetailByCandidate(object sender, EventArgs e)
        {
            this.SpawnMdiChild<CandidateMessagesReport>();
        }

        /// <summary>
        /// Handles the event that occurs when reporting messages by opened status.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void ReportMessageStatusSummary(object sender, EventArgs e)
        {
            this.SpawnMdiChild<PostedMessagesStatusReport>();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void CampMdiParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.CampWindowState = this.WindowState;
            Properties.Settings.Default.Save();
        }

        private void fileMenu_DropDownOpening(object sender, EventArgs e)
        {
            ICampMdiParentMergeable child = this.ActiveMdiChild as ICampMdiParentMergeable;
            if (child != null)
                child.FileMenuDropDownOpening(sender, e);
        }

        private void viewMenu_DropDownOpening(object sender, EventArgs e)
        {
            ICampMdiParentMergeable child = this.ActiveMdiChild as ICampMdiParentMergeable;
            if (child != null)
                child.ViewMenuDropDownOpening(sender, e);
        }

        private void toolsMenu_DropDownOpening(object sender, EventArgs e)
        {
            ICampMdiParentMergeable child = this.ActiveMdiChild as ICampMdiParentMergeable;
            if (child != null)
                child.ToolsMenuDropDownOpening(sender, e);
        }

        private void windowsMenu_DropDownOpening(object sender, EventArgs e)
        {
            ICampMdiParentMergeable child = this.ActiveMdiChild as ICampMdiParentMergeable;
            if (child != null)
                child.WindowsMenuDropDownOpening(sender, e);
        }

        private void helpMenu_DropDownOpening(object sender, EventArgs e)
        {
            ICampMdiParentMergeable child = this.ActiveMdiChild as ICampMdiParentMergeable;
            if (child != null)
                child.HelpMenuDropDownOpening(sender, e);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CampOptionsDialog dialog = new CampOptionsDialog())
            {
                dialog.ShowDialog();
            }
        }

        private void CampForm_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.CampLocation = this.Location;
            }
        }

        private void CampForm_ResizeEnd(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.CampWidth = this.Size.Width;
                Properties.Settings.Default.CampHeight = this.Size.Height;
            }
        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SpawnMdiChild<GroupAdministrationForm>();
        }

        private void CampForm_Shown(object sender, EventArgs e)
        {
            new MethodInvoker(() =>
            {
                this.newAccountToolStripMenuItem.Visible = this.accountToolStripMenuItem1.Visible = this.accountExplorerToolStripMenuItem.Visible = this.accountsToolStripDropDownButton.Visible = User.Current.IsMemberOf(CPProviders.SettingsProvider.AccountManagersGroupName);

                // admin-only options
                this.newAccountToolStripDropDownMenuItem.Visible = this.newAccountToolStripMenuItem.Visible = this.securityToolStripMenuItem.Visible = SettingsManager.AdministratorModeEnabled;
            }).BeginInvoke(null, null);
        }

        /// <summary>
        /// Handles the event that occurs when opening a Campaign Messages Online message by ID.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void OpenMessage(object sender, EventArgs e)
        {
            using (InputDialog dialog = new InputDialog()
            {
                Title = "Open Message",
                Prompt = "Message ID:",
                ValidationErrorText = "Invalid ID specified or message not found.",
                Icon = Properties.Resources.Email
            })
            {
                CmoMessage selection = null;
                dialog.ValidatingResponse += (string value) =>
                {
                    selection = CmoProviders.DataProvider.GetCmoMessage(value);
                    return selection != null;
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.SpawnMdiChild<CmoMessageForm>((CmoMessageForm m) =>
                    {
                        m.Message = selection;
                    });
                }
            }
        }

        private void ShowAccountExplorer(object sender, EventArgs e)
        {
            this.SpawnMdiChild<AccountExplorer>();
        }

        private void ReportAllAccounts(object sender, EventArgs e)
        {
            this.SpawnMdiChild<UserAccountsReport>().Title = "All User Accounts";
        }

        private void newAccountToolStripDropDownMenuItem_Click(object sender, EventArgs e)
        {
            UserAccountForm.CreateNewAccountForm(null, null, null, null).ShowAsOwnedBy(this);
        }

        private void findAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SpawnMdiChild<UserAccountSearchForm>();
        }
    }
}

