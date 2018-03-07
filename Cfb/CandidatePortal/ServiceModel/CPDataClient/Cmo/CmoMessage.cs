using System;
using System.Collections.Generic;
using System.Linq;
using Cfb.CandidatePortal.Cmo;
using Cfb.ExceptionHandling;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// Provides access to Campaign Messages Online message data by means of WCF client-server communication.
    /// </summary>
    partial class CPDataProvider
    {
        /// <summary>
        /// Creates a new Campaign Messages Online message and adds it to the persistence storage medium.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the message recipient candidate.</param>
        /// <param name="electionCycle">The message election cycle context.</param>
        /// <param name="title">The message title.</param>
        /// <param name="body">The message body.</param>
        /// <param name="creator">The internal username of the message creator.</param>
        /// <param name="openReceiptEmail">The receipient address for an open receipt e-mail.</param>
        /// <param name="categoryID">The message category ID.</param>
        /// <returns>A new <see cref="CmoMessage"/> object if the message was added successfully; otherwise, null.</returns>
        public CmoMessage AddCmoMessage(string candidateID, string electionCycle, string title, string body, string creator, string openReceiptEmail, byte categoryID)
        {
            using (DataClient client = new DataClient()) { return client.AddCmoMessage(candidateID, electionCycle, title, body, creator, openReceiptEmail, categoryID); }
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public CmoMessage GetCmoMessage(string candidateID, int messageID)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessageByCandidateMessage(candidateID, messageID); }
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="uniqueId">The unique message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public CmoMessage GetCmoMessage(string uniqueId)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessageByID(uniqueId); }
        }

        /// <summary>
        /// Retrieves all posted messages.
        /// </summary>
        /// <param name="showAllData">true to show all message data; otherwise, false to show only summaric results.</param>
        /// <returns>A collection of all posted messages.</returns>
        public List<CmoMessage> GetCmoPostedMessages(bool showAllData)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoPostedMessages(showAllData); }
        }

        /// <summary>
        /// Retrieves all messages matching a collection of unique message IDs.
        /// </summary>
        /// <param name="uniqueIDs">The IDs of the messages to retrieve.</param>
        /// <returns>A collection of messages for the IDs provided.</returns>
        public List<CmoMessage> GetCmoMessages(IEnumerable<string> uniqueIDs)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessages(uniqueIDs.ToList()); }
        }

        /// <summary>
        /// Sets the message's follow-up flag status.
        /// </summary>
        /// <param name="message">The message to flag.</param>
        /// <param name="flagged">true if the message flag is to be set; otherwise, false to clear the flag.</param>
        /// <param name="username">The C-Access username of the user changing the flag.</param>
        /// <returns>true if the flag change was saved successfully; otherwise, false.</returns>
        public bool SetFlagStatus(CmoMessage message, bool flagged, string username)
        {
            if (message == null) return false;
            using (DataClient client = new DataClient()) { return client.SetCmoMessageFlagStatus(message.CandidateID, message.ID, flagged, username, message.Version); }
        }

        /// <summary>
        /// Sets the message's archived status.
        /// </summary>
        /// <param name="message">The message to archive.</param>
        /// <param name="archived">true if the message is to be archived; otherwise, false to unarchive the message.</param>
        /// <param name="username">The C-Access username of the user changing the message archived status.</param>
        /// <returns>true if the message archived status was changed successfully; otherwise, false.</returns>
        public bool SetArchiveStatus(CmoMessage message, bool archived, string username)
        {
            if (message == null) return false;
            using (DataClient client = new DataClient()) { return client.SetCmoMessageArchiveStatus(message.CandidateID, message.ID, archived, username, message.Version); }
        }

        /// <summary>
        /// Searches for all messages matching the specified criteria.
        /// </summary>
        /// <param name="candidateID">A ID of the candidate to search for.</param>
        /// <param name="electionCycle">An election cycle to search for.</param>
        /// <param name="creator">A message creator username to search for.</param>
        /// <param name="category">A message category to search for.</param>
        /// <param name="title">A message title pattern to match against.</param>
        /// <param name="body">A message body pattern to match against.</param>
        /// <param name="postedStart">A posted date range filter start date.</param>
        /// <param name="postedEnd">A posted date range filter end date.</param>
        /// <param name="opened">A message opened status to search for.</param>
        /// <param name="hasAttachments">A message attachment presence to search for.</param>
        /// <returns>A collection of all messages matching the search criteria.</returns>
        public List<CmoMessage> FindCmoMessages(string candidateID, string electionCycle, string creator, byte? category, string title, string body, DateTime? postedStart, DateTime? postedEnd, bool? opened, bool? hasAttachments)
        {
            using (DataClient client = new DataClient())
            {
                return client.FindCmoMessages(candidateID, electionCycle, creator, category, title, body, postedStart, postedEnd, opened, hasAttachments);
            }
        }

        /// <summary>
        /// Retrieves all posted messages for a single candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to search for.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all messages for the specified candidate and election cycle.</returns>
        public List<CmoMessage> GetCmoPostedMessages(string candidateID, string electionCycle = null)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoPostedMessagesByCandidateElection(candidateID, electionCycle); }
        }

        /// <summary>
        /// Retrieves an array of the usernames of all users who have created messages, mapped to their respective full names.
        /// </summary>
        /// <returns>An array of the usernames of all users who have created messages, mapped to their respective full names.</returns>
        public Dictionary<string, string> GetCmoMessageCreators()
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessageCreators(); }
        }

        /// <summary>
        /// Posts a CMO message for viewing by C-Access accounts.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to post.</param>
        /// <param name="messageID">The ID of the message to post.</param>
        /// <param name="version">The version of the message to post.</param>
        /// <param name="notify">true if posted message notifications should be sent; otherwise, false.</param>
        /// <returns>true if the message was succesfully posted; otherwise, false.</returns>
        public bool PostCmoMessage(string candidateID, int messageID, byte[] version, bool notify)
        {
            using (DataClient client = new DataClient()) { return client.PostCmoMessage(candidateID, messageID, version, notify); }
        }

        /// <summary>
        /// Reloads the message from the persistence storage medium.
        /// </summary>
        /// <param name="message">The message to reload.</param>
        /// <returns>The current version of the message if found; otherwise, null.</returns>
        public CmoMessage Reload(CmoMessage message)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessageByCandidateMessage(message.CandidateID, message.ID); }
        }

        /// <summary>
        /// Marks the message as opened.
        /// </summary>
        /// <param name="message">The message to open.</param>
        /// <param name="username">The C-Access username of the user who opened the message.</param>
        /// <returns>true if the message was opened successfully; otherwise, false.</returns>
        public bool Open(CmoMessage message, string username)
        {
            using (DataClient client = new DataClient()) { return client.OpenCmoMessage(message.CandidateID, message.ID, username, message.Version); }
        }

        /// <summary>
        /// Updates a CMO message instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the CMO message instance was saved successfuly; otherwise, false.</returns>
        public bool Update(CmoMessage message)
        {
            using (DataClient client = new DataClient()) { return client.UpdateCmoMessage(message); }
        }

        /// <summary>
        /// Attaches a file attachment to the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="message">The CMO message to attach to.</param>
        /// <param name="data">The binary data contents of the attachment.</param>
        /// <param name="name">The filename of the attachment.</param>
        /// <returns>A <see cref="CmoAttachment"/> for the attachment if the file was attached successfully; otherwise, null.</returns>
        public CmoAttachment Attach(CmoMessage message, byte[] data, string name)
        {
            if (message.IsPosted)
                return null;
            CmoAttachment attachment = this.AddCmoAttachment(message.CandidateID, message.ID, data, name);
            if (attachment != null)
            {
                message.Attachments.Add(attachment.ID, attachment);
                return attachment;
            }
            return null;
        }

        /// <summary>
        /// Detaches a file attachment from the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="message">The CMO message to detach from.</param>
        /// <param name="attachmentID">The ID of the attachment to detach.</param>
        /// <returns>true if the attachment was removed successfully; otherwise, false.</returns>
        public bool Detach(CmoMessage message, byte attachmentID)
        {
            if (message.IsPosted) return false;
            CmoAttachment attachment;
            if (message.Attachments.TryGetValue(attachmentID, out attachment))
            {
                if (this.Delete(attachment))
                {
                    message.Attachments.Remove(attachment.ID);
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Deletes this instance from the persistence storage medium.
        /// </summary>
        /// <param name="message">The CMO message to delete.</param>
        /// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
        public bool Delete(CmoMessage message)
        {
            using (DataClient client = new DataClient()) { return client.DeleteCmoMessage(message.CandidateID, message.ID, message.Version); }
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public CmoMessage GetCmoMessageByCandidateMessage(string candidateID, int messageID)
        {
            using (DataClient client = new DataClient()) { return client.GetCmoMessageByCandidateMessage(candidateID, messageID); }
        }
    }
}