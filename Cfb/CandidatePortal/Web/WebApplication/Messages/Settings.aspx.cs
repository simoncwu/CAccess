using System;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;

namespace Cfb.CandidatePortal.Web.WebApplication.Messages
{
    public partial class Settings : CPPage, ISecurePage
    {
        /// <summary>
        /// The subject of the e-mail notification to be sent upon a successful paperless change.
        /// </summary>
        private readonly string _emailSubject = CPProviders.SettingsProvider.ApplicationName + " Password Change Notification";

        /// <summary>
        /// Format for the body of paperless confirmation e-mails.
        /// </summary>
        private const string _optInBodyFormat = @"Dear {0},

This e-mail is being sent to you because you recently changed your password for your NYC Campaign Finance Board {1} account. If you did not request this change, please contact our Candidate Services Unit immediately at (212) 306-7100. You may also request a new, randomly generated password via the {1} web site at:

{2}/ResetPassword.aspx

NOTE: Please do not reply to this message, as e-mail sent to this address will not be answered.
";

        /// <summary>
        /// Command name for initiating a stop to paper delivery.
        /// </summary>
        private const string StopPaperCommandName = "StopPaper";

        /// <summary>
        /// Command name for resuming paper delivery.
        /// </summary>
        private const string ResumePaperCommandName = "ResumePaper";

        /// <summary>
        /// Command name for completing a stop to paper delivery.
        /// </summary>
        private const string StopPaperCommandArgument = "StopPaper";

        /// <summary>
        /// The current candidate's Campaign Messages Online settings.
        /// </summary>
        private CmoSettings _settings;

        /// <summary>
        /// Handles the <see cref="Control.OnLoad" /> event that occurs as an instance of the page is being created.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Response.Expires = 0;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            _settings = CmoSettings.GetSettings(CPProfile.Cid);
            _forceSelectionPanel.Visible = _settings == null;
            // determine which initial view to show depending on paperless setting
            if (!Page.IsPostBack)
            {
                if (_settings == null)
                {
                    ShowOptInStart(true);
                }
                else if (_settings.IsPaperless)
                {
                    ShowResumePaper();
                }
                else
                {
                    ShowOptInStart(false);
                }
            }
        }

        /// <summary>
        /// Configures page for resuming paper delivery.
        /// </summary>
        private void ShowResumePaper()
        {
            _stopPaperPanel.Visible = _stopButton.Visible = false;
            _resumePaperPanel.Visible = _resumeButton.Visible = true;
            _resumeButton.CommandName = ResumePaperCommandName;
        }

        /// <summary>
        /// Configures page for stopping paper delivery.
        /// </summary>
        /// <param name="showCancel">true if keeping paper delivery options should also be displayed; otherwise, false.</param>
        private void ShowOptInStart(bool showOptOut)
        {
            _stopPaperPanel.Visible = _stopButton.Visible = true;
            _resumePaperPanel.Visible = false;
            _resumeButton.Visible = showOptOut;
            _stopButton.CommandName = StopPaperCommandName;
            if (showOptOut)
            {
                _resumeButton.CommandName = ResumePaperCommandName;
                _resumeButton.Text = _resumeButton.ToolTip = "Not Right Now";
            }
        }
        /// <summary>
        /// Handles paper delivery setting changes and updates page contents accordingly.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">An <see cref="CommandEventArgs" /> that contains the event data.</param>
        protected void SubmitChange(object sender, CommandEventArgs e)
        {
            if (Page.IsPostBack)
            {
                // check what action was just requested
                switch (e.CommandName)
                {
                    case StopPaperCommandName:
                        switch (e.CommandArgument as string)
                        {
                            case StopPaperCommandArgument:
                                // check for verification options and stop paper
                                if (Page.IsValid)
                                {
                                    bool success = false;
                                    if (_settings == null)
                                    {
                                        _settings = CmoSettings.Add(CPProfile.Cid, true, User.Identity.Name);
                                        success = _settings != null;
                                    }
                                    else
                                    {
                                        _settings.IsPaperless = true;
                                        _settings.UpdaterUserName = User.Identity.Name;
                                        success = _settings.Update();
                                    }
                                    if (success)
                                        ShowPaperStopped();
                                    else
                                        this.ShowSaveError();
                                }
                                else
                                {
                                    _validationSummary.Visible = true;
                                }
                                break;
                            default:
                                // show verification options
                                ShowStopPaperVerification();
                                break;
                        }
                        break;
                    case ResumePaperCommandName:
                        // resume paper
                        if (_settings == null)
                        {
                            _settings = CmoSettings.Add(CPProfile.Cid, false, User.Identity.Name);
                            if (_settings != null)
                                ShowDeferComplete();
                            else
                                this.ShowSaveError();
                        }
                        else
                        {
                            _settings.IsPaperless = false;
                            _settings.UpdaterUserName = User.Identity.Name;
                            if (_settings.Update())
                                ShowPaperResumed();
                            else
                                this.ShowSaveError();
                        }
                        break;
                }
                // TODO: send confirmation e-mail
                CPMailMessage message = new CPMailMessage();
                MailAddress email = CPProfile.GetMailAddress();
                message.Recipient = email;
                message.Subject = _emailSubject;
                message.Body = string.Format(_optInBodyFormat, email.DisplayName, CPProviders.SettingsProvider.ApplicationName, CPApplication.SiteUrl);
                //message.Send();
            }
        }

        /// <summary>
        /// Configures page to show a verification prompt for a request to stop paper delivery.
        /// </summary>
        private void ShowStopPaperVerification()
        {
            _stopPaperPanel.Visible = _resumePaperPanel.Visible = false;
            _confirmStopPaperanel.Visible = true;
            _stopButton.CommandArgument = StopPaperCommandArgument;
        }

        /// <summary>
        /// Configures page to show a confirmation for a request to stop paper delivery.
        /// </summary>
        private void ShowPaperStopped()
        {
            _changePanel.Visible = false;
            _paperStoppedPanel.Visible = _returnLink.Visible = true;
            _returnLink.NavigateUrl = "Default.aspx";
            _returnLink.Text = _returnLink.ToolTip = "View " + Resources.CPResources.cmo_inbox_title;
            if (Request.IsPitStop())
            {
                _returnLink.NavigateUrl = Request.GetReturnUrl();
                _returnLink.Text = _returnLink.ToolTip = "Continue";
            }
        }

        /// <summary>
        /// Configures page to show a confirmation for a Paperless Campaign deferral.
        /// </summary>
        private void ShowDeferComplete()
        {
            _changePanel.Visible = false;
            _deferPanel.Visible = _returnLink.Visible = true;
            if (Request.IsPitStop())
            {
                _returnLink.NavigateUrl = Request.GetReturnUrl();
                _returnLink.Text = _returnLink.ToolTip = "Continue";
            }
        }

        /// <summary>
        /// Configures page to show a confirmation for a request to resume paper delivery.
        /// </summary>
        private void ShowPaperResumed()
        {
            _changePanel.Visible = false;
            _paperResumedPanel.Visible = _returnLink.Visible = true;
            _returnLink.NavigateUrl = FormsAuthentication.DefaultUrl;
            _returnLink.Text = _returnLink.ToolTip = "Return to " + CPProviders.SettingsProvider.ApplicationName;
        }

        protected void ValidateCheckBox(object source, ServerValidateEventArgs args)
        {
            throw new Exception(args.Value);
        }

        private void ShowSaveError()
        {
            this.PageError = "We were unable to save your settings as requested. Please try again.";
        }
    }
}