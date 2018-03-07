using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public class AuditReviewTrackingViewModel : AuditViewModelBase
    {
        public string ReviewNumberName { get; set; }

        public string SentNamePrefix { get; set; }

        public IDictionary<char, string> CommiteeNames { get; set; }

        public IDictionary<char, IEnumerable<AuditReviewViewModel>> Reviews { get; set; }

        public bool IsComplianceVisits { get; set; }

        public AuditReviewTrackingViewModel()
        {
            ReviewNumberName = "Statement";
            SentNamePrefix = "Letter";
        }
    }

    public class AuditReviewViewModel
    {
        public string ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? AppointmentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? SentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = AuditViewModelBase.ResponseNotRequiredText)]
        public DateTime? ResponseDueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? OriginalDueDate { get; set; }

        public ushort ExtensionsGranted { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? ExtensionGrantedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = AuditViewModelBase.ResponseNotRequiredText)]
        public DateTime? ResponseReceivedDate { get; set; }

        public string MessageID { get; set; }
    }
}