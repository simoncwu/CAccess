using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.Cmo
{
    /// <summary>
    /// A Campaign Messages Online mailbox for providing C-Access account access to inbox amd messages.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class CmoMailbox : IQueryStringAware
    {
        /// <summary>
        /// The query string parameter for passing a mailbox view.
        /// </summary>
        public const string ViewParameter = "view";

        /// <summary>
        /// The query string parameter for passing a mailbox election cycle.
        /// </summary>
        public const string ElectionCycleParameter = "ec";

        /// <summary>
        /// The query string parameter for passing a mailbox sort order.
        /// </summary>
        public const string SortByParameter = "sortby";

        /// <summary>
        /// The query string parameter for passing a mailbox sort direction.
        /// </summary>
        public const string SortDirectionParameter = "sortdir";

        /// <summary>
        /// The query string value for passing an ascending sort direction.
        /// </summary>
        public const string AscendingSortValue = "asc";

        /// <summary>
        /// The default mailbox view.
        /// </summary>
        public const CmoMailboxView DefaultView = CmoMailboxView.Current;

        /// <summary>
        /// The default mailbox sort type.
        /// </summary>
        public const CmoMailboxSortType DefaultSortType = CmoMailboxSortType.Date;

        /// <summary>
        /// The default mailbox sort direction.
        /// </summary>
        public const bool DefaultSortDescending = true;

        /// <summary>
        /// The CFIS ID of the mailbox's candidate context.
        /// </summary>
        [DataMember(Name = "CandidateID")]
        private readonly string _candidateID;

        /// <summary>
        /// Gets the CFIS ID of the mailbox's candidate context.
        /// </summary>
        public string CandidateID
        {
            get { return _candidateID; }
        }

        /// <summary>
        /// A collection of election cycles displayed in the mailbox.
        /// </summary>
        [DataMember(Name = "ElectionCycles")]
        private readonly HashSet<string> _electionCycles;

        /// <summary>
        /// Gets a collection of displayed in the mailbox.
        /// </summary>
        public HashSet<string> ElectionCycles
        {
            get { return _electionCycles; }
        }

        /// <summary>
        /// Gets or sets the C-Access username of the mailbox user.
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// Gets the total number of unopened messages in the mailbox.
        /// </summary>
        [DataMember]
        public uint UnopenedCount { get; private set; }

        /// <summary>
        /// Gets or sets the messages in the mailbox.
        /// </summary>
        public List<CmoMessage> Messages { get; set; }

        /// <summary>
        /// Gets the mailbox view type.
        /// </summary>
        [DataMember]
        public CmoMailboxView View { get; set; }

        /// <summary>
        /// Gets or sets the mailbox sort column.
        /// </summary>
        [DataMember]
        public CmoMailboxSortType SortType { get; set; }

        /// <summary>
        /// Gets or sets whether to sort the mailbox in descending order.
        /// </summary>
        [DataMember]
        public bool SortDescending { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="CmoMailbox"/> class targeted towards a specific candidate.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the desired candidate.</param>
        private CmoMailbox(string candidateID)
        {
            _candidateID = candidateID;
            _electionCycles = new HashSet<string>();
            this.Messages = new List<CmoMessage>();
            this.View = DefaultView;
            this.SortType = DefaultSortType;
            this.SortDescending = true;
        }

        /// <summary>
        /// Sorts the mailbox messages according to the values of the <see cref="CmoMailbox.SortType"/> and <see cref="CmoMailbox.SortDescending"/> properties.
        /// </summary>
        public void SortMessages()
        {
            switch (this.SortType)
            {
                case CmoMailboxSortType.ElectionCycle:
                    this.Messages = this.SortDescending ? this.Messages.OrderByDescending(m => m.ElectionCycle).ToList() : this.Messages.OrderBy(m => m.ElectionCycle).ToList();
                    break;
                case CmoMailboxSortType.FollowUp:
                    this.Messages = this.SortDescending ? this.Messages.OrderByDescending(m => m.NeedsFollowUp).ToList() : this.Messages.OrderBy(m => m.NeedsFollowUp).ToList();
                    break;
                case CmoMailboxSortType.Subject:
                    this.Messages = this.SortDescending ? this.Messages.OrderByDescending(m => m.Title).ToList() : this.Messages.OrderBy(m => m.Title).ToList();
                    break;
                case CmoMailboxSortType.Date:
                default:
                    this.Messages = this.SortDescending ? this.Messages.OrderByDescending(m => m.PostDate).ToList() : this.Messages.OrderBy(m => m.PostDate).ToList();
                    break;
            }
        }

        #region IQueryStringAware Members

        /// <summary>
        /// Serializes properties to a query string.
        /// </summary>
        /// <returns>A <see cref="NameValueCollection"/> query string containing the properties of the mailbox as values.</returns>
        public NameValueCollection GetQueryString()
        {
            NameValueCollection c = new NameValueCollection();
            if (this.View != DefaultView)
                c[ViewParameter] = this.View.ToString();
            if (this.SortType != DefaultSortType)
                c[SortByParameter] = this.SortType.ToString();
            if (this.SortDescending != DefaultSortDescending)
                c[SortDirectionParameter] = AscendingSortValue;
            return c;

        }

        /// <summary>
        /// Parses and sets properties from values in a query string.
        /// </summary>
        /// <param name="queryString">The query string to parse.</param>
        public void ParseQueryString(NameValueCollection queryString)
        {
            if (queryString != null)
            {
                string view = queryString[ViewParameter];
                try
                {
                    this.View = CPConvert.ParseEnum<CmoMailboxView>(queryString[ViewParameter]);
                }
                catch { }
                try
                {
                    this.SortType = CPConvert.ParseEnum<CmoMailboxSortType>(queryString[SortByParameter]);
                }
                catch { }
                this.SortDescending = !AscendingSortValue.Equals(queryString[SortDirectionParameter], StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        /// <summary>
        /// Retrieves the mailbox for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateId">The CFIS ID of the candidate whose mailbox is being requested.</param>
        /// <param name="elections">A collection of election cycles to display in the mailbox.</param>
        /// <returns>A <see cref="CmoMailbox"/> for the targeted candidate.</returns>
        public static CmoMailbox GetMailbox(string candidateId, Elections elections)
        {
            return GetMailbox(candidateId, elections, null);
        }

        /// <summary>
        /// Retrieves the mailbox for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateId">The CFIS ID of the candidate whose mailbox is being requested.</param>
        /// <param name="elections">A collection of election cycles to display in the mailbox.</param>
        /// <param name="queryString">A query string specifying an initial mailbox state.</param>
        /// <returns>A <see cref="CmoMailbox"/> for the targeted candidate in the requested state.</returns>
        public static CmoMailbox GetMailbox(string candidateId, Elections elections, NameValueCollection queryString)
        {
            CmoMailbox mbx = new CmoMailbox(candidateId);
            if (elections != null)
                mbx.ElectionCycles.UnionWith(elections.Values.Select(e => e.Cycle));
            mbx.ParseQueryString(queryString);
            return mbx;
        }

        /// <summary>
        /// Retrieves a message from the mailbox by unique message ID.
        /// </summary>
        /// <param name="uniqueID">The unique ID of the message to retrieve.</param>
        /// <returns>A <see cref="CmoMessage"/> representing the requested message if found; otherwise, null.</returns>
        /// <remarks>Retrieving the message will also mark it as "opened".</remarks>
        public CmoMessage OpenMessage(string uniqueID)
        {
            CmoMessage message = CmoMessage.GetMessage(uniqueID);
            if (message != null)
            {
                // BUGFIX #52: Fixed loophole where other campaign users can mark other campaigns' unread messages as "open", by adding a check to see if the candidate ID context matches before opening a message in a mailbox.
                if (!string.IsNullOrEmpty(this.Username) && message.IsPosted && _electionCycles.Contains(message.ElectionCycle) && string.Equals(message.CandidateID, _candidateID, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (message.Open(this.Username))
                        return message;
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves a message from the mailbox by searching the current contents for a unique message ID match.
        /// </summary>
        /// <param name="uniqueID">The unique ID of the message to retrieve.</param>
        /// <param name="previousID">When this method returns, contains the ID of the message immediately preceding the message retrieved, or 0 if there is no previous message. This parameter is passed uninitialized.</param>
        /// <param name="nextID">When this method returns, contains the ID of the message immediately following the message retrieved, or 0 if there is no subsequent message. This parameter is passed uninitialized.</param>
        /// <param name="index">When this method returns, contains the index of the message to retrieve, or -1 if the message was not found. This parameter is passed uninitialized.</param>
        /// <returns>A <see cref="CmoMessage"/> representing the requested message if found; otherwise, null.</returns>
        /// <remarks>Retrieving the message will also mark it as "opened". This method will only be able to find messages currently in the mailbox via the <see cref="Messages"/> property.</remarks>
        public CmoMessage OpenMessage(string uniqueID, out string previousID, out string nextID, out int index)
        {
            previousID = nextID = string.Empty;
            index = this.Messages.FindIndex(m => (m.UniqueID == uniqueID));
            if (index == -1)
            {
                previousID = null;
                nextID = null;
            }
            else
            {
                if (index > 0)
                    previousID = this.Messages[index - 1].UniqueID;
                if (index < this.Messages.Count - 1)
                    nextID = this.Messages[index + 1].UniqueID;
            }
            return OpenMessage(uniqueID);
        }

        /// <summary>
        /// Archives a message.
        /// </summary>
        /// <param name="messageID">The ID of the message to archive.</param>
        /// <returns>true if the message was archived successfully; otherwise, false.</returns>
        public bool Archive(int messageID)
        {
            CmoMessage m = CmoMessage.GetMessage(_candidateID, messageID);
            return m != null ? m.Archive(this.Username) : false;
        }

        /// <summary>
        /// Unarchives a message.
        /// </summary>
        /// <param name="messageID">The ID of the message to unarchive.</param>
        /// <returns>true if the message was unarchived successfully; otherwise, false.</returns>
        public bool Unarchive(int messageID)
        {
            CmoMessage m = CmoMessage.GetMessage(_candidateID, messageID);
            return m != null ? m.Unarchive(this.Username) : false;
        }

        /// <summary>
        /// Sets a message's follow-up flag.
        /// </summary>
        /// <param name="messageID">The ID of the message to flag for follow-up.</param>
        /// <returns>true if the message's follow-up flag was set successfully; otherwise, false.</returns>
        public bool SetFlag(int messageID)
        {
            CmoMessage m = CmoMessage.GetMessage(_candidateID, messageID);
            return m != null ? m.SetFlag(this.Username) : false;
        }

        /// <summary>
        /// Clears a message's follow-up flag.
        /// </summary>
        /// <param name="messageID">The ID of the message to unflag for follow-up.</param>
        /// <returns>true if the message's follow-up flag was cleared successfully; otherwise, false.</returns>
        public bool ClearFlag(int messageID)
        {
            CmoMessage m = CmoMessage.GetMessage(_candidateID, messageID);
            return m != null ? m.ClearFlag(this.Username) : false;
        }

        /// <summary>
        /// Retrieves messages from the mailbox.
        /// </summary>
        /// <param name="view">The desired mailbox view.</param>
        /// <returns>A collection of <see cref="CmoMessage"/> objects matching the specified view.</returns>
        public List<CmoMessage> GetMessages(CmoMailboxView view)
        {
            this.View = view;
            this.GetMessages();
            return this.Messages;
        }

        /// <summary>
        /// Retrieves messages from the mailbox for the current mailbox view.
        /// </summary>
        /// <returns>A collection of <see cref="CmoMessage"/> objects for the current mailbox view.</returns>
        public void GetMessages()
        {
            this.Messages.Clear();
            this.Messages = CmoProviders.DataProvider.GetMailboxMessages(_candidateID, this.View, _electionCycles);
            this.SortMessages();
        }

        /// <summary>
        /// Gets the total number of unread messages in the mailbox.
        /// </summary>
        /// <returns>The total number of unread messges in the mailbox.</returns>
        public uint CountUnopened()
        {
            return CmoProviders.DataProvider.CountUnopenedMailboxMessages(_candidateID, _electionCycles);
        }
    }
}
