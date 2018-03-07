using Cfb.CandidatePortal.Web.Mvc.Strings;
using Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Controllers;
using Cfb.CandidatePortal.Web.MvcApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public abstract class ProfileViewModelBase : ITabPageViewModel
    {
        public string Area
        {
            get { return Mvc.Strings.Areas.Profile; }
        }

        public IDictionary<string, string> Controllers { get; set; }

        public string AreaName
        {
            get { return Mvc.Strings.AreaNames.Profile; }
        }

        public ProfileViewModelBase()
        {
            this.Controllers = new Dictionary<string, string> {
                { "Candidate Profile", CandidateController.ControllerName },
                { "Committee Profile", CommitteeController.ControllerName },
                { "Financial Summary", FinancialSummaryController.ControllerName },
                { "Campaign Training", "Training" }
            };
        }
    }

    public class CandidateProfileViewModel : ProfileViewModelBase
    {
        #region Candidate Information

        [DataType(DataType.Text)]
        [Display(Name = "CFB ID")]
        [Editable(false)]
        public string ID { get; set; }

        public PersonNameViewModel Name { get; set; }

        [Display(Name = "Candidate Name")]
        [Editable(false)]
        public string FullName { get; set; }

        public ContactInfoViewModel ContactInfo { get; set; }

        public EmployerViewModel Employer { get; set; }

        #endregion

        #region Activation Information

        [Display(Name = "CFB Classification")]
        public CfbClassification Classification { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Registered Political Party")]
        public string PoliticalParty { get; set; }

        [Display(Name = "Office Sought")]
        public NycPublicOfficeType OfficeSought { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Office Sought")]
        [Editable(false)]
        public string OfficeSoughtDetails { get; set; }

        [Display(Name = "Borough")]
        public NycBorough Borough { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "District")]
        public byte District { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Filer Registration Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate, NullDisplayText = "(unregistered)")]
        [Editable(false)]
        public DateTime? RegistrationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Certifcation Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate, NullDisplayText = "(uncertified)")]
        [Editable(false)]
        public DateTime? CertificationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Termination Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate, NullDisplayText = "(n/a)")]
        [Editable(false)]
        public DateTime? TerminationDate { get; set; }

        [Display(Name = "Public Funds Direct Deposit")]
        public bool DDAuth { get; set; }

        [Display(Name = "Runoff/Rerun Direct Deposit")]
        public bool RRAuth { get; set; }

        public bool HasRRAccounts { get; set; }

        #endregion

        [Display(Name = "Principal Committee")]
        [Editable(false)]
        public string PrincipalCommittee { get; set; }

        [Display(Name = "CSU Liaison")]
        [Editable(false)]
        public string CsuLiaison { get; set; }

        [Display(Name = "Audit Liaison")]
        [Editable(false)]
        public string Auditor { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsTie { get; set; }
    }

    public class FinancialSummaryViewModel : ProfileViewModelBase
    {
        public string ElectionCycle { get; set; }

        public bool IsTie { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Candidate")]
        public string CandidateName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Office Sought")]
        public string OfficeSought { get; set; }

        [Display(Name = "CFB Classification")]
        public CfbClassification Classification { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Filed Statement")]
        [DisplayFormat(NullDisplayText = "Has not submitted yet")]
        public string LastFiledStatement { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Net Contributions")]
        public decimal NetContributions { get; set; }

        [Display(Name = "Miscellaneous Receipts")]
        [DataType(DataType.Currency)]
        public decimal MiscReceipts { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Loans Received")]
        public decimal LoansReceived { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Public Funds Received")]
        public decimal PublicFundsReceived { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Net Expenditures")]
        public decimal NetExpenditures { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Loans Paid")]
        public decimal LoansPaid { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Outstanding Bills")]
        public decimal OutstandingBills { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Public Funds Returned")]
        public decimal FundsReturned { get; set; }

        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        [Display(Name = "Number of Contributors")]
        public int ContributorsNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Matching Claims")]
        public decimal MatchingClaims { get; set; }
    }
}