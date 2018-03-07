using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Settings;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;
using Cfb.DirectoryServices;
using Cfb.Extensions;

namespace Cfb.Camp.Security
{
    /// <summary>
    /// A form for displaying and managing a user account.
    /// </summary>
    public partial class UserAccountForm : CampMdiChild
    {
        /// <summary>
        /// The default text to display for the form.
        /// </summary>
        private readonly string _defaultText;

        /// <summary>
        /// The overlay image for indicating a disabled user.
        /// </summary>
        private readonly Image _disabledOverlay;

        /// <summary>
        /// The text for the [OK] button.
        /// </summary>
        private readonly string _okButtonText;

        /// <summary>
        /// Whether or not the form is read-only.
        /// </summary>
        private bool _readOnly;

        /// <summary>
        /// Whether or not administrator-only features are enabled.
        /// </summary>
        private readonly bool _adminMode;

        /// <summary>
        /// The collection of C-Access Security permission flag images.
        /// </summary>
        private readonly List<PictureBox> _permissionFlags;

        /// <summary>
        /// The format string to use when displaying dates.
        /// </summary>
        private const string DateFormatString = "f";

        /// <summary>
        /// The text to display for a create new account button.
        /// </summary>
        private const string CreateButtonText = "&Create";

        /// <summary>
        /// The text to display for a new account form.
        /// </summary>
        private const string CreateFormText = "Create New Account";

        /// <summary>
        /// The C-Access user to display.
        /// </summary>
        private CPUser _user;

        /// <summary>
        /// The CFB-registered campaign entity associated with the account.
        /// </summary>
        private Entity _entity;

        /// <summary>
        /// The type of CFB-registered campaign entity associated with the account.
        /// </summary>
        private EntityType _entityType;

        /// <summary>
        /// The ID of the candidate associated with the user account.
        /// </summary>
        private string _candidateID;

        /// <summary>
        /// The ID of the committee associated with the user account.
        /// </summary>
        private char? _committeeID;

        /// <summary>
        /// The election cycle associated with the user account.
        /// </summary>
        private string _electionCycle;

        /// <summary>
        /// The ID of the liaison associated with the user account.
        /// </summary>
        private byte? _liaisonID;

        /// <summary>
        /// Whether or not the form is still initializing.
        /// </summary>
        private bool _initializing;

        /// <summary>
        /// The original location of the OK button.
        /// </summary>
        private Point _okButtonLocation;

        /// <summary>
        /// The original location of the Cancel button.
        /// </summary>
        private Point _cancelButtonLocation;

        /// <summary>
        /// Creates a new instance of the <see cref="UserAccountForm"/> form.
        /// </summary>
        private UserAccountForm()
        {
            InitializeComponent();
            _initializing = true;
            _groupsListBox.DisplayMember = "Name";
            _disabledOverlay = _userImage.Image;
            _userImage.Image = null;
            _okButtonText = _okButton.Text;
            _defaultText = this.Text;
            _okButtonLocation = _okButton.Location;
            _cancelButtonLocation = _cancelButton.Location;
            _adminMode = SettingsManager.AdministratorModeEnabled;
            if (_adminMode)
                _candidatePicker.SetElectionCycle(null);
            _readOnly = !_adminMode && (User.Current == null || !User.Current.IsMemberOf(SettingsManager.GlobalSettings.AccountManagersGroupName));

            // set up error providers
            _passwordErrorProvider.SetIconAlignment(_passwordTextBox, ErrorIconAlignment.MiddleLeft);
            _passwordErrorProvider.SetIconPadding(_passwordTextBox, 2);
            _usernameErrorProvider.SetIconAlignment(_newUsernameTextBox, ErrorIconAlignment.MiddleLeft);
            _usernameErrorProvider.SetIconPadding(_newUsernameTextBox, 2);
            _emailErrorProvider.SetIconAlignment(_emailTextBox, ErrorIconAlignment.MiddleLeft);
            _emailErrorProvider.SetIconPadding(_emailTextBox, 2);
            _cfisEmailErrorProvider.SetIconAlignment(_cfisEmailTextBox, ErrorIconAlignment.MiddleLeft);
            _cfisEmailErrorProvider.SetIconPadding(_cfisEmailTextBox, 2);

            // set up security tab
            List<Label> labels = new List<Label>();
            List<PictureBox> boxes = new List<PictureBox>();
            int lastY = _permissionLabel.Location.Y;
            int labelX = _permissionLabel.Location.X;
            int boxX = _permissionPictureBox.Location.X;
            _permissionFlags = new List<PictureBox>(new[] { _permissionPictureBox });
            foreach (CPUserRights right in new[] { 
                CPUserRights.VoterGuide, 
                CPUserRights.VoterGuideSubmit, 
                CPUserRights.Csmart, 
                CPUserRights.CsmartModify, 
                CPUserRights.CsmartDelete, 
                CPUserRights.CsmartSubmit, 
                CPUserRights.CsmartEncryptionKey })
            {
                if (right == CPUserRights.CAccess)
                    continue;
                PictureBox box = new PictureBox();
                ((System.ComponentModel.ISupportInitialize)box).BeginInit();
                box.Image = _permissionPictureBox.Image;
                box.Location = new System.Drawing.Point(boxX, lastY += 17);
                box.Size = _permissionPictureBox.Size;
                box.SizeMode = _permissionPictureBox.SizeMode;
                box.TabIndex = _permissionPictureBox.TabIndex;
                box.TabStop = _permissionPictureBox.TabStop;
                box.Tag = right.ToString();
                box.Visible = _permissionPictureBox.Visible;
                ((System.ComponentModel.ISupportInitialize)box).EndInit();
                Label label = new Label();
                label.AutoSize = _permissionLabel.AutoSize;
                label.Location = new System.Drawing.Point(labelX, lastY + 1);
                label.Margin = _permissionLabel.Margin;
                label.TabIndex = _permissionLabel.TabIndex + 1;
                label.Text = right.ToDisplayString();
                _permissionsPanel.Controls.Add(box);
                _permissionsPanel.Controls.Add(label);
                _permissionFlags.Add(box);
            }
            _initializing = false;
        }

        /// <summary>
        /// Sets a CFB-registered entity to be displayed.
        /// </summary>
        /// <param name="entity">The entity to display.</param>
        /// <param name="type">The type of entity to display.</param>
        /// <param name="candidateID">The candidate ID of the entity, if applicable.</param>
        /// <param name="election">The election cycle in which the entity was registered, if applicable.</param>
        /// <param name="committeeID">The committee affiliated with the entity, if applicable.</param>
        /// <param name="liaisonID">The liaison ID of the entity, if applicable.</param>
        private void SetEntity(Entity entity, EntityType type, string candidateID, string election, char? committeeID, byte? liaisonID)
        {
            _entity = entity;
            _entityType = type;
            _candidateID = candidateID;
            _electionCycle = election;
            _committeeID = committeeID;
            _liaisonID = liaisonID;
            _typeTextBox.Text = type.ToString();

            // detect mode
            bool entityExists = entity != null;
            bool adhocCreation = !entityExists && this.IsCreateMode && _adminMode;

            // C-Access tab
            _newUsernameLabel.Visible = _emailTextBox.TabStop = _cfisEmailTextBox.TabStop = _cfisDisplayNameTextBox.TabStop = _displayNameTextBox.TabStop = _candidatePicker.Visible = _adminMode;
            _candidatePicker.ReadOnly = _cfisDisplayNameTextBox.ReadOnly = _cfisEmailTextBox.ReadOnly = _emailTextBox.ReadOnly = _displayNameTextBox.ReadOnly = !_adminMode;
            _candidateTextBox.Visible = !(_candidatePicker.Visible = _adminMode);

            // CFIS tab
            _usernameButton.Visible = _newUsernamePlaceholder.Visible = _adminMode;
            _firstNameTextBox.ReadOnly = _miTextBox.ReadOnly = _lastNameTextBox.ReadOnly = _cfisDisplayNameTextBox.ReadOnly = _cfisEmailTextBox.ReadOnly = !adhocCreation;
            _firstNameTextBox.TabStop = _miTextBox.TabStop = _lastNameTextBox.TabStop = _cfisDisplayNameTextBox.TabStop = _cfisEmailTextBox.TabStop = adhocCreation;
            _firstNameLabel.Visible = _firstNameTextBox.Visible = _miLabel.Visible = _miTextBox.Visible = _lastNameLabel.Visible = _lastNameTextBox.Visible = _cfisEmailLabel.Visible = _cfisEmailTextBox.Visible = _cfisDisplayNameLabel.Visible = _cfisDisplayNameTextBox.Visible = entityExists || adhocCreation;

            // detect non-account-manager mode
            if (_readOnly)
                _electionsCheckedListBox.Enabled = _enableButton.Enabled = _resetButton.Enabled = _addGroupButton.Enabled = _removeGroupButton.Enabled = _cancelButton.Enabled = _okButton.Enabled = false;

            if (_syncButton.Visible = entityExists)
            {
                // show entity properties
                _syncButton.Enabled &= !_readOnly;
                if (_ecLabel.Visible = _ecTextBox.Visible = _committeeLabel.Visible = _committeeTextBox.Visible = committeeID.HasValue && new[] { EntityType.Treasurer, EntityType.Liaison, EntityType.Consultant }.Contains(type))
                {
                    Committee com = null;
                    if (type == EntityType.Treasurer)
                    {
                        // show Election box
                        _ecLabel.Text = "Election";
                        _ecTextBox.Text = election;
                        var comms = CPProviders.DataProvider.GetAuthorizedCommittees(_candidateID, election).Committees;
                        if (comms.ContainsKey(committeeID.Value))
                            com = comms[committeeID.Value];
                    }
                    else
                    {
                        // show Liaison ID box
                        _ecLabel.Text = "Liaison ID";
                        _ecTextBox.Text = liaisonID.HasValue ? liaisonID.Value.ToString() : null;
                        if (liaisonID.HasValue)
                        {
                            var comms = CPProviders.DataProvider.GetCommittees(_candidateID, committeeID);
                            com = comms.Where(o => o.ID == committeeID.Value).FirstOrDefault();
                        }
                    }
                    _committeeTextBox.Text = com != null ? string.Format("{0} [{1}]", com.Name, committeeID.Value) : string.Format("(not found: {0})", committeeID.Value);
                }
                _firstNameTextBox.Text = entity.FirstName;
                _miTextBox.Text = entity.MiddleInitial.HasValue ? entity.MiddleInitial.Value.ToString() : null;
                _lastNameTextBox.Text = entity.LastName;
                _cfisDisplayNameTextBox.Text = entity.Name;
                _cfisEmailTextBox.Text = entity.Email;
            }
            else
            {
                // no entity - generic account
                _typeTextBox.Text = EntityType.Generic.ToString();
                _ecTextBox.Text = _committeeTextBox.Text = _firstNameTextBox.Text = _miTextBox.Text = _lastNameTextBox.Text = _cfisDisplayNameTextBox.Text = _cfisEmailTextBox.Text = null;
                _ecLabel.Visible = _ecTextBox.Visible = _committeeLabel.Visible = _committeeTextBox.Visible = false;
            }
        }

        /// <summary>
        /// Sets a C-Access user account to be displayed.
        /// </summary>
        /// <param name="user">The user to display.</param>
        /// <remarks>Setting a null user context will generate an error message and close the form.</remarks>
        private void SetUser(CPUser user)
        {
            if (user == null)
            {
                this.ShowTransactionError();
                this.Close();
                return;
            }
            this.Tag = _user = user;
            _readOnly = !_adminMode && user.SourceType == EntityType.Generic;

            // base properties
            _initializing = true;
            _displayNameTextBox.Text = user.DisplayName;
            _displayNameTextBox.ReadOnly = !_adminMode;
            _userNameTextBox.Text = user.UserName.ToLowerInvariant();
            _emailTextBox.Text = user.Email;
            var dt = user.LastLoginDate;
            _loginTextBox.Text = dt.HasValue && dt.Value != DateTime.MinValue ? user.LastLoginDate.Value.ToString(DateFormatString) : "(never logged in)";
            dt = user.LastLockoutDate;
            _lockoutTextBox.Text = dt.HasValue && dt.Value != DateTime.MinValue ? user.LastLockoutDate.Value.ToString(DateFormatString) : "(never locked out)";
            dt = user.LastPasswordChangedDate;
            _pwSetTextBox.Text = dt.HasValue && dt.Value != DateTime.MinValue ? user.LastPasswordChangedDate.Value.ToString(DateFormatString) : "(password never changed)";
            _createdTextBox.Text = user.CreationDate.ToString(DateFormatString);
            _unlockButton.Enabled = user.LockedOut && !_readOnly;
            _userImage.Image = _user.Enabled ? null : _disabledOverlay;
            SetEnableButtonProperties();
            this.Text = string.Format("{0} - {1}", _defaultText, user.DisplayName);
            SetCandidate(user.Cid);

            // security tab
            AddGroups(CPSecurity.Provider.GetGroupMembership(user.UserName), false);

            // election cycles tab
            _electionsCheckedListBox.Items.Clear();
            foreach (var ec in CPProviders.DataProvider.GetActiveElectionCycles(user.Cid, CPProviders.SettingsProvider.MinimumElectionCycle))
            {
                _electionsCheckedListBox.Items.Add(ec);
            }
            _electionsCandidateToolTip.Active = _electionCandidateInfoPanel.Visible = _user.IsCandidate;
            _allCyclesCheckBox.Visible = _adminMode && user.SourceType == EntityType.Generic;
            _allCyclesCheckBox.Checked = _user.ImplicitElectionCycles;
            CheckElectionCycleCheckBoxItems();

            // entity tab
            Entity entity = null;
            switch (_user.SourceType)
            {
                case EntityType.Candidate:
                    entity = CPProviders.DataProvider.GetCandidate(_user.Cid);
                    break;
                case EntityType.Treasurer:
                    var committees = CPProviders.DataProvider.GetAuthorizedCommittees(_user.Cid, _user.SourceElectionCycle);
                    AuthorizedCommittee comm;
                    if (committees != null && _user.SourceCommitteeID.HasValue && committees.Committees.TryGetValue(_user.SourceCommitteeID.Value, out comm))
                        entity = comm.Treasurer;
                    break;
                case EntityType.Liaison:
                case EntityType.Consultant:
                    if (_user.SourceCommitteeID.HasValue && _user.SourceLiaisonID.HasValue)
                    {
                        var liaisons = CPProviders.DataProvider.GetLiaisonsByCommittee(_user.Cid, _user.SourceCommitteeID.Value);
                        Liaison l;
                        if (liaisons.TryGetValue(_user.SourceLiaisonID.Value, out l))
                            entity = l;
                    }
                    break;
            }
            _entityType = _user.SourceType;
            _electionCycle = _user.SourceElectionCycle;
            _committeeID = _user.SourceCommitteeID;
            _liaisonID = _user.SourceLiaisonID;
            SetEntity(entity, _user.SourceType, _user.Cid, _user.SourceElectionCycle, _user.SourceCommitteeID, _user.SourceLiaisonID);

            // switch to existing account view
            if (!_mainTabControl.TabPages.Contains(_generalTabPage))
                _mainTabControl.TabPages.Insert(0, _generalTabPage);
            if (!_mainTabControl.TabPages.Contains(_securityTabPage))
                _mainTabControl.TabPages.Insert(1, _securityTabPage);
            if (!_mainTabControl.TabPages.Contains(_electionsTabPage))
                _mainTabControl.TabPages.Insert(2, _electionsTabPage);
            this.IsCreateMode = false;
            _cancelButton.Location = _cancelButtonLocation;
            _applyButton.Visible = true;
            _cfisDisplayNameTextBox.ReadOnly = _cfisEmailTextBox.ReadOnly = true;
            _newUsernameLabel.Visible = _newUsernameTextBox.Visible = _newUsernamePlaceholder.Visible = _usernameButton.Visible = _passwordTextBox.Visible = _passwordLabel.Visible = false;
            _passwordTextBox.Text = null;
            _newUsernameTextBox.CausesValidation = _passwordTextBox.CausesValidation = _cfisEmailTextBox.CausesValidation = _cfisDisplayNameTextBox.CausesValidation = false;
            _initializing = false;
        }

        /// <summary>
        /// Sets the properties of  the enable/disable user button.
        /// </summary>
        private void SetEnableButtonProperties()
        {
            if (_user != null)
            {
                _enableButton.Text = _user.Enabled ? "&Disable User" : "&Enable User";
                _enableButton.Image = _user.Enabled ? Cfb.Camp.Security.Properties.Resources.Block_16x16 : Cfb.Camp.Security.Properties.Resources.ApprovedUser_16x16;
            }
        }

        /// <summary>
        /// Sets the check state of all election cycle options according to the current user's access and the current election cycle selections.
        /// </summary>
        private void CheckElectionCycleCheckBoxItems()
        {
            bool allCycles = _user.IsCandidate || _allCyclesCheckBox.Checked;
            _electionsCheckedListBox.Dock = _user.IsCandidate ? DockStyle.None : DockStyle.Fill;
            _electionsCheckedListBox.Enabled = !(allCycles || _readOnly);
            for (int i = 0; i < _electionsCheckedListBox.Items.Count; i++)
            {
                _electionsCheckedListBox.SetItemChecked(i, allCycles || _user.ElectionCycles.Contains(_electionsCheckedListBox.Items[i] as string));
            }
        }

        /// <summary>
        /// Sets the C-Access Security groups to display.
        /// </summary>
        /// <param name="groups">The collection of groups to display.</param>
        /// <param name="append">Indicates whether to append to the currently displayed groups or overwrite them.</param>
        private void AddGroups(IEnumerable<CPGroup> groups, bool append = true)
        {
            _groupsListBox.BeginUpdate();
            if (!append)
                _groupsListBox.Items.Clear();
            if (groups != null)
            {
                groups = groups.OrderBy(g => g.Name);
                foreach (var group in groups)
                {
                    if (!GetGroupsListGroups().Any(g => g.ID == group.ID))
                        _groupsListBox.Items.Add(group);
                }
            }
            _groupsListBox.EndUpdate();
            _groupsListBox_SelectedIndexChanged(this, new EventArgs());
        }

        /// <summary>
        /// Sets the candidate to display.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate to display.</param>
        private void SetCandidate(string candidateID)
        {
            _candidateID = candidateID;
            if (_adminMode)
            {
                _candidatePicker.SelectCandidate(candidateID);
            }
            else
            {
                _candidateTextBox.Text = string.Format("{0} [{1}]", CPProviders.DataProvider.GetCandidateName(candidateID, true), candidateID);
            }
        }

        /// <summary>
        /// Saves changes to the user.
        /// </summary>
        private void SaveUser()
        {
            if (!_user.Save())
            {
                MessageBox.Show(this, "An error occurred while saving your changes. Please try again.", "Save Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            _user.Refresh();
            SetUser(_user);
        }

        /// <summary>
        /// Prompts for confirmation of a password reset request.
        /// </summary>
        /// <returns>true if the request was confirmed; otherwise, false.</returns>
        private bool ConfirmPasswordReset()
        {
            if (_user == null)
                return false;
            return MessageBox.Show(this, string.Format("You are about to reset the password for user {0}. This process is irreversible, and the original password cannot be recovered. This will also send an e-mail to the user at {1}.\n\nClick [OK] if you understand and wish to continue; otherwise, click [Cancel].", _user.UserName, _user.Email), "Password Reset Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
        }

        /// <summary>
        /// Refreshes all open Account Explorers.
        /// </summary>
        private void RefreshAccountExplorers()
        {
            if (this.Owner is CampMdiParent)
            {
                ((CampMdiParent)this.Owner).RefreshAccountExplorers();
            }
        }

        /// <summary>
        /// Retrieves a collection of the groups displayed in the groups ListBox.
        /// </summary>
        /// <returns>A collection of the groups displayed.</returns>
        private IEnumerable<CPGroup> GetGroupsListGroups()
        {
            return _groupsListBox.Items.OfType<CPGroup>();
        }

        /// <summary>
        /// Retrieves a collection of the checked election cycles in the elections CheckedListBox.
        /// </summary>
        /// <returns>A collection of the checked election cycles.</returns>
        private IEnumerable<string> GetCheckedElections()
        {
            return _electionsCheckedListBox.CheckedItems.OfType<string>();
        }

        /// <summary>
        /// Verifies that the values supplied in the form are valid.
        /// </summary>
        /// <returns>true if all applicable values pass validation; otherwise, false.</returns>
        private bool ValidateForm()
        {
            if (_mainTabControl.TabCount > 1 && _electionsCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Account must be authorized for at least one election cycle. To disable access to all election cycles, disable the account.", "Election Cycle Authorization", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                _mainTabControl.SelectedTab = _electionsTabPage;
                _electionsCheckedListBox.Focus();
                return false;
            }
            return this.ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Selectable | ValidationConstraints.TabStop);
        }

        private void _resetButton_Click(object sender, EventArgs e)
        {
            // reset password
            if (_user != null && ConfirmPasswordReset())
            {
                if (CPSecurity.Provider.ResetPassword(_user.UserName))
                {
                    MessageBox.Show(this, string.Format("Password successfully reset. An e-mail was also automatically sent to the user at {0}.", _user.Email), "Password Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    this.ShowTransactionError();
                    SetUser(CPSecurity.Provider.GetUser(_user.UserName));
                }
            }
        }

        private void _syncButton_Click(object sender, EventArgs e)
        {
            // synchronize user
            if (_user != null)
            {
                UpdateResult result = CPSecurity.Provider.SynchronizeUser(_user.UserName);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var state in Enum.GetValues(typeof(UpdateResultState)).Cast<UpdateResultState>())
                    {
                        if ((result.State & state) == state)
                        {
                            sb.Append(", ");
                            sb.Append(state.ToString());
                        }
                    }
                    if (sb.Length > 0)
                        sb.Remove(0, ", ".Length);
                    MessageBox.Show(this, "The following errors were occurred while updating the user: " + sb.ToString(), "Update Failure", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                SetUser(result.User);
                this.RefreshAccountExplorers();
            }
        }

        private void _unlockButton_Click(object sender, EventArgs e)
        {
            // unlock user
            if (_user != null)
            {
                _user.LockedOut = false;
                SaveUser();
            }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.IsCreateMode)
            {
                // validate
                if (this.ValidateForm())
                {
                    // create new C-Access user account				
                    char? middleInitial = string.IsNullOrWhiteSpace(_miTextBox.Text) ? null : (char?)_miTextBox.Text.Trim().ToCharArray()[0];
                    string first = _firstNameTextBox.Text.Trim();
                    string last = _lastNameTextBox.Text.Trim();
                    CPUser user = CPSecurity.Provider.CreateUser(first, middleInitial, last, _candidateID, _passwordTextBox.Text.Trim(), _cfisEmailTextBox.Text.Trim(), SettingsManager.CurrentUser.Username, _entityType, _committeeID, _electionCycle, _liaisonID, _newUsernameTextBox.Visible ? _newUsernameTextBox.Text : null);
                    if (user == null)
                    {
                        if (MessageBox.Show(this, string.Format("Unable to create a C-Access account for {0}. Retry?", Entity.ToFullName(first, last, middleInitial, false)), "Account Creation Failure", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                            _okButton_Click(sender, e);
                    }
                    else
                    {
                        // auto-set security group
                        var groups = from g in CPSecurity.Provider.GetGroups()
                                     where g.Name == user.SourceType.ToString() + "s"
                                     select g.ID;
                        if (groups.Any())
                            CPSecurity.Provider.AddUserToGroups(user.UserName, groups.ToList());

                        // auto-set election cycle access
                        if (!user.IsCandidate && !string.IsNullOrWhiteSpace(_electionCycle))
                        {
                            user.ElectionCycles.Clear();
                            user.ImplicitElectionCycles = false;
                            user.ElectionCycles.Add(_electionCycle);
                            user.Save();
                        }
                        else
                        {
                            user.Refresh();
                        }

                        // refresh form
                        this.SetUser(user);
                        _mainTabControl.SelectedTab = _generalTabPage;
                        _cancelButton.Location = _cancelButtonLocation;
                        RefreshAccountExplorers();
                    }
                }
            }
            else if (_applyButton.Enabled)
            {
                _applyButton.PerformClick();
                if (!_applyButton.Enabled)
                    this.Close();
            }
            else
            {
                this.Close();
            }
            this.Cursor = Cursors.Default;
        }

        private void _enableButton_Click(object sender, EventArgs e)
        {
            if (_user != null)
            {
                // check for account deletion
                if (_adminMode && _enableButton.Text.Contains("Delete"))
                {
                    if (MessageBox.Show(this, string.Format("Are you sure you wish to delete account {0}?\n\nTHIS ACTION CANNOT BE REVERSED.", _user.UserName), "Delete Account?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (CPSecurity.Provider.DeleteUser(_user.UserName))
                        {
                            MessageBox.Show(this, string.Format("Account {0} is now deleted.", _user.UserName), "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            this.RefreshAccountExplorers();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Your request to delete this account could not be completed. Please try again, or contact an administrator for further assistance.", "Account Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                    SetEnableButtonProperties();
                    return;
                }
                _user.Enabled = !_user.Enabled;
                SaveUser();
                this.RefreshAccountExplorers();
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _applyButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.ValidateForm())
            {
                bool error = false;
                bool userUpdate = false;
                bool explorerRefreshRequired = false;
                string username = _user.UserName;

                // groups
                var oldGroupIDs = from g in CPSecurity.Provider.GetGroupMembership(_user.UserName)
                                  select g.ID;
                var newGroupIDs = from g in GetGroupsListGroups()
                                  select g.ID;
                if (!CPSecurity.Provider.AddUserToGroups(username, newGroupIDs.ToList()) || !CPSecurity.Provider.RemoveUserFromGroups(username, oldGroupIDs.Except(newGroupIDs).ToList()))
                    error = true;

                // election cycles
                if ((_user.IsCandidate || _allCyclesCheckBox.Checked) && !_user.ImplicitElectionCycles)
                {
                    _user.ImplicitElectionCycles = true;
                    _user.ElectionCycles.Clear();
                    userUpdate = true;
                }
                else if (_electionsCheckedListBox.Enabled)
                {
                    var selections = GetCheckedElections();
                    HashSet<string> ecChanges = new HashSet<string>(_user.ElectionCycles);
                    ecChanges.SymmetricExceptWith(selections);
                    if (ecChanges.Count > 0 || _user.ImplicitElectionCycles)
                    {
                        _user.ImplicitElectionCycles = false;
                        _user.ElectionCycles.Clear();
                        _user.ElectionCycles.AddRange(selections);
                        userUpdate = true;
                    }
                }

                if (_adminMode)
                {
                    // email
                    if (_user.Email != _emailTextBox.Text)
                    {
                        _user.Email = _emailTextBox.Text;
                        explorerRefreshRequired = userUpdate = true;
                    }
                    // display name
                    if (_user.DisplayName != _displayNameTextBox.Text.TrimAll())
                    {
                        _user.DisplayName = _displayNameTextBox.Text.TrimAll();
                        explorerRefreshRequired = userUpdate = true;
                    }
                    // candidate
                    string cid = _candidatePicker.SelectedCid;
                    if (!string.Equals(_user.Cid, cid, StringComparison.OrdinalIgnoreCase))
                    {
                        _user.Cid = cid;
                        explorerRefreshRequired = userUpdate = true;
                    }
                }

                // apply changes and check for errors
                if (userUpdate && !_user.Save())
                    error = true;
                if (error)
                    this.ShowTransactionError();
                this.SetUser(_user);
                _applyButton.Enabled = false;
                if (explorerRefreshRequired)
                    this.RefreshAccountExplorers();
            }
            this.Cursor = Cursors.Default;
        }

        private void _usernameButton_Click(object sender, EventArgs e)
        {
            _usernameButton.Visible = _newUsernamePlaceholder.Visible = false;
            _newUsernameTextBox.CausesValidation = true;
            _newUsernameTextBox.Visible = true;
            if (_entity != null)
                _newUsernameTextBox.Text = CPSecurity.Provider.GenerateUserName(_entity.FirstName, _entity.MiddleInitial, _entity.LastName, _candidateID).ToLowerInvariant();
        }

        private void _groupsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPUserRights rights = CPUserRights.None;
            IEnumerable items = _groupsListBox.SelectedItems.Count > 0 ? _groupsListBox.SelectedItems as IEnumerable : _groupsListBox.Items;
            foreach (var i in items)
            {
                CPGroup group = i as CPGroup;
                if (group == null)
                    continue;
                rights |= group.UserRights;
            }
            foreach (var flag in _permissionFlags)
            {
                CPUserRights flagRights;
                flag.Visible = Enum.TryParse(flag.Tag as string, out flagRights) && rights.HasRights(flagRights);
            }
        }

        private void _addGroupButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            IEnumerable<byte> current = GetGroupsListGroups().Select(g => g.ID);
            PickListDialog dialog = new PickListDialog()
            {
                Icon = Properties.Resources.Groups,
                Text = "Group Selection",
                DisplayMember = "Name",
                ValueMember = "ID",
                DataSource = CPSecurity.Provider.GetGroups().Where(g => !current.Contains(g.ID)).ToList()
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                AddGroups(dialog.SelectedItems.OfType<CPGroup>().AsEnumerable());
                _applyButton.Enabled = true;
            };
            this.Cursor = Cursors.Default;
        }

        private void _removeGroupButton_Click(object sender, EventArgs e)
        {
            if (_groupsListBox.SelectedItems.Count == 0)
                return;
            this.Cursor = Cursors.WaitCursor;
            var removalList = _groupsListBox.SelectedItems.OfType<CPGroup>().ToList();
            var candidatesGroup = CPSecurity.Provider.GetGroup(CPProviders.SettingsProvider.CandidatesSecurityGroupName);
            if (_user != null && _user.IsCandidate && removalList.Any(g => g.ID == candidatesGroup.ID))
            {
                MessageBox.Show(this, string.Format("Candidates cannot be removed from the \"{0}\" security group.", candidatesGroup.Name), "Security Group Membership", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                removalList.RemoveAll(g => g.ID == candidatesGroup.ID);
            }
            _groupsListBox.BeginUpdate();
            removalList.ForEach(i => _groupsListBox.Items.Remove(i));
            _groupsListBox.EndUpdate();
            _applyButton.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void _emailTextBox_TextChanged(object sender, EventArgs e)
        {
            MakeDraft();
        }

        private void _electionsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MakeDraft();
        }

        private void _candidatePicker_SelectedValueChanged(object sender, EventArgs e)
        {
            _candidateID = _candidatePicker.SelectedCid;
            MakeDraft();
        }

        private void _displayNameTextBox_TextChanged(object sender, EventArgs e)
        {
            MakeDraft();
        }

        /// <summary>
        /// Puts the form into un-saved draft mode.
        /// </summary>
        private void MakeDraft()
        {
            if (_initializing)
                return;
            _applyButton.Enabled = true;
        }

        private void _allCyclesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _electionsCheckedListBox.Enabled = !_allCyclesCheckBox.Checked;
            CheckElectionCycleCheckBoxItems();
            MakeDraft();
        }

        /// <summary>
        /// Gets or sets whether or not the form is in creation mode.
        /// </summary>
        public bool IsCreateMode
        {
            get
            {
                return _okButton.Text == CreateButtonText;
            }
            set
            {
                _okButton.Text = value ? CreateButtonText : _okButtonText;
                _okButton.Location = value ? _cancelButtonLocation : _okButtonLocation;
                _cancelButton.Location = value ? _applyButton.Location : _cancelButtonLocation;
                _displayNameTextBox.Visible = _applyButton.Visible = !(_displaynamePlaceholder.Visible = _newUsernamePlaceholder.Visible = value);
            }
        }

        private void _mainTabControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (_adminMode && e.Control && e.Alt)
            {
                _enableButton.Text = "Delete User";
                _enableButton.Image = Properties.Resources.Delete_16x16;
            }
        }

        private void _mainTabControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (_adminMode)
                SetEnableButtonProperties();
        }

        #region Form Validation Methods

        private void _passwordTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel = _passwordTextBox.Text.Contains(' '))
            {
                _passwordErrorProvider.SetError(_passwordTextBox, "Password cannot contain spaces.");
            }
            else
            {
                byte minLength = SettingsManager.GlobalSettings.MinimumPasswordLength;
                if (e.Cancel = _passwordTextBox.Text.Length < minLength)
                    _passwordErrorProvider.SetError(_passwordTextBox, string.Format("Password must be at least {0} characters long.", minLength));
            }
            if (e.Cancel)
                _passwordTextBox.SelectAll();
            else
                _passwordErrorProvider.SetError(_passwordTextBox, null);
        }

        private void _newUsernameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel = string.IsNullOrWhiteSpace(_newUsernameTextBox.Text))
                _usernameErrorProvider.SetError(_newUsernameTextBox, e.Cancel ? "Username cannot be blank." : null);
            else if (e.Cancel = _newUsernameTextBox.Text.Contains(' '))
                _usernameErrorProvider.SetError(_newUsernameTextBox, e.Cancel ? "Username cannot contain spaces." : null);
            if (e.Cancel)
                _newUsernameTextBox.SelectAll();
            else
                _usernameErrorProvider.SetError(_newUsernameTextBox, null);
        }

        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null)
                return;
            if (!tb.Text.IsValidEmail())
            {
                e.Cancel = true;
                if (tb == _emailTextBox)
                    _mainTabControl.SelectedTab = _generalTabPage;
                tb.SelectAll();
            }
            ErrorProvider ep = tb == _cfisEmailTextBox ? _cfisEmailErrorProvider : _emailErrorProvider;
            ep.SetError(tb, e.Cancel ? "E-mail address must be in valid e-mail format." : null);
        }

        #endregion

        #region Creation Methods

        /// <summary>
        /// Creates a user account form for creating a new user account.
        /// </summary>
        /// <param name="entity">The entity to create a user account for.</param>
        /// <param name="candidateID">The ID of the candidate to associate with the new account.</param>
        /// <param name="electionCycle">The election cycle for which the new account is registered.</param>
        /// <param name="entityKey">The campaign-relative unique account analysis identifier for the entity.</param>
        /// <returns>A <see cref="UserAccountForm"/> purposed for creating a new user account for <paramref name="entity"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public static UserAccountForm CreateNewAccountForm(Entity entity, string candidateID, string electionCycle, string entityKey)
        {
            UserAccountForm form = new UserAccountForm() { Text = CreateFormText };
            form._mainTabControl.TabPages.Remove(form._generalTabPage);
            form._mainTabControl.TabPages.Remove(form._securityTabPage);
            form._mainTabControl.TabPages.Remove(form._electionsTabPage);
            form.IsCreateMode = true;
            form._initializing = true;
            form.SetCandidate(candidateID);
            form._initializing = false;
            // set CFIS entity context
            form.SetEntity(entity, AccountAnalysis.ParseEntityType(entityKey), candidateID, electionCycle, AccountAnalysis.ParseCommitteeID(entityKey), AccountAnalysis.ParseLiaisonID(entityKey));
            return form;
        }

        /// <summary>
        /// Creates a user account form for displaying an existing user account.
        /// </summary>
        /// <param name="user">The user to display.</param>
        /// <returns>A <see cref="UserAccountForm"/> purposed for displaying user <paramref name="user"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="user"/> is null.</exception>
        public static UserAccountForm CreateExistingAccountForm(CPUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "User cannot be null.");
            UserAccountForm form = new UserAccountForm();
            form.SetUser(user);
            return form;
        }

        /// <summary>
        /// Shows a form for displaying an existing user account.
        /// </summary>
        /// <param name="user">The user to display.</param>
        /// <param name="owner">The existing form that should own the new form.</param>
        public static void ShowExistingAccountForm(CPUser user, Form owner = null)
        {
            if (owner != null)
            {
                // find already-open form and, if found, show that instead of a new one
                var existing = (from f in owner.OwnedForms
                                where f is UserAccountForm && f.Tag is CPUser && ((CPUser)f.Tag).UserName == user.UserName
                                select f).FirstOrDefault();
                if (existing != null)
                {
                    existing.BringToFront();
                    return;
                }
            }
            UserAccountForm.CreateExistingAccountForm(user).ShowAsOwnedBy(owner);
        }

        #endregion
    }
}
