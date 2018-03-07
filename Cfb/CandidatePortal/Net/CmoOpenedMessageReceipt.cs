using System;
using System.Net.Mail;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Net
{
    /// <summary>
    /// An e-mail notification receipt indicating that a CMO message was opened.
    /// </summary>
    public class CmoOpenedMessageReceipt : IDisposable
    {
        /// <summary>
        /// Occurs when a message requests information about a candidate.
        /// </summary>
        public event LoadCandidateEventHandler LoadCandidate;

        /// <summary>
        /// The format string for the subject of open receipt e-mail notifications.
        /// </summary>
        private const string OpenReceiptSubjectFormat = "C-Access Open Message Receipt for Message {0}";

        /// <summary>
        /// The format string for the body of open receipt e-mail notifications.
        /// </summary>
        private const string OpenReceiptBodyFormat = @"<html>
<head>
<style type=""text/css"">
th, td {{ padding:5px; }}
th {{ text-align:right; }}
td {{ text-align:left; }}
</style>
</head>
<body>
<p>The following message was opened in C-Access:</p>
<table border=""0"" cellpadding=""0"" cellspacing=""0"">
<tr><th>Election Cycle:</th><td>{3}</td></tr>
<tr><th>Candidate:</th><td>{0} ({1})</td></tr>
<tr><th>Message ID:</th><td>{2}</td></tr>
<tr><th>Subject:</th><td>{9}</td></tr>
<tr><th>Created by:</th><td>{4}</td></tr>
<tr><th>Sent on:</th><td>{5:d} {5:t}</td></tr>
<tr><th>Opened by:</th><td>{7} ({8})</td></tr>
<tr><th>Opened on:</th><td>{6:d} {6:t}</td></tr>
</table>
</body>
</html>";

        /// <summary>
        /// The opened message.
        /// </summary>
        private readonly CmoMessage _message;

        /// <summary>
        /// The <see cref="CPMailMessage"/> to send.
        /// </summary>
        private readonly CPMailMessage _mail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CmoOpenedMessageReceipt"/> class.
        /// </summary>
        /// <param name="message">The newly posted CMO message.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> has not been posted.</exception>
        public CmoOpenedMessageReceipt(CmoMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message", "CMO message must not be null.");
            _message = message;
            if (!message.IsOpened)
                throw new ArgumentException("message", "CMO message must be opened first before a receipt can be sent.");
            _mail = new CPMailMessage();
            _mail.Sender = new MailAddress(_mail.Sender.Address);
            _mail.Subject = string.Format(OpenReceiptSubjectFormat, _message.UniqueID);
            _mail.AddRecipients(message.OpenReceiptEmail);
            _mail.IsBodyHtml = true;
        }

        /// <summary>
        /// Sends the current notification.
        /// </summary>
        public void Send()
        {
            Candidate candidate = this.LoadCandidate != null ? this.LoadCandidate(_message.CandidateID) : null;
            try
            {
                _mail.Body = string.Format(OpenReceiptBodyFormat,
                    candidate != null ? candidate.Name : string.Format("candidate #{0}", _message.CandidateID),
                    _message.CandidateID,
                    _message.UniqueID,
                    _message.ElectionCycle,
                    CfbDirectorySearcher.GetUser(_message.Creator).DisplayName,
                    _message.PostDate,
                    _message.OpenDate,
                    CPSecurity.Provider.GetDisplayName(_message.Opener),
                    _message.Opener,
                    _message.Title);
                _mail.Send();
            }
            catch
            {
            }
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
    }
}
