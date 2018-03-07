using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Models
{
    public class MessageCenterViewModel
    {
        public IDictionary<string, CmoMailboxView> Views { get; set; }

        public CmoMailboxView CurrentView { get; set; }

        public IEnumerable<MessageHeaderViewModel> Messages { get; set; }

        public MessageViewModel SelectedMessage { get; set; }
    }

    public class MessageHeaderViewModel
    {
        public string ID { get; set; }

        [Display(Name = "Opened")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDateTime)]
        public DateTime? OpenedDate { get; set; }

        [Display(Name = "Election")]
        public string ElectionCycle { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public bool HasAttachments { get; set; }

        [Display(Name = "Posted")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime Posted { get; set; }

        public bool Flagged { get; set; }
    }

    public class MessageViewModel : MessageHeaderViewModel
    {
        [Display(Name = "Archived")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDateTime)]
        public DateTime? ArchivedDate { get; set; }

        public string ArchiveUser { get; set; }

        public string OpenUser { get; set; }

        [Display(Name = "Flagged")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDateTime)]
        public DateTime? FlagDate { get; set; }

        public string FlagUser { get; set; }

        public string Body { get; set; }

        public IEnumerable<byte> Attachments { get; set; }

        public CmoMailboxView CurrentView { get; set; }

        public int CurrentIndex { get; set; }

        public int MailboxViewCount { get; set; }

        public string PreviousID { get; set; }

        public string NextID { get; set; }
    }

    public sealed class MessagesViewModelsFactory
    {
        public static MessageViewModel MessageFrom(string id, CmoMailbox mailbox)
        {
            if (string.IsNullOrWhiteSpace(id) || mailbox == null)
                return new MessageViewModel();
            string previousId, nextId;
            int messageIndex;
            var source = mailbox.OpenMessage(id, out previousId, out nextId, out messageIndex);
            if (source == null)
            {
                mailbox.View = CmoMailboxView.All;
                source = mailbox.OpenMessage(id);
            }
            return source == null || !source.IsPosted ? new MessageViewModel() : new MessageViewModel
            {
                ID = source.UniqueID,
                ElectionCycle = source.ElectionCycle,
                Title = source.Title,
                Flagged = source.NeedsFollowUp,
                HasAttachments = source.HasAttachment,
                OpenedDate = source.OpenDate,
                Posted = source.PostDate.Value,
                ArchivedDate = source.ArchiveDate,
                ArchiveUser = source.Archiver,
                FlagDate = source.FollowUpDate,
                FlagUser = source.FollowUpFlagger,
                OpenUser = source.Opener,
                Body = source.Body,
                CurrentView = mailbox.View,
                CurrentIndex = messageIndex + 1,
                MailboxViewCount = mailbox.Messages.Count,
                NextID = nextId,
                PreviousID = previousId,
                Attachments = source.Attachments.Values.Select(a => a.ID)
            };
        }

        public static MessageHeaderViewModel MessageHeaderFrom(CmoMessage source)
        {
            return source == null || !source.IsPosted ? new MessageHeaderViewModel() : new MessageHeaderViewModel
            {
                ID = source.UniqueID,
                ElectionCycle = source.ElectionCycle,
                Title = source.Title,
                Flagged = source.NeedsFollowUp,
                HasAttachments = source.HasAttachment,
                OpenedDate = source.OpenDate,
                Posted = source.PostDate.Value
            };
        }

        public static MessageCenterViewModel MessageCenterFrom(CmoMailbox source, MessageViewModel message = null)
        {
            return source == null ? new MessageCenterViewModel() : new MessageCenterViewModel
            {
                Messages = source.Messages.Select(m => MessageHeaderFrom(m)),
                CurrentView = source.View,
                Views = new Dictionary<string, CmoMailboxView>()
                {
                    { "Current", CmoMailboxView.Current },
                    { "Unopened", CmoMailboxView.Unopened },
                    { "Flagged", CmoMailboxView.FollowUp },
                    { "Archived", CmoMailboxView.Archived },
                    { "All", CmoMailboxView.All }
                },
                SelectedMessage = message
            };
        }
    }
}