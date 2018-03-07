using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class StatementsOfNeed : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasStatementsOfNeed;
        }
    }
}