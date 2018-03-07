using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.Web.Controls;

namespace Cfb.CandidatePortal.Web.WebApplication.Messages
{
    public partial class Default : CPPage, ISecurePage
    {
        /// <summary>
        /// An array of delimiters for separating posted message IDs.
        /// </summary>
        private readonly char[] MessageIdDelimiters = { ',' };

        /// <summary>
        /// Text to display when an action is requested on selected messages, but no messages have been selected.
        /// </summary>
        private const string NoSelectionText = "No messages have been selected.";

        /// <summary>
        /// The source Campaign Messages Online mailbox to display.
        /// </summary>
        public CmoMailbox DataSource { get; set; }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.ArchiveButton.CommandName = this.ArchiveButton.Text = this.ArchiveButton.ToolTip = Messages.ArchiveCommandName;
            this.UnarchiveButton.CommandName = this.UnarchiveButton.Text = this.UnarchiveButton.ToolTip = Messages.UnarchiveCommandName;
            this.FlagButton.CommandName = this.FlagButton.Text = this.FlagButton.ToolTip = Messages.FlagCommandName;
            this.ClearFlagButton.CommandName = this.ClearFlagButton.Text = this.ClearFlagButton.ToolTip = Messages.ClearFlagCommandName;
        }

        /// <summary>
        /// Handles the <see cref="Control.OnLoad" /> event that occurs as an instance of the page is being created.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DataSource = CmoMailbox.GetMailbox(CPProfile.Cid, CPProfile.Elections);
            this.DataSource.Username = User.Identity.Name;
            this.DataSource.ParseQueryString(Request.QueryString);
            if (!Page.IsPostBack)
            {
                this.DataBind();
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.DataBinding"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnDataBinding(EventArgs e)
        {
            this.DataSource.GetMessages();
            this.CmoMailboxMessageList.DataSource = this.DataSource;
            this.CmoMailboxMessageList.DataBind();
            this.UnarchiveButton.Visible = (this.DataSource.View != CmoMailboxView.Unopened) && (this.DataSource.View != CmoMailboxView.Current);
            this.ArchiveButton.Visible = (this.DataSource.View != CmoMailboxView.Archived) && (this.DataSource.View != CmoMailboxView.Unopened);
            this.FlagButton.Visible = this.DataSource.View != CmoMailboxView.FollowUp;
            this.Master.DataSource = this.DataSource;
            this.Master.DataBind();
        }

        /// <summary>
        /// Handles the event that occurs when an action button that affects selected messages is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> object that contains the event data.</param>
        protected void MultipleMessageAction(object sender, CommandEventArgs e)
        {
            string selectionRaw = Request.Form[CmoMailboxMessageList.MessageCheckboxName];
            // check for selected messages
            if (string.IsNullOrEmpty(selectionRaw))
            {
                this.PageError = NoSelectionText;
            }
            else
            {
                string[] selection = selectionRaw.Split(MessageIdDelimiters, StringSplitOptions.RemoveEmptyEntries);
                if (selection.Count() == 0)
                {
                    this.PageError = NoSelectionText;
                }
                else
                {
                    // determine action
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
                        // iterate through messages and monitor failures
                        bool error = false;
                        uint successCount = 0;
                        foreach (string messageId in selection)
                        {
                            CmoMessage m = CmoMessage.GetMessage(messageId);
                            if (m == null)
                            {
                                error = true;
                            }
                            else
                            {
                                CmoMessage.MessageAction ma = Messages.GetAction(e.CommandName, m);
                                if (ma != null)
                                {
                                    if (ma(this.DataSource.Username))
                                        successCount++;
                                    else
                                        error = true;
                                }
                            }
                        }
                        this.PageResult = string.Format("{0} message{1} {2} successfully{3}.", successCount, successCount == 1 ? string.Empty : "s", action, error ? ", with exceptions" : string.Empty);
                    }
                }
            }
            this.DataBind();
        }
    }
}