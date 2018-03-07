using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class FilerRegistrations : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasFilerRegistrations;
        }
    }
}