using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.Data.PostElectionTdsTableAdapters;
using Cfb.DirectoryServices;
//using CmoCategory = Cfb.CandidatePortal.Cmo.CmoCategory;
using CmoRepository = Cfb.CandidatePortal.Cmo.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
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
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        CmoMessage AddCmoMessage(string candidateID, string electionCycle, string title, string body, string creator, string openReceiptEmail, byte categoryID);

        /// <summary>
        /// Deletes a CMO message from the persistence storage medium.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to delete.</param>
        /// <param name="messageID">The ID of the message to delete.</param>
        /// <param name="version">The version of the message to open.</param>
        /// <returns>true if the CMO message instance was deleted successfully; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "DeleteCmoMessage")]
        [FaultContract(typeof(OfflineDataException))]
        bool Delete(string candidateID, int messageID, byte[] version);

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
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        List<CmoMessage> FindCmoMessages(string candidateID, string electionCycle, string creator, byte? category, string title, string body, DateTime? postedStart, DateTime? postedEnd, bool? opened, bool? hasAttachments);

        /// <summary>
        /// Retrieves a collection of the usernames of all users who have created messages, mapped to their respective full names.
        /// </summary>
        /// <returns>A collection of the useranmes of all users who have created messages, mapped to their respective full names.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Dictionary<string, string> GetCmoMessageCreators();

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetCmoMessageByCandidateMessage")]
        [FaultContract(typeof(OfflineDataException))]
        CmoMessage GetCmoMessage(string candidateID, int messageID);

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="uniqueID">The unique message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetCmoMessageByID")]
        [FaultContract(typeof(OfflineDataException))]
        CmoMessage GetCmoMessage(string uniqueID);

        /// <summary>
        /// Retrieves all messages matching a collection of unique message IDs.
        /// </summary>
        /// <param name="uniqueIDs">The IDs of the messages to retrieve.</param>
        /// <returns>A collection of messages for the IDs provided.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        List<CmoMessage> GetCmoMessages(IEnumerable<string> uniqueIDs);

        /// <summary>
        /// Retrieves all posted messages for a single candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to search for.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all messages for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetCmoPostedMessagesByCandidateElection")]
        [FaultContract(typeof(OfflineDataException))]
        List<CmoMessage> GetCmoPostedMessages(string candidateID, string electionCycle = null);

        /// <summary>
        /// Retrieves all posted messages.
        /// </summary>
        /// <param name="showAllData">true to show all message data; otherwise, false to show only summaric results.</param>
        /// <returns>A collection of all posted messages.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        List<CmoMessage> GetCmoPostedMessages(bool showAllData);

        /// <summary>
        /// Marks a CMO message as opened.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to open.</param>
        /// <param name="messageID">The ID of the message to open.</param>
        /// <param name="username">The C-Access username of the user who opened the message.</param>
        /// <param name="version">The version of the message to open.</param>
        /// <returns>true if the message was opened successfully; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        bool OpenCmoMessage(string candidateID, int messageID, string username, byte[] version);

        /// <summary>
        /// Posts a CMO message for viewing by C-Access accounts.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to post.</param>
        /// <param name="messageID">The ID of the message to post.</param>
        /// <param name="version">The version of the message to post.</param>
        /// <param name="notify">true if posted message notifications should be sent; otherwise, false.</param>
        /// <returns>true if the message was succesfully posted; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        bool PostCmoMessage(string candidateID, int messageID, byte[] version, bool notify);

        /// <summary>
        /// Sets the message's archived status.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to update.</param>
        /// <param name="messageID">The ID of the message to update.</param>
        /// <param name="archived">true if the message is to be archived; otherwise, false to unarchive the message.</param>
        /// <param name="username">The C-Access username of the user changing the message archived status.</param>
        /// <param name="version">The version of the message to update.</param>
        /// <returns>true if the message archived status was changed successfully; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        bool SetCmoMessageArchiveStatus(string candidateID, int messageID, bool archived, string username, byte[] version);

        /// <summary>
        /// Sets the message's follow-up flag status.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to update.</param>
        /// <param name="messageID">The ID of the message to update.</param>
        /// <param name="flagged">true if the message flag is to be set; otherwise, false to clear the flag.</param>
        /// <param name="username">The C-Access username of the user changing the flag.</param>
        /// <param name="version">The version of the message to update.</param>
        /// <returns>true if the flag change was saved successfully; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        bool SetCmoMessageFlagStatus(string candidateID, int messageID, bool flagged, string username, byte[] version);

        /// <summary>
        /// Updates a CMO message instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the CMO message instance was saved successfuly; otherwise, false.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "UpdateCmoMessage")]
        [FaultContract(typeof(OfflineDataException))]
        bool Update(CmoMessage message);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Posts a CMO message for viewing by C-Access accounts and .
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to post.</param>
        /// <param name="messageID">The ID of the message to post.</param>
        /// <param name="version">The version of the message to post.</param>
        /// <param name="notify">true if posted message notifications should be sent; otherwise, false.</param>
        /// <returns>true if the message was succesfully posted; otherwise, false.</returns>
        /// <remarks>For Post Election Audit messages, posting will fail if the CFIS dates were unsuccessfully updated, the post election audit workflow would be disrupted, or a response was already received.</remarks>
        public bool PostCmoMessage(string candidateID, int messageID, byte[] version, bool notify)
        {
            CmoMessage message = GetCmoMessage(candidateID, messageID);
            if (message != null)
            {
                if (!message.IsPosted)
                {
                    if (RequiresPostElectionDateUpdate(message))
                    {
                        // check for post election audit workflow violation
                        AuditReportBase pear = null;
                        if (message.IsInitialDocumentationRequest)
                            pear = GetInitialDocumentationRequest(candidateID, message.ElectionCycle, false);
                        else if (message.IsIdrInadequateResponseLetter)
                            pear = GetIdrInadequateEvent(candidateID, message.ElectionCycle, false);
                        else if (message.IsDraftAuditReport)
                            pear = GetDraftAuditReport(candidateID, message.ElectionCycle, false);
                        if (pear != null && (pear.ResponseReceived || !HasValidPostElectionRequestType(message, pear.IsSecondRequest)))
                            return false;

                        // update CFIS dates for post election audit messages
                        if (!UpdatePostElectionDates(message))
                            return false;
                    }
                    using (Data.CmoEntities context = new Data.CmoEntities())
                    {
                        var ret = context.CmoPostMessage(candidateID, messageID, version).FirstOrDefault();
                        if (ret.HasValue && ret.Value > 0)
                        {
                            // secure all attachments
                            if ((CmoRepository.Repository != null) && message.HasAttachment)
                                CmoRepository.Repository.LockDown(message);
                            message.Reload();

                            // send notification
                            if (notify)
                            {
                                // BUGFIX #60: To prevent timeout of long-running email notification operations, failed recipient notification 
                                // will be included in the internal carbon-copy, and sending will be executed asynchronously.
                                Func<CmoMessage, LoadCandidateEventHandler, IEnumerable<CPUser>, bool> notifyAsync = CmoPostedMessageNotice.SendFor;
                                notifyAsync.BeginInvoke(message, this.GetCandidate, null, null, null);
                            }

                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether or not a <see cref="CmoMessage"/> requires updating of post election audit dates.
        /// </summary>
        /// <param name="message">The message to analyze.</param>
        /// <returns>true if <paramref name="message"/> requires updating of post election audit dates; otherwise, false.</returns>
        private bool RequiresPostElectionDateUpdate(CmoMessage message)
        {
            return message != null && (message.IsInitialDocumentationRequest || message.IsInadequateResponseLetter);
        }

        /// <summary>
        /// Updates the post election dates corresponding to a CMO post election audit message in CFIS.
        /// </summary>
        /// <param name="message">The post election audit message to update.</param>
        /// <returns>true if the post election dates were successfully updated in CFIS; otherwise, false.</returns>
        private bool UpdatePostElectionDates(CmoMessage message)
        {
            if (message == null)
                return false;
            AuditReportType type;
            if (message.IsInitialDocumentationRequest)
                type = AuditReportType.InitialDocumentationRequest;
            else if (message.IsIdrInadequateResponseLetter)
                type = AuditReportType.IdrInadequateResponse;
            else if (message.IsIdrAdditionalInadequateLetter)
                type = AuditReportType.IdrAdditionalInadequateResponse;
            else if (message.IsDarInadequateResponseLetter)
                type = AuditReportType.DarInadequateResponse;
            else if (message.IsDarAdditionalInadequateLetter)
                type = AuditReportType.DarAdditionalInadequateResponse;
            else
                return false;
            using (PostElectionTableAdapter ta = new PostElectionTableAdapter())
            {
                object retObj; // SQL error code as object
                int retVal; // SQL error code
                int? eventNumber = message.TollingEventNumber; // number of event generated or source tolling event, as appropriate
                retObj = ta.UpdatePostElectionDates(message.CandidateID, message.ElectionCycle, CPConvert.ToCfisCode(type), message.PostElectionAuditRequestType == AuditRequestType.SecondRequest || message.PostElectionAuditRequestType == AuditRequestType.SecondRepost ? "Y" : "N", ref eventNumber);
                if (retObj != null && int.TryParse(retObj.ToString(), out retVal))
                {
                    if (retVal == 0)
                    {
                        // save event number for tolling letters
                        if (eventNumber.HasValue && message.IsTollingLetter)
                        {
                            message.TollingEventNumber = eventNumber;
                            SetCmoMessageTolling(message);
                        }
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Marks a CMO message as opened.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to open.</param>
        /// <param name="messageID">The ID of the message to open.</param>
        /// <param name="username">The C-Access username of the user who opened the message.</param>
        /// <param name="version">The version of the message to open.</param>
        /// <returns>true if the message was opened successfully; otherwise, false.</returns>
        public bool OpenCmoMessage(string candidateID, int messageID, string username, byte[] version)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                if (context.CmoMessages.Any(m => m.CandidateId == candidateID && m.MessageId == messageID && m.OpenDate.HasValue))
                    return true;
                var ret = context.CmoOpenMessage(candidateID, messageID, username, version).FirstOrDefault();
                if (ret.HasValue && ret.Value > 0)
                {
                    try
                    {
                        // send open receipt e-mail
                        using (CmoOpenedMessageReceipt receipt = new CmoOpenedMessageReceipt(GetCmoMessage(candidateID, messageID)))
                        {
                            receipt.LoadCandidate += this.GetCandidate;
                            receipt.Send();
                        }
                    }
                    catch { }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Sets the message's archived status.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to update.</param>
        /// <param name="messageID">The ID of the message to update.</param>
        /// <param name="archived">true if the message is to be archived; otherwise, false to unarchive the message.</param>
        /// <param name="username">The C-Access username of the user changing the message archived status.</param>
        /// <param name="version">The version of the message to update.</param>
        /// <returns>true if the message archived status was changed successfully; otherwise, false.</returns>
        public bool SetCmoMessageArchiveStatus(string candidateID, int messageID, bool archived, string username, byte[] version)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var ret = context.CmoArchiveMessage(candidateID, messageID, username, archived, version).FirstOrDefault();
                return ret.HasValue && ret.Value > 0;
            }
        }

        /// <summary>
        /// Sets the message's follow-up flag status.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to update.</param>
        /// <param name="messageID">The ID of the message to update.</param>
        /// <param name="flagged">true if the message flag is to be set; otherwise, false to clear the flag.</param>
        /// <param name="username">The C-Access username of the user changing the flag.</param>
        /// <param name="version">The version of the message to update.</param>
        /// <returns>true if the flag change was saved successfully; otherwise, false.</returns>
        public bool SetCmoMessageFlagStatus(string candidateID, int messageID, bool flagged, string username, byte[] version)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var ret = context.CmoFollowUpMessage(candidateID, messageID, username, flagged, version).FirstOrDefault();
                return ret.HasValue && ret.Value > 0;
            }
        }

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
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var ret = context.CmoSaveMessage(candidateID, byte.MinValue, electionCycle, title, body, creator, openReceiptEmail, categoryID, new byte[] { }).FirstOrDefault();
                return ret.HasValue && ret.Value > 0 ? CreateCmoMessage(context.CmoMessages.Where(m => m.CandidateId == candidateID && m.MessageId == ret.Value).FirstOrDefault()) : null;
            }
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="uniqueID">The unique message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public CmoMessage GetCmoMessage(string uniqueID)
        {
            string candidateID;
            int messageID;
            return CmoMessage.TryParseUniqueID(uniqueID, out candidateID, out messageID) ? GetCmoMessage(candidateID, messageID) : null;
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public CmoMessage GetCmoMessage(string candidateID, int messageID)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                return CreateCmoMessage(context.CmoMessages.FirstOrDefault(m => m.CandidateId == candidateID && m.MessageId == messageID));
            }
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
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                IQueryable<Data.CmoMessage> results = context.CmoMessages;
                if (candidateID != null)
                    results = results.Where(m => m.CandidateId == candidateID);
                if (electionCycle != null)
                    results = results.Where(m => m.ElectionCycle == electionCycle);
                if (creator != null)
                    results = results.Where(m => m.CreatorADUserName == creator);
                if (category.HasValue)
                    results = results.Where(m => m.CmoCategory.CategoryId == category.Value);
                if (title != null)
                    results = results.Where(m => m.Title != null && m.Title.Contains(title));
                if (body != null)
                    results = results.Where(m => m.Body != null && m.Body.Contains(body));
                if (postedStart.HasValue || postedEnd.HasValue)
                    results = results.Where(m => m.PostDate.HasValue);
                if (postedStart.HasValue)
                    results = results.Where(m => EntityFunctions.TruncateTime(postedStart) <= m.PostDate);
                if (postedEnd.HasValue)
                    results = results.Where(m => EntityFunctions.TruncateTime(m.PostDate) <= EntityFunctions.TruncateTime(postedEnd));
                if (opened.HasValue)
                    results = results.Where(m => m.OpenDate.HasValue == opened.Value);
                if (hasAttachments.HasValue)
                {
                    results = hasAttachments.Value ? results.Where(m => m.CmoAttachments.Count > 0) : results.Where(m => m.CmoAttachments.Count == 0);
                }
                return results.AsEnumerable().Select(m => CreateCmoMessage(m, true)).ToList();
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
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                return context.CmoMessages.Where(m => m.PostDate != null && m.CandidateId == candidateID && (electionCycle == null || m.ElectionCycle == electionCycle)).AsEnumerable().Select(m => CreateCmoMessage(m)).ToList();
            }
        }

        /// <summary>
        /// Retrieves all posted messages.
        /// </summary>
        /// <param name="showAllData">true to show all message data; otherwise, false to show only summaric results.</param>
        /// <returns>A collection of all posted messages.</returns>
        public List<CmoMessage> GetCmoPostedMessages(bool showAllData)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                return context.CmoMessages.Where(m => m.PostDate != null).AsEnumerable().Select(m => CreateCmoMessage(m, !showAllData)).ToList();
            }
        }

        /// <summary>
        /// Retrieves all messages matching a collection of unique message IDs.
        /// </summary>
        /// <param name="uniqueIDs">The IDs of the messages to retrieve.</param>
        /// <returns>A collection of messages for the IDs provided.</returns>
        public List<CmoMessage> GetCmoMessages(IEnumerable<string> uniqueIDs)
        {
            if (uniqueIDs == null)
                return new List<CmoMessage>(0);
            return uniqueIDs.Select(i => GetCmoMessage(i)).Where(m => m != null).ToList();
        }

        /// <summary>
        /// Updates a CMO message instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the CMO message instance was saved successfuly; otherwise, false.</returns>
        public bool Update(CmoMessage message)
        {
            if (message != null && !message.IsPosted)
            {
                using (Data.CmoEntities context = new Data.CmoEntities())
                {
                    var ret = context.CmoSaveMessage(message.CandidateID, message.ID, message.ElectionCycle, message.Title, message.Body, message.Creator, message.OpenReceiptEmail, message.CategoryID, message.Version).FirstOrDefault();
                    if (!ret.HasValue || ret.Value != message.ID)
                        return false;
                    // update metadata
                    if (!SetCmoMessageAuditReviewNumber(message))
                        return false;
                    if (!SetCmoMessagePostElectionAuditRequestType(message))
                        return false;
                    if (!SetCmoMessageTolling(message))
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the tolling information for a tolling letter CMO message.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the tolling information was set or cleared successfully; otherwise, false.</returns>
        /// <remarks>The tolling information can only be successfully set if the message is a tolling letter and cleared if the message is not a tolling letter. The information will automatically be cleared for messages that are not tolling letters regardless of the message's tolling values.</remarks>
        private bool SetCmoMessageTolling(CmoMessage message)
        {
            if (message == null)
                return false;

            string candidateID = message.CandidateID;
            int messageID = message.ID;
            TollingLetter letter = message.TollingLetter;
            if (message.IsInadequateResponseLetter)
            {
                // force interpretation of inadequate response letters as tolling letters
                if (letter == null)
                    letter = message.TollingLetter = GetTollingLetter(CPConvert.ToCfisCode(AuditReportType.IdrInadequateResponse), message.IsIdrAdditionalInadequateLetter || message.IsDarAdditionalInadequateLetter ? "ADDINA" : "INARES", "INAD");
                if (!message.TollingEventNumber.HasValue)
                    message.TollingEventNumber = int.MinValue;
            }
            bool hasTolling = message.TollingEventNumber.HasValue && letter != null;
            bool isTolling = message.IsTollingLetter;
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var tl = context.CmoTollingLetters.FirstOrDefault(l => l.CandidateId == message.CandidateID && l.MessageId == message.ID);
                if (tl != null)
                {
                    if (isTolling)
                        tl.EventNumber = message.TollingEventNumber ?? int.MinValue;
                    else
                        context.DeleteObject(tl);
                }
                else if (isTolling)
                {
                    context.AddToCmoTollingLetters(Data.CmoTollingLetter.CreateCmoTollingLetter(candidateID, messageID, message.TollingEventNumber ?? int.MinValue, letter.ID));
                }
                try
                {
                    int updates = context.SaveChanges();
                    return isTolling ? updates > 0 : !hasTolling;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Sets the audit review number for an audit review CMO message.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the number was set or cleared successfully; otherwise, false.</returns>
        /// <remarks>The audit review number can only be successfully set if the message is an audit review and cleared if the message is not an audit review. The number will automatically be cleared for messages that are not audit reviews regardless of the message's review number value.</remarks>
        private bool SetCmoMessageAuditReviewNumber(CmoMessage message)
        {
            if (message == null)
                return false;
            bool hasNumber = message.AuditReviewNumber.HasValue;
            bool isReview = message.IsAuditReview && hasNumber;
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var review = context.CmoAuditReviews.FirstOrDefault(r => r.CandidateId == message.CandidateID && r.MessageId == message.ID);
                if (review != null)
                {
                    // update or delete existing row
                    if (isReview)
                    {
                        review.ReviewNumber = message.AuditReviewNumber.Value;
                    }
                    else
                    {
                        // always delete row if not audit review, but return true only if there is no longer a review number
                        context.DeleteObject(review);
                    }
                }
                else if (isReview)
                {
                    // no audit review entries were found, so try to add a new one if applicable
                    context.AddToCmoAuditReviews(Data.CmoAuditReview.CreateCmoAuditReview(message.CandidateID, message.ID, message.AuditReviewNumber.Value));
                }
                try
                {
                    int updates = context.SaveChanges();
                    return isReview ? updates > 0 : !hasNumber;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Sets the post election audit request type for an audit review CMO message.
        /// </summary>
        /// <param name="message">The CMO message to update.</param>
        /// <returns>true if the type was set or cleared successfully; otherwise, false.</returns>
        /// <remarks>The post election audit request type can only be successfully set if the message is a post election audit request and cleared if the message is not a post election audit request. The type will automatically be cleared for messages that are not post election audit requests regardless of the value in <paramref name="message"/>.</remarks>
        private bool SetCmoMessagePostElectionAuditRequestType(CmoMessage message)
        {
            if (message == null)
                return false;
            AuditRequestType? type = message.PostElectionAuditRequestType;
            bool hasRequestType = type.HasValue;
            bool isPea = message.IsPostElectionAudit && hasRequestType;
            bool isRepost = hasRequestType && (type == AuditRequestType.FirstRepost || type == AuditRequestType.SecondRepost);
            bool isSecond = hasRequestType && (type == AuditRequestType.SecondRepost || type == AuditRequestType.SecondRequest);
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var request = context.CmoPostElectionRequests.FirstOrDefault(r => r.CandidateId == message.CandidateID && r.MessageId == message.ID);
                if (request != null)
                {
                    // update or delete existing row
                    if (isPea)
                    {
                        request.Repost = isRepost;
                        request.SecondRequest = isSecond;
                    }
                    else
                    {
                        // always delete row if not a request, but return true only if there is no longer a request type
                        context.DeleteObject(request);
                    }
                }
                else if (isPea)
                {
                    // no post election audit request entries were found, so try to add a new one if applicable
                    context.AddToCmoPostElectionRequests(Data.CmoPostElectionRequest.CreateCmoPostElectionRequest(message.CandidateID, message.ID, isRepost, isSecond));
                }
                try
                {
                    int updates = context.SaveChanges();
                    return isPea ? updates > 0 : !hasRequestType;
                }
                catch (OptimisticConcurrencyException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Determines whether or not a CMO message has a valid post election audit request type.
        /// </summary>
        /// <param name="message">The message to validate.</param>
        /// <param name="secondExists">Whether or not a second request already exists.</param>
        /// <returns>true if the message violates post election audit workflow.</returns>
        private bool HasValidPostElectionRequestType(CmoMessage message, bool secondExists)
        {
            if (message != null && message.PostElectionAuditRequestType.HasValue)
            {
                AuditRequestType type = message.PostElectionAuditRequestType.Value;
                bool isRepost = type == AuditRequestType.FirstRepost || type == AuditRequestType.SecondRepost;
                bool isSecond = type == AuditRequestType.SecondRepost || type == AuditRequestType.SecondRequest;
                // since a current status already exists, only allow reposts and original second requests
                return (isRepost && isSecond == secondExists) || (isSecond && !secondExists);
            }
            return true;
        }

        /// <summary>
        /// Deletes a CMO message from the persistence storage medium.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate recipient of the message to delete.</param>
        /// <param name="messageID">The ID of the message to delete.</param>
        /// <param name="version">The version of the message to update.</param>
        /// <returns>true if the CMO message instance was deleted successfully; otherwise, false.</returns>
        public bool Delete(string candidateID, int messageID, byte[] version)
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                var msg = context.CmoMessages.FirstOrDefault(m => m.CandidateId == candidateID && m.MessageId == messageID && m.Version == version && !m.PostDate.HasValue);
                if (msg != null)
                {
                    // delete attachments first
                    foreach (var a in msg.CmoAttachments)
                    {
                        if (!Delete(GetCmoAttachment(candidateID, messageID, a.AttachmentId)))
                            return false;
                    }

                    context.DeleteObject(msg);
                    try
                    {
                        if (context.SaveChanges() > 0)
                            return true;
                    }
                    catch (OptimisticConcurrencyException) { }
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves a collection of the usernames of all users who have created messages, mapped to their respective full names.
        /// </summary>
        /// <returns>A collection of the useranmes of all users who have created messages, mapped to their respective full names.</returns>
        public Dictionary<string, string> GetCmoMessageCreators()
        {
            using (Data.CmoEntities context = new Data.CmoEntities())
            {
                return (from m in context.CmoMessages
                        where m != null
                        select m.CreatorADUserName).Distinct().ToDictionary(n => n, n => CfbDirectorySearcher.GetUser(n).DisplayName ?? n);
            }
        }

        /// <summary>
        /// Creates a new <see cref="CmoMessage"/> instance using C-Access Messages Online message data.
        /// </summary>
        /// <param name="data">The CMO message to use.</param>
        /// <param name="summary">Indicates whether or not only summaric information is requested.</param>
        /// <returns>A new CMO message instance using the specified data if valid; otherwise, null.</returns>
        private CmoMessage CreateCmoMessage(Data.CmoMessage data, bool summary = false)
        {
            if (data == null)
                return null;
            CmoMessage message = new CmoMessage(data.CandidateId, data.MessageId, data.Version)
            {
                ElectionCycle = data.ElectionCycle,
                Title = data.Title,
                PostDate = data.PostDate,
                Opener = data.OpenUserName,
                OpenDate = data.OpenDate,
                Creator = data.CreatorADUserName,
                NeedsFollowUp = data.FollowUp
            };
            // load attachments
            foreach (var attachment in data.CmoAttachments.Select(a => CreateCmoAttachment(a)))
            {
                message.Attachments.Add(attachment.ID, attachment);
            }
            if (!summary)
            {
                message.OpenReceiptEmail = data.OpenReceiptEmail;
                message.Body = data.Body;
                message.Category = CreateCmoCategory(data.CmoCategory);
                message.Archiver = data.ArchiveUserName;
                message.ArchiveDate = data.ArchiveDate;
                message.FollowUpFlagger = data.FollowUpUserName;
                message.FollowUpDate = data.FollowUpDate;
                if (message.IsAuditReview && data.CmoAuditReview != null)
                {
                    message.AuditReviewNumber = data.CmoAuditReview.ReviewNumber;
                }
                else if (message.IsPostElectionAudit)
                {
                    var pe = data.CmoPostElectionRequest;
                    if (pe != null)
                    {
                        if (pe.SecondRequest)
                            message.PostElectionAuditRequestType = pe.Repost ? AuditRequestType.SecondRepost : AuditRequestType.SecondRequest;
                        else
                            message.PostElectionAuditRequestType = pe.Repost ? AuditRequestType.FirstRepost : AuditRequestType.FirstRequest;
                    }
                }
                if (message.IsTollingLetter)
                {
                    var tl = data.CmoTollingLetter;
                    if (tl != null)
                    {
                        message.TollingEventNumber = tl.EventNumber;
                        message.TollingLetter = GetTollingLetter(tl.LetterId);
                    }
                }
            }
            return message;
        }
    }
}
