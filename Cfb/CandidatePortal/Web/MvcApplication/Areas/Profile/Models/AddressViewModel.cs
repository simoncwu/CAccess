using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class AddressViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Address Line 1")]
        public string FirstLine { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Building #")]
        public string StreetNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string StreetName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Apt #")]
        public string Apartment { get; set; }

        [DataType(DataType.Text)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string StateCode { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        public bool HasApartment { get; set; }

        public bool HasSecondLine { get; set; }
    }
}