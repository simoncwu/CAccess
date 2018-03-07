using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cfb.CandidatePortal.Web.MvcApplication.Controllers
{
    [Authorize]
    [RoutePrefix(MessagesController.ControllerName)]
    [Route("{action=index}")]
    public class MessagesController : Controller
    {
        public const string ControllerName = "Messages";

        public const string ActionName_Index = "Index";
        public const string ActionName_CountUnopened = "CountUnopened";
        public const string ActionName_Details = "Details";
        public const string ActionName_Update = "Update";
        public const string ActionName_Archive = "Archive";
        public const string ActionName_Unarchive = "Unarchive";
        public const string ActionName_Flag = "Flag";
        public const string ActionName_ClearFlag = "ClearFlag";
        public const string ActionName_Attachment = "Attachment";

        public const string ViewName_MessagesList = "_MessagesList";
        public const string ViewName_MessageHeader = "_MessageHeader";
        public const string ViewName_UpdateResultMessage = "_UpdateResultMessage";

        [HttpGet]
        [Route("{view}/{id?}")]
        public ActionResult Index(CmoMailboxView view, string id = null)
        {
            var mailbox = GetMailbox(view);
            MessageViewModel message = null;
            if (id != null)
            {
                message = MessagesViewModelsFactory.MessageFrom(id, mailbox); ;
                if (message == null)
                    return HttpNotFound();
                if (Request.IsAjaxRequest())
                    return PartialView("Details", message);
            }
            var model = MessagesViewModelsFactory.MessageCenterFrom(mailbox, message);
            if (Request.IsAjaxRequest())
                return PartialView("_MailboxViewPanel", model);
            return View(model);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction(ActionName_Index, new { view = CmoMailboxView.Current });
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var message = MessagesViewModelsFactory.MessageFrom(id, GetMailbox());
            if (message == null || message.ID == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(ActionName_Details, message);
            return View(message);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult CountUnopened()
        {
            return Content(CmoProviders.DataProvider.CountUnopenedMailboxMessages(CPProfile.Cid, new HashSet<string>(CPProfile.Elections.Keys)).ToString());
        }

        [HttpGet]
        [Route(ActionName_Details + "/{messageid}/" + ActionName_Attachment + "/{id}")]
        public ActionResult Attachment(string messageid, string id)
        {
            // check for valid, opened message
            var message = CmoMessage.GetMessage(messageid);
            if (message == null)
                return HttpNotFound();
            if (!message.IsOpened)
                return RedirectToAction(ActionName_Details, new { id = messageid });

            // find attachment and download
            var attachment = CmoAttachment.GetAttachment(string.Join("-", messageid, id));
            if (attachment == null)
                return HttpNotFound();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", attachment.FileName));
            return new FilePathResult("~/App_Data/SampleAttachment.pdf", "application/octet-stream")
            {
                FileDownloadName = attachment.FileName
            };
        }

        [HttpPost]
        [Route(ActionName_Update + "/{id}")]
        public ActionResult Update(string id, string updateAction)
        {
            bool success = UpdateMessage(id, updateAction);
            SetUpdateResultMessage(updateAction, 1, !success);
            var message = MessagesViewModelsFactory.MessageFrom(id, GetMailbox());
            if (message == null || message.ID == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView(ViewName_MessageHeader, MessagesViewModelsFactory.MessageFrom(id, GetMailbox()));
            return Details(id);
        }

        [HttpPost]
        [Route("{view}/" + ActionName_Update)]
        public ActionResult Update(CmoMailboxView view, string updateAction, string[] message_ids)
        {
            int count = 0;
            bool error = false;

            foreach (var id in message_ids)
            {
                if (UpdateMessage(id, updateAction))
                    count++;
                else
                    error = true;
            }
            SetUpdateResultMessage(updateAction, count, error);

            if (Request.IsAjaxRequest())
                return PartialView(ViewName_MessagesList, MessagesViewModelsFactory.MessageCenterFrom(GetMailbox(view)).Messages);
            return Index(view, null);
        }

        private void SetUpdateResultMessage(string updateAction, int count, bool error)
        {
            ViewData["UpdateCount"] = count.ToString();
            if (error)
                ViewData["UpdateError"] = error;
            ViewData["UpdateAction"] = updateAction;
        }

        private bool UpdateMessage(string id, string updateAction)
        {
            var msg = CmoMessage.GetMessage(id);
            if (msg != null)
            {
                CmoMessage.MessageAction action = null;
                switch (updateAction)
                {
                    case ActionName_Archive:
                        action = msg.Archive;
                        break;
                    case ActionName_Unarchive:
                        action = msg.Unarchive;
                        break;
                    case ActionName_Flag:
                        action = msg.SetFlag;
                        break;
                    case ActionName_ClearFlag:
                        action = msg.ClearFlag;
                        break;
                }
                return action != null && action((User.Identity.Name));
            }
            return false;
        }

        private CmoMailbox GetMailbox(CmoMailboxView? view = null)
        {
            var mailbox = CmoMailbox.GetMailbox(CPProfile.Cid, CPProfile.Elections);
            mailbox.Username = User.Identity.Name;
            if (view.HasValue)
                mailbox.GetMessages(view.Value);
            return mailbox;
        }
    }
}