using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using Cfb.CandidatePortal.Net.Properties;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.Net
{
    /// <summary>
    /// Represents an e-mail message that can be sent via SMTP from the Candidate Portal.
    /// </summary>
    public sealed class CPMailMessage : IDisposable
    {
        /// <summary>
        /// The default SMTP server to use for outgoing e-mails.
        /// </summary>
        private const string DefaultSmtpHost = "192.168.100.25";

        /// <summary>
        /// The default SMTP port to use for outgoing e-mails.
        /// </summary>
        private const byte DefaultSmtpPort = 25;

        /// <summary>
        /// The template for message delivery failure notifications.
        /// </summary>
        private const string DeliveryFailureTemplate = @"Unable to deliver the following message to the recipient at {0}:

{1}";

        /// <summary>
        /// A <see cref="SmtpClient"/> for sending e-mail messages via SMTP.
        /// </summary>
        private SmtpClient _client;

        /// <summary>
        /// A <see cref="MailMessage"/> representing the e-mail message to be sent.
        /// </summary>
        private MailMessage _message;

        /// <summary>
        /// Gets or sets the subject of the e-mail message.
        /// </summary>
        public string Subject
        {
            get { return _message.Subject; }
            set { _message.Subject = value; }
        }

        /// <summary>
        /// Gets or sets the body of the e-mail message.
        /// </summary>
        public string Body
        {
            get { return _message.Body; }
            set { _message.Body = value; }
        }

        /// <summary>
        /// Gets the encoding used to encode the message body.
        /// </summary>
        public Encoding BodyEncoding
        {
            get { return _message.BodyEncoding; }
        }

        /// <summary>
        /// Gets or sets the e-mail recipient.
        /// </summary>
        public MailAddress Recipient
        {
            get
            {
                return _message.To.Count > 0 ? _message.To[0] : null;
            }
            set
            {
                _message.To.Clear();
                _message.To.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the e-mail sender.
        /// </summary>
        public MailAddress Sender
        {
            get { return _message.From; }
            set { _message.From = value; }
        }

        /// <summary>
        /// Gets the address collection that contains the recipients of this e-mail message.
        /// </summary>
        public MailAddressCollection To
        {
            get { return _message.To; }
        }

        /// <summary>
        /// Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message.
        /// </summary>
        public MailAddressCollection CC
        {
            get { return _message.CC; }
        }

        /// <summary>
        /// Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message.
        /// </summary>
        public MailAddressCollection Bcc
        {
            get { return _message.Bcc; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mail message body is in HTML.
        /// </summary>
        public bool IsBodyHtml
        {
            get { return _message.IsBodyHtml; }
            set { _message.IsBodyHtml = value; }
        }

        /// <summary>
        /// Initializes a new instance of an SMTP e-mail message using configuration file settings.
        /// </summary>
        public CPMailMessage()
        {
            _client = new SmtpClient();
            if (string.Empty.Equals(_client.Host))
                _client.Host = DefaultSmtpHost;
            if (_client.Port < 1)
                _client.Port = DefaultSmtpPort;
            // set message properties
            _message = new MailMessage();
            var smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
            ISettingsProvider settings = CPProviders.SettingsProvider;
            try
            {
                _message.From = new MailAddress(settings.MessageSenderEmail, settings.MessageSenderDisplayName);
            }
            catch
            {
                _message.From = new MailAddress(smtpSection.From);
            }
            _message.SubjectEncoding = Encoding.UTF8;
            _message.BodyEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// Sends the current e-mail message.
        /// </summary>
        public void Send()
        {
            try
            {
                _client.Send(_message);
            }
            catch (SmtpFailedRecipientException)
            {
                // only catch and notify messages from us to the candidate
                if (_message.From.Address.EndsWith("@nyccfb.info") || _message.From.Address.EndsWith("@cfb.nyc.ny.us"))
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = _message.From;
                        message.To.Add("caccess@nyccfb.info");
                        message.SubjectEncoding = Encoding.UTF8;
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = _message.IsBodyHtml;
                        message.Subject = "Message Delivery Failure Notification: " + _message.Subject;
                        message.Body = string.Format(DeliveryFailureTemplate, _message.To.ToString(), _message.Body);
                        try
                        {
                            _client.Send(message);
                        }
                        catch { }
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// Adds a list of e-mail address to the recipients of the e-mail message.
        /// </summary>
        /// <param name="addresses">The e-mail addresses to add as recipients. Multiple e-mail addresses must be separated with a comma character (",").</param>
        public void AddRecipients(string addresses)
        {
            _message.To.Add(addresses);
        }

        /// <summary>
        /// Gets a <see cref="MailAddress"/> for contacting a user account via e-mail.
        /// </summary>
        /// <param name="user">The C-Access user to contact.</param>
        /// <returns>A <see cref="MailAddress"/> suitable for contating the user account specified via e-mail.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="user"/> is null.</exception>
        public MailAddress GetMailAddress(CPUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "User must not be null.");
            return new MailAddress(user.Email, user.DisplayName, this.BodyEncoding);
        }

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by the <see cref="CPMailMessage"/>.
        /// </summary>
        public void Dispose()
        {
            if (_message != null)
                _message.Dispose();
        }

        #endregion
    }
}