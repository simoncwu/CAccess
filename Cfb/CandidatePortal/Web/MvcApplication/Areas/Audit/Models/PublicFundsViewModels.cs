using Cfb.CandidatePortal.Web.Mvc.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public class PublicFundsTrackingViewModel : AuditViewModelBase
    {
        public IEnumerable<PublicFundsViewModel> Payments { get; set; }

        [Display(Name = "Total Payments")]
        [DisplayFormat(DataFormatString = DataFormats.WholeNumber)]
        public int Count
        {
            get { return Payments.Count(p => p.Amount.HasValue); }
        }

        [Display(Name = "Total Paid")]
        [DisplayFormat(DataFormatString = DataFormats.Currency)]
        public decimal TotalAmount
        {
            get { return Payments.Where(p => p.Amount.HasValue).Sum(p => p.Amount.Value); }
        }
    }

    public class PublicFundsViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormats.ShortDate)]
        public DateTime? Date { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = DataFormats.Currency, NullDisplayText = "No payment issued")]
        public decimal? Amount { get; set; }

        public PaymentMethod? Method { get; set; }

        public ElectionType ElectionType { get; set; }

        public int MessageID { get; set; }
    }
}