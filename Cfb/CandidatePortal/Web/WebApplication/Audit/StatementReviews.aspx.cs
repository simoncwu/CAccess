using System;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class StatementReviews : CPPage, ISecurePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var data = CPProfile.StatementReviews;
            _history.DataSource = data == null ? null : data.Reviews;
            _history.MessageIDDataSource = CmoAuditReview.GetStatementReviewMessageIDs(CPProfile.Cid, CPProfile.ElectionCycle);
            _history.DataBind();
        }

        protected override bool HasData()
        {
            return CPProfile.HasStatementReviews;
        }
    }
}