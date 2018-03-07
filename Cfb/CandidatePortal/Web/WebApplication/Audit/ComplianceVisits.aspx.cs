using System;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class ComplianceVisits : CPPage, ISecurePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var data = CPProfile.ComplianceVisits;
            _history.DataSource = data == null ? null : data.Visits;
            _history.MessageIDDataSource = CmoAuditReview.GetComplianceVisitMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle);
            _history.DataBind();
        }

        protected override bool HasData()
        {
            return CPProfile.HasComplianceVisits;
        }
    }
}