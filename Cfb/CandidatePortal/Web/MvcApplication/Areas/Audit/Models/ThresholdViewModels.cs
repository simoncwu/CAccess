using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public class ThresholdRevisionViewModel
    {
        [Display(Name = "Office Sought")]
        public NycPublicOfficeType OfficeSought { get; set; }

        [Display(Name = "Threshold #")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber, NullDisplayText = Text.NullDisplayNA)]
        public ushort? Number { get; set; }

        [Display(Name = "Required #")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber, NullDisplayText = Text.NullDisplayNA)]
        public ushort? NumberRequired { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Threshold $")]
        [DisplayFormat(DataFormatString = DataFormats.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Required $")]
        [DisplayFormat(DataFormatString = DataFormats.Currency, NullDisplayText = Text.NullDisplayNA)]
        public decimal? AmountRequired { get; set; }

        [Display(Name = "Version")]
        public ThresholdRevisionType Version { get; set; }

        [Display(Name = "Statement")]
        public string Statement { get; set; }

        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime Date { get; set; }
    }

    public class ThresholdStatusViewModel : AuditViewModelBase
    {
        public IDictionary<byte, IEnumerable<ThresholdRevisionViewModel>> Revisions { get; set; }

        public ThresholdRevisionViewModel CurrentRevision
        {
            get
            {
                return Revisions == null || !Revisions.Any() ? null :
                    (from r in Revisions[Revisions.Max(m => m.Key)]
                     orderby r.Date descending
                     select r).FirstOrDefault();
            }
        }
    }
}