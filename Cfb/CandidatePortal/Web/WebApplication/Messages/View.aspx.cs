using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.Web.Controls;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.WebApplication.Messages
{
    public partial class View : CPPage, ISecurePage
    {
        /// <summary>
        /// Format string for formatting dates with associated usernames.
        /// </summary>
        private const string UserActionDateFormat = "{0:d} {0:t} by user {1}";

        /// <summary>
        /// A delegate for performing actions on selected messages.
        /// </summary>
        /// <param name="username">The C-Access username of the user performing the action.</param>
        /// <returns>true if the action was completed successfully; otherwise, false.</returns>
        private delegate bool MessageAction(string username);

        /// <summary>
        /// The source Campaign Messages Online message to display.
        /// </summary>
        private CmoMessage _datasource;

        /// <summary>
        /// The current Campaign Messages Online mailbox context.
        /// </summary>
        private CmoMailbox _mailbox;

        /// <summary>
        /// The unique message ID of the preceding message in the mailbox.
        /// </summary>
        private string _previousId;

        /// <summary>
        /// The unique message ID of the next message in the mailbox.
        /// </summary>
        private string _nextId;

        /// <summary>
        /// The mailbox index of the message to display.
        /// </summary>
        private int _messageIndex;

        /// <summary>
        /// Handles the <see cref="Control.OnLoad" /> event that occurs as an instance of the page is being created.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string UniqueID = Request.QueryString[CmoMailboxMessageList.MessageIdParameterName];
            _mailbox = CmoMailbox.GetMailbox(CPProfile.Cid, CPProfile.Elections, Request.QueryString);
            _mailbox.Username = User.Identity.Name;
            if (!Page.IsPostBack)
            {
                _datasource = GetMessage(UniqueID);
                this.DataBind();
            }
            Messages master = this.Master as Messages;
            if (master != null)
            {
                master.DataSource = _mailbox;
                master.DataBind();
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.DataBinding"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnDataBinding(EventArgs e)
        {
            if (_datasource == null || !_datasource.IsPosted)
            {
                ShowMessageNotFound();
            }
            else
            {
                // bind mailbox data
                if (_mailbox != null)
                {
                    if (string.IsNullOrWhiteSpace(_previousId))
                        this.PreviousLinkLI.Attributes["class"] += " disabled";
                    else
                        this.PreviousLink.NavigateUrl = GetMessageUrl(_previousId);
                    if (string.IsNullOrWhiteSpace(_nextId))
                        this.NextLinkLI.Attributes["class"] += " disabled";
                    else
                        this.NextLink.NavigateUrl = GetMessageUrl(_nextId);
                    this.MessageIndex.Text = (++_messageIndex).ToString();
                    if (_mailbox.Messages.Count > 0)
                        this.MessageIndex.Text += " / " + _mailbox.Messages.Count;
                    // save previous/next message ID if message gets removed and the state of the mailbox messages has since changed
                    this.ArchiveButton.CommandArgument = this.FlagButton.CommandArgument = string.IsNullOrEmpty(_nextId) ? _previousId : _nextId;
                    this.ReturnLink.Attributes["title"] = "Return to " + _mailbox.View;
                }

                // bind message data
                this.ArchiveButton.CommandName = this.ArchiveButton.Text = this.ArchiveButton.ToolTip = _datasource.IsArchived ? Messages.UnarchiveCommandName : Messages.ArchiveCommandName;
                this.FlagButton.CommandName = this.FlagButton.Text = this.FlagButton.ToolTip = _datasource.NeedsFollowUp ? Messages.ClearFlagCommandName : Messages.FlagCommandName;
                this.MessageTitle.Text = _datasource.Title;
                if (this.MessageFollowUp.Visible = _datasource.NeedsFollowUp)
                {
                    this.MessageFollowUpDate.Text = string.Format(UserActionDateFormat, _datasource.FollowUpDate, _datasource.FollowUpFlagger);
                }
                this.MessageId.Text = _datasource.UniqueID;
                this.MessageEc.Text = _datasource.ElectionCycle;
                this.MessageReceived.Text = string.Format("{0:f}", _datasource.PostDate);
                this.MessageOpened.Text = string.Format(UserActionDateFormat, _datasource.OpenDate, _datasource.Opener);
                if (this.MessageHeaderArchivedRow.Visible = _datasource.IsArchived)
                {
                    this.MessageArchived.Text = string.Format(UserActionDateFormat, _datasource.ArchiveDate, _datasource.Archiver);
                }
                this.MessageBody.Text = Server.HtmlFormat(_datasource.Body);

                // bind message attachments
                this.MessageAttachments.Visible = _datasource.HasAttachment;
                this.AttachmentsList.Items.Clear();
                foreach (CmoAttachment attachment in _datasource.Attachments.Values)
                {
                    AddAttachmentsListItem(attachment);
                }
            }
        }

        /// <summary>
        /// Adds a list item for an attachment to the attachments list.
        /// </summary>
        /// <param name="attachment">The attachment to add a list item for.</param>
        /// <param name="numbered">Indicates whether or not to number the list item.</param>
        private void AddAttachmentsListItem(CmoAttachment attachment)
        {
            ListItem item = new ListItem(string.Format("View Attachment {0}", attachment.DownloadName), string.Format("Attachments/{0}.{1}", Server.UrlEncode(attachment.DownloadName), attachment.Extension));
            item.Attributes["class"] = string.Format("popup filetype {0}-file", attachment.Extension.ToLowerInvariant());
            this.AttachmentsList.Items.Add(item);
        }

        /// <summary>
        /// Handles the event that occurs when an action button that affects the current message is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> object that contains the event data.</param>
        protected void DoMessageAction(object sender, CommandEventArgs e)
        {
            string action = null;
            switch (e.CommandName)
            {
                case Messages.ArchiveCommandName:
                    action = "archived";
                    break;
                case Messages.UnarchiveCommandName:
                    action = "unarchived";
                    break;
                case Messages.FlagCommandName:
                    action = "flagged";
                    break;
                case Messages.ClearFlagCommandName:
                    action = "unflagged";
                    break;
                default:
                    this.PageError = "You have requested an unsupported action. Please try again.";
                    break;
            }
            if (!string.IsNullOrEmpty(action))
            {
                _datasource = GetMessage(Request.QueryString[CmoMailboxMessageList.MessageIdParameterName]);
                if (_datasource != null)
                {
                    // perform requested action
                    CmoMessage.MessageAction ma = Messages.GetAction(e.CommandName, _datasource);
                    if (ma != null)
                    {
                        if (ma(_mailbox.Username))
                        {
                            // success
                            this.PageResult = string.Format("Message {0} {1} successfully.", _datasource.UniqueID, action);
                            if ((_mailbox.View == CmoMailboxView.Archived || _mailbox.View == CmoMailboxView.Current) && (e.CommandName == Messages.ArchiveCommandName || e.CommandName == Messages.UnarchiveCommandName))
                            {
                                // re-get archived messages to determine if still valid for current view
                                _datasource = GetMessage(_datasource.UniqueID);
                                if (_messageIndex == -1)
                                {
                                    // message no longer valid for this view, return to mailbox
                                    Response.Redirect("/Messages/", _mailbox.GetQueryString());
                                }
                            }
                        }
                        else
                        {
                            // failure
                            this.PageError = "There was an error processing your request. Please try again.";
                        }
                        this.DataBind();
                    }
                }
                else
                {
                    ShowMessageNotFound();
                }
                MessageHeaderUpdatePanel.Update();
            }
        }

        /// <summary>
        /// Configures the page to show a "message not found" error instead of message details.
        /// </summary>
        private void ShowMessageNotFound()
        {
            this.CmoMessageNotFound.Visible = true;
            this.CmoMessageDetails.Visible = false;
        }

        /// <summary>
        /// Retrieves a message from the current mailbox.
        /// </summary>
        /// <param name="UniqueID">The unique message ID of the message to retrieve.</param>
        /// <returns>A <see cref="CmoMessage"/> representing the requested message if found; otherwise, null.</returns>
        /// <remarks>Retrieving the message will initiate a fetch of the mailbox message contents as well as attempt to populate the previous message ID, next message ID, and current message index properties.</remarks>
        private CmoMessage GetMessage(string UniqueID)
        {
            _mailbox.GetMessages();
            CmoMessage message = _mailbox.OpenMessage(UniqueID, out _previousId, out _nextId, out _messageIndex);
            if (message == null)
                message = _mailbox.OpenMessage(UniqueID);
            return message;
        }

        /// <summary>
        /// Gets a URL for viewing a message.
        /// </summary>
        /// <param name="UniqueID">The unique ID of the message to view.</param>
        /// <returns>A URL for viewing the message specified by <paramref name="UniqueID"/>.</returns>
        private string GetMessageUrl(string UniqueID)
        {
            if (_mailbox == null)
                return string.Format("?{0}={1}", CmoMailboxMessageList.MessageIdParameterName, UniqueID);
            else
                return string.Format("?{0}&{1}={2}", _mailbox.GetQueryString().ToQueryString(Server), CmoMailboxMessageList.MessageIdParameterName, UniqueID);
        }
    }
}