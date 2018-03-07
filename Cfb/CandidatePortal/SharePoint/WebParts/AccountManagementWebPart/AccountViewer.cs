using System;
using System.Linq;
using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.SharePoint.WebParts.Properties;
using Cfb.CandidatePortal.Web.Security;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <summary>
    /// Control for displaying portal account details for a candidate.
    /// </summary>
    internal class AccountViewer : AccountManagementControl
    {
        /// <summary>
        /// Javascript for password reset prompt.
        /// </summary>
        const string PasswordResetJavaScript = @"
    //<![CDATA[
    function confirmReset(username,email){{if (confirm(""WARNING!!!\n\nYou are about to reset the password for user ""+username+"". This process is irreversible,\nand the original password cannot be recovered. This will also send an e-mail to the\nuser at ""+email+"".\n\nClick [OK] if you understand and wish to continue; otherwise, click [Cancel]."")) return confirmResetFinal();return false;}}
    function confirmResetFinal(){{if (confirm(""Final confirmation for password reset.\n\nContinue?"")) return true;return false;}}
    //]]>";

        #region Command Names

        /// <summary>
        /// Command name value for CFIS synchronization action.
        /// </summary>
        const string SynchronizeCommandName = "Synchronize";

        /// <summary>
        /// Command name value for return action.
        /// </summary>
        const string ReturnCommandName = "Return";

        /// <summary>
        /// Command name value for password reset action.
        /// </summary>
        const string ResetPasswordCommandName = "Reset Password";

        /// <summary>
        /// Command name value for account impersonation action.
        /// </summary>
        const string ImpersonateCommandName = "Impersonate";

        /// <summary>
        /// Command name value for unlocking an account.
        /// </summary>
        const string UnlockCommandName = "Unlock Account";

        /// <summary>
        /// Command name value for activating an account.
        /// </summary>
        const string ActivateCommandName = "Activate Account";

        /// <summary>
        /// Command name value for deactivating an account.
        /// </summary>
        const string DeactivateCommandName = "Deactivate Account";

        /// <summary>
        /// Command name value for editing a comment.
        /// </summary>
        const string EditCommentCommandName = "[edit]";

        /// <summary>
        /// Command name value for saving a comment.
        /// </summary>
        const string SaveCommentCommandName = "[save]";

        /// <summary>
        /// Command name value for cancelling a comment change.
        /// </summary>
        const string CancelCommentCommandName = "[cancel]";

        #endregion

        #region Child Controls

        /// <summary>
        /// Button for synchronizing e-mail address and display name with CFIS.
        /// </summary>
        Button _synchronizeButton;

        /// <summary>
        /// Button for submitting close actions.
        /// </summary>
        Button _returnButton;

        /// <summary>
        /// Button for resetting the password on an account.
        /// </summary>
        Button _resetPasswordButton;

        /// <summary>
        /// Button for initiating account impersonation.
        /// </summary>
        Button _impersonateButton;

        /// <summary>
        /// Button for unlocking a locked account.
        /// </summary>
        Button _unlockButton;

        /// <summary>
        /// Button for activating or deactiving an account.
        /// </summary>
        Button _activationButton;

        /// <summary>
        /// LinkButton for editing comments.
        /// </summary>
        LinkButton _editCommentButton;

        /// <summary>
        /// LinkButton for cancelling comment changes.
        /// </summary>
        LinkButton _cancelCommentButton;

        /// <summary>
        /// Label for showing the associated candidate's name.
        /// </summary>
        Label _candidateName;

        /// <summary>
        /// Label for showing an account's C-Access user name.
        /// </summary>
        Label _username;

        /// <summary>
        /// Label for displaying the type of contact being issued a new account.
        /// </summary>
        Label _contactType;

        /// <summary>
        /// Label for showing an account's display name.
        /// </summary>
        Label _displayName;

        /// <summary>
        /// Label for showing an account's e-mail address.
        /// </summary>
        Label _email;

        /// <summary>
        /// Checkbox for indicating active status.
        /// </summary>
        CheckBox _activeStatus;

        /// <summary>
        /// Checkbox for indicating locked-out status.
        /// </summary>
        CheckBox _lockOutStatus;

        /// <summary>
        /// Checkbox for indicating online status.
        /// </summary>
        CheckBox _onlineStatus;

        /// <summary>
        /// Label for showing the date and time when the user was last authenticated or accessed the candidate portal.
        /// </summary>
        Label _lastActivity;

        /// <summary>
        /// Label for showing the date and time when the user was last locked out.
        /// </summary>
        Label _lastLockedOut;

        /// <summary>
        /// Label for showing the date and time when the user was last authenticated.
        /// </summary>
        Label _lastLoggedIn;

        /// <summary>
        /// Label for showing the date and time when the user last performed a password change.
        /// </summary>
        Label _lastPasswordChange;

        /// <summary>
        /// Label for showing the comment for the user account.
        /// </summary>
        Label _comment;

        /// <summary>
        /// Hidden field for persisting C-Access ID across postbacks.
        /// </summary>
        HiddenField _caid;

        /// <summary>
        /// TextBox for changing the comment for the user account.
        /// </summary>
        TextBox _newComment;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the <see cref="AccountViewer"/> is closed.
        /// </summary>
        public event AccountManagementEventHandler Closed;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCreator"/> control.
        /// </summary>
        public AccountViewer()
        {
        }

        #region Page Life Cycle Events

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            // add confirmation prompt for password reset
            HtmlGenericControl headerJS = new HtmlGenericControl("script");
            headerJS.Attributes["type"] = "text/javascript";
            headerJS.InnerHtml = PasswordResetJavaScript;
            Page.Header.Controls.Add(headerJS);
            _resetPasswordButton = new Button();
            _resetPasswordButton.Command += ExecuteCommand;
            _resetPasswordButton.CommandName = _resetPasswordButton.Text = ResetPasswordCommandName;
            _synchronizeButton = new Button();
            _synchronizeButton.Command += ExecuteCommand;
            _synchronizeButton.CommandName = _synchronizeButton.Text = SynchronizeCommandName;
            _impersonateButton = new Button();
            _impersonateButton.Command += ExecuteCommand;
            _impersonateButton.CommandName = _impersonateButton.Text = ImpersonateCommandName;
            _returnButton = new Button();
            _returnButton.Command += ExecuteCommand;
            _returnButton.CommandName = _returnButton.Text = ReturnCommandName;
            _unlockButton = new Button();
            _unlockButton.Command += ExecuteCommand;
            _unlockButton.CommandName = _unlockButton.Text = UnlockCommandName;
            _activationButton = new Button();
            _activationButton.Command += ExecuteCommand;
            _editCommentButton = new LinkButton();
            _editCommentButton.Command += ExecuteCommand;
            _editCommentButton.CommandName = _editCommentButton.Text = EditCommentCommandName;
            _cancelCommentButton = new LinkButton();
            _cancelCommentButton.Command += ExecuteCommand;
            _cancelCommentButton.CommandName = _cancelCommentButton.Text = CancelCommentCommandName;
            _candidateName = new Label();
            _username = new Label();
            _contactType = new Label();
            _displayName = new Label();
            _email = new Label();
            _activeStatus = new CheckBox();
            _lockOutStatus = new CheckBox();
            _onlineStatus = new CheckBox();
            _activeStatus.Enabled = _lockOutStatus.Enabled = _onlineStatus.Enabled = false;
            _lastActivity = new Label();
            _lastLockedOut = new Label();
            _lastLoggedIn = new Label();
            _lastPasswordChange = new Label();
            _comment = new Label();
            _caid = new HiddenField();
            _newComment = new TextBox();
            _newComment.TextMode = TextBoxMode.MultiLine;
            _newComment.Rows = 8;
            _newComment.Columns = 40;
            _newComment.Visible = false;
            Controls.Add(_synchronizeButton);
            Controls.Add(_resetPasswordButton);
            Controls.Add(_impersonateButton);
            Controls.Add(_returnButton);
            Controls.Add(_unlockButton);
            Controls.Add(_activationButton);
            Controls.Add(_editCommentButton);
            Controls.Add(_cancelCommentButton);
            Controls.Add(_candidateName);
            Controls.Add(_username);
            Controls.Add(_contactType);
            Controls.Add(_displayName);
            Controls.Add(_email);
            Controls.Add(_activeStatus);
            Controls.Add(_lockOutStatus);
            Controls.Add(_onlineStatus);
            Controls.Add(_lastActivity);
            Controls.Add(_lastLockedOut);
            Controls.Add(_lastLoggedIn);
            Controls.Add(_lastPasswordChange);
            Controls.Add(_comment);
            Controls.Add(_caid);
            Controls.Add(_newComment);
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetView(RenderViewerView);
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine("<div class=\"cp-{0}\">", typeof(AccountViewer).Name);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.WriteLine("</div>");
        }

        /// <summary>
        /// Renders controls for account creation.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderViewerView(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(_messageText))
            {
                writer.WriteLine(@"
    <table class=""ms-informationbar"" border=""0"" cellpadding=""2"" cellspacing=""0"">
        <tr>
            <td>{0}</td>
        </tr>
    </table>
    <br />", _messageText);
            }
            writer.Write(@"
    <table class=""ms-formtable"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Selected Candidate</h3>
            </td>
            <td class=""ms-formbody"">");
            _candidateName.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">C-Access Username</h3>
            </td>
            <td class=""ms-formbody"">");
            _username.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Contact Type</h3>
            </td>
            <td class=""ms-formbody"">");
            _contactType.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Display Name</h3>
            </td>
            <td class=""ms-formbody"">");
            _displayName.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">E-mail Address</h3>
            </td>
            <td class=""ms-formbody"">");
            _email.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Account Is Active?</h3>
            </td>
            <td class=""ms-formbody"">");
            _activeStatus.RenderControl(writer);
            writer.Write(" ");
            _activationButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Last Logged In</h3>
            </td>
            <td class=""ms-formbody"">");
            _lastLoggedIn.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">User Is Online?</h3>
            </td>
            <td class=""ms-formbody"">");
            _onlineStatus.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Last Activity</h3>
            </td>
            <td class=""ms-formbody"">");
            _lastActivity.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">User Is Locked Out?</h3>
            </td>
            <td class=""ms-formbody"">");
            _lockOutStatus.RenderControl(writer);
            writer.Write(" ");
            _unlockButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Last Locked Out</h3>
            </td>
            <td class=""ms-formbody"">");
            _lastLockedOut.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Last Password Change</h3>
            </td>
            <td class=""ms-formbody"">");
            _lastPasswordChange.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Comment</h3>
            </td>
            <td class=""ms-formbody"">");
            if (_newComment.Visible)
                _newComment.RenderControl(writer);
            else
                _comment.RenderControl(writer);
            writer.Write("<br />");
            _editCommentButton.RenderControl(writer);
            if (_newComment.Visible)
            {
                writer.Write(" ");
                _cancelCommentButton.RenderControl(writer);
            }
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td colspan=""2"" style=""text-align:right;"">");
            _resetPasswordButton.RenderControl(writer);
            writer.Write("<span style=\"display:none\">&nbsp;");
            _synchronizeButton.RenderControl(writer);
            writer.Write("</span>&nbsp;");
            _impersonateButton.RenderControl(writer);
            writer.Write("&nbsp;");
            _returnButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
    </table>");
            _caid.RenderControl(writer);
        }

        #endregion

        /// <summary>
        /// Handles viewer postback commands.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        void ExecuteCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case AccountViewer.ReturnCommandName:
                    // return to account management screen
                    if (Closed != null)
                    {
                        Closed(this, new AccountManagementEventArgs());
                    }
                    break;
                case AccountViewer.SynchronizeCommandName:
                    // synchronize current account
                    var result = CPSecurity.Provider.SynchronizeUser(_user.UserName);
                    var updated = result.User;
                    switch (result.State)
                    {
                        case UpdateResultState.CfisContactNotFound:
                            MessageText = string.Format("<span class=\"ms-error\">Unable to find a matching contact in CFIS for the current user. Please contact an Administrator for further assistance. (CAID: {0})</span>", _caid.Value);
                            break;
                        case UpdateResultState.EmailAddressInUse:
                            MessageText = "<span class=\"ms-error\">Error: New e-mail address is already registered with another account for the current campaign.</span>";
                            break;
                        case UpdateResultState.Success:
                            MessageText = "There are no disparities to synchronize.";
                            break;
                        default:
                            if ((result.State & UpdateResultState.DisplayNameChanged) == UpdateResultState.DisplayNameChanged)
                                MessageText += "<li>Display name updated.</li>";
                            if ((result.State & UpdateResultState.EmailAddressChanged) == UpdateResultState.EmailAddressChanged)
                                MessageText += "<li>E-mail address updated.</li>";
                            ReloadUser();
                            MessageText = "<span class=\"ms-error\">An error occurred while updating the user. Please see an Administrator for assistance.</span>";
                            break;
                    }
                    break;
                case AccountViewer.ResetPasswordCommandName:
                    // reset password on account
                    if (_user != null)
                    {
                        CPUser user = CPSecurity.Provider.GetUser(_user.UserName);
                        if (user.ResetPassword())
                        {
                            MessageText = string.Format("Password successfully reset. An e-mail was also automatically sent to the user at <strong>{0}</strong>.", _user.Email);
                        }
                        else
                        {
                            MessageText = "<span class=\"ms-error\">Unlock attempt failed. Please see an Administrator for assistance.</span>";
                        }
                        ReloadUser();
                    }
                    break;
                case AccountViewer.ImpersonateCommandName:
                    // TODO: add code for impersonating current account
                    MessageText = "Sorry, impersonation is not yet available.";
                    break;
                case UnlockCommandName:
                    // unlock account
                    if (_user != null)
                    {
                        MessageText = UserManagement.UnlockUser(_user) ? "Account successfully unlocked." : "<span class=\"ms-error\">Unlock attempt failed. Please see an Administrator for assistance.</span>";
                        ReloadUser();
                        GetLockoutStatus(_user);
                    }
                    break;
                case ActivateCommandName:
                    // activate account
                    if (_user != null)
                    {
                        MessageText = UserManagement.SetStatus(_user, true) ? "Account successfully activated." : "<span class=\"ms-error\">Account activation attempt failed. Please see an Administrator for assistance.</span>";
                        ReloadUser();
                        GetActiveStatus(_user);
                    }
                    break;
                case DeactivateCommandName:
                    // deactivate account
                    if (_user != null)
                    {
                        MessageText = UserManagement.SetStatus(_user, false) ? "Account successfully deactivated." : "<span class=\"ms-error\">Account deactivation attempt failed. Please see an Administrator for assistance.</span>";
                        ReloadUser();
                        GetActiveStatus(_user);
                    }
                    break;
                case EditCommentCommandName:
                    _newComment.Visible = true;
                    _editCommentButton.CommandName = _editCommentButton.Text = SaveCommentCommandName;
                    break;
                case SaveCommentCommandName:
                    _user.Comment = _newComment.Text;
                    Membership.UpdateUser(_user);
                    ReloadUser();
                    _newComment.Visible = false;
                    _editCommentButton.CommandName = _editCommentButton.Text = EditCommentCommandName;
                    break;
                case CancelCommentCommandName:
                    ReloadUser();
                    _newComment.Visible = false;
                    _editCommentButton.CommandName = _editCommentButton.Text = EditCommentCommandName;
                    break;
            }
        }

        /// <summary>
        /// Targets account viewer controls toward a specific user account.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> account to use.</param>
        public new void SetUser(MembershipUser user)
        {
            // check for null user
            if (object.Equals(user, null)) return;
            // check for known user
            if (object.Equals(Membership.GetUser(user.ProviderUserKey), null)) return;
            base.SetUser(user);
            if (_candidate != null)
                _candidateName.Text = string.Format("{0} (ID: {1})", _candidate.Name, _candidate.ID);
            _username.Text = user.UserName;
            _displayName.Text = UserManagement.GetFullName(user);
            _email.Text = user.Email;
            GetActiveStatus(user);
            GetLockoutStatus(user);
            _onlineStatus.Checked = user.IsOnline;
            _lastActivity.Text = user.LastActivityDate.Equals(user.CreationDate) ? Properties.Resources.NeverActiveText : user.LastActivityDate.ToString();
            _lastLockedOut.Text = user.LastLockoutDate < user.CreationDate ? Properties.Resources.NeverLockedOutText : user.LastLockoutDate.ToString();
            _lastLoggedIn.Text = user.LastLoginDate.Equals(user.CreationDate) ? Properties.Resources.NeverLoggedInText : user.LastLoginDate.ToString();
            _lastPasswordChange.Text = user.LastPasswordChangedDate.Equals(user.CreationDate) ? Properties.Resources.NeverChangedPasswordText : user.LastPasswordChangedDate.ToString();
            _newComment.Text = user.Comment;
            _comment.Text = Page.Server.HtmlEncode(user.Comment).Replace("\n", "<br />");
            _resetPasswordButton.OnClientClick = string.Format("return confirmReset('{0}','{1}');", user.UserName, user.Email);
            string caid = UserManagement.GetCaid(user.UserName);
            _contactType.Text = AccountAnalysis.ParseEntityType(caid).ToString();
            _caid.Value = caid;
            _synchronizeButton.Enabled = !string.IsNullOrEmpty(caid);
        }

        /// <summary>
        /// Refreshes controls with the current lockout status of a <see cref="MembershipUser"/>.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> to use for refreshing.</param>
        void GetLockoutStatus(MembershipUser user)
        {
            _lockOutStatus.Checked = _unlockButton.Enabled = user.IsLockedOut;
        }

        /// <summary>
        /// Refreshes controls with the current active status of a <see cref="MembershipUser"/>.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> to use for refreshing.</param>
        void GetActiveStatus(MembershipUser user)
        {
            _activationButton.CommandName = _activationButton.Text = (_activeStatus.Checked = user.IsApproved) ? DeactivateCommandName : ActivateCommandName;
        }

        /// <summary>
        /// Sets the view to be rendered and updates the behavior of controls accordingly.
        /// </summary>
        /// <param name="renderer">A <see cref="AccountManagementControl.RenderDelegate"/> for rendering the control.</param>
        protected override void SetView(RenderDelegate renderer)
        {
            this._renderByView = renderer;
        }

        /// <summary>
        /// Reloads the current <see cref="MembershipUser"/> from the <see cref="Membership"/> database store.
        /// </summary>
        protected override void ReloadUser()
        {
            base.ReloadUser();
            SetUser(_user);
        }
    }
}
