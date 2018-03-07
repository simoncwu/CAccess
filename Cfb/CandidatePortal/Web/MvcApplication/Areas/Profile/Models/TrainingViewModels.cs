using Cfb.CandidatePortal.TrainingTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Profile.Models
{
    public class TrainingTrackingViewModel : ProfileViewModelBase
    {
        public string AgencyPhoneNumber { get; set; }

        public bool HasCsmartComplianceTrainings { get; set; }

        public bool HasAuditTrainings { get; set; }

        [Display(Name = "Training Requirement Fulfilled")]
        public bool HasCsmartCompliance { get; set; }

        [Display(Name = "Expedited Audit Achieved")]
        public bool HasAuditCompliance { get; set; }

        public IDictionary<TrainingCourseType, TrainingCourseViewModel> Courses { get; set; }
    }

    public class TrainingCourseViewModel
    {
        public TrainingCourseType CourseType { get; set; }

        public IEnumerable<TraineeViewModel> Trainees { get; set; }
    }

    public class TrainingSessionViewModel
    {
        public bool AchievesCompliance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Mvc.Strings.DataFormats.ShortDate)]
        public DateTime Date { get; set; }

        [UIHint("YesNoScheduled")]
        public bool? Attended { get; set; }

        [UIHint("YesNoScheduled")]
        public bool? Completed { get; set; }
    }

    public class TraineeViewModel
    {
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public TrainingTracking.CampaignRelationship Relationship { get; set; }

        public IEnumerable<TrainingSessionViewModel> Sessions { get; set; }
    }
}