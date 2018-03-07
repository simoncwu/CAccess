using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Net.Properties;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.Net
{
    /// <summary>
    /// An e-mail message notifying a campaign's C-Access users of a newly posted Campaign Messages Online message.
    /// </summary>
    public class CmoPostedMessageNotice : IDisposable
    {
        /// <summary>
        /// Occurs when a message requests information about a candidate.
        /// </summary>
        public event LoadCandidateEventHandler LoadCandidate;

        /// <summary>
        /// The newly posted <see cref="CmoMessage"/> .
        /// </summary>
        private readonly CmoMessage _message;

        /// <summary>
        /// The <see cref="CPMailMessage"/> to send.
        /// </summary>
        private readonly CPMailMessage _mail;

        /// <summary>
        /// Gets or sets the posted message e-mail recipients.
        /// </summary>
        public IEnumerable<CPUser> Recipients { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmoPostedMessageNotice"/> class.
        /// </summary>
        /// <param name="message">The newly posted CMO message.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> has not been posted.</exception>
        private CmoPostedMessageNotice(CmoMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message", "CMO message must not be null.");
            _message = message;
            if (!message.IsPosted)
                throw new ArgumentException("message", "CMO message must be posted first before a notification can be sent.");
            _mail = new CPMailMessage();
            ISettingsProvider settings = CPProviders.SettingsProvider;
            _mail.Sender = settings == null ? new MailAddress(_mail.Sender.Address) : new MailAddress(settings.MessageSenderEmail, settings.MessageSenderDisplayName);
            _mail.Subject = message.Title;
            _mail.IsBodyHtml = true;
        }

        /// <summary>
        /// Sends the current notification.
        /// </summary>
        /// <returns>true if and only if all recipients were successfully notified; otherwise, false.</returns>
        public bool Send()
        {
            Dictionary<string, string> failedRecipients = new Dictionary<string, string>(); // track failed recipients

            // prepare and send message
            if (this.Recipients != null)
            {
                Settings settings = Properties.Settings.Default;
                MailAddressCollection recipients = new MailAddressCollection(); // all receipients for logging purposes

                Candidate candidate = null;
                if (this.LoadCandidate != null)
                {
                    try
                    {
                        candidate = this.LoadCandidate(_message.CandidateID);
                    }
                    catch // BUGFIX #47: catch exceptions during candidate load so delivery is not interrupted
                    {
                        candidate = null;
                    }
                }

                // retrieve message parameters and prepare body
                string election = _message.ElectionCycle;
                string cid = _message.CandidateID;
                string name = candidate != null ? candidate.Name : string.Format("candidate #{0}", cid);
                StringBuilder sb = new StringBuilder();

                // alternate message formats
                if (_message.CategoryID == CmoCategory.TollingLetterCategoryID && _message.TollingLetter != null)
                {
                    if (_message.TollingLetter.IsExtension)
                    {
                        sb.AppendFormat(settings.CmoMessageExtensionTemplate, election, name, cid);
                    }
                    else
                    {
                        sb.AppendFormat(settings.CmoMessageTollingTemplate, election, name, cid, GetMessageType());
                    }
                }
                else if (_message.IsInadequateResponseLetter)
                {
                    sb.AppendFormat(settings.CmoMessageInadequateTemplate, election, name, cid, GetMessageType());
                }
                else
                {
                    sb.AppendFormat(settings.CmoMessageTemplate, election, name, cid, GetMessageType());
                }
                sb.AppendLine();
                string url = CPProviders.SettingsProvider.ApplicationUrl;
                sb.AppendFormat(settings.CmoMessageViewLinkTemplate, url, _message.UniqueID);
                string paragraph = sb.ToString();

                // send to each recipient
                foreach (var user in this.Recipients)
                {
                    // personalize and send message
                    try
                    {
                        MailAddress recipient = _mail.GetMailAddress(user);
                        recipients.Add(recipient);
                        _mail.Recipient = recipient;
                        _mail.Body = string.Format(settings.MessageBodyTemplate, recipient.DisplayName, paragraph, url);
                        _mail.Send();
                    }
                    catch (Exception e)
                    {
                        failedRecipients[string.Format("{0} [{1}] ({2})", user.DisplayName, user.UserName, string.IsNullOrEmpty(user.Email) ? "<no e-mail address>" : user.Email)] = e.Message;
                    }
                }

                // carbon-copy C-Access for records
                _mail.To.Clear();
                _mail.CC.Clear();
                _mail.Bcc.Clear();
                _mail.CC.Add(settings.CmoMessageCCEmailAddress);
                sb = new StringBuilder();
                foreach (MailAddress address in recipients)
                {
                    sb.AppendFormat("{0} ({1}), ", address.DisplayName, address.Address);
                }
                string allRecipients = sb.ToString();
                if (allRecipients.EndsWith(", "))
                    allRecipients = allRecipients.Remove(allRecipients.Length - 2);

                // BUGFIX #60: To prevent timeout of long-running email notification operations, failed recipient notification 
                // will be included in the internal carbon-copy, and sending will be executed asynchronously.
                int bodyStartIndex = settings.MessageBodyTemplate.IndexOf("<body>");
                if (bodyStartIndex != -1)
                    bodyStartIndex += 6;
                StringBuilder body = new StringBuilder(settings.MessageBodyTemplate.Substring(0, bodyStartIndex));
                if (failedRecipients.Count > 0)
                    body.Append("<span style=\"color:red\">Unable to successfully e-mail the following C-Access users:</span><ol>");
                foreach (var entry in failedRecipients)
                {
                    body.AppendFormat("<li>{0}: {1}</li>", entry.Key, entry.Value);
                }
                body.Append("</ol>");
                body.AppendFormat(settings.MessageBodyTemplate.Substring(bodyStartIndex), allRecipients, paragraph, url);
                _mail.Body = body.ToString();
                _mail.Send();
            }

            // return failed recipients
            return failedRecipients.Count == 0;
        }

        /// <summary>
        /// Gets the type of the current message as a user-friendly string.
        /// </summary>
        /// <returns>A user-friendly <see cref="String"/> indicating the current message type.</returns>
        private string GetMessageType()
        {
            if (_message.CategoryID == CmoCategory.GenericCategoryID)
                return "C-Access";
            else if (_message.CategoryID == CmoCategory.StatementReviewCategoryID)
                return string.Format("Statement #{0} Review", _message.StatementNumber);
            else if (_message.CategoryID == CmoCategory.TollingLetterCategoryID && _message.TollingLetter != null)
                return _message.TollingLetter.Description;
            else
                return _message.Category.Name;
        }

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the <see cref="CmoPostedMessageNotice"/>.
        /// </summary>
        public void Dispose()
        {
            if (_mail != null)
                _mail.Dispose();
        }

        #endregion

        /// <summary>
        /// Sends an email notification for a posted CMO message.
        /// </summary>
        /// <param name="message">The message to send posted notifications for.</param>
        /// <param name="loadCandidate">A delegate for handling requests for candidate information.</param>
        /// <param name="recipients">A collection of users who will receive the notifications.</param>
        /// <returns>true if and only if all recipients were successfully notified; otherwise, false.</returns>
        public static bool SendFor(CmoMessage message, LoadCandidateEventHandler loadCandidate = null, IEnumerable<CPUser> recipients = null)
        {
            if (loadCandidate == null)
                loadCandidate = CPProviders.DataProvider.GetCandidate;
            if (recipients == null)
                recipients = CPSecurity.Provider.GetCampaignUsers(message.CandidateID, message.ElectionCycle, false);
            using (CmoPostedMessageNotice notice = new CmoPostedMessageNotice(message))
            {
                notice.LoadCandidate += loadCandidate;
                notice.Recipients = recipients;
                return notice.Send();
            }
        }
    }
}
