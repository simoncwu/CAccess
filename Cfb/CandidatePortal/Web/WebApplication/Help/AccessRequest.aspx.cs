using System;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.Web.WebApplication.Help
{
    public partial class AccessRequest : CPPage, ISecurePage
    {
        /// <summary>
        /// Command name for account requests for liaisons.
        /// </summary>
        private const string LiaisonCommandName = "Liaison";

        /// <summary>
        /// Command name for account requests for treasurers.
        /// </summary>
        private const string TreasurerCommandName = "Treasurer";

        /// <summary>
        /// Command name for account requests for consultants.
        /// </summary>
        private const string ConsultantCommandName = "Consultant";

        /// <summary>
        /// Command name for account requests for the candidate.
        /// </summary>
        private const string CandidateCommandName = "Candidate";

        /// <summary>
        /// The subject of the e-mail to be generated.
        /// </summary>
        private const string _subjectFormat = "{0} Account Request (Candidate: {1})";

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.EnsureChildControls();
            if (!Page.IsPostBack)
            {
                AnalysisResults results = CPProfile.AnalysisResults;
                results.BypassEmail(CPSecurity.Provider.GetEmail(User.Identity.Name));
                // popuplate eligible accounts
                _contactsList.Items.Clear();
                foreach (string id in results.EligibleContacts.Keys)
                {
                    _contactsList.Items.Add(new ListItem(results.EligibleContacts[id].Name, id));
                }
                if (_contactsList.Items.Count == 0)
                {
                    _getContact.Enabled = false;
                    _contactsList.Visible = false;
                    _noContactsMessage.Visible = true;
                }

                // populate current accounts
                foreach (var user in results.CurrentUsers)
                {
                    _currentUsersList.Items.Add(string.Format("{0} ({1})", user.DisplayName, user.Email));
                }
                if (_currentUsersList.Items.Count == 0)
                {
                    _currentUsersList.Visible = false;
                    _noUsersMessage.Visible = true;
                }

                // populate ineligible accounts
                if (results.IneligibleContacts.Count > 0)
                {
                    _ineligibleUsers.Visible = true;
                    _ineligibleUsers.DataSource = results.IneligibleContacts;
                    _ineligibleUsers.DataBind();
                }
            }
        }

        /// <summary>
        /// Sends a new account request for the currently loaded contact.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> that contains the event data.</param>
        protected void RequestAccountCommand(object sender, CommandEventArgs e)
        {
            string contactType = "Unknown";
            switch (e.CommandName)
            {
                case LiaisonCommandName:
                case TreasurerCommandName:
                case ConsultantCommandName:
                case CandidateCommandName:
                    contactType = e.CommandName;
                    break;
            }
            // send access request for user via e-mail
            StringBuilder body = new StringBuilder();
            MembershipUser requestor = Membership.GetUser();
            MailAddress requestorEmail = CPProfile.GetMailAddress();
            body.AppendFormat("{0} user {1} (username: {2}) has requested an additional account for the following campaign contact:", CPProviders.SettingsProvider.ApplicationName, requestorEmail.DisplayName, requestor.UserName);
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("First Name: " + (_firstNameResult.Text = _firstName.Text));
            body.AppendLine("Middle Initial: " + (_miResult.Text = _mi.Text));
            body.AppendLine("Last Name: " + (_lastNameResult.Text = _lastName.Text));
            body.AppendLine("E-mail Address: " + (_emailResult.Text = _email.Text));
            body.AppendLine();
            body.AppendLine("Candidate ID: " + CPProfile.Cid);
            body.AppendLine("Contact Type: " + contactType);
            // assemble message
            CPMailMessage message = new CPMailMessage();
            message.Sender = requestorEmail;
            message.Recipient = new MailAddress(CPApplication.CsuEmail);
            message.Subject = string.Format(_subjectFormat, CPProviders.SettingsProvider.ApplicationName, CPProfile.ActiveCandidate.Name);
            message.Body = body.ToString();
            // send message
            message.Send();
            _successPanel.Visible = true;
            _contactDetails.Visible = _contactSelection.Visible = false;
        }

        /// <summary>
        /// Retrieves the details of a contact for a user account request.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> that contains the event data.</param>
        protected void FetchContactCommand(object sender, CommandEventArgs e)
        {
            // retrieve treasurer or liaison/consultant contact details for display in create panel
            AnalysisResults results = CPProfile.AnalysisResults;
            Entity contact;
            string selection = _contactsList.SelectedValue;
            _contactSelection.Visible = false;
            if (_contactDetails.Visible = results.EligibleContacts.TryGetValue(selection, out contact))
            {
                _firstName.Text = contact.FirstName;
                _mi.Text = contact.MiddleInitial.HasValue ? contact.MiddleInitial.Value.ToString() : null;
                _lastName.Text = contact.LastName;
                _email.Text = contact.Email;
                switch (AccountAnalysis.ParseEntityType(selection))
                {
                    case EntityType.Liaison: _submitRequest.CommandName = LiaisonCommandName; break;
                    case EntityType.Treasurer: _submitRequest.CommandName = TreasurerCommandName; break;
                    case EntityType.Consultant: _submitRequest.CommandName = ConsultantCommandName; break;
                    case EntityType.Candidate: _submitRequest.CommandName = CandidateCommandName; break;
                }
            }
            else
            {
                //_errorPanel.Visible = true;
            }
        }
    }
}