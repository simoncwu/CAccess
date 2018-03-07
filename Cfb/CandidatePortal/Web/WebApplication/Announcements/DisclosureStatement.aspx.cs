using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication
{
    public partial class Announcements_DisclosureStatement : CPPage, ISecurePage
    {
        protected override void OnPreInit(EventArgs e)
        {
            // temporarily moved to step-by-step filing day guide, until page content has been updated to reflect C-SMART web filing procedures
            Response.Redirect("http://www.nyccfb.info/PDF/FilingDayGuide.pdf", true);
        }

        /// <summary>
        /// Raises the <see mref="Load"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // parse and validate statement number
            string electionCycle = CPProfile.ElectionCycle;
            Dictionary<byte, Statement> statementDates = Statement.GetStatementDatesByElectionCycle(electionCycle);
            Statement deadline;
            byte? param = Request.GetStatementNumber();
            if (param.HasValue)
            {
                byte sn = param.Value;
                // retrieve filing date announcement and display if found, otherwise show generic template
                Announcement announcement = CPProviders.DataProvider.GetFilingDateAnnouncement(sn, electionCycle);
                if (announcement != null)
                {
                    DataBind(announcement);
                }
                else if (statementDates.TryGetValue(sn, out deadline))
                {
                    // show reminder messages and information
                    SetTitle(deadline.DueDate > DateTime.Today ? string.Format(Resources.CPResources.upcomingDS_title, sn) : string.Format(Resources.CPResources.dsnumber_title_format, sn), sn);
                    _deadlineLong.Text = deadline.DueDate.ToLongDateString();
                    _earlyBirdDate.Text = deadline.DueDate.AddDays(-3).ToLongDateString();
                }
            }
        }

        /// <summary>
        /// Binds an <see cref="Announcement" /> to the server control and all its child controls.
        /// </summary>
        /// <param name="announcement">The announcement data souce to bind to.</param>
        void DataBind(Announcement announcement)
        {
            if (announcement == null || !announcement.Approved)
                return;
            if (announcement.OverridesFilingInfoTemplate)
            {
                // override default message
                SetTitle(announcement.Title);
                _generalInfo.Visible = false;
                _override.Visible = true;
                _override.Text = Server.HtmlDecode(announcement.Body);
            }
            else
            {
                // customize message with item properties
                _description.Text = string.Format("<h2 class=\"ms-pagetitle\">{0}</h2>{1}", announcement.Title, Server.HtmlDecode(announcement.Body));
                _proceduresHeader.Visible = true;
            }

            // show attachments
            var attachmentUrls = Cfb.SharePoint.ListsClient.GetListItemAttachmentUrls("Announcements", announcement.ID.ToString());
            if (this._attachments.Visible = attachmentUrls.Count > 0)
            {
                _attachmentList.Controls.Clear();
                Literal ul = new Literal();
                ul.Text = "<ul>";
                _attachmentList.Controls.Add(ul);
                foreach (var url in attachmentUrls)
                {
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        int filenameIndex = url.LastIndexOf('/');
                        if (++filenameIndex < url.Length)
                        {
                            Literal li = new Literal();
                            li.Text = string.Format("<li><a href=\"{0}\" title=\"{1}\" target=\"_blank\">{1}</a></li>", url, url.Substring(filenameIndex));
                            _attachmentList.Controls.Add(li);
                        }
                    }
                }
                ul = new Literal();
                ul.Text = "</ul>";
                _attachmentList.Controls.Add(ul);
            }
            this._attachments.Visible = false; // disabled due to lack of use and not-yet-implemented intranet-side URL handler
        }

        /// <summary>
        /// Sets the title of the page in both the header and body sections for a specific statement.
        /// </summary>
        /// <param name="title">The desired title for the page.</param>
        /// <param name="statement">The target statement number.</param>
        void SetTitle(string title, byte? statement = null)
        {
            if (!statement.HasValue)
            {
                _headTitle.Text = _titleText.Text = title;
                return;
            }
            _headTitle.Text = _titleText.Text = string.Format(Resources.CPResources.dsnumber_title_format, statement.Value);
            _proceduresHeader.Visible = _reminderHeader.Visible = true;
            Election election = CPApplication.Elections[CPProfile.ElectionCycle];
            if (election != null)
            {
                Dictionary<byte, Statement> statements = election.Statements;
                _lastStatement.Visible = statements.Count > 0 && statements.OrderBy(s => s.Value.DueDate).Last().Key == statement;
            }
        }
    }
}