using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class PaymentPlan : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasPaymentPlan;
        }
    }
}