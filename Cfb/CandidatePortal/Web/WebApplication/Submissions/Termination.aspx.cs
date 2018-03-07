using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class Termination : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasTerminations;
        }
    }
}