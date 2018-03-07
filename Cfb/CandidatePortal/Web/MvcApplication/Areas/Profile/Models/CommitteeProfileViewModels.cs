using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class CommitteeProfileViewModel : ProfileViewModelBase
    {
        [DataType(DataType.Text)]
        [Display(Name = "CFB ID")]
        [Editable(false)]
        public char ID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Committee Name")]
        public string Name { get; set; }

        public ContactInfoViewModel ContactInfo { get; set; }

        [Display(Name = "Principal Committee")]
        public bool IsPrincipal { get; set; }

        [Display(Name = "Active Committee")]
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "B.O.E. Authorization Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate, NullDisplayText = "(unauthorized)")]
        public DateTime? BoeAuthDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Termination Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate, NullDisplayText = "(n/a)")]
        [Editable(false)]
        public DateTime? TerminationDate { get; set; }

        public AddressViewModel MailingAddress { get; set; }

        public CampaignStaffViewModel Treasurer { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Election")]
        public DateTime? LastElectionDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Office or Position Sought")]
        public string LastOfficeSought { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "District")]
        public string LastDistrict { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Party Primary Entered")]
        public string LastPrimaryParty { get; set; }
    }

    public class BankAccountViewModel
    {
        [Editable(false)]
        public byte ID { get; set; }

        [Editable(false)]
        public char CommitteeID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Bank/Depository Name")]
        public string BankName { get; set; }

        [DataType(DataType.Text)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string StateCode { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Account Name")]
        public string FriendlyName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Type of Account")]
        public string Type { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Purpose of Account")]
        public string Purpose { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Opening Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate)]
        public DateTime? OpenedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Closing Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate)]
        public DateTime? ClosedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Balance Date")]
        [DisplayFormat(DataFormatString = DataFormats.MediumDate)]
        public DateTime? BalanceDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [Display(Name = "Direct Deposit Public Funds")]
        public bool DirectDeposit { get; set; }
    }
}