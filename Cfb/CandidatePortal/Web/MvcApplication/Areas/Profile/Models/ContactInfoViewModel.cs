using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class ContactInfoViewModel
    {
        public bool IsEmployer { get; set; }

        public bool HasUrls { get; set; }

        public AddressViewModel Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Daytime Phone #")]
        public string DaytimePhone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Ext.")]
        public string DaytimePhoneExt { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Evening Phone #")]
        public string EveningPhone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Ext.")]
        public string EveningPhoneExt { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Fax #")]
        public string Fax { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Website Address(es)")]
        public string Urls { get; set; }
    }
}