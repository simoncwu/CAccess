using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public class PostElectionViewModel : AuditViewModelBase
    {
        public AuditReportType Stage { get; set; }

        public string AgencyPhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Original Deadline")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = Text.NullDisplayNA)]
        public DateTime? DarOriginalDeadline { get; set; }

        [Display(Name = "DAR Tolling Days Incurred")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        public int DarTollingDays { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "DAR Target Deadline")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = Text.NullDisplayNA)]
        public DateTime? DarTargetDeadline { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Original Deadline")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = Text.NullDisplayNA)]
        public DateTime? FarOriginalDeadline { get; set; }

        [Display(Name = "FAR Tolling Days Incurred")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        public int FarTollingDays { get; set; }

        [Display(Name = "Total Tolling Days")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        public int TotalTollingDays { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "FAR Target Deadline")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = Text.NullDisplayNA)]
        public DateTime? FarTargetDeadline { get; set; }

        public bool HasTrainingCompliance { get; set; }

        public bool Suspended { get; set; }

        [Display(Name = "Current Status")]
        public PostElectionStatus CurrentStatus { get; set; }
    }

    public class PostElectionResponseViewModel
    {
        public string ReportTypeText { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = "Not Yet Issued")]
        public DateTime? SentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? SecondSentDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Response Due")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Response Received")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = "Not Received")]
        public DateTime? ReceivedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Response Postmarked")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? PostmarkedDate { get; set; }

        public string MessageID { get; set; }

        public bool Overdue { get; set; }

        public AuditReportType ReportType { get; set; }

        public bool IsInadequate { get; set; }

        public bool HasNoFindings { get; set; }
    }

    public class PostElectionSummaryViewModel
    {
        public PostElectionResponseViewModel ReportResponse { get; set; }

        public PostElectionResponseViewModel InadequateResponse { get; set; }
    }

    public enum PostElectionStatus : byte
    {
        Unknown,

        Suspended,

        [Description("Final Audit Report issued&mdash;Post Election Audit complete")]
        FinalAuditComplete,

        [Description("Campaign&rsquo;s response under CFB review")]
        UnderReview,

        [Description("Awaiting campaign&rsquo;s reply to the Draft Audit Report inadequate response notice")]
        PendingDarInadequate,

        [Description("Awaiting campaign&rsquo;s reply to the Draft Audit Report")]
        PendingDar,

        [Description("Awaiting campaign&rsquo;s reply to the Request for Documentation inadequate response notice")]
        PendingIdrInadequate,

        [Description("Awaiting campaign&rsquo;s reply to the Request for Documentation")]
        PendingIdr
    }

    public class TollingEventViewModel
    {
        public string MessageID { get; set; }

        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate, NullDisplayText = "(ongoing)")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "End Reason")]
        public TollingEndReason EndReason { get; set; }

        [Display(Name = "# of Days")]
        public int TollingDays { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? DueDate { get; set; }
    }

    public class TollingEventsListViewModel
    {
        [DisplayFormat(NullDisplayText = "All")]
        public AuditReportType? ReportType { get; set; }

        public IEnumerable<TollingEventViewModel> Events { get; set; }
    }
}