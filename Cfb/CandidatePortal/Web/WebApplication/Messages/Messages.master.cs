using System;
using System.Web.UI;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.Web.WebApplication.Messages
{
    /// <summary>
    /// Summary description for CmoPage
    /// </summary>
    public partial class Messages : CPMasterPage
    {
        /// <summary>
        /// Command name for archive action.
        /// </summary>
        public const string ArchiveCommandName = "Archive";

        /// <summary>
        /// Command name for unarchive action.
        /// </summary>
        public const string UnarchiveCommandName = "Unarchive";

        /// <summary>
        /// Command name for flag-for-follow-up action.
        /// </summary>
        public const string FlagCommandName = "Follow-Up";

        /// <summary>
        /// Command name for clear-follow-up-flag action.
        /// </summary>
        public const string ClearFlagCommandName = "Clear Flag";

        /// <summary>
        /// The source Campaign Messages Online mailbox to display.
        /// </summary>
        public CmoMailbox DataSource { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="CmoPage"/> class.
        /// </summary>
        public Messages()
        {
        }

        #region CPMasterPage Members

        public override System.Web.UI.WebControls.WebControl ErrorWebControl
        {
            get { return this.Master.ErrorWebControl; }
        }

        public override System.Web.UI.WebControls.WebControl ResultWebControl
        {
            get { return this.Master.ResultWebControl; }
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Master.HideInaccurateDataNotice();
        }

        /// <summary>
        /// Raises the <see cref="Control.DataBinding"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnDataBinding(EventArgs e)
        {
            this.CmoTabs.DataSource = this.DataSource;
            this.CmoTabs.DataBind();
        }

        /// <summary>
        /// Gets a delegate reference to a method for a Campaign Messages Online message.
        /// </summary>
        /// <param name="commandName">A command name value representing the type of method delegate to get.</param>
        /// <param name="message">The targeted <see cref="CmoMessage"/> object.</param>
        /// <returns>A <see cref="CmoMessage.MessageAction"/> delegate reference if a matching method is found.</returns>
        public static CmoMessage.MessageAction GetAction(string commandName, CmoMessage message)
        {
            CmoMessage.MessageAction ma = null;
            if (message != null)
            {
                switch (commandName)
                {
                    case ArchiveCommandName:
                        ma = message.Archive;
                        break;
                    case UnarchiveCommandName:
                        ma = message.Unarchive;
                        break;
                    case FlagCommandName:
                        ma = message.SetFlag;
                        break;
                    case ClearFlagCommandName:
                        ma = message.ClearFlag;
                        break;
                }
            }
            return ma;
        }
    }
}