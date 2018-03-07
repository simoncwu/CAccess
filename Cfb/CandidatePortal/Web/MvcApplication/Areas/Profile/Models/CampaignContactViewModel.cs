using Cfb.Cfis.CampaignContacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class CampaignStaffViewModel
    {
        [Editable(false)]
        public char CommitteeID { get; set; }

        [Editable(false)]
        public byte ID { get; set; }

        public PersonNameViewModel Name { get; set; }

        public ContactInfoViewModel ContactInfo { get; set; }

        public EmployerViewModel Employer { get; set; }

        [Display(Name = "Contact Order")]
        public ContactOrder ContactOrder { get; set; }

        [Display(Name = "Contact Type")]
        public LiaisonType? ContactType { get; set; }

        [Display(Name = "Managerial Control")]
        public bool ManagerialControl { get; set; }

        [Display(Name = "Voter Guide Liaison")]
        public bool VGLiaison { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Consultant Entity Name")]
        public string ConsultantEntityName { get; set; }
    }
}