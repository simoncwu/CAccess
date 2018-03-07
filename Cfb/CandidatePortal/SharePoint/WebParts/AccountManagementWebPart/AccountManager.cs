using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <summary>
    /// Control for dispatching account management operations and changing management modes.
    /// </summary>
    internal class AccountManager : AccountManagementControl
    {
        /// <summary>
        /// Enumeration representing the different account management modes available.
        /// </summary>
        enum ManagerMode : byte
        {
            /// <summary>
            /// Mode for selecting an account management task.
            /// </summary>
            Task,
            /// <summary>
            /// Mode for selecting a candidate.
            /// </summary>
            SelectCandidate,
            /// <summary>
            /// Mode for selecting a user account.
            /// </summary>
            SelectUser,
            /// <summary>
            /// Mode for creating an account.
            /// </summary>
            Create,
            /// <summary>
            /// Mode for creating an account by candidate.
            /// </summary>
            CreateByCandidate,
            /// <summary>
            /// Mode for creating an account by election cycle.
            /// </summary>
            CreateByElection,
            /// <summary>
            /// Mode for viewing account details.
            /// </summary>
            View
        }

        #region Command Names

        /// <summary>
        /// Command name value for account edit action.
        /// </summary>
        const string _createCommandName = "Create New Account";

        /// <summary>
        /// Command name value for account view action.
        /// </summary>
        const string _viewCommandName = "View Existing Account";

        /// <summary>
        /// Command name value for account edit action.
        /// </summary>
        const string _editCommandName = "Edit Existing Account";

        #endregion

        #region Child Controls

        /// <summary>
        /// Hidden field for persisting the requested manager mode.
        /// </summary>
        HiddenField _modeField;

        /// <summary>
        /// Drop-down list of all candidates.
        /// </summary>
        DropDownList _accountList;

        /// <summary>
        /// Drop-down list of all supported election cycles.
        /// </summary>
        DropDownList _electionsList;

        /// <summary>
        /// Control for displaying account creation view.
        /// </summary>
        AccountCreator _creator;

        /// <summary>
        /// Control for displaying account viewer view.
        /// </summary>
        AccountViewer _viewer;

        /// <summary>
        /// Button for initiating account creation mode.
        /// </summary>
        Button _createButton;

        /// <summary>
        /// Button for initiating account viewer mode.
        /// </summary>
        Button _viewButton;

        #endregion

        /// <summary>
        /// The manager's current account management mode.
        /// </summary>
        ManagerMode _mode;

        /// <summary>
        /// An error message to be shown to the user.
        /// </summary>
        string _errorMessage;

        /// <summary>
        /// Gets or sets an error message to be shown to the user.
        /// </summary>
        string ErrorMessage
        {
            get { return string.Format(@"<span class=""ms-formvalidation"">Error: {0} Please contact your system administrator.</span>", _errorMessage); }
            set { _errorMessage = value; }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            _modeField = new HiddenField();
            _createButton = new Button();
            _createButton.Command += new CommandEventHandler(ExecuteCommand);
            _createButton.Text = _createButton.CommandName = _createCommandName;
            _viewButton = new Button();
            _viewButton.Command += new CommandEventHandler(ExecuteCommand);
            _viewButton.Text = _viewButton.CommandName = _viewCommandName;
            Controls.Add(_modeField);
            Controls.Add(_createButton);
            Controls.Add(_viewButton);
            if (Page.IsPostBack)
            {
                _accountList = new DropDownList();
                _accountList.SelectedIndexChanged += new EventHandler(SelectedAccountChanged);
                _accountList.AutoPostBack = true;
                _electionsList = new DropDownList();
                _electionsList.SelectedIndexChanged += new EventHandler(SelectedElectionChanged);
                _electionsList.AutoPostBack = true;
                _viewer = new AccountViewer();
                _viewer.Closed += this.ShowTasks;
                _creator = new AccountCreator();
                _creator.Success += this.ShowViewer;
                _creator.Cancel += this.ShowTasks;
                Controls.Add(_accountList);
                Controls.Add(_electionsList);
                Controls.Add(_viewer);
                Controls.Add(_creator);
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowTasks(this, new AccountManagementEventArgs());
            }
            else
            {
                // load previous view for postbacks from child AccountManagementControls
                SetMode(ParseMode(_modeField.Value));
                switch (_mode)
                {
                    case ManagerMode.Create:
                    case ManagerMode.CreateByCandidate:
                    case ManagerMode.CreateByElection:
                        SetView(RenderCreatorView);
                        break;
                    case ManagerMode.View:
                        SetView(RenderViewerView);
                        break;
                    default:
                        SetView(RenderTaskView);
                        break;
                }
            }
        }

        /// <summary>
        /// Handles manager task postback commands.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> object that contains the event data.</param>
        void ExecuteCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case _createCommandName:
                    FillCandidates(_accountList);
                    FillElectionCycles(_electionsList);
                    SetMode(ManagerMode.Create);
                    break;
                case _viewCommandName:
                    FillUsers(_accountList);
                    SetMode(ManagerMode.View);
                    break;
                default:
                    SetMode(ManagerMode.Task);
                    break;
            }
            SetView(RenderSelectView);
        }

        /// <summary>
        /// Occurs when the top-level election cycle selection changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void SelectedElectionChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_electionsList.SelectedValue))
            {
                SetView(RenderSelectView);
                return;
            }
            if (_mode == ManagerMode.Create) SetMode(ManagerMode.CreateByElection);
            switch (_mode)
            {
                case ManagerMode.CreateByElection:
                    // show creator for creating an account for the selected election cycle
                    string ec = _electionsList.SelectedValue;
                    if (!string.IsNullOrEmpty(ec))
                    {
                        _creator.SetElectionCycle(ec);
                        SetView(RenderCreatorView);
                    }
                    else
                    {
                        this.ErrorMessage = "Unable to successfully retrieve information for the selected candidate.";
                    }
                    break;
            }
        }

        /// <summary>
        /// Occurs when the top-level account selection changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void SelectedAccountChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_accountList.SelectedValue))
            {
                SetView(RenderSelectView);
                return;
            }
            if (_mode == ManagerMode.Create) SetMode(ManagerMode.CreateByCandidate);
            switch (_mode)
            {
                case ManagerMode.CreateByCandidate:
                    // show creator for creating an account for the selected candidate
                    Candidate selection = Cfis.GetCandidate(_accountList.SelectedValue);
                    if (selection != null)
                    {
                        _creator.SetCandidate(selection);
                        SetView(RenderCreatorView);
                    }
                    else
                    {
                        this.ErrorMessage = "Unable to successfully retrieve information for the selected candidate.";
                    }
                    break;
                case ManagerMode.View:
                    // display the selected account in viewer
                    MembershipUser user = Membership.GetUser(_accountList.SelectedValue);
                    if (user != null)
                    {
                        _viewer.SetUser(user);
                        SetView(RenderViewerView);
                    }
                    else
                    {
                        this.ErrorMessage = "Unable to successfully retrieve information for the selected account.";
                    }
                    break;
            }
        }

        /// <summary>
        /// Sets the view to be rendered and updates the behavior of controls accordingly.
        /// </summary>
        /// <param name="renderer">A <see cref="AccountManagementControl.RenderDelegate"/> for rendering the control.</param>
        protected override void SetView(RenderDelegate renderer)
        {
            this._renderByView = renderer;
            if (renderer == this.RenderTaskView)
            {
            }
            else if (renderer == this.RenderCreatorView)
            {
            }
            else if (renderer == this.RenderViewerView)
            {
                SetMode(ManagerMode.View);
            }
        }

        /// <summary>
        /// Sets and persists the current mode.
        /// </summary>
        /// <param name="mode">The mode to set.</param>
        void SetMode(ManagerMode mode)
        {
            _mode = mode;
            _modeField.Value = Enum.GetName(typeof(ManagerMode), mode);
        }

        /// <summary>
        /// Renders the candidate selection screen.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderSelectView(HtmlTextWriter writer)
        {
            string title;
            string message;
            switch (_mode)
            {
                case ManagerMode.Create:
                    title = _createCommandName;
                    message = "For which candidate are you creating a new account? Please choose from the following list.";
                    break;
                case ManagerMode.View:
                    title = _viewCommandName;
                    message = "Please select an account from the following list to view its details.";
                    break;
                default:
                    RenderTaskView(writer);
                    return;
            }
            writer.WriteLine(@"
<div class=""cp-AccountManagerSelectView"">
    <h2 class=""ms-pagetitle"">{0}</h2>
    <p>{1}</p>
    <div class=""cp-AccountManagerSelectionList"">
", title, message);
            _accountList.RenderControl(writer);
            writer.WriteLine(@"
    </div>");
            if (_mode == ManagerMode.Create)
            {
                writer.WriteLine(@"
    <div class=""cp-AccountManagerSelectionList"">
        <p>Or, narrow down the list by election cycle: ");
                _electionsList.RenderControl(writer);
                writer.WriteLine(@"
        </p>
    </div>");
            }
            _modeField.RenderControl(writer);
            writer.WriteLine(@"
</div>");
        }

        /// <summary>
        /// Renders the task selection screen.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderTaskView(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
<div class=""cp-AccountManagerTaskView"">
    <h2 class=""ms-pagetitle"">CAMPSite Account Management</h2>
    <p>Use the CAMPSite Account Management module to create, view, update, and run reports on C-Access user accounts.</p>
    <p>To get started, please choose one of the following tasks:</p>
    <table class=""ms-formtable"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
            <td class=""ms-sectionheader"">
");
            _createButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
            <td class=""ms-formbody"">
                Create a new C-Access user account.
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
");
            _viewButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
            <td class=""ms-formbody"">
                View the details of an existing C-Access user account.
            </td>
        </tr>
    </table>");
            _modeField.RenderControl(writer);
            writer.WriteLine(@"
</div>");
        }

        /// <summary>
        /// Renders a screen for creating an account.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderCreatorView(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
<div class=""cp-AccountManagerSelectView"">
    <h2 class=""ms-pagetitle"">{0}</h2>
    <div class=""cp-AccountManagerSelectionList"">
        <p>", _createCommandName);
            if (_mode == ManagerMode.CreateByCandidate)
            {
                writer.WriteLine(@"
            <strong>Candidate</strong> ");
                _accountList.RenderControl(writer);
            }
            else if (_mode == ManagerMode.CreateByElection)
            {
                writer.WriteLine(@"
            <strong>Election Cycle</strong> ");
                _electionsList.RenderControl(writer);
            }
            writer.WriteLine(@"
        </p>
    </div>");
            _creator.RenderControl(writer);
            _modeField.RenderControl(writer);
            writer.WriteLine(@"
</div>");
        }

        /// <summary>
        /// Renders a screen for viewing an account.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderViewerView(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
<div class=""cp-AccountManagerViewerView"">
    <h2 class=""ms-pagetitle"">{0}</h2>
    <div class=""cp-AccountManagerSelectionList"">
        <p>
            <strong>Account</strong> ", _viewCommandName);
            _accountList.RenderControl(writer);
            writer.WriteLine(@"
        </p>
    </div>");
            _viewer.RenderControl(writer);
            _modeField.RenderControl(writer);
            writer.WriteLine(@"
</div>");
        }

        /// <summary>
        /// Shows default view for account management tasks.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="AccountManagementEventArgs"/> that contains the event data.</param>
        void ShowTasks(object sender, AccountManagementEventArgs e)
        {
            _renderByView = RenderTaskView;
        }

        /// <summary>
        /// Shows controls for creating an account.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="AccountManagementEventArgs"/> that contains the event data.</param>
        void ShowCreator(object sender, AccountManagementEventArgs e)
        {
            SetView(RenderCreatorView);
            _creator.SetCandidate(e.Candidate);
        }

        /// <summary>
        /// Shows controls for viewing an account.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="AccountManagementEventArgs"/> that contains the event data.</param>
        void ShowViewer(object sender, AccountManagementEventArgs e)
        {
            SetView(RenderViewerView);
            _viewer.MessageText = e.Description;
            FillUsers(_accountList);
            if (e.User != null)
            {
                _viewer.SetUser(e.User);
                ListItem selected = _accountList.Items.FindByValue(e.User.UserName);
                if (selected != null)
                    selected.Selected = true;
            }
        }

        /// <summary>
        /// Converts the string representation of the name of a manager mode to a <see cref="ManagerMode"/> enumeration.
        /// </summary>
        /// <param name="mode">The mode name to convert.</param>
        /// <returns>The <see cref="ManagerMode"/> enumeration represented by the name provided.</returns>
        ManagerMode ParseMode(string mode)
        {
            if (Enum.GetName(typeof(ManagerMode), ManagerMode.Create).Equals(mode))
                return ManagerMode.Create;
            else if (Enum.GetName(typeof(ManagerMode), ManagerMode.CreateByCandidate).Equals(mode))
                return ManagerMode.CreateByCandidate;
            else if (Enum.GetName(typeof(ManagerMode), ManagerMode.CreateByElection).Equals(mode))
                return ManagerMode.CreateByElection;
            else if (Enum.GetName(typeof(ManagerMode), ManagerMode.View).Equals(mode))
                return ManagerMode.View;
            else if (Enum.GetName(typeof(ManagerMode), ManagerMode.SelectCandidate).Equals(mode))
                return ManagerMode.SelectCandidate;
            else if (Enum.GetName(typeof(ManagerMode), ManagerMode.SelectUser).Equals(mode))
                return ManagerMode.SelectUser;
            else
                return ManagerMode.Task;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> control.
        /// </summary>
        public AccountManager()
        {
            _mode = ManagerMode.Task;
        }
    }
}
