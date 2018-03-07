using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cfb.Camp.Forms;
using Cfb.Camp.Reports;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Security;
using Cfb.DirectoryServices;
using Cfb.Extensions;
using Microsoft.Reporting.WinForms;

namespace Cfb.Camp.Cmo
{
    /// <summary>
    /// A form for viewing and creating Campaign Messages Online messages.
    /// </summary>
    public partial class CmoMessageForm : ElectionCandidateFormBase
    {
        /// <summary>
        /// Error message for failed posts of post election audit messages.
        /// </summary>
        private const string PostElectionAuditFailedPostMessage = @"The message could not be posted. Some possible causes are:
    - CAMP was unable to update the tracking dates in CFIS.
    - This type of request has been created already and can only be reposted.
    - A response has already been received.
    - The post election audit workflow is already past the selected category.

Please consult an administrator for further assistance. Do not attempt to recreate this message.";

        /// <summary>
        /// Error message for failed saves of post election audit messages.
        /// </summary>
        private const string PostElectionAuditFailedSaveMessage = @"The message could not be saved. Some possible causes are:
    - This type of request has been created already and can only be reposted.
    - A response has already been received.
    - The post election audit workflow is already past the selected category.

Please consult an administrator for further assistance. Do not attempt to recreate this message.";

        /// <summary>
        /// The temporary storage location for uncommitted attachments.
        /// </summary>
        private string _tempFolder = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "TEMP");

        /// <summary>
        /// The display name for the current user.
        /// </summary>
        private readonly User _currentUser;

        /// <summary>
        /// Whether or not the form is still initializing.
        /// </summary>
        private readonly bool _initializing;

        /// <summary>
        /// Whether or not a message is still loading.
        /// </summary>
        private bool _loading;

        /// <summary>
        /// The message data source for this <see cref="CmoMessageForm"/>.
        /// </summary>
        private CmoMessage _message;

        /// <summary>
        /// Gets or sets the message data source for this <see cref="CmoMessageForm"/>.
        /// </summary>
        public CmoMessage Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// A collection of paths to files that have been attached to the message and are awaiting commital, mapped to their original names.
        /// </summary>
        private Dictionary<string, string> _addedAttachments;

        /// <summary>
        /// A collection of IDs of attachments that have been removed from the message and are awaiting committal.
        /// </summary>
        private HashSet<byte> _removedAttachments;

        /// <summary>
        /// Gets the ID of the currently selected category.
        /// </summary>
        public byte? SelectedCategoryId
        {
            get { return _categoryComboBox.SelectedCategory; }
        }

        /// <summary>
        /// Gets or sets whether the message can be saved.
        /// </summary>
        public bool SaveEnabled
        {
            get { return _saveButton.Enabled; }
            set { _saveButton.Enabled = saveToolStripMenuItem.Enabled = saveToolStripButton.Enabled = _postButton.Enabled = postToolStripMenuItem.Enabled = postToolStripButton.Enabled = detachToolStripMenuItem.Enabled = _attachButton.Enabled = _detachButton.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the message can be deleted.
        /// </summary>
        public bool DeleteEnabled
        {
            get { return _deleteButton.Enabled || deleteToolStripMenuItem.Enabled || deleteToolStripButton.Enabled; }
            set { _deleteButton.Enabled = deleteToolStripMenuItem.Enabled = deleteToolStripButton.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets whether the message can be refreshed.
        /// </summary>
        public bool RefreshEnabled
        {
            get { return refreshToolStripMenuItem.Enabled || refreshToolStripButton.Enabled; }
            set { refreshToolStripMenuItem.Enabled = refreshToolStripButton.Enabled = value; }
        }

        /// <summary>
        /// Sets whether or not the form is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            set
            {
                this.SaveEnabled = this.AllowDrop = !value;
                _subjectTextBox.ReadOnly = value || this.HasTemplatedTitle;
                _bodyRichTextBox.ReadOnly = value || this.HasTemplatedBody;
                this.ActiveCandidatePicker.ReadOnly = _categoryComboBox.ReadOnly = _auditReviewsComboBox.ReadOnly = _postElectionVersionComboBox.ReadOnly = _receiptEmailTextBox.ReadOnly = value;
                _auditReviewsComboBox.Enabled = !value && this.IsAuditReview;
                this.DeleteEnabled = !value && (_message != null) && !_message.IsPosted;
                if (value)
                    _bodyRichTextBox.BackColor = _attachmentsListView.BackColor = Color.FromArgb(_subjectTextBox.BackColor.A, _subjectTextBox.BackColor);
            }
        }

        /// <summary>
        /// Sets whether or not the form is enabled for data entry.
        /// </summary>
        public bool IsEnabled
        {
            set
            {
                this.SaveEnabled = _categoryComboBox.Enabled = _auditReviewsComboBox.Enabled = _subjectTextBox.Enabled = _bodyRichTextBox.Enabled = _receiptEmailTextBox.Enabled = _attachmentsListView.Enabled = value;
                this.DeleteEnabled = value && _message != null && !_message.IsPosted;
            }
        }

        /// <summary>
        /// Gets whether or not the current message has a templated title.
        /// </summary>
        public bool HasTemplatedTitle
        {
            get { return _message != null && _message.Category != null && _message.Category.MessageTemplateTitle != null; }
        }

        /// <summary>
        /// Gets whether or not the current message has a templated body.
        /// </summary>
        public bool HasTemplatedBody
        {
            get { return _message != null && _message.Category != null && _message.Category.MessageTemplateBody != null; }
        }

        /// <summary>
        /// Gets whether or not the form is showing a statement review message.
        /// </summary>
        public bool IsStatementReview
        {
            get { return this.SelectedCategoryId == CmoCategory.StatementReviewCategoryID; }
        }

        /// <summary>
        /// Gets whether or not the form is showing a compliance visit message.
        /// </summary>
        public bool IsComplianceVisit
        {
            get { return this.SelectedCategoryId == CmoCategory.ComplianceVisitCategoryID; }
        }

        /// <summary>
        /// Gets whether or not the form is showing a doing business review message.
        /// </summary>
        public bool IsDoingBusinessReview
        {
            get { return this.SelectedCategoryId == CmoCategory.DoingBusinessReviewCategoryID; }
        }

        /// <summary>
        /// Gets whether or not the form is showing an audit review message.
        /// </summary>
        public bool IsAuditReview
        {
            get { return this.IsStatementReview || this.IsComplianceVisit || this.IsDoingBusinessReview || this.IsFundsPayment; }
        }

        /// <summary>
        /// Gets whether or not the form is showing a public funds payment message.
        /// </summary>
        public bool IsFundsPayment
        {
            get { return this.SelectedCategoryId == CmoCategory.PaymentCategoryID || this.IsPostElectionPayment; }
        }

        /// <summary>
        /// Gets whether or not the form is showing a post-election public funds payment message.
        /// </summary>
        public bool IsPostElectionPayment
        {
            get { return this.SelectedCategoryId == CmoCategory.PostElectionPaymentCategoryID; }
        }

        /// <summary>
        /// Gets whether or not the form requires an attachment.
        /// </summary>
        public bool RequiresAttachment
        {
            get { return this.IsAuditReview || this.IsPostElection || this.IsFundsPayment || this.SelectedCategoryId == CmoCategory.NoPayFindingsCategoryID; }
        }

        /// <summary>
        /// Gets the currently selected statement review number.
        /// </summary>
        public byte? SelectedAuditReviewNumber
        {
            get { return this.IsAuditReview ? Convert.ToByte(_auditReviewsComboBox.SelectedValue) as byte? : null; }
        }

        /// <summary>
        /// Gets the currently selected post election request type.
        /// </summary>
        public AuditRequestType? SelectedPostElectionType
        {
            get { return this.IsPostElection ? _postElectionVersionComboBox.SelectedValue as AuditRequestType? : null; }
        }

        /// <summary>
        /// Validates all required fields.
        /// </summary>
        /// <returns>true if all required fields have values; otherwise, false.</returns>
        private bool IsValid
        {
            get { return UpdateValidationStatus(false); }
        }

        /// <summary>
        /// Gets whether or not the message is a post election message.
        /// </summary>
        public bool IsPostElection
        {
            get { return this.SelectedCategoryId.HasValue && new[] { CmoCategory.IdrCategoryID, CmoCategory.IdrInadequateCategoryID, CmoCategory.IdrAdditionalInadequateCategoryID, CmoCategory.DarCategoryID, CmoCategory.DarInadequateCategoryID, CmoCategory.DarAdditionalInadequateCategoryID, CmoCategory.FarCategoryID }.Contains(this.SelectedCategoryId.Value); }
        }

        /// <summary>
        /// Gets whether or not the message passes post election audit workflow validation.
        /// </summary>
        public bool IsPostElectionWorkflowValid
        {
            get
            {
                string cid = this.ActiveCandidatePicker.SelectedCid;
                string ec = this.ActiveCandidatePicker.SelectedElection.Cycle;
                AuditReportBase pear = null;
                byte? category = this.SelectedCategoryId;

                // get most recently completed post election item
                if (category == CmoCategory.IdrCategoryID)
                {
                    pear = InitialDocumentationRequest.GetInitialDocumentationRequest(cid, ec, false);
                }
                else if (category == CmoCategory.IdrInadequateCategoryID)
                {
                    // check for existence of IDR response before allowing an Inadequate Response letter
                    pear = InitialDocumentationRequest.GetInitialDocumentationRequest(cid, ec, false);
                    if (pear == null || !pear.ResponseReceived)
                    {
                        MessageBox.Show("An Inadequate Response Letter cannot be created before the IDR response has been received. Please first enter the response received date in CFIS and then try again.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (!IdrResponseAnalysis.HasIdrResponseAnalysis(cid, ec))
                    {
                        MessageBox.Show("An Inadequate Response Letter cannot be created before the IDR Post Election Response Analysis has been finalized. Please first complete and finalize a response analysis worksheet in CFIS and then try again.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    pear = IdrInadequateEvent.GetIdrInadequateEvent(cid, ec, false);
                }
                else if (category == CmoCategory.DarInadequateCategoryID)
                {
                    // check for existence of DAR response before allowing an Inadequate Response letter
                    pear = DraftAuditReport.GetDraftAuditReport(cid, ec, false);
                    if (pear == null || !pear.ResponseReceived)
                    {
                        MessageBox.Show("An Inadequate Response Letter cannot be created before the DAR response has been received. Please first enter the response received date in CFIS and then try again.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    pear = DarInadequateEvent.GetDarInadequateEvent(cid, ec, false);
                }
                else if (category == CmoCategory.IdrAdditionalInadequateCategoryID || category == CmoCategory.DarAdditionalInadequateCategoryID)
                {
                    // prevent additional inadequates if FAR has been sent
                    pear = FinalAuditReport.GetFinalAuditReport(cid, ec);
                    if (pear != null)
                    {
                        MessageBox.Show("Additional Inadequate Response letters can no longer be sent because the Final Audit Report has been issued.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (this.SelectedCategoryId == CmoCategory.IdrAdditionalInadequateCategoryID)
                    {
                        // prevent additional IDR inadequates if DAR has been sent
                        pear = DraftAuditReport.GetDraftAuditReport(cid, ec, false);
                        if (pear != null)
                        {
                            MessageBox.Show("Additional Inadequate IDR Response letters can no longer be sent because the Draft Audit Report has been issued.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        // check for existence of IDR inadequate before allowing an additional
                        pear = IdrInadequateEvent.GetIdrInadequateEvent(cid, ec, false);
                        if (pear == null || !pear.ResponseReceived)
                        {
                            MessageBox.Show("An Additional Inadequate Response Letter cannot be created before the response has been received for the first Inadequate Response Letter. Please first enter the response received date in CFIS and then try again.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        // check for existence of DAR inadequate before allowing an additional
                        pear = DarInadequateEvent.GetDarInadequateEvent(cid, ec, false);
                        if (pear == null || !pear.ResponseReceived)
                        {
                            MessageBox.Show("An Additional Inadequate Response Letter cannot be created before the response has been received for the first Inadequate Response Letter. Please first enter the response received date in CFIS and then try again.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    pear = null;
                }
                else
                {
                    return true; // all other message categories pass validation
                }

                // prevent additional inadequates via inadequate category and additional IDRs after response has been received
                if (pear != null && pear.ResponseReceived)
                {
                    MessageBox.Show(pear.AuditReportType == AuditReportType.IdrInadequateResponse || pear.AuditReportType == AuditReportType.DarInadequateResponse ? "\"Inadequate Response Letter\" categories can no longer be used because the response has already been received. If you wish to send an additional inadequate response letter, please use an \"Additional Inadequate Letter\" category instead." : "An Initial Documentation Request can no longer be resent because the response has already been received.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //if (!this.SelectedPostElectionType.HasValue)
                //    return true;
                AuditRequestType? type = this.SelectedPostElectionType;
                if (pear == null)
                    return type == AuditRequestType.FirstRequest;
                if (pear.IsSecondRequest)
                    return type == AuditRequestType.SecondRepost;
                return type == AuditRequestType.FirstRepost || type == AuditRequestType.SecondRequest;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="CmoMessageForm"/> form.
        /// </summary>
        public CmoMessageForm()
        {
            InitializeComponent();
            _initializing = true;
            _bodyRichTextBox.DragEnter += new DragEventHandler(_bodyRichTextBox_DragEnter);
            this.ActiveCandidatePicker.ElectionPicker.SetCandidateID(null);
            this.ActiveCandidatePicker.CandidatePicker.SelectedIndexChanged += new EventHandler(CandidatePicker_SelectedIndexChanged);
            _addedAttachments = new Dictionary<string, string>();
            _removedAttachments = new HashSet<byte>();
            _currentUser = CfbDirectorySearcher.GetUser(Environment.UserName);
            _categoryComboBox.LoadCategories();
            _initializing = false;
        }

        /// <summary>
        /// Updates the validation status for all required fields.
        /// </summary>
        /// <param name="reset">Indicates whether to reset the status of all fields.</param>
        /// <returns>true if all required fields pass validation; otherwise, false.</returns>
        /// <remarks>If <paramref name="reset"/> is true, then true is always returned.</remarks>
        private bool UpdateValidationStatus(bool reset)
        {
            if (_message == null)
                return false;
            bool passes = true;
            if (!SetValidStatusColor(_subjectTextBox, !string.IsNullOrEmpty(_message.Title) || reset))
                passes = false;
            if (!SetValidStatusColor(_bodyRichTextBox, !string.IsNullOrEmpty(_bodyRichTextBox.Text) || reset))
                passes = false;
            if (!SetValidStatusColor(_categoryComboBox, this.SelectedCategoryId.HasValue || reset))
                passes = false;
            if (!SetValidStatusColor(_auditReviewsComboBox, !this.IsAuditReview || _auditReviewsComboBox.SelectedValue != null || reset))
                passes = false;
            // audit reviews require attachment
            if (!SetValidStatusColor(_attachmentsListView, !this.RequiresAttachment || _attachmentsListView.Items.Count > 0 || reset))
                passes = false;
            return passes;
        }

        /// <summary>
        /// Handles the event that occurs when the "Save" button is clicked.
        /// </summary>
        /// <returns>true if the message was successfully saved; otherwise, false.</returns>
        private bool SaveMessage()
        {
            if (!this.IsPostElectionWorkflowValid)
            {
                MessageBox.Show(this, "This message is no longer valid because a subsequent Post Election workflow letter has since been sent to the campaign.", "Post Election Workflow Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Cursor oldCursor = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ToolStripStatusLabel.Text = "Saving message...";
                bool success = false;

                // add if new message, otherwise update
                if (_message == null)
                {
                    _message = CmoMessage.Add(this.ActiveCandidatePicker.SelectedCid, this.ActiveCandidatePicker.SelectedElection.Cycle, _subjectTextBox.Text, _bodyRichTextBox.Text, Environment.UserName, _receiptEmailTextBox.Text, this.SelectedCategoryId.HasValue ? this.SelectedCategoryId.Value : CmoCategory.GenericCategoryID);
                    if (success = _message != null)
                    {
                        bool needsUpdate = false;
                        if (this.SelectedAuditReviewNumber.HasValue)
                        {
                            // save statement/payment number
                            _message.AuditReviewNumber = this.SelectedAuditReviewNumber;
                            needsUpdate = true;
                        }
                        else if (this.SelectedPostElectionType.HasValue)
                        {
                            // save post election properties
                            _message.PostElectionAuditRequestType = this.SelectedPostElectionType;
                            needsUpdate = true;
                        }
                        if (needsUpdate)
                        {
                            if (!_message.Update())
                            {
                                _message.Delete();
                                _message = null;
                                success = false;
                            }
                        }
                    }
                }
                else
                {
                    if (_message.IsPosted)
                    {
                        // only save unposted messages
                        MessageBox.Show(this, Properties.Resources.AlreadyPostedErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        // save all properties
                        _message.Category = _categoryComboBox.SelectedItem as CmoCategory;
                        _message.AuditReviewNumber = this.SelectedAuditReviewNumber;
                        _message.PostElectionAuditRequestType = this.SelectedPostElectionType;
                        _message.Title = _subjectTextBox.Text;
                        _message.Body = _bodyRichTextBox.Text;
                        _message.OpenReceiptEmail = _receiptEmailTextBox.Text;
                        success = _message.Update();
                    }
                }

                // check for additional inadequate event number.
                if (success && _message.IsIdrAdditionalInadequateLetter)
                {
                    if (_addedAttachments.Count > 1)
                    {
                        MessageBox.Show(this, "Multiple attachments not supported for this category. Please remove all additional attachments and try again.", "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        success = false;
                    }
                    else
                    {
                        int eventNumber;
                        foreach (string filename in _addedAttachments.Values)
                        {
                            if (TryParseEventNumber(filename, out eventNumber))
                            {
                                _message.TollingEventNumber = eventNumber;
                                success = _message.Update();
                            }
                            else
                            {
                                MessageBox.Show(this, "Unable to determine the tolling event number from the attachment filename. Please check the message attachment and try again.", "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                success = false;
                            }
                        }
                    }
                }

                // update attachments
                if (success)
                {
                    // remove attachments
                    foreach (byte id in _removedAttachments.ToArray())
                    {
                        if (_message.Detach(id))
                            _removedAttachments.Remove(id);
                        else
                            success = false;
                    }
                    // save new attachments
                    foreach (KeyValuePair<string, string> f in _addedAttachments.ToArray())
                    {
                        CmoAttachment attachment = null;
                        try
                        {
                            attachment = _message.Attach(File.ReadAllBytes(f.Key), f.Value);
                            _addedAttachments.Remove(f.Key);
                            DeleteWithSuppress(f.Key);
                        }
                        catch
                        {
                            // rollback attachment upon error
                            if (attachment != null)
                            {
                                _message.Detach(attachment.ID);
                            }
                            this.Detach(f.Key);
                            success = false;
                        }
                    }
                    if (success)
                    {
                        // show success
                        this.ToolStripStatusLabel.Text = string.Format("Message {0} saved successfully.", _message.UniqueID);
                    }
                    else
                    {
                        MessageBox.Show(this, "An error occurred while updating one or more attachment files. Please check the message attachments and try again.", "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ToolStripStatusLabel.Text = string.Format("Message {0} saved successfully, with exceptions.", _message.UniqueID);
                    }
                }
                else
                {
                    if (_message != null && _message.IsPosted)
                        this.ToolStripStatusLabel.Text = "Message has already been posted and can no longer be modified.";
                    else if (this.IsPostElection)
                    {
                        MessageBox.Show(this, PostElectionAuditFailedSaveMessage, string.Format("{0} ({1})", "Post Election Transaction Error", this.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ToolStripStatusLabel.Text = "Post election audit message could not be saved. Please see an administrator.";
                    }
                    else if (this.ShowTransactionError(retry: true) == DialogResult.Retry)
                        return SaveMessage();
                    else
                        this.ToolStripStatusLabel.Text = "Message could not be saved due to an error.";
                }
                return success;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.SetMessage(_message);
                this.Cursor = oldCursor;
            }
        }

        /// <summary>
        /// Parses a filename for a tolling event number.
        /// </summary>
        /// <param name="filename">The filename to parse.</param>
        /// <param name="eventNumber">When this method returns, contains the 32-bit signed integer value equivalent to the event number contained in <paramref name="filename"/>, if the parse succeeded, or zero if the parse failed. The parse fails if the <paramref name="filename"/> parameter is null, is not of the correct format, or represents a number less than <see cref="Int32.MinValue"/> or greater than <see cref="Int32.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <returns>true if <paramref name="filename"/> was converted successfully; otherwise, false.</returns>
        private bool TryParseEventNumber(string filename, out int eventNumber)
        {
            // get event number for additional inadequate responses
            int parsePosition = filename.LastIndexOf(Properties.Settings.Default.CfisGeneratedFilenameDelimiter) + 1;
            int length = filename.LastIndexOf('.');
            length = length < 0 ? filename.Length - parsePosition : length - parsePosition;
            if (parsePosition > 0 && length > 0 && int.TryParse(filename.Substring(parsePosition, length), out eventNumber))
            {
                return true;
            }
            else
            {
                eventNumber = 0;
                return false;
            }
        }

        /// <summary>
        /// Handles the event that occurs when the "Post" button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void PostMessage(object sender, EventArgs e)
        {
            // save message first before posting
            if (!SaveMessage())
                return;
            if (_message != null)
            {
                if (_message.IsPosted)
                {
                    // only post unposted messages
                    MessageBox.Show(this, Properties.Resources.AlreadyPostedErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (!this.IsValid)
                {
                    // only post valid messages
                    MessageBox.Show(this, Properties.Resources.IncompleteErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    this.ToolStripStatusLabel.Text = "Posting message...";
                    Cursor oldCursor = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;
                    // check presence of any C-Access accounts for the candidate before posting
                    DialogResult confirmation = DialogResult.Yes;
                    var users = CPSecurity.Provider.GetCampaignUsers(_message.CandidateID, _message.ElectionCycle, false);
                    if (users.Count() == 0)
                        confirmation = MessageBox.Show(this, string.Format("Warning: There are no C-Access accounts in EC{0} for {1} ({2}).\n\nDo you still wish to post this message?", _message.ElectionCycle, Cfb.CandidatePortal.Cfis.GetCandidateName(_message.CandidateID, false), _message.CandidateID), "Warning: No C-Accounts Found!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (confirmation == DialogResult.Yes)
                    {
                        var success = _message.Post();
                        if (success)
                        {
                            this.ToolStripStatusLabel.Text = string.Format("Message {0} posted successfully.", _message.UniqueID);
                        }
                        else
                        {
                            if (this.IsPostElection)
                            {
                                MessageBox.Show(this, PostElectionAuditFailedPostMessage, string.Format("{0} ({1})", "Post Election Transaction Error", this.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.ToolStripStatusLabel.Text = "Post election audit message could not be posted. Please see an administrator.";
                            }
                            else if (this.ShowTransactionError(retry: true) == DialogResult.Retry)
                                PostMessage(sender, e);
                            else
                                this.ToolStripStatusLabel.Text = "Message could not be posted due to an error.";
                        }
                    }
                    else
                    {
                        this.ToolStripStatusLabel.Text = "Message post was cancelled.";
                    }
                    this.Cursor = oldCursor;
                }
            }
            else
            {
                if (this.ShowTransactionError(retry: true) == DialogResult.Retry)
                    PostMessage(sender, e);
                else
                    this.ToolStripStatusLabel.Text = "Message could not be posted due to an error.";
            }
            SetMessage(_message);
        }

        /// <summary>
        /// Refreshes the message displayed with the latest version.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void RefreshMessage(object sender, EventArgs e)
        {
            if (_message != null)
                _message.Reload();
            SetMessage(_message);
        }

        /// <summary>
        /// Sets the message data source for this <see cref="CmoMessageForm"/>.
        /// </summary>
        public void SetMessage(CmoMessage value)
        {
            try
            {
                _loading = true;
                Application.DoEvents();
                Cursor oldCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                this.Reset();
                this.Icon = value == null ? global::Cfb.Camp.Cmo.Properties.Resources.NewEmail : value.IsOpened ? global::Cfb.Camp.Cmo.Properties.Resources.OpenEmail : global::Cfb.Camp.Cmo.Properties.Resources.Unread;
                _message = value;
                foreach (string path in _addedAttachments.Keys.ToArray())
                {
                    Detach(path);
                }
                _removedAttachments.Clear();
                if (this.RefreshEnabled = value != null)
                {
                    this.Text = string.Format("Campaign Message {0} Details", value.UniqueID);
                    this.ActiveCandidatePicker.SetActiveCandidate(value.ElectionCycle, value.CandidateID);
                    // load message
                    if (value.Category != null)
                    {
                        if (value.Category.Hidden)
                            _categoryComboBox.DataSource = new CmoCategory[] { value.Category };
                        _categoryComboBox.SelectedCategory = value.Category.ID;
                    }
                    else
                    {
                        ResetCategory();
                    }
                    if (value.StatementNumber.HasValue)
                        _auditReviewsComboBox.SelectedValue = value.StatementNumber.Value;
                    else if (value.ComplianceVisitNumber.HasValue)
                        _auditReviewsComboBox.SelectedValue = value.ComplianceVisitNumber.Value;
                    else if (value.PaymentRun.HasValue)
                        _auditReviewsComboBox.SelectedValue = value.PaymentRun.Value;
                    else if (value.PostElectionAuditRequestType.HasValue)
                        _postElectionVersionComboBox.SelectedValue = value.PostElectionAuditRequestType.Value;
                    _idTextBox.Text = value.UniqueID;
                    _subjectTextBox.Text = value.Title;
                    _bodyRichTextBox.Text = value.Body;
                    _creatorTextBox.Text = CfbDirectorySearcher.GetUser(value.Creator).DisplayName;
                    _receiptEmailTextBox.Text = value.OpenReceiptEmail;
                    if (_openStatusLabel.Visible = _openStatusValue.Visible = _statusValue.Enabled = value.IsPosted)
                    {
                        _statusValue.Text = string.Format("Posted {0:d} {0:t}", value.PostDate);
                    }
                    else
                    {
                        _statusValue.Text = "(draft)";
                    }
                    if (value.IsOpened)
                    {
                        _openStatusValue.Text = string.Format("Opened {0:d} {0:t} by {1}", value.OpenDate, value.Opener);
                        _openStatusValue.ForeColor = Color.Green;
                    }
                    else
                    {
                        _openStatusValue.Text = "(not yet opened)";
                        _openStatusValue.ForeColor = Color.Red;
                    }
                    _archiveStatusValue.Text = value.IsArchived ? string.Format("Archived {0:d} {0:t} by {1}", value.ArchiveDate, value.Archiver) : "(not archived)";
                    _followUpStatusValue.Text = value.NeedsFollowUp ? string.Format("Flagged {0:d} {0:t} by {1}", value.FollowUpDate, value.FollowUpFlagger) : "(not flagged)";
                    // load attachments
                    _attachmentsListView.Items.Clear();
                    foreach (CmoAttachment attachment in value.Attachments.Values)
                    {
                        ListViewItem item = new ListViewItem(new string[] { attachment.ID.ToString(), attachment.FileName });
                        item.Text = string.Format("[{0}] {1}", attachment.ID, attachment.FileName);
                        item.Name = attachment.ID.ToString();
                        _attachmentsListView.Items.Add(item);
                    }
                    this.ActiveCandidatePicker.ReadOnly = true;
                    this.IsReadOnly = value.IsPosted;
                    _bodyRichTextBox.Focus();
                }
                else
                {
                    Reset();
                }
                this.RefreshEnabled = value != null;
                UpdateClipboardControls();
                this.Cursor = oldCursor;
            }
            catch
            {
                throw;
            }
            finally
            {
                _loading = false;
            }

        }

        /// <summary>
        /// Resets the form for a new message.
        /// </summary>
        private void Reset()
        {
            this.Text = "New Campaign Message";
            ResetCategory();
            _auditReviewsComboBox.Visible = false;
            _idTextBox.Text = _subjectTextBox.Text = _bodyRichTextBox.Text = _receiptEmailTextBox.Text = _statusValue.Text = _openStatusValue.Text = _archiveStatusValue.Text = _followUpStatusValue.Text = null;
            _receiptEmailTextBox.Text = _loading ? null : _currentUser.Email;
            _creatorTextBox.Text = _loading ? null : _currentUser.DisplayName;
            _attachmentsListView.Items.Clear();
            _addedAttachments.Clear();
            _removedAttachments.Clear();
            _bodyRichTextBox.BackColor = _attachmentsListView.BackColor = SystemColors.Window;
            CandidatePicker_SelectedIndexChanged(this, new EventArgs());
            UpdateClipboardControls();
        }

        /// <summary>
        /// Resets the category selection as well as other linked dropdown lists.
        /// </summary>
        private void ResetCategory()
        {
            if (_bodyRichTextBox.ReadOnly)
                _bodyRichTextBox.Text = null;
            _categoryComboBox.SelectedIndex = -1;
            _categoryComboBox.Enabled = true;
            _auditReviewsComboBox.ClearDataSourceItems();
            _postElectionVersionComboBox.ClearDataSourceItems();
        }

        /// <summary>
        /// Sets the background color of a control to indicate its validation state.
        /// </summary>
        /// <param name="control">The control to modify.</param>
        /// <param name="isValid">Whether or not the control is valid.</param>
        /// <returns>The control's validation state.</returns>
        private bool SetValidStatusColor(Control control, bool isValid)
        {
            control.BackColor = isValid ? SystemColors.Window : Color.LightPink;
            return isValid;
        }

        /// <summary>
        /// Handles the event that occurs when the "Delete" button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void DeleteMessage(object sender, EventArgs e)
        {
            if (_message != null)
            {
                if (_message.IsPosted)
                {
                    // only delete unposted messages
                    RefreshMessage(sender, e);
                    MessageBox.Show(this, Properties.Resources.AlreadyPostedErrorMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    this.ToolStripStatusLabel.Text = "Deleting message...";
                    if (_message.Delete())
                    {
                        string oldId = _message.UniqueID;
                        _message = null;
                        RefreshMessage(sender, e);
                        Reset();
                        this.ToolStripStatusLabel.Text = string.Format("Message {0} deleted successfully.", oldId);
                    }
                    else
                    {
                        if (this.ShowTransactionError(retry: true) == DialogResult.Retry)
                            DeleteMessage(sender, e);
                        else
                            this.ToolStripStatusLabel.Text = "Message could not be deleted due to an error.";
                        RefreshMessage(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// Attaches one or more files to the current message. Does not upload the attachment to a repository.
        /// </summary>
        /// <param name="paths">An array of paths to files for attaching.</param>
        private void Attach(string[] paths)
        {
            foreach (string f in paths)
            {
                if (!Attach(f)) break;
            }
        }

        /// <summary>
        /// Attaches a file to the current message. Does not upload the attachment to a repository.
        /// </summary>
        /// <param name="path">The path to a file to attach.</param>
        /// <returns>true if the file was successfully attached; otherwise, false.</returns>
        /// <remarks>Upon completion, the attachment is only temporarily attached to the message and stored in a staging area. Making the attachment permanent requires at least saving the message via the <see cref="SaveMessage"/> method, which will generate a database entry and also move the attachment from the staging area to the CMO attachment repository.</remarks>
        private bool Attach(string path)
        {
            try
            {
                // copy file to temporary location
                if (!Directory.Exists(_tempFolder))
                    Directory.CreateDirectory(_tempFolder);
                int inc = 1;
                FileInfo fi = new FileInfo(path);
                string originalName = fi.GetName() + fi.Extension;
                string target = GetTempCopyPath(fi.Name);
                // auto-name file in temp folder
                while (File.Exists(target))
                {
                    target = GetTempCopyPath(string.Format("{0} ({1}){2}", originalName, inc++, fi.Extension));
                }
                fi.CopyTo(target, false);
                // register new attachment record with form
                ListViewItem item = new ListViewItem(new string[] { target, originalName });
                item.Text = originalName;
                item.Name = target;
                _attachmentsListView.Items.Add(item);
                _addedAttachments.Add(target, originalName);
                return true;
            }
            catch (Exception e)
            {
                switch (MessageBox.Show(this, string.Format("An error occurred attaching one or more files:\n\n{0}\n\nPlease check the message attachments and try again.", e.Message), "Attachment Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                {
                    case DialogResult.Abort:
                        return false;
                    case DialogResult.Retry:
                        return Attach(path);
                }
            }
            return false;
        }

        /// <summary>
        /// Converts a file path to its temporary folder copy.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>The path of the file indicated by <paramref name="path"/> in the temporary folder.</returns>
        private string GetTempCopyPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            return string.Format("{0}\\{1}", _tempFolder, path.Substring(path.LastIndexOf('\\') + 1));
        }

        /// <summary>
        /// Detaches an unsaved pending attachment from the message.
        /// </summary>
        /// <param name="path">The path of the attachment file to detach.</param>
        private void Detach(string path)
        {
            _addedAttachments.Remove(path);
            DeleteWithSuppress(path);
        }

        /// <summary>
        /// Detaches all selected items in the attachments area from the message.
        /// </summary>
        private void DetachSelected()
        {
            if ((_message == null) || !_message.IsPosted)
            {
                // delete selected attachments when uncommitted, 
                // or queue them for permanent deletion when already committed
                foreach (ListViewItem item in _attachmentsListView.SelectedItems)
                {
                    byte attId;
                    if (_addedAttachments.ContainsKey(item.Name))
                        Detach(item.Name);
                    else if (byte.TryParse(item.Name, out attId))
                        _removedAttachments.Add(attId);
                    _attachmentsListView.Items.Remove(item);
                }
            }
        }

        /// <summary>
        /// Loads the review selection list with completed audit reviews for a candidate.
        /// </summary>
        private void LoadAuditReviews()
        {
            string cid = this.ActiveCandidatePicker.SelectedCid;
            Election election = this.ActiveCandidatePicker.SelectedElection;
            if (string.IsNullOrEmpty(cid) || election == null)
                return;
            string cycle = election.Cycle;
            if (this.IsFundsPayment)
            {
                _auditReviewsComboBox.ClearDataSourceItems();
                _auditReviewsComboBox.ValueMember = "Run";
                _auditReviewsLabel.Text = "Run #";
                _auditReviewsComboBox.DataSource = PublicFundsHistory.GetPublicFundsHistory(cid, cycle).Determinations.OrderByDescending(d => d.Run).ToList();
            }
            else if (this.IsAuditReview)
            {
                _auditReviewsComboBox.ClearDataSourceItems();
                _auditReviewsComboBox.ValueMember = "Number";
                if (this.IsComplianceVisit)
                {
                    _auditReviewsLabel.Text = "Visit #";
                    _auditReviewsComboBox.DataSource = ComplianceVisits.GetComplianceVisits(cid, cycle).Visits.OrderByDescending(v => v.Number).ToList();
                }
                else
                {
                    _auditReviewsLabel.Text = "Statement #";
                    if (this.IsDoingBusinessReview)
                        _auditReviewsComboBox.DataSource = DoingBusinessReviews.GetDoingBusinessReviews(cid, cycle).Reviews.OrderByDescending(r => r.Statement.Number).ToList();
                    else
                        _auditReviewsComboBox.DataSource = StatementReviews.GetStatementReviews(cid, cycle).Reviews.OrderByDescending(r => r.Statement.Number).ToList();
                }
            }
        }

        /// <summary>
        /// Loads the post election audit documentation request version selection list with versions available for the candidate's current post election audit status.
        /// </summary>
        private void LoadPostElectionVersions()
        {
            bool loadingState = _loading;
            try
            {
                _loading = true; // suppress workflow validation while loading versions
                _postElectionVersionComboBox.ClearDataSourceItems();
                string cid = this.ActiveCandidatePicker.SelectedCid;
                Election election = this.ActiveCandidatePicker.SelectedElection;
                if (this.IsPostElection && !string.IsNullOrEmpty(cid) && election != null)
                {
                    // retrieve current post election workflow
                    AuditReportBase pear = null;
                    byte? category = this.SelectedCategoryId;
                    if (category == CmoCategory.IdrCategoryID)
                    {
                        pear = InitialDocumentationRequest.GetInitialDocumentationRequest(cid, election.Cycle, false);
                    }
                    else if (category == CmoCategory.IdrInadequateCategoryID)
                    {
                        pear = IdrInadequateEvent.GetIdrInadequateEvent(cid, election.Cycle, false);
                    }
                    else if (category == CmoCategory.DarInadequateCategoryID)
                    {
                        pear = DarInadequateEvent.GetDarInadequateEvent(cid, election.Cycle, false);
                    }

                    // show/hide versions based upon current post election workflow state
                    Dictionary<string, AuditRequestType> versions = new Dictionary<string, AuditRequestType>(4);
                    if (pear == null)
                    {
                        versions["First Request"] = AuditRequestType.FirstRequest;
                    }
                    else if (!pear.IsSecondRequest)
                    {
                        versions["First Request (Repost)"] = AuditRequestType.FirstRepost;
                        versions["Second Request"] = AuditRequestType.SecondRequest;
                    }
                    else
                    {
                        versions["Second Request (Repost)"] = AuditRequestType.SecondRepost;
                    }

                    // force listing of version for currently displayed message
                    if (_message != null)
                    {
                        switch (_message.PostElectionAuditRequestType)
                        {
                            case AuditRequestType.FirstRepost:
                                versions["First Request (Repost)"] = AuditRequestType.FirstRepost;
                                break;
                            case AuditRequestType.FirstRequest:
                                versions["First Request"] = AuditRequestType.FirstRequest;
                                break;
                            case AuditRequestType.SecondRepost:
                                versions["Second Request (Repost)"] = AuditRequestType.SecondRepost;
                                break;
                            case AuditRequestType.SecondRequest:
                                versions["Second Request"] = AuditRequestType.SecondRequest;
                                break;
                        }
                    }

                    _postElectionVersionComboBox.DisplayMember = "Key";
                    _postElectionVersionComboBox.ValueMember = "Value";
                    _postElectionVersionComboBox.DataSource = versions.ToArray();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _loading = loadingState;
            }
        }

        /// <summary>
        /// Deletes the specified file while suppressing any exceptions thrown in the process.
        /// </summary>
        /// <param name="path">The name of the file to be deleted.</param>
        private void DeleteWithSuppress(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch { }
        }

        /// <summary>
        /// Prints the current message.
        /// </summary>
        /// <param name="showDialog">true to show a print dialog before printing; otherwise, false to print immediately.</param>
        public void PrintMessage(bool showDialog)
        {
            if (_message != null)
            {
                if (showDialog)
                {
                    if (this.PrintDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    this.PrintDocument.PrinterSettings = this.PrintDialog.PrinterSettings;
                }
                using (LocalReport report = GetCmoReport())
                {
                    using (ReportPrintDocument rpd = new ReportPrintDocument(report))
                    {
                        rpd.PrinterSettings = this.PrintDocument.PrinterSettings;
                        rpd.RenderReport();
                        rpd.Print();
                    }
                }
            }
        }

        /// <summary>
        /// Loads a message template based on category.
        /// </summary>
        private void LoadTemplate()
        {
            if (!this.SelectedCategoryId.HasValue || _loading)
                return;
            CmoCategory category = CmoCategory.GetCmoCategory(this.SelectedCategoryId.Value);
            if (category != null)
            {
                if (_subjectTextBox.ReadOnly = category.MessageTemplateTitle != null)
                    _subjectTextBox.Text = string.Format(category.MessageTemplateTitle, _auditReviewsComboBox.Text);
                else
                    _subjectTextBox.Text = null;
                _bodyRichTextBox.ReadOnly = category.MessageTemplateBody != null;
                _bodyRichTextBox.Text = category.MessageTemplateBody;
                _subjectTextBox.ReadOnly &= !category.MessageTemplateEditable;
                _bodyRichTextBox.ReadOnly &= !category.MessageTemplateEditable;
            }
            else
            {
                _subjectTextBox.ReadOnly = _bodyRichTextBox.ReadOnly = false;
                _subjectTextBox.Text = _bodyRichTextBox.Text = null;
            }
        }

        /// <summary>
        /// Occurs once for each page to be printed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="PrintPageEventArgs"/> that contains the event data.</param>
        protected override void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            base.OnPrintPage(sender, e);
        }

        /// <summary>
        /// Gets the form toolstrips that are to be merged with the main CAMP toolstrip.
        /// </summary>
        /// <returns>A collection of toolstrips to merge.</returns>
        public override IEnumerable<ToolStrip> GetMergingToolStrips()
        {
            return new[] { this.toolStrip };
        }

        /// <summary>
        /// Updates the cut, copy, and paste controls to be enabled or disabled only for supported text input controls.
        /// </summary>
        private void UpdateClipboardControls()
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null)
            {
                copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = copyToolStripMenuItem1.Enabled = !string.IsNullOrEmpty(richTextBox.SelectedText);
                if (richTextBox.Enabled && !richTextBox.ReadOnly)
                {
                    cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = cutToolStripMenuItem1.Enabled = !string.IsNullOrEmpty(richTextBox.SelectedText);
                    DataFormats.Format textFormat = DataFormats.GetFormat(DataFormats.Text);
                    pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = pasteToolStripMenuItem1.Enabled = richTextBox.CanPaste(textFormat);
                    undoToolStripMenuItem.Enabled = _bodyRichTextBox.CanUndo;
                    redoToolStripMenuItem.Enabled = _bodyRichTextBox.CanRedo;
                }
                else
                {
                    undoToolStripMenuItem.Enabled = redoToolStripMenuItem.Enabled = cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = cutToolStripMenuItem1.Enabled = pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = pasteToolStripMenuItem1.Enabled = false;
                }
                selectAllToolStripMenuItem.Enabled = true;
            }
            else
            {
                // items enabled only for RichtTextBox controls
                selectAllToolStripMenuItem.Enabled = undoToolStripMenuItem.Enabled = redoToolStripMenuItem.Enabled = copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = copyToolStripMenuItem1.Enabled = cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = cutToolStripMenuItem1.Enabled = pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = pasteToolStripMenuItem1.Enabled = false;
            }
        }

        /// <summary>
        /// Retrieves a <see cref="LocalReport"/> for printing the current CMO message.
        /// </summary>
        /// <returns>A Microsoft report for printing the current CMO message.</returns>
        private LocalReport GetCmoReport()
        {
            LocalReport report = new LocalReport();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = _message;
            report.ReportEmbeddedResource = "Cfb.Camp.Cmo.Reports.CmoMessageReport.rdlc";
            report.DataSources.Add(new ReportDataSource("CmoMessage", bindingSource));
            return report;
        }

        #region Control Event Handlers

        private void CmoMessageForm_Load(object sender, EventArgs e)
        {
            if (_message == null)
                Reset();
            else
                SetMessage(_message);
        }

        private void CandidatePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing)
                return;
            string cid = this.ActiveCandidatePicker.SelectedCid;
            if (string.IsNullOrEmpty(cid))
            {
                this.IsEnabled = false;
                this.ActiveCandidatePicker.ReadOnly = false;
                this.ActiveCandidatePicker.Enabled = true;
            }
            else
            {
                _categoryComboBox.Enabled = true;
                _categoryComboBox_SelectedIndexChanged(sender, e);
            }
            LoadAuditReviews();
            LoadPostElectionVersions();
        }

        private void _categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing)
                return;

            bool previousState = _categoryComboBox.Enabled;
            this.IsEnabled = this.SelectedCategoryId.HasValue;
            _categoryComboBox.Enabled = previousState;

            // show/hide audit review number based on category
            _auditReviewsLabel.Visible = _auditReviewsComboBox.Visible = _auditReviewsComboBox.Enabled = this.IsAuditReview;
            LoadAuditReviews();

            // show/hide post election version box based on category
            _postElectionVersionLabel.Visible = _postElectionVersionComboBox.Visible = (_postElectionVersionComboBox.Enabled = this.IsPostElection) && this.SelectedCategoryId != CmoCategory.IdrAdditionalInadequateCategoryID && this.SelectedCategoryId != CmoCategory.DarAdditionalInadequateCategoryID;
            LoadPostElectionVersions();
            if (_postElectionVersionComboBox.ClearDataSourcePending)
                LoadPostElectionVersions();

            // load message template
            LoadTemplate();
        }

        private void _auditReviewsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            if (this.IsStatementReview)
            {
                StatementReview s = e.ListItem as StatementReview;
                if (s != null)
                    e.Value = string.Format(_auditReviewsComboBox.FormatString, s.Number, s.Statement.DueDate);
            }
            else if (this.IsComplianceVisit)
            {
                ComplianceVisit v = e.ListItem as ComplianceVisit;
                if (v != null)
                    e.Value = string.Format(_auditReviewsComboBox.FormatString, v.Number, v.Date);
            }
            else if (this.IsDoingBusinessReview)
            {
                DoingBusinessReview d = e.ListItem as DoingBusinessReview;
                if (d != null)
                    e.Value = string.Format(_auditReviewsComboBox.FormatString, d.Number, d.Statement.DueDate);
            }
            else if (this.IsFundsPayment)
            {
                PublicFundsDetermination d = e.ListItem as PublicFundsDetermination;
                if (d != null)
                    e.Value = string.Format(_auditReviewsComboBox.FormatString, d.Run, d.Date);
            }
        }

        private void CmoMessageForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                Attach(e.Data.GetData(DataFormats.FileDrop, false) as string[]);
        }

        private void CmoMessageForm_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : e.Effect = DragDropEffects.None;
        }

        private void ViewSelectedAttachments(object sender, EventArgs e)
        {
            foreach (ListViewItem item in _attachmentsListView.SelectedItems)
            {
                byte attId;
                if (byte.TryParse(item.Name, out attId))
                {
                    // saved attachment, so retrieve path from record
                    CmoAttachment attachment;
                    if ((_message != null) && _message.Attachments.TryGetValue(attId, out attachment))
                    {
                        Process.Start(attachment.Path);
                    }
                    else
                    {
                        MessageBox.Show(this, "One or more requested attachments could not be found. Please try again.", "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RefreshMessage(sender, e);
                        break;
                    }
                }
                else
                {
                    // unsaved attachment, so retrieve path directly
                    Process.Start(item.Name);
                }
            }
        }

        private void _attachmentsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DetachSelected();
        }

        private void DetachSelectedAttachments(object sender, EventArgs e)
        {
            DetachSelected();
        }

        private void _attachButton_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
                this.Attach(_openFileDialog.FileNames);
        }

        private void CmoMessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string path in _addedAttachments.Keys)
            {
                DeleteWithSuppress(path); // clean-up unsaved pending attachments
            }
        }

        private void _bodyRichTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.None;
        }

        private void _attachmentContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = _attachmentsListView.SelectedItems.Count == 0;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintMessage(true);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            PrintMessage(false);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_message != null)
            {
                using (LocalReport report = GetCmoReport())
                {
                    using (ReportPrintDocument rpd = new ReportPrintDocument(report))
                    {
                        rpd.PrinterSettings = this.PrintDocument.PrinterSettings;
                        rpd.RenderReport();
                        this.PrintPreviewDialog.Document = rpd;
                        this.PrintPreviewDialog.ShowDialog();
                    }
                }
            }
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            SaveMessage();
            UpdateValidationStatus(true);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && !string.IsNullOrEmpty(richTextBox.SelectedText))
                richTextBox.Cut();
            UpdateClipboardControls();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && !string.IsNullOrEmpty(richTextBox.SelectedText))
                richTextBox.Copy();
            UpdateClipboardControls();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && richTextBox.Enabled && !richTextBox.ReadOnly)
            {
                DataFormats.Format textFormat = DataFormats.GetFormat(DataFormats.Text);
                if (richTextBox.CanPaste(textFormat))
                    richTextBox.Paste(textFormat);
            }
        }

        private void _bodyRichTextBox_Enter(object sender, EventArgs e)
        {
            UpdateClipboardControls();
        }

        private void _bodyRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            UpdateClipboardControls();
        }

        private void _bodyRichTextBox_Leave(object sender, EventArgs e)
        {
            UpdateClipboardControls();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && richTextBox.CanUndo && !richTextBox.ReadOnly && richTextBox.Enabled)
            {
                richTextBox.Undo();
                UpdateClipboardControls();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && richTextBox.CanRedo && !richTextBox.ReadOnly && richTextBox.Enabled)
            {
                richTextBox.Redo();
                UpdateClipboardControls();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox != null && richTextBox.Enabled)
                richTextBox.SelectAll();
        }

        private void _bodyContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!_bodyRichTextBox.Focused)
                _bodyRichTextBox.Focus();
        }

        private void _auditReviewsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTemplate();
        }

        private void _postElectionVersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((_message == null || (_message != null && !_loading)) && !this.IsPostElectionWorkflowValid) // do not validate while loading a message
            {
                if (_postElectionVersionComboBox.SelectedIndex == -1 && _postElectionVersionComboBox.Items.Count > 0)
                {
                    _postElectionVersionComboBox.SelectedIndex = 0; // auto-select post election request type
                }
                else if (_message == null)
                {
                    ResetCategory();
                }
                else
                {
                    // restore message category and post election request type
                    try
                    {
                        _loading = true;
                        _categoryComboBox.SelectedValue = _message.CategoryID;
                        if (_message.PostElectionAuditRequestType.HasValue)
                        {
                            _postElectionVersionComboBox.SelectedValue = _message.PostElectionAuditRequestType.Value;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        _loading = false;
                    }
                }
            }
        }

        #endregion
    }
}