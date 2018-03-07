using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class PreElectionPage : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasPreElectionDisclosures;
        }
    }
}