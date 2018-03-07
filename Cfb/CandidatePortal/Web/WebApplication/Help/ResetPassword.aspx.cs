using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Web.WebApplication.Help
{
    /// <summary>
    /// Page for handling resetting of a forgotten password.
    /// </summary>
    public partial class ResetPasswordPage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            this.EnableViewState = false;
            if (!Page.IsPostBack)
            {
                _email.Focus();
            }
        }

        /// <summary>
        /// Attempts to look up a user by email address and associated candidate ID and reset that user's password.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void OnSubmit(object sender, EventArgs e)
        {
            string email = _email.Text.Trim();
            string cid = _cid.Text.Trim();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(cid))
                return;

            // try to find user
            CPUser user = CPSecurity.Provider.GetCampaignUsers(cid).FirstOrDefault(u => string.Equals(email, u.Email, StringComparison.InvariantCultureIgnoreCase));
            if (user == null)
            {
                _errorMessage.Visible = true;
                return;
            }

            // check for deactivated account
            if (!user.Enabled)
            {
                _deactivatedMessage.Visible = true;
                return;
            }

            // reset password
            if (!CPSecurity.Provider.ResetPassword(user.UserName))
            {
                Server.Transfer(PageUrlManager.ErrorPageUrl);
            }
            else
            {
                _resetPasswordPanel.Visible = false;
                _errorMessage.Visible = false;
                _successMessage.Visible = true;
            }
        }
    }
}