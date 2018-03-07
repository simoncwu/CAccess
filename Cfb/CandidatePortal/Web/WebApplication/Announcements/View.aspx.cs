using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.Web;
using Microsoft.SharePoint;

namespace Cfb.CandidatePortal.Web.WebApplication.Announcements
{
    public partial class View : CPPage, ISecurePage
    {
        /// <summary>
        /// Raises the <see mref="Load"/> event.
        /// </summary>
        /// <param name="args">An <see cref="EventArgs"/> object that contains the event data.</param>
        /// <exception cref="HttpException">Thrown if the requested list item could not be found.</exception>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack || !this.EnableViewState)
            {
                DataBind(CPProviders.DataProvider.GetAnnouncement(Request.QueryString["ID"]));
            }
        }

        /// <summary>
        /// Binds an <see cref="SPItem" /> to the server control and all its child controls.
        /// </summary>
        /// <param name="announcement">The announcement data source to bind to.</param>
        private void DataBind(Announcement announcement)
        {
            if (announcement == null || !announcement.Approved)
                ThrowNotFoundException();
            this._title.Text = announcement.Title;
            this._description.Text = Server.HtmlDecode(announcement.Body);
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
        /// Throws an exception indicating that the requested announcement was not found.
        /// </summary>
        /// <exception cref="HttpException">Thrown to indicate taht the requested announcement was not found.</exception>
        private void ThrowNotFoundException()
        {
            throw new HttpException(404, Resources.CPResources.annNotFound_text);
        }
    }
}