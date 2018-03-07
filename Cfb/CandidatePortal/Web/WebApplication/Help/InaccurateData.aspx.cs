using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Web.Controls;
using Microsoft.SharePoint;

namespace Cfb.CandidatePortal.Web.WebApplication.Help
{
    /// <summary>
    /// A contact page.
    /// </summary>
    public partial class InaccurateData : CPPage, ISecurePage
    {
        /// <summary>
        /// The subject of the e-mail to be generated.
        /// </summary>
        private const string _subject = "Data Inaccuracy Issues";

        /// <summary>
        /// Sends an e-mail message with the contents of the form when submitted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSubmit(object sender, EventArgs e)
        {
            StringBuilder body = new StringBuilder();
            MembershipUser user = Membership.GetUser();
            body.AppendLine("Sender Details:");
            body.AppendLine("---------------");
            body.AppendFormat("{0} Username: {1}\r\n", CPProviders.SettingsProvider.ApplicationName, user.UserName);
            body.AppendLine("Candidate ID: " + CPProfile.Cid);
            body.AppendLine("Candidate Name: " + CPProfile.ActiveCandidate.Name);
            body.AppendLine("Election Cycle: " + CPProfile.ElectionCycle);
            body.AppendLine("---------------");
            body.AppendLine();
            body.AppendLine("Message Details:");
            body.AppendLine("----------------");
            body.AppendLine(Message.Text);
            // assemble message
            CPMailMessage message = new CPMailMessage();
            message.Recipient = new MailAddress(CPApplication.CsuEmail);
            message.Sender = CPProfile.GetMailAddress();
            message.Subject = _subject;
            message.Body = body.ToString();
            // send message
            message.Send();
            _contactForm.Visible = false;
            _confirmation.Visible = true;
        }
    }
}