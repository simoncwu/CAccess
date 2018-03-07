using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService.Properties;

namespace Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService
{
    /// <summary>
    /// An e-mail message reminding a campaign's C-Access users of upcoming events.
    /// </summary>
    public class EventReminderMessage : IDisposable
    {
        /// <summary>
        /// The ID of the candidate for the campaign.
        /// </summary>
        private readonly string _candidateID;

        /// <summary>
        /// The <see cref="CPMailMessage"/> to send.
        /// </summary>
        private readonly CPMailMessage _mail;

        /// <summary>
        /// Gets or sets the posted message e-mail recipients.
        /// </summary>
        public IEnumerable<CPUser> Recipients { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReminderMessage"/> class.
        /// </summary>
        /// <param name="candidateID">The ID of the campaign candidate.</param>
        /// <param name="electionCycle">The election cycle during which the event occurs.</param>
        /// <param name="item">The upcoming event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="item"/> is not a supported event type. Supported events include Doing Business review responses only.</exception>
        public EventReminderMessage(string candidateID, string electionCycle, CPCalendarItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "The calendar item must not be null.");
            _candidateID = candidateID;
            _mail = new CPMailMessage();
            _mail.IsBodyHtml = true;
            Settings settings = Properties.Settings.Default;
            string siteUrl = CPProviders.SettingsProvider.ApplicationUrl;
            _mail.Sender = new MailAddress(string.IsNullOrEmpty(settings.SenderEmail) ? _mail.Sender.Address : settings.SenderEmail, settings.SenderDisplayName);

            // set up message subject and body based on event type
            if (item is DbrResponseDeadline)
            {
                DbrResponseDeadline deadline = item as DbrResponseDeadline;
                _mail.Subject = settings.DoingBusinessSubject;
                StringBuilder body = new StringBuilder();
                body.AppendFormat(settings.BodyTemplateHeader, siteUrl);
                body.AppendFormat(settings.DoingBusinessBodyTemplate, deadline.Date.Subtract(DateTime.Today).Days, electionCycle, deadline.ReviewSentDate);
                body.Append(settings.BodyTemplateFooter);
                _mail.Body = body.ToString();
            }
            else
            {
                throw new ArgumentException("item", Resources.UnsupportedExceptionText);
            }
        }

        /// <summary>
        /// Sends the current notification.
        /// </summary>
        /// <returns>A <see cref="NameValueCollection"/> mapping failed e-mail recipients to failure messages.</returns>
        public NameValueCollection Send()
        {
            NameValueCollection failedRecipients = new NameValueCollection();
            MailAddressCollection recipients = new MailAddressCollection();
            Settings settings = Properties.Settings.Default;
            string header = settings.BodyTemplateHeader;
            string footer = settings.BodyTemplateFooter;
            if (this.Recipients != null)
            {
                foreach (var cpuser in this.Recipients)
                {
                    try
                    {
                        MailAddress recipient = _mail.GetMailAddress(cpuser);
                        recipients.Add(recipient);
                        _mail.Recipient = recipient;
                        _mail.Send();
                    }
                    catch (Exception e)
                    {
                        // track failed recipients
                        failedRecipients[string.Format("{0} [{1}] ({2})", cpuser.DisplayName, cpuser.UserName, string.IsNullOrEmpty(cpuser.Email) ? "<no e-mail address>" : cpuser.Email)] = e.Message;
                    }
                }
            }

            // return failed recipients
            return failedRecipients;
        }

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the <see cref="EventReminderMessage"/>.
        /// </summary>
        public void Dispose()
        {
            if (_mail != null)
                _mail.Dispose();
        }

        #endregion
    }
}
