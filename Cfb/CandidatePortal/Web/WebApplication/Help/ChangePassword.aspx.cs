using System;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.Security;

namespace Cfb.CandidatePortal.Web.WebApplication.Help
{
    public partial class ChangePassword : CPPage
    {
        /// <summary>
        /// The subject of the e-mail notification to be sent upon a successful password change.
        /// </summary>
        readonly string _emailSubject = CPProviders.SettingsProvider.ApplicationName + " Password Change Notification";

        const string _bodyFormat = @"Dear {0},

This e-mail is being sent to you because you recently changed your password for your NYC Campaign Finance Board {1} account. If you did not request this change, please contact our Candidate Services Unit immediately at {3}. You may also request a new, randomly generated password via the {1} web site at:

{2}/ResetPassword.aspx

NOTE: Please do not reply to this message, as e-mail sent to this address will not be answered.
";

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (Request.IsPitStop())
                this.MasterPageFile = PageUrlManager.MinimalMasterPageUrl;
        }

        /// <summary>
        /// Handles the <see cref="Control.Init" /> event that occurs as an instance of the page is being created.
        /// </summary>
        /// <param name="e"><see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // check for forced password change
            var user = this.CPUser;
            if (user != null && user.PasswordExpired)
            {
                _forceChangeMessage.Visible = _logoutButton.Visible = true;
                _cancelButton.Visible = false;
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.OnLoad" /> event that occurs as an instance of the page is being created.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_changePasswordPanel.Visible)
                _currentPassword.Focus();
        }

        /// <summary>
        /// Handles password change attempts and updates page contents accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected void SubmitChange(object sender, EventArgs e)
        {
            _wrongPasswordMessage.Visible = false;
            _usernameAsPasswordMessage.Visible = false;
            if (Page.IsValid)
            {
                var user = this.CPUser;
                if (_newPassword.Text.Equals(user.UserName, StringComparison.InvariantCultureIgnoreCase))
                {
                    _usernameAsPasswordMessage.Visible = true;
                }
                else
                {
                    if (user.ChangePassword(_currentPassword.Text, _newPassword.Text)) // TODO: implement ChangePassword security service web method
                    {
                        // unexpire the password
                        if (user != null && user.PasswordExpired)
                        {
                            user.PasswordExpired = false;
                            user = CPSecurity.Provider.UpdateUser(user).User;
                        }
                        // present success confirmation
                        _changePasswordPanel.Visible = false;
                        _successPanel.Visible = true;
                        if (Request.IsPitStop())
                        {
                            this.ReturnToHomeLink.NavigateUrl = Request.GetReturnUrl();
                            this.ReturnToHomeLink.Text = this.ReturnToHomeLink.ToolTip = "Continue";
                        }
                        // send confirmation e-mail
                        CPMailMessage message = new CPMailMessage();
                        MailAddress email = CPProfile.GetMailAddress();
                        message.Recipient = email;
                        message.Subject = _emailSubject;
                        message.Body = string.Format(_bodyFormat, email.DisplayName, CPProviders.SettingsProvider.ApplicationName, CPProviders.SettingsProvider.ApplicationUrl, CPProviders.SettingsProvider.AgencyPhoneNumber);
                        message.Send();
                        // BUGFIX #18 - re-add authentication cookie
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                    }
                    else
                    {
                        _wrongPasswordMessage.Visible = true;
                    }
                }
            }
        }
    }
}