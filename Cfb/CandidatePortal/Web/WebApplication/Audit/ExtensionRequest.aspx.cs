using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.Extensions;
using Extension = Cfb.CandidatePortal.ExtensionRequest;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class ExtensionRequest : CPPage
    {
        /// <summary>
        /// Command name value for indicating a confirmed extension request submission.
        /// </summary>
        private const string SubmitCommandName = "ER_SUBMIT_COMMAND";

        /// <summary>
        /// Command name value for indicating an extension request modification.
        /// </summary>
        private const string ModifyCommandName = "ER_MODIFY_COMMAND";

        /// <summary>
        /// Command name value for cancelling an extension request.
        /// </summary>
        private const string CancelCommandName = "ER_CANCEL_COMMAND";

        /// <summary>
        /// HTML template for outgoing extension request e-mail message body.
        /// </summary>
        private const string EmailBodyFormat = @"<html>
<head>
<style type=""text/css"">
th {{ padding:5px;text-align:right; }}
td {{ padding:5px;text-align:left; }}
</style>
</head>
<body>
<p>The following extension request was received via C-Access:</p>
<table border=""0"" cellpadding=""0"" cellspacing=""0"">
<tr><th>Candidate:</th><td>{0} ({1})</td></tr>
<tr><th>C-Access User:</th><td>{2} ({3})</td></tr>
<tr><th>Election Cycle:</th><td>{4}</td></tr>
<tr><th>Response Type:</th><td>{5}</td></tr>
<tr><th>{6}</th><td>{7}</td></tr>
<tr><th>Requested Due Date:</th><td>{8}</td></tr>
</table>
</body>
</html>";

        /// <summary>
        /// The delimiter separating the review number from the review response due date for items in the review dropdown list.
        /// </summary>
        private const char ReviewItemDelimiter = '_';

        /// <summary>
        /// Raises the <see cref="Control.PreInit"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            base.OnPreInit(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                EnsureChildControls();
                // session tracking object to prevent accidental resubmissions via page refresh
                Session[Page.UniqueID] = new object();
                // prepopulate values, if available
                var selected = Request.GetExtensionType();
                _types.SelectedValue = selected.HasValue ? selected.Value.ToString() : null;
                SelectedTypeChanged(_types, e, selected);
            }
        }

        /// <summary>
        /// Occurs when the form is submitted.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="CommandEventArgs"/> that contains the event data.</param>
        protected void OnCommand(object sender, CommandEventArgs e)
        {
            string command = e.CommandName;
            if (command == CancelCommandName)
            {
                var type = Request.GetExtensionType();
                string target = ".";
                if (type.HasValue)
                {
                    switch (type.Value)
                    {
                        case ExtensionType.StatementReview:
                            target = PageUrlManager.StatementReviewsPageUrl;
                            break;
                        case ExtensionType.ComplianceVisitReview:
                            target = PageUrlManager.ComplianceVisitsPageUrl;
                            break;
                    }
                }
                Response.Redirect(target);
                return;
            }
            else if (Session[Page.UniqueID] == null)
            {
                // page refresh, so start from beginning
                Session.Remove(Page.UniqueID);
                Server.Transfer(Request.RawUrl);
            }
            else if (Page.IsValid)
            {
                switch (command)
                {
                    case SubmitCommandName:
                        // submit and show verification
                        if (SubmitExtensionRequest())
                        {
                            // show confirmation
                            SetConfirmationMode(true);
                            _submit.Visible = _modify.Visible = false;
                            _cancel.Text = _cancel.ToolTip = "Return";
                            _confirmText.Text = string.Format("The following extension request has been received by the {0}. Please allow a few days for your auditor to review your request.", Resources.CPResources.agency_name);
                            Session.Remove(Page.UniqueID);
                        }
                        else
                        {
                            // show error
                            SetConfirmationMode(false);
                            _submit.CommandName = null;
                            _introText.Text = "An error occurred while processing your extension request. Please review the extension request details and attempt to resubmit. If you continue to experience problems submitting an extension request, please contact our Audit department directly at (212) 306-7100.";
                        }
                        break;
                    case ModifyCommandName:
                        // show edit mode
                        SetConfirmationMode(false);
                        _submit.Text = "Continue &amp; Verify";
                        _submit.ToolTip = "Continue and Verify Extension Request";
                        _submit.CommandName = null;
                        break;
                    default:
                        // show confirmation mode
                        SetConfirmationMode(true);
                        _typesValue.Text = _types.SelectedItem.Text;
                        _numberValue.Text = _numberDropDown.SelectedItem.Text;
                        _dueDateValue.Text = _dueDateDropDown.SelectedItem.Text;
                        _reasonValue.Text = _reason.Text;
                        _submit.Text = "Submit Request";
                        _submit.CommandName = SubmitCommandName;
                        _submit.Enabled = true;
                        break;
                }
            }
            _requestFormUpdatePanel.Update();
        }

        /// <summary>
        /// Attempts to submit the current extension request.
        /// </summary>
        /// <returns>true if the extension request was successfully submitted; otherwise, false.</returns>
        private bool SubmitExtensionRequest()
        {
            try
            {
                byte number;
                DateTime dueDate;
                DateTime requestedDate;
                if (TryParseReviewNumberListItemValue(_numberDropDown.SelectedValue, out number, out dueDate) && DateTime.TryParse(_dueDateDropDown.SelectedValue, out requestedDate))
                {
                    Extension req = Extension.Add(CPProfile.Cid, _electionCycle.Text, CPConvert.ParseEnum<ExtensionType>(_types.SelectedValue), number, DateTime.Now, requestedDate, _reason.Text);
                    if (req != null)
                    {
                        // send e-mail notification
                        CPMailMessage message = new CPMailMessage();
                        message.Sender = CPProfile.GetMailAddress();
                        message.To.Add(CPApplication.AuditExtensionRequestsEmail);
                        message.IsBodyHtml = true;
                        message.Subject = string.Format("Request for Extension to EC{0} {1} {2}", req.ElectionCycle, _typesValue.Text, req.ReviewNumber);
                        message.Body = string.Format(EmailBodyFormat,
                            Cfis.GetCandidateName(CPProfile.Cid, true),
                            CPProfile.Cid,
                            message.Sender.DisplayName,
                            User.Identity.Name,
                            req.ElectionCycle,
                            _typesValue.Text,
                            _numberLabelText.Text,
                            _numberValue.Text,
                            _dueDateValue.Text);
                        message.Send();
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!Page.IsPostBack)
            {
                _electionCycle.Text = CPProfile.ElectionCycle;
                // populate extension types
                _types.Items.Clear();
                bool hasReviews = CPProfile.StatementReviews.ResponseDeadlines.Count > 0;
                bool hasVisits = CPProfile.ComplianceVisits.ResponseDeadlines.Count > 0;
                _types.Items.Add(new ListItem(hasReviews || hasVisits ? "(select a response type)" : "(no audit responses due)", ""));
                if (hasReviews)
                    _types.Items.Add(new ListItem("Statement Review", ExtensionType.StatementReview.ToString()));
                if (hasVisits)
                    _types.Items.Add(new ListItem("Compliance Visit", ExtensionType.ComplianceVisitReview.ToString()));
                // wire-up button command names
                _modify.CommandName = ModifyCommandName;
                _cancel.CommandName = CancelCommandName;
            }
        }

        /// <summary>
        /// Occurs when the selected extension type is changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected void SelectedTypeChanged(object sender, EventArgs e)
        {
            SelectedTypeChanged(sender, e, GetSelectedExtensionType());
        }

        /// <summary>
        /// Occurs when the selected extension type is changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <param name="type">The selected extension type.</param>
        protected void SelectedTypeChanged(object sender, EventArgs e, ExtensionType? type)
        {
            if (!Page.IsCallback)
            {
                // set review/visit number upon extension type selection
                ResetResponseNumberDisplay();
                string value = _types.SelectedValue;
                byte? selectedNumber = Request.GetReviewNumber();
                if (type.HasValue)
                {
                    switch (type.Value)
                    {
                        case ExtensionType.StatementReview:
                            _numberLabelText.Text = "Statement Number:";
                            foreach (SRResponseDeadline d in CPProfile.StatementReviews.ResponseDeadlines)
                            {
                                if (!d.ResponseReceived)
                                    _numberDropDown.Items.Add(new ListItem(string.Format("{0} (Response Due {1:d})", d.ReviewNumber, d.Date), MakeReviewNumberListItemValue(d.ReviewNumber, d.Date))
                                    {
                                        Selected = !Page.IsPostBack && selectedNumber.HasValue && d.ReviewNumber == selectedNumber
                                    });
                            }
                            if (_numberDropDown.Items.Count == 0)
                                _numberDropDown.Items.Add(new ListItem("(no outstanding responses)", null));
                            _numberRow.Visible = true;
                            break;
                        case ExtensionType.ComplianceVisitReview:
                            _numberLabelText.Text = "Visit Number:";
                            foreach (CVResponseDeadline d in CPProfile.ComplianceVisits.ResponseDeadlines)
                            {
                                if (!d.ResponseReceived)
                                    _numberDropDown.Items.Add(new ListItem(string.Format("{0} (Response Due {1:d})", d.ReviewNumber, d.Date), MakeReviewNumberListItemValue(d.ReviewNumber, d.Date)));
                            }
                            if (_numberDropDown.Items.Count == 0)
                                _numberDropDown.Items.Add(new ListItem("(no outstanding responses)", null));
                            _numberRow.Visible = true;
                            break;
                    }
                }
            }
            SelectedReviewNumberChanged(sender, e);
        }

        /// <summary>
        /// Occurs when the selected review number is changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected void SelectedReviewNumberChanged(object sender, EventArgs e)
        {
            ResetDueDateDisplay();
            // populate extension due dates
            byte number;
            DateTime originalDate;
            DateTime? selectedDate = Request.GetDate();
            var type = GetSelectedExtensionType();
            if (type.HasValue)
            {
                if (TryParseReviewNumberListItemValue(_numberDropDown.SelectedValue, out number, out originalDate))
                {
                    DateTime targetDate = originalDate;
                    byte maxLength = CPProfile.ElectionCycle == "2009" && type.Value == ExtensionType.StatementReview ? Extension.GetMaxExtensionLength(number) : Extension.DefaultMaxExtensionLength;
                    for (byte i = 1; i <= maxLength; i++)
                    {
                        targetDate = targetDate.AddDays(1);
                        switch (targetDate.DayOfWeek)
                        {
                            case DayOfWeek.Saturday:
                            case DayOfWeek.Sunday:
                                // only count business days
                                i--;
                                break;
                            default:
                                _dueDateDropDown.Items.Add(new ListItem(string.Format("{0:ddd}, {0:d}", targetDate), targetDate.ToShortDateString())
                                {
                                    Selected = selectedDate.HasValue && selectedDate.Value.Date == targetDate.Date
                                });
                                break;
                        }
                    }
                    _dueDateRow.Visible = true;
                }
            }
            SelectedDateChanged(sender, e);
        }

        /// <summary>
        /// Occurs when the selected extension due date is changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected void SelectedDateChanged(object sender, EventArgs e)
        {
            _hiddenSubmit.Enabled = _submit.Enabled = _reasonValidator.Enabled = _dueDateDropDown.Visible && !string.IsNullOrEmpty(_dueDateDropDown.SelectedValue);
            _requestFormUpdatePanel.Update();
        }

        /// <summary>
        /// Creates a list item value for a review number and response due date.
        /// </summary>
        /// <param name="number">The number of the audit review.</param>
        /// <param name="date">The due date for the audit review response.</param>
        /// <returns>A list item value string for the specified review number and response due date.</returns>
        private string MakeReviewNumberListItemValue(int number, DateTime date)
        {
            return string.Format("{0}{1}{2}", number, ReviewItemDelimiter, date);
        }

        /// <summary>
        /// Parses a list item value string for a review number and response due date. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="value">A string containing the list item value to parse.</param>
        /// <param name="number">When this method returns, contains the <see cref="Byte"/> value equivalent to the review number contained in <paramref name="value"/>, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the value is null, is not of the correct format, or represents a number less than <see cref="Int32.MinValue"/> or greater than <see cref="Int32.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <param name="date">When this method returns, contains the <see cref="DateTime"/> value equivalent to the response due date and time contained in <paramref name="value"/>, if the conversion succeeded, or <see cref="DateTime.MinValue"/> if the conversion failed. The conversion fails if the value is null, or does not contain a valid representation of a response due date. This parameter is passed uninitialized.</param>
        /// <returns>true if <paramref name="value"/> was parsed successfully; otherwise, false.</returns>
        private bool TryParseReviewNumberListItemValue(string value, out byte number, out DateTime date)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int delimIndex = value.IndexOf(ReviewItemDelimiter);
                    if (delimIndex == value.LastIndexOf(ReviewItemDelimiter))
                    {
                        string s = value.Substring(0, delimIndex);
                        if (!string.IsNullOrEmpty(s) && byte.TryParse(s, out number))
                        {
                            s = value.Substring(delimIndex + 1);
                            if (!string.IsNullOrEmpty(s) && DateTime.TryParse(s, out date))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            number = byte.MinValue;
            date = DateTime.MinValue;
            return false;
        }

        /// <summary>
        /// Retrieves the selected extension type.
        /// </summary>
        /// <returns>The currently selected extension type if valid; otherwise, null.</returns>
        /// <exception cref="InvalidOperationException">The selected extension type is invalid.</exception>
        private ExtensionType? GetSelectedExtensionType()
        {
            string value = _types.SelectedValue;
            if (string.IsNullOrEmpty(value))
                return null;
            if (!Enum.IsDefined(typeof(ExtensionType), value))
                throw new InvalidOperationException("Selected extension type cannot be null.");
            return CPConvert.ParseEnum<ExtensionType>(value);
        }

        /// <summary>
        /// Clears and hides the audit response number label and dropdown list.
        /// </summary>
        private void ResetResponseNumberDisplay()
        {
            _numberLabelText.Text = null;
            _numberDropDown.Items.Clear();
            _numberRow.Visible = false;
        }

        /// <summary>
        /// Clears and hides the extension due date label and dropdown list.
        /// </summary>
        private void ResetDueDateDisplay()
        {
            _dueDateDropDown.Items.Clear();
            _dueDateRow.Visible = false;
        }

        /// <summary>
        /// Sets whether or not to display the page in confirmation mode.
        /// </summary>
        /// <param name="isConfirmation">Whether or not the page is a confirmation page.</param>
        private void SetConfirmationMode(bool isConfirmation)
        {
            _confirmText.Visible = _typesValue.Visible = _numberValue.Visible = _dueDateValue.Visible = _reasonValue.Visible = _modify.Visible = _cancel.Visible = _reasonLabelText.Visible = isConfirmation;
            _introText.Visible = _types.Visible = _numberDropDown.Visible = _dueDateDropDown.Visible = _hiddenSubmit.Visible = _reason.Visible = _reasonIntroText.Visible = !isConfirmation;
        }

        protected override bool HasData()
        {
            return false;
        }
    }
}
