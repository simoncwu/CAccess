using System;

namespace Cfb.CandidatePortal.Web.WebApplication.Submissions
{
    public partial class Certifications : CPPage, ISecurePage
    {
        protected override bool HasData()
        {
            return CPProfile.HasCertifications;
        }
    }
}