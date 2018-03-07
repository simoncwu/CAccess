using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cfb.CandidatePortal.Web.WebApplication.Profile
{
    public partial class FinanceSummary : CPPage, ISecurePage
    {
        protected bool _isTIE = false;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (!Page.IsPostBack)
            {
                Election ec = CPApplication.Elections[CPProfile.ElectionCycle];
                _isTIE = ec != null && ec.IsTIE;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                ActiveCandidate ac = CPProfile.ActiveCandidate;
                if (ac != null)
                {
                    // general candidate information
                    this.CandidateName.Text = string.Format("{0} (ID: {1})", ac.Name, ac.ID);
                    if (ac.Office != null)
                        this.OfficeLabel.Text = ac.Office.ToString();
                    this.ClassificationLabel.Text = CPConvert.ToString(ac.Classification);
                    Election ec = CPApplication.Elections[CPProfile.ElectionCycle];
                    if (ec != null)
                    {
                        FinancialSummary fs = FinancialSummary.GetFinancialSummary(CPProfile.Cid, CPProfile.ElectionCycle);
                        if (fs != null)
                        {
                            Statement lastStatement;
                            if (ec.Statements.TryGetValue(fs.LastStatementSubmitted, out lastStatement))
                                // receipts
                                this.NetContributionsLabel.Text = FormatCurrency(fs.NetContributions);
                            this.NumberOfContributorsLabel.Text = string.Format("{0:N0}", fs.ContributorCount);
                            this.MiscellaneousReceiptsLabel.Text = FormatCurrency(fs.MiscellaneousReceipts);
                            this.MatchingClaimsLabel.Text = FormatCurrency(fs.MatchingClaims);
                            this.LoansReceivedLabel.Text = FormatCurrency(fs.LoansReceived);
                            // disbursements
                            this.NetExpendituresLabel.Text = FormatCurrency(fs.NetExpenditures);
                            this.LoansPaidLabel.Text = FormatCurrency(fs.LoansPaid);
                            this.OustandingBillsLabel.Text = FormatCurrency(fs.OutstandingBills);
                            if (!_isTIE)
                            {
                                this.PublicFundsReceivedLabel.Text = FormatCurrency(fs.PublicFundsReceived);
                                this.PublicFundsReturnedLabel.Text = FormatCurrency(fs.PublicFundsReturned);
                            }
                        }
                        if (_isTIE)
                        {
                            FSClassificationCol.Visible = FSClassificationHeader.Visible = FSClassificationCell.Visible = false;
                            ContributorsPanel.CssClass = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Formats a monetary amount in standard currency format.
        /// </summary>
        /// <param name="amount">The monetary amount to format.</param>
        /// <returns>A string representation of the amount in standard currency format.</returns>
        string FormatCurrency(decimal amount)
        {
            return string.Format("{0:C}", amount);
        }
    }
}