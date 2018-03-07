using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Cfb.CandidatePortal.PostElection;
using Cfb.DirectoryServices;
using Cfb.Extensions;
using System.Collections.Specialized;

namespace Cfb.CandidatePortal.Cmo
{
    /// <summary>
    /// A Campaign Message Online message for faciliatating communication between the CFB and campaigns via the C-Access Candidate Portal.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class CmoMessage : IPersistable
    {
        /// <summary>
        /// The character separating the left and right parts of a unique message identifier.
        /// </summary>
        internal const char UniqueIdSeparator = '-';

        /// <summary>
        /// A delegate for performing actions on selected messages.
        /// </summary>
        /// <param name="username">The C-Access username of the user performing the action.</param>
        /// <returns>true if the action was completed successfully; otherwise, false.</returns>
        public delegate bool MessageAction(string username);

        /// <summary>
        /// The candidate ID for the targeted candidate recipient.
        /// </summary>
        [DataMember(Name = "CandidateID")]
        private readonly string _candidateID;

        /// <summary>
        /// Gets the ID of the targeted candidate recipient.
        /// </summary>
        public string CandidateID
        {
            get { return _candidateID; }
        }

        /// <summary>
        /// Gets the full name of the targeted candidate recipient.
        /// </summary>
        public string CandidateName
        {
            get { return CPProviders.DataProvider.GetCandidateName(_candidateID, false); }
        }

        /// <summary>
        /// The unique-by-candidate message identifier.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly int _id;

        /// <summary>
        /// Gets the unique-by-candidate message identifier.
        /// </summary>
        public int ID
        {
            get { return _id; }
        }

        /// <summary>
        /// The message attachments.
        /// </summary>
        [DataMember(Name = "Attachments")]
        private readonly Dictionary<byte, CmoAttachment> _attachments;

        /// <summary>
        /// Gets the message attachments.
        /// </summary>
        public Dictionary<byte, CmoAttachment> Attachments
        {
            get { return _attachments; }
        }

        /// <summary>
        /// Gets a sequence of names for all message attachments, separated by line terminators.
        /// </summary>
        public string AttachmentNames
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (CmoAttachment a in this.Attachments.Values)
                {
                    sb.AppendLine(a.FileName);
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets a user-friendly unique message identifier.
        /// </summary>
        public string UniqueID
        {
            get { return ToUniqueID(this.CandidateID, _id); }
        }

        /// <summary>
        /// Gets or sets the relevant election cycle.
        /// </summary>
        [DataMember]
        public string ElectionCycle { get; set; }

        /// <summary>
        /// Gets or sets the message title.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the message body HTML.
        /// </summary>
        [DataMember]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the username of the message creator.
        /// </summary>
        [DataMember]
        public string Creator { get; set; }

        /// <summary>
        /// Gets the full name of the message creator as defined on the network.
        /// </summary>
        public string CreatorName
        {
            get
            {
                User u = CfbDirectorySearcher.GetUser(this.Creator);
                return u.IsAdHoc ? u.Username : u.DisplayName;
            }
        }

        /// <summary>
        /// Gets or sets the recipient address for an open receipt e-mail.
        /// </summary>
        [DataMember]
        public string OpenReceiptEmail { get; set; }

        /// <summary>
        /// Gets or sets the date when the message was sent.
        /// </summary>
        [DataMember]
        public DateTime? PostDate { get; set; }

        /// <summary>
        /// Gets whether or not the message has been posted.
        /// </summary>
        public bool IsPosted
        {
            get { return this.PostDate.HasValue; }
        }

        /// <summary>
        /// Gets or sets the username of the first user to open the message.
        /// </summary>
        [DataMember]
        public string Opener { get; set; }

        /// <summary>
        /// Gets or sets the date when the message was first opened.
        /// </summary>
        [DataMember]
        public DateTime? OpenDate { get; set; }

        /// <summary>
        /// Gets whether or not the message has been opened.
        /// </summary>
        public bool IsOpened
        {
            get { return this.OpenDate.HasValue; }
        }

        /// <summary>
        /// Gets or sets the username of the user who archived the message.
        /// </summary>
        [DataMember]
        public string Archiver { get; set; }

        /// <summary>
        /// Gets or sets the date when the message was archived.
        /// </summary>
        [DataMember]
        public DateTime? ArchiveDate { get; set; }

        /// <summary>
        /// Gets whether or not the message has been archived.
        /// </summary>
        public bool IsArchived
        {
            get { return this.ArchiveDate.HasValue; }
        }

        /// <summary>
        /// Gets or sets the username of the user who flagged the message for follow-up.
        /// </summary>
        [DataMember]
        public string FollowUpFlagger { get; set; }

        /// <summary>
        /// Gets or sets the date when the message's follow up flag was last updated.
        /// </summary>
        [DataMember]
        public DateTime? FollowUpDate { get; set; }

        /// <summary>
        /// Gets whether or not the message is flagged for follow-up.
        /// </summary>
        [DataMember]
        public bool NeedsFollowUp { get; set; }

        /// <summary>
        /// Gets whether or not the message has an attachment.
        /// </summary>
        public bool HasAttachment
        {
            get { return _attachments.Count > 0; }
        }

        /// <summary>
        /// Gets the version of the message.
        /// </summary>
        [DataMember]
        public byte[] Version { get; private set; }

        /// <summary>
        /// Gets or sets the message category.
        /// </summary>
        [DataMember]
        public CmoCategory Category { get; set; }

        /// <summary>
        /// Gets the message category name.
        /// </summary>
        public string CategoryName
        {
            get { return this.Category == null ? null : this.Category.Name; }
        }

        /// <summary>
        /// Gets the message category ID.
        /// </summary>
        public byte? CategoryID
        {
            get { return Category == null ? null : (byte?)Category.ID; }
        }

        /// <summary>
        /// Gets or sets the review number for the associated audit review.
        /// </summary>
        [DataMember]
        public byte? AuditReviewNumber { get; set; }

        /// <summary>
        /// Gets the run number of the associated payment, or null if no payment is associated.
        /// </summary>
        public byte? PaymentRun
        {
            get { return this.IsPaymentLetter ? this.AuditReviewNumber : null; }
        }

        /// <summary>
        /// Gets or sets the event number of the assocaited tolling letter.
        /// </summary>
        [DataMember]
        public int? TollingEventNumber { get; set; }

        /// <summary>
        /// Gets or sets the associated tolling letter.
        /// </summary>
        [DataMember]
        public TollingLetter TollingLetter { get; set; }

        /// <summary>
        /// Gets the number of the associated statement review, or null if no review is associated.
        /// </summary>
        public byte? StatementNumber
        {
            get { return this.IsStatementReview || this.IsDoingBusinessReview ? this.AuditReviewNumber : null; }
        }

        /// <summary>
        /// Gets the number of the associated compliance visit, or null if no visit is associated.
        /// </summary>
        public byte? ComplianceVisitNumber
        {
            get { return this.IsComplianceVisit ? this.AuditReviewNumber : null; }
        }

        /// <summary>
        /// Gets or sets the post election audit request type.
        /// </summary>
        [DataMember]
        public AuditRequestType? PostElectionAuditRequestType { get; set; }

        /// <summary>
        /// Gets whether or not this message is a statement review.
        /// </summary>
        public bool IsStatementReview
        {
            get { return this.CategoryID == CmoCategory.StatementReviewCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a compliance visit review.
        /// </summary>
        public bool IsComplianceVisit
        {
            get { return this.CategoryID == CmoCategory.ComplianceVisitCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a Doing Business review.
        /// </summary>
        public bool IsDoingBusinessReview
        {
            get { return this.CategoryID == CmoCategory.DoingBusinessReviewCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is an audit review.
        /// </summary>
        public bool IsAuditReview
        {
            get { return this.IsStatementReview || this.IsComplianceVisit || this.IsDoingBusinessReview || this.IsPaymentLetter; }
        }

        /// <summary>
        /// Gets whether or not this message is a payment letter.
        /// </summary>
        public bool IsPaymentLetter
        {
            get { return this.CategoryID == CmoCategory.PaymentCategoryID || this.CategoryID == CmoCategory.PostElectionPaymentCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is an initial documentation request.
        /// </summary>
        public bool IsInitialDocumentationRequest
        {
            get { return this.CategoryID == CmoCategory.IdrCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a draft audit report.
        /// </summary>
        public bool IsDraftAuditReport
        {
            get { return this.CategoryID == CmoCategory.DarCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a final audit report.
        /// </summary>
        public bool IsFinalAuditReport
        {
            get { return this.CategoryID == CmoCategory.FarCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a post election audit.
        /// </summary>
        public bool IsPostElectionAudit
        {
            get { return this.IsInitialDocumentationRequest || this.IsInadequateResponseLetter || this.IsDraftAuditReport || this.IsFinalAuditReport; }
        }

        /// <summary>
        /// Gets whether or not this message is a tolling letter.
        /// </summary>
        public bool IsTollingLetter
        {
            get { return this.CategoryID == CmoCategory.TollingLetterCategoryID || this.IsInadequateResponseLetter; }
        }

        /// <summary>
        /// Gets whether or not this message is an initial documentation request inadequate response letter.
        /// </summary>
        public bool IsIdrInadequateResponseLetter
        {
            get { return this.CategoryID == CmoCategory.IdrInadequateCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a draft audit report inadequate response letter.
        /// </summary>
        public bool IsDarInadequateResponseLetter
        {
            get { return this.CategoryID == CmoCategory.DarInadequateCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is an initial documentation request additional inadequate response letter.
        /// </summary>
        public bool IsIdrAdditionalInadequateLetter
        {
            get { return this.CategoryID == CmoCategory.IdrAdditionalInadequateCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is a draft audit report additional inadequate response letter.
        /// </summary>
        public bool IsDarAdditionalInadequateLetter
        {
            get { return this.CategoryID == CmoCategory.DarAdditionalInadequateCategoryID; }
        }

        /// <summary>
        /// Gets whether or not this message is an inadequate response letter.
        /// </summary>
        public bool IsInadequateResponseLetter
        {
            get { return this.CategoryID.HasValue && new[] { CmoCategory.IdrInadequateCategoryID, CmoCategory.IdrAdditionalInadequateCategoryID, CmoCategory.DarInadequateCategoryID, CmoCategory.DarAdditionalInadequateCategoryID }.Contains(this.CategoryID.Value); }
        }

        /// <summary>
        /// Gets the current delivery status of the message.
        /// </summary>
        public CmoMessageStatus Status
        {
            get
            {
                // lifecycle of message status: draft, posted, opened, archived
                if (!this.PostDate.HasValue)
                    return CmoMessageStatus.Draft;
                else if (string.IsNullOrEmpty(this.Opener) || !this.OpenDate.HasValue)
                    return CmoMessageStatus.Posted;
                else if (string.IsNullOrEmpty(this.Archiver) || !this.ArchiveDate.HasValue)
                    return CmoMessageStatus.Opened;
                else
                    return CmoMessageStatus.Archived;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmoMessage"/> class.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the message recipient candidate.</param>
        /// <param name="id">The message's unique identifier.</param>
        /// <exception cref="ArgumentNullException"><paramref name="candidateID"/> is null or empty.</exception>
        internal CmoMessage(string candidateID, int id)
        {
            if (string.IsNullOrEmpty(candidateID))
                throw new ArgumentNullException("candidateID", "Candidate ID must not be null or empty.");
            _candidateID = candidateID.ToUpper();
            _id = id;
            _attachments = new Dictionary<byte, CmoAttachment>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CmoMessage"/> class.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the message recipient candidate.</param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        internal CmoMessage(string candidateID, int id, byte[] version)
            : this(candidateID, id)
        {
            this.Version = version;
        }

        /// <summary>
        /// Gets the name and ID of the message's targeted candidate recipient as a user-friendly string.
        /// </summary>
        public string GetCandidateNameID()
        {
            return string.Format("{0} ({1})", CPProviders.DataProvider.GetCandidateName(_candidateID, true), _candidateID);
        }

        /// <summary>
        /// Posts the message for viewing by C-Access accounts.
        /// </summary>
        /// <param name="notify">true if posted message notifications should be sent; otherwise, false.</param>
        /// <returns>true if the message was succesfully posted; otherwise, false.</returns>
        public bool Post(bool notify = true)
        {
            try
            {
                return CmoProviders.DataProvider.PostCmoMessage(_candidateID, _id, this.Version, notify);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Marks the message as opened.
        /// </summary>
        /// <param name="username">The C-Access username of the user who opened the message.</param>
        /// <returns>true if the message was opened successfully; otherwise, false.</returns>
        public bool Open(string username)
        {
            try
            {
                return CmoProviders.DataProvider.Open(this, username);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Archives the message.
        /// </summary>
        /// <param name="username">The C-Access username of the user who archived the message.</param>
        /// <returns>true if the message was archived successfully; otherwise, false.</returns>
        public bool Archive(string username)
        {
            try
            {
                return CmoProviders.DataProvider.SetArchiveStatus(this, true, username);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Unarchives the message.
        /// </summary>
        /// <param name="username">The C-Access username of the user who unarchived the message.</param>
        /// <returns>true if the message was unarchived successfully; otherwise, false.</returns>
        public bool Unarchive(string username)
        {
            try
            {
                return CmoProviders.DataProvider.SetArchiveStatus(this, false, username);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Sets the message's follow-up flag.
        /// </summary>
        /// <param name="username">The C-Access username of the user setting the flag.</param>
        /// <returns>true if the flag was set successfully; otherwise, false.</returns>
        public bool SetFlag(string username)
        {
            try
            {
                return CmoProviders.DataProvider.SetFlagStatus(this, true, username);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Clears the message's follow-up flag.
        /// </summary>
        /// <param name="username">The C-Access username of the user clearing the flag.</param>
        /// <returns>true if the flag was cleared successfully; otherwise, false.</returns>
        public bool ClearFlag(string username)
        {
            try
            {
                return CmoProviders.DataProvider.SetFlagStatus(this, false, username);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Attaches a file attachment to the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="data">The binary data contents of the attachment.</param>
        /// <param name="name">The filename of the attachment.</param>
        /// <returns>A <see cref="CmoAttachment"/> for the attachment if the file was attached successfully; otherwise, null.</returns>
        public CmoAttachment Attach(byte[] data, string name)
        {
            return CmoProviders.DataProvider.Attach(this, data, name);
        }

        /// <summary>
        /// Detaches a file attachment from the message using the default CMO attachment repository.
        /// </summary>
        /// <param name="attachmentID">The ID of the attachment to detach.</param>
        /// <returns>true if the attachment was removed successfully; otherwise, false.</returns>
        public bool Detach(byte attachmentID)
        {
            return CmoProviders.DataProvider.Detach(this, attachmentID);
        }

        #region IPersistable Members

        /// <summary>
        /// Reloads the message from the persistence storage medium.
        /// </summary>
        /// <returns>true if this instance was reloaded successfully; otherwise, false.</returns>
        public bool Reload()
        {
            CmoMessage current = CmoProviders.DataProvider.GetCmoMessage(_candidateID, _id);
            if (current != null)
            {
                _attachments.Clear();
                foreach (byte id in current.Attachments.Keys)
                {
                    _attachments[id] = current.Attachments[id];
                }
                this.ArchiveDate = current.ArchiveDate;
                this.Archiver = current.Archiver;
                this.AuditReviewNumber = current.AuditReviewNumber;
                this.Body = current.Body;
                this.Category = current.Category;
                this.Creator = current.Creator;
                this.ElectionCycle = current.ElectionCycle;
                this.FollowUpDate = current.FollowUpDate;
                this.FollowUpFlagger = current.FollowUpFlagger;
                this.NeedsFollowUp = current.NeedsFollowUp;
                this.OpenDate = current.OpenDate;
                this.Opener = current.Opener;
                this.OpenReceiptEmail = current.OpenReceiptEmail;
                this.PostDate = current.PostDate;
                this.PostElectionAuditRequestType = current.PostElectionAuditRequestType;
                this.Title = current.Title;
                this.TollingEventNumber = current.TollingEventNumber;
                this.TollingLetter = current.TollingLetter;
                this.Version = current.Version;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates this instance in the persistence storage medium by overwriting the existing record.
        /// </summary>
        /// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
        public bool Update()
        {
            try
            {
                return CmoProviders.DataProvider.Update(this);
            }
            finally
            {
                this.Reload();
            }
        }

        /// <summary>
        /// Deletes this instance from the persistence storage medium.
        /// </summary>
        /// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
        public bool Delete()
        {
            return CmoProviders.DataProvider.Delete(this);
        }

        #endregion

        /// <summary>
        /// Converts a message candidate ID and message ID into a unique ID for the message.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate context.</param>
        /// <param name="messageID">The ID of the candidate's message.</param>
        /// <returns>A unique identifier for the message.</returns>
        public static string ToUniqueID(string candidateID, int messageID)
        {
            return string.Format("{0}{1}{2}", candidateID, UniqueIdSeparator, messageID);
        }

        /// <summary>
        /// Parses a unique message ID into its candidate ID and message ID components.
        /// </summary>
        /// <param name="uniqueID">The unique message ID to parse.</param>
        /// <param name="candidateID">When this method returns, contains the candidate ID component of the unique ID contained in <paramref name="uniqueID"/>, if the parse succeeded, or null if the parse failed. The parse fails if the <paramref name="uniqueID"/> parameter is null or is not of the correct format. This parameter is passed uninitialized.</param>
        /// <param name="messageID">When this method returns, contains the message ID component of the unique ID contained in <paramref name="uniqueID"/>, if the parse succeeded, or zero if the parse failed. The parse fails if the <paramref name="uniqueID"/> parameter is null or is not of the correct format. This parameter is passed uninitialized.</param>
        /// <returns></returns>
        public static bool TryParseUniqueID(string uniqueID, out string candidateID, out int messageID)
        {
            candidateID = null;
            messageID = 0;
            if (!string.IsNullOrEmpty(uniqueID))
            {
                int separatorIndex = uniqueID.IndexOf(CmoMessage.UniqueIdSeparator);
                if ((separatorIndex > 0) && (separatorIndex + 1 < uniqueID.Length))
                {
                    if (int.TryParse(uniqueID.Substring(separatorIndex + 1), out messageID))
                    {
                        candidateID = uniqueID.Substring(0, separatorIndex);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="uniqueID">The unique message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public static CmoMessage GetMessage(string uniqueID)
        {
            return CmoProviders.DataProvider.GetCmoMessage(uniqueID);
        }

        /// <summary>
        /// Gets a specific Campaign Messages Online message.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate who is the message recipient.</param>
        /// <param name="messageID">The message identifier.</param>
        /// <returns>The requested message if found; otherwise, null.</returns>
        public static CmoMessage GetMessage(string candidateID, int messageID)
        {
            return CmoProviders.DataProvider.GetCmoMessage(candidateID, messageID);
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
        public static List<CmoMessage> FindCmoMessages(string candidateID = null, string electionCycle = null, string creator = null, byte? category = null, string title = null, string body = null, DateTime? postedStart = null, DateTime? postedEnd = null, bool? opened = null, bool? hasAttachments = null)
        {
            return CmoProviders.DataProvider.FindCmoMessages(candidateID, electionCycle, creator, category, title, body, postedStart, postedEnd, opened, hasAttachments);
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
        public static CmoMessage Add(string candidateID, string electionCycle, string title, string body, string creator, string openReceiptEmail, byte categoryID)
        {
            return CmoProviders.DataProvider.AddCmoMessage(candidateID, electionCycle, title, body, creator, openReceiptEmail, categoryID);
        }
    }
}
