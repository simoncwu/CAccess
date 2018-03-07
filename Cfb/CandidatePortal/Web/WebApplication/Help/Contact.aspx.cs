using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Web.Controls;
using Cfb.CandidatePortal.Web.WebApplication;

namespace Cfb.CandidatePortal.Web.WebApplication.Help
{
    /// <summary>
    /// A contact page.
    /// </summary>
    public partial class Contact : CPPage
    {
        protected void OnSubmit(object sender, EventArgs e)
        {
            string toEmail;
            string subject;
            switch (Category.SelectedValue.ToLowerInvariant())
            {
                case "audit":
                    toEmail = CPApplication.AuditEmail;
                    subject = Resources.CPResources.audit_email_subject;
                    break;
                case "csu":
                    toEmail = CPApplication.CsuEmail;
                    subject = Resources.CPResources.csu_email_subject;
                    break;
                default:
                    toEmail = CPApplication.GeneralEmail;
                    subject = Resources.CPResources.general_email_subject;
                    break;
            }
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            StringBuilder body = new StringBuilder();
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
            message.Sender = CPProfile.GetMailAddress();
            message.Recipient = new MailAddress(toEmail);
            message.Subject = subject;
            message.Body = body.ToString();
            // send message
            message.Send();
            _contactForm.Visible = false;
            _confirmation.Visible = true;
        }
    }
}