using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class EmployerViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Employer Name")]
        public string Name { get; set; }

        public ContactInfoViewModel ContactInfo { get; set; }
    }
}