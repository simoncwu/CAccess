using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models
{
    public abstract class FilingsViewModelBase : Web.MvcApplication.Models.ITabPageViewModel
    {
        public string Area
        {
            get { return Mvc.Strings.Areas.Filings; }
        }

        public IDictionary<string, string> Controllers { get; set; }

        public string AreaName
        {
            get { return "Campaign Filings"; }
        }

        public FilingsViewModelBase()
        {
            var c = this.Controllers = new Dictionary<string, string>();
            if (CPProfile.HasFilerRegistrations)
                c.Add("Filer Registration", FilerRegistrationController.ControllerName);
            if (CPProfile.HasCsmartIdsRequests)
                c.Add("C-SMART/IDS", CsmartIDSRequestController.ControllerName);
            if (CPProfile.HasDisclosureStatements)
                c.Add("Disclosure Statement", DisclosureStatementController.ControllerName);
            if (CPProfile.HasCertifications)
                c.Add("Certification", CertificationController.ControllerName);
            if (CPProfile.HasCoibReceipts)
                c.Add("COIB Receipt", COIBController.ControllerName);
            if (CPProfile.HasPreElectionDisclosures)
                c.Add("Daily Pre-Election", DailyPreElectionController.ControllerName);
            if (CPProfile.HasStatementsOfNeed)
                c.Add("Statement of Need", StatementOfNeedController.ControllerName);
            if (CPProfile.HasTerminations)
                c.Add("Termination", TerminationController.ControllerName);
        }
    }

    public class HomeViewModel : FilingsViewModelBase
    {
    }

    public class DocumentStatusViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Received")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Version")]
        public SubmissionType SubmissionType { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Status Date")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? StatusDate { get; set; }

        [Display(Name = "Pages")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        public short PageCount { get; set; }

        [Display(Name = "Submitted By")]
        public DeliveryType DeliveryType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Postmarked On")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? PostmarkDate { get; set; }
    }

    public class DocumentStatusListViewModel : FilingsViewModelBase
    {
        public IEnumerable<DocumentStatusViewModel> Documents { get; set; }
    }

    public class PrimaryGeneralStatusListViewModel : FilingsViewModelBase
    {
        public IEnumerable<DocumentStatusViewModel> PrimaryElection { get; set; }

        public IEnumerable<DocumentStatusViewModel> GeneralElection { get; set; }
    }

    public class DisclosureStatementViewModel : DocumentStatusViewModel
    {
        public bool IsSmallCampaign { get; set; }

        public bool Deferred { get; set; }

        [Display(Name = "Submission Format")]
        public string DataFormat { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Backup Received")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? BackupReceivedDate { get; set; }

        [Display(Name = "Backup Sent By")]
        public DeliveryType? BackupDeliveryType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Postmarked On")]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? BackupPostmarkDate { get; set; }
    }

    public class DisclosureStatementListViewModel : FilingsViewModelBase
    {
        [Display(Name = "Committee")]
        public char CommitteeID { get; set; }

        [Display(Name = "Statement #")]
        public byte StatementNumber { get; set; }

        public IEnumerable<SelectListItem> Committees { get; set; }

        public IEnumerable<SelectListItem> Statements { get; set; }

        public IEnumerable<DisclosureStatementViewModel> Documents { get; set; }
    }
}