using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.Cmo
{
    /// <summary>
    /// Defines methods for accessing C-Access data.
    /// </summary>
    public interface ICmoDataProvider
    {
        #region CMO Setting Methods

        /// <summary>
        /// Creates a new settings entry for a candidate and adds it to the persistence storage medium.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to whom this preference applies.</param>
        /// <param name="isPaperless">Whether or not the candidate has a paperless preference.</param>
        /// <param name="username">The C-Access username of the updating user.</param>
        /// <returns>A new <see cref="CmoSettings"/> object if the settings were added successfully; otherwise, null.</returns>
        CmoSettings AddCmoSettings(string candidateID, bool isPaperless, string username);

        /// <summary>
        /// Gets the C-Access Campaign Message Online settings for a specific candidate.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to query.</param>
        /// <returns>The specified candidate's Campaign Message Online settings if one exists; otherwise, null.</returns>
        CmoSettings GetCmoSettings(string candidateID);

        /// <summary>
        /// Updates this instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="settings">The settings to update.</param>
        /// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
        bool Update(CmoSettings settings);

        #endregion

        #region CMO Message Methods

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
        CmoMessage AddCmoMessage(string candidateID, string electionCycle, string title, string body, string creator, string openReceiptEmail, byte categoryID);

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        CmoMessage GetCmoMessage(string candidateID, int messageID);

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="uniqueId">The unique message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        CmoMessage GetCmoMessage(string uniqueId);

        /// <summary>
        /// Retrieves all posted messages.
        /// </summary>
        /// <param name="showAllData">true to show all message data; otherwise, false to show only summaric results.</param>
        /// <returns>A collection of all posted messages.</returns>
        List<CmoMessage> GetCmoPostedMessages(bool showAllData);

        /// <summary>
        /// Retrieves all messages matching a collection of unique message IDs.
        /// </summary>
        /// <param name="uniqueIDs">The IDs of the messages to retrieve.</param>
        /// <returns>A collection of messages for the IDs provided.</returns>
        List<CmoMessage> GetCmoMessages(IEnumerable<string> uniqueIDs);

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
        List<CmoMessage> FindCmoMessages(string candidateID = null, string electionCycle = null, string creator = null, byte? category = null, string title = null, string body = null, DateTime? postedStart = null, DateTime? postedEnd = null, bool? opened = null, bool? hasAttachments = null);

        /// <summary>
        /// Retrieves all posted messages for a single candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to search for.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all messages for the specified candidate and election cycle.</returns>
        List<CmoMessage> GetCmoPostedMessages(string candidateID, string electionCycle = null);

        /// <summary>
        /// Retrieves an array of the usernames of all users who have created messages, mapped to their respective full names.
        /// </summary>
        /// <returns>An array of the usernames of all users who have created messages, mapped to their respective full names.</returns>
        Dictionary<string, string> GetCmoMessageCreators();

        /// <summary>
        /// Posts a CMO message for viewing by C-Access accounts.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to post.</param>
        /// <param name="messageID">The ID of the message to post.</param>
        /// <param name="version">The version of the message to post.</param>
        /// <param name="notify">true if posted message notifications should be sent; otherwise, false.</param>
        /// <returns>true if the message was succesfully posted; otherwise, false.</returns>
        bool PostCmoMessage(string candidateID, int messageID, byte[] version, bool notify);

        /// <summary>
        /// Marks the message as opened.
        /// </summary>
        /// <param name="message">The message to open.</param>
        /// <param name="username">The C-Access username of the user who opened the message.</param>
        /// <returns>true if the message was opened successfully; otherwise, false.</returns>
        bool Open(CmoMessage message, string username);

        /// <summary>
        /// Sets the message's archived status.
        /// </summary>
        /// <param name="message">The message to archive.</param>
        /// <param name="archived">true if the message is to be archived; otherwise, false to unarchive the message.</param>
        /// <param name="username">The C-Access username of the user changing the message archived status.</param>
        /// <returns>true if the message archived status was changed successfully; otherwise, false.</returns>
        bool SetArchiveStatus(CmoMessage message, bool archived, string username);

        /// <summary>
        /// Sets the message's follow-up flag status.
        /// </summary>
        /// <param name="message">The message to flag.</param>
        /// <param name="flagged">true if the message flag is to be set; otherwise, false to clear the flag.</param>
        /// <param name="username">The C-Access username of the user changing the flag.</param>
        /// <returns>true if the flag change was saved successfully; otherwise, false.</returns>
        bool SetFlagStatus(CmoMessage message, bool flagged, string username);

        /// <summary>
        /// Updates a CMO message instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the CMO message instance was saved successfuly; otherwise, false.</returns>
        bool Update(CmoMessage message);

        /// <summary>
        /// Attaches a file attachment to the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="message">The CMO message to attach to.</param>
        /// <param name="data">The binary data contents of the attachment.</param>
        /// <param name="name">The filename of the attachment.</param>
        /// <returns>A <see cref="CmoAttachment"/> for the attachment if the file was attached successfully; otherwise, null.</returns>
        CmoAttachment Attach(CmoMessage message, byte[] data, string name);

        /// <summary>
        /// Detaches a file attachment from the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="message">The CMO message to detach from.</param>
        /// <param name="attachmentID">The ID of the attachment to detach.</param>
        /// <returns>true if the attachment was removed successfully; otherwise, false.</returns>
        bool Detach(CmoMessage message, byte attachmentID);

        /// <summary>
        /// Deletes this instance from the persistence storage medium.
        /// </summary>
        /// <param name="message">The CMO message to delete.</param>
        /// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
        bool Delete(CmoMessage message);

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        CmoMessage GetCmoMessageByCandidateMessage(string candidateID, int messageID);

        #endregion

        #region CMO Attachment Methods

        /// <summary>
        /// Deletes this instance from the persistence storage medium.
        /// </summary>
        /// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
        bool Delete(CmoAttachment attachment);

        /// <summary>
        /// Adds a CMO message attachment.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message.</param>
        /// <param name="messageID">The message ID.</param>
        /// <param name="data">The raw binary attachment data.</param>
        /// <param name="name">The attachment file name to be use for display.</param>
        /// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
        CmoAttachment AddCmoAttachment(string candidateID, int messageID, byte[] data, string name);

        /// <summary>
        /// Retrieves an attachment.
        /// </summary>
        /// <param name="uniqueID">The unique ID of the attachment to retrieve.</param>
        /// <returns>The requested <see cref="CmoAttachment"/> if found; otherwise, null.</returns>
        CmoAttachment GetCmoAttachment(string uniqueID);

        /// <summary>
        /// Retrieves the full system file path to an attachment.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate the attachment is for.</param>
        /// <param name="messageID">The ID of the message the attachment is for.</param>
        /// <param name="id">The ID of the attachment to retrieve.</param>
        /// <returns>A system file path to the requested attachment if found; otherwise, null.</returns>
        /// <remarks>A UNC file path should be returned, provided the application configuration file defines the CMO repository location as a UNC file share location.</remarks>
        string GetCmoAttachmentPath(string candidateID, int messageID, byte id);

        #endregion

        #region CMO Audit Review Methods

        /// <summary>
        /// Gets a collection of IDs for all statement review messages for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
        /// <param name="electionCycle">The election cycle of the reviews.</param>
        /// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
        Dictionary<byte, string> GetStatementReviewMessageIDs(string candidateID, string electionCycle);

        /// <summary>
        /// Gets a collection of IDs for all compliance visit messages for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
        /// <param name="electionCycle">The election cycle of the reviews.</param>
        /// <returns>A collection of unique IDs for all compliance visit CMO messages found.</returns>
        Dictionary<byte, string> GetComplianceVisitMessageIDs(string candidateID, string electionCycle);

        /// <summary>
        /// Gets a collection of IDs for all Doing Business review messages for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
        /// <param name="electionCycle">The election cycle of the reviews.</param>
        /// <returns>A collection of unique IDs for all Doing Business review CMO messages found.</returns>
        Dictionary<byte, string> GetDoingBusinessReviewMessageIDs(string candidateID, string electionCycle);

        /// <summary>
        /// Gets a collection of IDs for all post election audit tolling messages for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
        /// <param name="electionCycle">The election cycle of the reviews.</param>
        /// <param name="far">true to specify Final Audit Report tolling; otherwise, false to specify Draft Audit Report tolling.</param>
        /// <returns>A collection of unique IDs for all post election audit tolling CMO messages found.</returns>
        Dictionary<int, string> GetTollingMessageIDs(string candidateID, string electionCycle, bool far);

        /// <summary>
        /// Gets a collection of IDs for all post election audit reports for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
        /// <param name="electionCycle">The election cycle of the reviews.</param>
        /// <returns>A collection of unique IDs for all post election audit report CMO messages found.</returns>
        Dictionary<AuditReportType, string> GetAuditReportMessageIDs(string candidateID, string electionCycle);

        #endregion

        #region CMO Category Methods

        /// <summary>
        /// Gets a collection of all Campaign Messages Online categories, indexed by category ID.
        /// </summary>
        /// <returns>A collection of all Campaign Messages Online categories, indexed by category ID.</returns>
        Dictionary<byte, CmoCategory> GetCmoCategories();

        /// <summary>
        /// Gets a Campaign Message Online category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to get.</param>
        /// <returns>The CMO category matching the specified ID if found; otherwise, null.</returns>
        CmoCategory GetCmoCategory(byte id);

        /// <summary>
        /// Updates this instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="category">The category to update.</param>
        /// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
        bool Update(CmoCategory category);

        /// <summary>
        /// Deletes this instance from the persistence storage medium.
        /// </summary>
        /// <param name="category">The category to delete.</param>
        /// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
        bool Delete(CmoCategory category);

        #endregion

        #region CMO Mailbox Methods

        /// <summary>
        /// Retrieves messages from a candidate's CMO mailbox.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the desired candidate.</param>
        /// <param name="view">The mailbox view type.</param>
        /// <param name="elections">A collection of election cycles to filter against.</param>
        /// <returns>A collection of <see cref="CmoMessage"/> objects for the specified mailbox sorted according to the properties of the mailbox.</returns>
        List<CmoMessage> GetMailboxMessages(string candidateID, CmoMailboxView view, HashSet<string> elections);

        /// <summary>
        /// Gets the total number of unread messages in a candidate's CMO mailbox.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the desired candidate.</param>
        /// <param name="elections">A collection of election cycles to filter against.</param>
        /// <returns>The total number of unread messges in the specified candidate's mailbox.</returns>
        uint CountUnopenedMailboxMessages(string candidateID, HashSet<string> elections);

        #endregion
    }
}
