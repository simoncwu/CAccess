using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class DisclosureStatements : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasDisclosureStatements;
        }
    }
}