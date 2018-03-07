using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class CoibReceipts : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasCoibReceipts;
        }
    }
}