using System;
using System.Web.UI;

namespace Cfb.CandidatePortal.Web.WebApplication.Audit
{
    public partial class PublicFunds : CPPage, ISecurePage
    {
        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                this.EnsureChildControls();
                _paymentGrid.DataSource = CPProfile.PublicFundsHistory;
                _paymentGrid.DataBind();
            }
        }

        protected override bool HasData()
        {
            return CPProfile.HasPublicFundsHistory;
        }
    }
}