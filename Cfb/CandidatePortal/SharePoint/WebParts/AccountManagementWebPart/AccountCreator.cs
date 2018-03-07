using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Cfis.CampaignContacts;
using Microsoft.SharePoint;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <summary>
    /// Control for displaying portal account creation view for a candidate.
    /// </summary>
    internal class AccountCreator : AccountManagementControl
    {
        #region Command Names

        /// <summary>
        /// Command name value for showing creator action.
        /// </summary>
        const string _showCreatorCommandName = "Create Account";

        /// <summary>
        /// Command name value for account creation action.
        /// </summary>
        const string _createAccountCommandName = "Create";

        /// <summary>
        /// Command name value for retrying account creation action.
        /// </summary>
        const string _retryCreateCommandName = "Retry";

        /// <summary>
        /// Command name value for cancel action.
        /// </summary>
        const string _cancelCommandName = "Cancel";

        #endregion

        #region Child Controls

        /// <summary>
        /// Button for submitting actions.
        /// </summary>
        Button _actionButton;

        /// <summary>
        /// Button for canceling account creation.
        /// </summary>
        Button _cancelButton;

        /// <summary>
        /// Label for displaying the type of contact being issued a new account.
        /// </summary>
        Label _contactType;

        /// <summary>
        /// Label for displaying the last name for a new account.
        /// </summary>
        Label _lastName;

        /// <summary>
        /// Label for displaying the first name for a new account.
        /// </summary>
        Label _firstName;

        /// <summary>
        /// Label for displaying the middle initial for a new account.
        /// </summary>
        Label _middleInitial;

        /// <summary>
        /// Label for capturing the display name for a new account.
        /// </summary>
        Label _displayName;

        /// <summary>
        /// Label for displaying the e-mail address for a new account.
        /// </summary>
        Label _email;

        /// <summary>
        /// Input field for capturing the password for a new account.
        /// </summary>
        TextBox _password;

        /// <summary>
        /// List of all candidates for the current election.
        /// </summary>
        DropDownList _candidatesList;

        /// <summary>
        /// List of all election cycles in which the current candidate is active.
        /// </summary>
        DropDownList _electionCyclesList;

        /// <summary>
        /// List of all contacts available for new accounts.
        /// </summary>
        DropDownList _contactsList;

        /// <summary>
        /// Hidden field for persisting currently selected election cycle.
        /// </summary>
        HiddenField _electionCycle;

        /// <summary>
        /// GridView for showing contacts whom are ineligible for C-Access accounts.
        /// </summary>
        GridView _ineligibleContacts;

        /// <summary>
        /// BulletedList for showing a list of existing accounts for the current campaign.
        /// </summary>
        BulletedList _existingAccounts;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when an account is created successfully.
        /// </summary>
        public event AccountManagementEventHandler Success;

        /// <summary>
        /// Occurs when the account creation process is cancelled.
        /// </summary>
        public event AccountManagementEventHandler Cancel;

        #endregion

        /// <summary>
        /// Whether or not the form is missing a last name.
        /// </summary>
        bool _hasNoLastName;

        /// <summary>
        /// Whether or not the form is missing a first name.
        /// </summary>
        bool _hasNoFirstName;

        /// <summary>
        /// Whether or not the form is missing a display name.
        /// </summary>
        bool _hasNoDisplayName;

        /// <summary>
        /// Whether or not the form has an invalid e-mail address.
        /// </summary>
        bool _hasInvalidEmail;

        /// <summary>
        /// Whether or not the password on the form is too short.
        /// </summary>
        bool _hasShortPassword;

        /// <summary>
        /// Whether or not the password on the form has spaces in it.
        /// </summary>
        bool _passwordHasSpaces;

        /// <summary>
        /// Whether or not to show an introductory message before displaying creation view.
        /// </summary>
        bool _showIntro;

        /// <summary>
        /// Gets or sets whether or not to show an introductory message before displaying creation view.
        /// </summary>
        public bool ShowIntro
        {
            get { return _showIntro; }
            set { _showIntro = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountCreator"/> control.
        /// </summary>
        public AccountCreator()
        {
            this._showIntro = false;
        }

        /// <summary>
        /// Targets account creation controls towards a specific candidate.
        /// </summary>
        /// <param name="candidate">The targeted candidate.</param>
        public new void SetCandidate(Candidate candidate)
        {
            base.SetCandidate(candidate);
            _candidatesList.Visible = false;
            _electionCyclesList.Visible = true;
            _candidatesList.Items.Clear();
            FillActiveElections(_electionCyclesList);
            SetView(RenderElectionSelection);
        }

        /// <summary>
        /// Targets account creation controls towards a specific election cycle.
        /// </summary>
        /// <param name="ec">The targeted election cycle.</param>
        public void SetElectionCycle(string ec)
        {
            _electionCycle.Value = ec;
            _electionCyclesList.Visible = false;
            _candidatesList.Visible = true;
            _electionCyclesList.Items.Clear();
            FillActiveCandidates(_candidatesList, ec);
            SetView(RenderCandidateSelection);
        }

        /// <summary>
        /// Sets the view to be rendered and updates the behavior of controls accordingly.
        /// </summary>
        /// <param name="renderer">A <see cref="AccountManagementControl.RenderDelegate"/> for rendering the control.</param>
        protected override void SetView(RenderDelegate renderer)
        {
            _renderByView = renderer;
            if (renderer == this.RenderIntroView)
            {
                _actionButton.Text = _actionButton.CommandName = _showCreatorCommandName;
            }
            //else if (renderer == this.RenderElectionSelection)
            //{
            //}
            //else if (renderer == this.RenderContactSelection)
            //{
            //}
            else if (renderer == this.RenderCreateForm)
            {
                _actionButton.Text = _actionButton.CommandName = _createAccountCommandName;
            }
            else if (renderer == this.RenderCreateErrorView)
            {
                _actionButton.Text = _actionButton.CommandName = _retryCreateCommandName;
            }
        }

        /// <summary>
        /// Handles creator postback commands.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void ExecuteCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case AccountCreator._showCreatorCommandName:
                    SetView(this.RenderCreateForm);
                    break;
                case AccountCreator._createAccountCommandName:
                    // attempt to create account using values supplied
                    if (this.IsValid)
                    {
                        MembershipUser user = UserManagement.CreateUser(UserManagement.GenerateUserName(_firstName.Text.Trim(), _middleInitial.Text.Trim().Length > 0 ? (char?)_middleInitial.Text.Trim().ToArray()[0] : null, _lastName.Text.Trim(), _candidate.ID), _password.Text.Trim(), _email.Text.Trim(), GetCreatorUserName());
                        if (user != null)
                        {
                            if (Success != null)
                            {
                                // view account details
                                Success(this, new AccountManagementEventArgs(user, string.Format("You have successfully created a C-Access user account. The details of the new account are shown below.<br />Initial password for the account: <strong>{0}</strong>", Page.Server.HtmlEncode(_password.Text))));
                            }
                        }
                        else
                        {
                            // show error message
                            SetView(RenderCreateErrorView);
                        }
                    }
                    else
                    {
                        SetView(RenderCreateForm);
                    }
                    break;
                case AccountCreator._retryCreateCommandName:
                    // reset screen to default
                    SetView(RenderCreateForm);
                    break;
                case AccountCreator._cancelCommandName:
                    // return to account management screen
                    if (Cancel != null)
                    {
                        Cancel(this, new AccountManagementEventArgs());
                    }
                    break;
            }
        }

        /// <summary>
        /// Gets the name of the account creator user.
        /// </summary>
        /// <returns>The name of the account creator user.</returns>
        string GetCreatorUserName()
        {
            return SPContext.Current.Web.CurrentUser.LoginName;
        }

        /// <summary>
        /// Validates account creation input values.
        /// </summary>
        bool IsValid
        {
            get
            {
                bool hasError = false;
                if (string.IsNullOrEmpty(_firstName.Text.Trim()))
                {
                    hasError = _hasNoFirstName = true;
                }
                if (string.IsNullOrEmpty(_lastName.Text.Trim()))
                {
                    hasError = _hasNoLastName = true;
                }
                if (string.IsNullOrEmpty(_displayName.Text.Trim()))
                {
                    hasError = _hasNoDisplayName = true;
                }
                if (!new Regex("^[\\w._%+-]+@[\\w.-]+\\.[a-zA-Z]{2,}").IsMatch(_email.Text.Trim()))
                {
                    hasError = _hasInvalidEmail = true;
                }
                if (_password.Text.Length < 8)
                {
                    hasError = _hasShortPassword = true;
                }
                if (_password.Text.Contains(' '))
                {
                    hasError = _passwordHasSpaces = true;
                }
                return !hasError;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            _candidatesList = new DropDownList();
            _candidatesList.SelectedIndexChanged += new EventHandler(SelectedCandidateChanged);
            _candidatesList.AutoPostBack = true;
            _electionCyclesList = new DropDownList();
            _electionCyclesList.SelectedIndexChanged += new EventHandler(SelectedElectionCycleChanged);
            _electionCyclesList.AutoPostBack = true;
            _contactsList = new DropDownList();
            _contactsList.SelectedIndexChanged += new EventHandler(SelectedContactChanged);
            _contactsList.AutoPostBack = true;
            _actionButton = new Button();
            _actionButton.Command += new CommandEventHandler(ExecuteCommand);
            _cancelButton = new Button();
            _cancelButton.Command += new CommandEventHandler(ExecuteCommand);
            _cancelButton.Text = _cancelButton.CommandName = AccountCreator._cancelCommandName;
            _contactType = new Label();
            _firstName = new Label();
            _middleInitial = new Label();
            _lastName = new Label();
            _displayName = new Label();
            _email = new Label();
            _password = new TextBox();
            _electionCycle = new HiddenField();
            _ineligibleContacts = new GridView();
            _ineligibleContacts.AutoGenerateColumns = false;
            _ineligibleContacts.CellPadding = 2;
            BoundField bf = new BoundField();
            bf.DataField = "Entity";
            bf.HeaderText = "Name";
            _ineligibleContacts.Columns.Add(bf);
            bf = new BoundField();
            bf.DataField = "Status";
            bf.HeaderText = "Reason";
            _ineligibleContacts.Columns.Add(bf);
            _ineligibleContacts.RowDataBound += new GridViewRowEventHandler(OnRowDataBound);
            _ineligibleContacts.Visible = false;
            _existingAccounts = new BulletedList();
            _existingAccounts.Visible = false;
            Controls.Add(_candidatesList);
            Controls.Add(_electionCyclesList);
            Controls.Add(_contactsList);
            Controls.Add(_actionButton);
            Controls.Add(_cancelButton);
            Controls.Add(_contactType);
            Controls.Add(_firstName);
            Controls.Add(_middleInitial);
            Controls.Add(_lastName);
            Controls.Add(_displayName);
            Controls.Add(_email);
            Controls.Add(_password);
            Controls.Add(_electionCycle);
            Controls.Add(_ineligibleContacts);
            Controls.Add(_existingAccounts);
        }

        /// <summary>
        /// Raises the <see cref="GridView.RowDataBound"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="GridViewRowEventArgs"/> that contains event data.</param>
        void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // additional databinding because data values are not accessible until databinding events
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AccountEligibilityStatus status = e.Row.DataItem as AccountEligibilityStatus;
                if (status != null)
                {
                    e.Row.Cells[0].Text = status.Entity.Name;
                    e.Row.Cells[1].Text = status.Status.GetDescription();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this._showIntro)
            {
                SetView(this.RenderIntroView);
            }
            else
            {
                if (string.IsNullOrEmpty(_electionCycle.Value))
                    SetView(RenderElectionSelection);
                else
                    SetView(RenderCandidateSelection);
            }
        }

        /// <summary>
        /// Renders an introductory screen for account creation.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderIntroView(HtmlTextWriter writer)
        {
            // TODO: add code for rendering introduction
        }

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.WriteLine("<div class=\"cp-{0}\">", typeof(AccountCreator).Name);
        }

        /// <summary>
        /// Renders the HTML closing tag of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            _electionCycle.RenderControl(writer);
            writer.WriteLine("</div>");
        }

        /// <summary>
        /// Renders controls for election selection.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderElectionSelection(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (_renderByView == RenderElectionSelection)
            {
                writer.WriteLine("<p>Please choose the election cycle that pertains to the campaign contact for whom you wish to create an account.</p>");
                _electionCyclesList.RenderControl(writer);
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.WriteLine("<strong>Election Cycle</strong> ");
                _electionCyclesList.RenderControl(writer);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders controls for candidate selection.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderCandidateSelection(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (_renderByView == RenderCandidateSelection)
            {
                writer.WriteLine("<p>For which candidate are you creating a new account? Please choose from the following list.</p>");
                _candidatesList.RenderControl(writer);
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.WriteLine("<strong>Candidate</strong> ");
                _candidatesList.RenderControl(writer);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders controls for contact selection.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderContactSelection(HtmlTextWriter writer)
        {
            if (_electionCyclesList.Visible)
                RenderElectionSelection(writer);
            else
                RenderCandidateSelection(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (_renderByView == RenderContactSelection)
            {
                writer.WriteLine("<p>Please choose the campaign contact for whom you wish to create an account.</p>");
                _contactsList.RenderControl(writer);
                if (_existingAccounts.Visible)
                {
                    writer.WriteLine("<p>The following users already have accounts:</p>");
                    _existingAccounts.RenderControl(writer);
                }
                if (_ineligibleContacts.Visible)
                {
                    writer.WriteLine("<p>The following campaign contacts are ineligible for accounts:</p>");
                    _ineligibleContacts.RenderControl(writer);
                }
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.WriteLine("<strong>Campaign Contact</strong> ");
                _contactsList.RenderControl(writer);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders an account creation form.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderCreateForm(HtmlTextWriter writer)
        {
            RenderContactSelection(writer);
            writer.WriteLine(@"
    <table class=""ms-formtable"" cellspacing=""0"" cellpadding=""0"" border=""0"">
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Selected Candidate</h3>
            </td>
            <td class=""ms-formbody"">
                {0} (ID: {1})
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Contact Type</h3>
            </td>
            <td class=""ms-formbody"">", this._candidate.Name, this._candidate.ID);
            _contactType.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">First Name</h3>
            </td>
            <td class=""ms-formbody"">");
            _firstName.RenderControl(writer);
            if (_hasNoFirstName)
            {
                RenderError(writer, Properties.Resources.NoFirstNameError);
            }
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Middle Initial</h3>
            </td>
            <td class=""ms-formbody"">");
            _middleInitial.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Last Name</h3>
            </td>
            <td class=""ms-formbody"">");
            _lastName.RenderControl(writer);
            if (_hasNoLastName)
            {
                RenderError(writer, Properties.Resources.NoLastNameError);
            }
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Display Name</h3>
            </td>
            <td class=""ms-formbody"">");
            this._displayName.RenderControl(writer);
            if (_hasNoDisplayName)
            {
                RenderError(writer, Properties.Resources.NoDisplayNameError);
            }
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">E-mail Address</h3>
            </td>
            <td class=""ms-formbody"">");
            this._email.RenderControl(writer);
            if (_hasInvalidEmail)
            {
                RenderError(writer, Properties.Resources.InvalidEmailError);
            }
            writer.WriteLine(@"	
            </td>
        </tr>
        <tr>
            <td class=""ms-sectionheader"">
                <h3 class=""ms-standardheader"">Password</h3>
            </td>
            <td class=""ms-formbody"">");
            this._password.RenderControl(writer);
            if (_hasShortPassword)
            {
                RenderError(writer, Properties.Resources.PasswordLengthError);
            }
            if (_passwordHasSpaces)
            {
                RenderError(writer, Properties.Resources.SpacesInPasswordError);
            }
            writer.WriteLine(@"
            </td>
        </tr>
        <tr>
            <td></td>
            <td style=""text-align:right;"">");
            this._actionButton.RenderControl(writer);
            writer.Write("&nbsp;");
            this._cancelButton.RenderControl(writer);
            writer.WriteLine(@"
            </td>
        </tr>
    </table>");
        }

        /// <summary>
        /// Renders a failure message indicating an unsuccessful account creation attempt.
        /// </summary>
        /// <param name="writer">The <see cref="HtmlTextWriter"/> object that receives the server control content.</param>
        void RenderCreateErrorView(HtmlTextWriter writer)
        {
            writer.WriteLine(@"<p><span class=""ms-error"">{0}</span></p>", Properties.Resources.CreateError);
            writer.WriteLine("<div>");
            _actionButton.RenderControl(writer);
            writer.WriteLine("</div>");
        }

        /// <summary>
        /// Renders an error message to an <see cref="HtmlTextWriter"/>.
        /// </summary>
        /// <param name="writer">The HTML output stream.</param>
        /// <param name="error">The message to render.</param>
        void RenderError(HtmlTextWriter writer, string error)
        {
            writer.WriteLine(ToErrorHtml(error));
        }

        /// <summary>
        /// Converts an error message into HTML for displaying as an error.
        /// </summary>
        /// <param name="error">An error message.</param>
        /// <returns>The supplied message as HTML, formatted to display as an error.</returns>
        string ToErrorHtml(string error)
        {
            return string.Format(@"<br />
<span class=""ms-formvalidation"">{0}</span>", error);
        }

        /// <summary>
        /// Populates a <see cref="DropDownList"/> with known contacts for a campaign for a specific election cycle, and also shows the status of other campaign contacts and accounts.
        /// </summary>
        /// <param name="list">The <see cref="DropDownList"/> to populate.</param>
        /// <param name="electionCycle">The election cycle in which to search for contacts.</param>
        new void FillEligibleContacts(DropDownList list, string electionCycle)
        {
            base.FillEligibleContacts(list, electionCycle);
            // populate current accounts
            if (_candidate == null) return;
            AnalysisResults aa = AccountAnalysis.Analyze(_candidate, electionCycle);
            _existingAccounts.Items.Clear();
            foreach (CPUser user in aa.CurrentUsers)
            {
                _existingAccounts.Items.Add(string.Format("{0} ({1})", user.DisplayName, user.Email));
            }
            _existingAccounts.Visible = _existingAccounts.Items.Count > 0;
            // populate ineligible contacts
            _ineligibleContacts.Visible = aa.IneligibleContacts.Count > 0;
            _ineligibleContacts.DataSource = aa.IneligibleContacts;
            _ineligibleContacts.DataBind();

        }

        /// <summary>
        /// Handles candidate selection changes by showing a list of contacts for that candidate within the current election cycle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void SelectedCandidateChanged(object sender, EventArgs e)
        {
            Candidate selection = Cfis.GetCandidate(_candidatesList.SelectedValue);
            string ec = _electionCycle.Value;
            base.SetCandidate(selection);
            if (!string.IsNullOrEmpty(_candidatesList.SelectedValue) && (selection != null) && !string.IsNullOrEmpty(ec))
            {
                FillEligibleContacts(_contactsList, ec);
                SetView(RenderContactSelection);
            }
            else
            {
                SetView(RenderCandidateSelection);
            }
        }

        /// <summary>
        /// Handles election cycle selection changes by showing a list of contacts for that election cycle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void SelectedElectionCycleChanged(object sender, EventArgs e)
        {
            string ec = _electionCyclesList.SelectedValue;
            if (!string.IsNullOrEmpty(ec))
            {
                FillEligibleContacts(_contactsList, ec);
                SetView(RenderContactSelection);
            }
            else
            {
                SetView(RenderElectionSelection);
            }
        }

        /// <summary>
        /// Handles contact selection by populating and showing an account creation form with the selected contact's information.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        void SelectedContactChanged(object sender, EventArgs e)
        {
            // verify a selected contact
            string contactID = _contactsList.SelectedValue;
            if (string.IsNullOrEmpty(contactID))
            {
                SetView(RenderContactSelection);
            }
            // verify a selected election cycle
            string ec = _electionCyclesList.Visible ? _electionCyclesList.SelectedValue : _electionCycle.Value;
            if (string.IsNullOrEmpty(ec))
            {
                Cancel(this, new AccountManagementEventArgs());
            }
            AnalysisResults aa = AccountAnalysis.Analyze(_candidate, ec);
            // get selected contact and populate form
            Entity contact;
            if (aa.EligibleContacts.TryGetValue(contactID, out contact))
            {
                EntityType et = AccountAnalysis.ParseEntityType(contactID);
                _contactType.Text = et.ToString();
                switch (et)
                {
                    case EntityType.Candidate:
                    case EntityType.Consultant:
                    case EntityType.Liaison:
                    case EntityType.Treasurer:
                        break;
                    default:
                        _contactType.Text = "Unknown";
                        _actionButton.Enabled = false;
                        break;
                }
                _firstName.Text = contact.FirstName;
                _middleInitial.Text = contact.MiddleInitial.ToString();
                _lastName.Text = contact.LastName;
                _displayName.Text = contact.Name;
                _email.Text = contact.Email;
                _password.Text = Membership.GeneratePassword(10, 0);
                SetView(RenderCreateForm);
            }
            else
            {
                MessageText = string.Format("Error occurred retrieving selected contact (CAID: {0}). Please contact an Administrator for assistance.", contactID);
            }
        }
    }
}
