using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Cfb.CandidatePortal.Web;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.SharePoint.WebParts
{
    /// <overview>
    /// A web part for displaying the details of a candidate's payment plan.
    /// </overview>
    public class PaymentPlanWebPart : WebPart
    {
        /// <overview>
        /// Top-level container for all sub-controls.
        /// </overview>
        private Panel _container;

        /// <overview>
        /// The candidate's payment plan.
        /// </overview>
        private PaymentPlan _plan;

        /// <overview>
        /// Initializes the web part.
        /// </overview>
        public PaymentPlanWebPart()
        {
            this.ExportMode = WebPartExportMode.All;
            this.EnableViewState = false;
        }

        #region WebPart Member Methods

        /// <overview>
        /// Sends server control content to a provided <see cref="HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </overview>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            if (_plan != null)
                _container.RenderControl(writer);
            else
                writer.Write(string.Format(Properties.Resources.EmptyDataTextTemplate, CPProfile.ElectionCycle));
        }

        /// <overview>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </overview>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            _plan = PaymentPlan.GetPaymentPlan(CPProfile.Cid, CPProfile.ElectionCycle);
            if (_plan == null)
                return;
            CultureInfo enCulture = CultureInfo.CreateSpecificCulture("en-US");
            Table table;
            bool altColor;
            PaymentInstallment current = _plan.Schedule.Current;

            // overview
            // overview: next payment
            Panel overview = MakeSectionPanel(Properties.Resources.OverviewTitle);
            overview.CssClass += " cp-PaymentPlanOverviewPanel";
            table = MakeSectionTable();
            table.CssClass += " cp-leftcolumn";
            overview.Controls.Add(table);
            table.Rows.Add(MakeLabeledDataRow(
                "Next due date",
                (current == null) ? "(n/a)" : current.DueDate.ToString(Properties.Resources.DateFormat)
            ));
            table.Rows.Add(MakeLabeledDataRow(
                "Payment amount:",
                (current == null) ? "(n/a)" : current.AmountDue.ToString("C", enCulture)
            ));
            // overview: plan dates
            table = MakeSectionTable();
            table.CssClass += " cp-leftcolumn";
            overview.Controls.Add(table);
            table.Rows.Add(MakeLabeledDataRow(
                "Initial payment:",
                _plan.FirstPaymentDate.ToString(Properties.Resources.DateFormat)
            ));
            table.Rows.Add(MakeLabeledDataRow(
                "Final payment:",
                _plan.FinalPaymentDueDate.ToString(Properties.Resources.DateFormat)
            ));
            // overview: totals
            table = MakeSectionTable();
            overview.Controls.Add(table);
            table.Rows.Add(MakeLabeledDataRow(
                "Total amount:",
                _plan.Total.ToString("C", enCulture)
            ));
            table.Rows.Add(MakeLabeledDataRow(
                "Amount outstanding:",
                _plan.GetBalance().ToString("C", enCulture)
            ));
            if (_plan.PastDueBalance > 0)
            {
                table.Rows.Add(MakeLabeledDataRow("Balance past due:", _plan.PastDueBalance.ToString("C", enCulture)));
            }

            // schedule
            Panel schedule = MakeSectionPanel(Properties.Resources.ScheduleTitle);
            schedule.CssClass += " cp-PaymentPlanSchedulePanel";
            table = MakeSectionTable();
            schedule.Controls.Add(table);
            table.Rows.Add(MakeHeaderRow("Payment Schedule", "Start Date", "End Date"));
            foreach (PaymentPlanSummary summary in _plan.Summaries)
            {
                table.Rows.Add(MakeDataRow(
                    string.Format("{0} {1} {2} of {3}", summary.PaymentCount, CPConvert.ToString(_plan.Period),
                    (summary.PaymentCount > 0) ? "payments" : "payment",
                    summary.PeriodPaymentAmount.ToString("C", enCulture)), summary.FirstPaymentDate.ToString(Properties.Resources.DateFormat), summary.FinalPaymentDueDate.ToString(Properties.Resources.DateFormat)
                ));
            }

            // balances
            Panel balances = MakeSectionPanel(Properties.Resources.BalancesTitle);
            balances.CssClass += " cp-PaymentPlanBalancesPanel";
            table = MakeSectionTable();
            balances.Controls.Add(table);
            table.Rows.Add(MakeHeaderRow("Payment #", "Due Date", "Amount Due", "Amount Paid"));
            altColor = false;
            foreach (PaymentInstallment installment in _plan.Installments)
            {
                uint? amountPaid = installment.AmountPaid;
                table.Rows.Add(MakeDataRow(altColor,
                    installment.Number.ToString(),
                    installment.DueDate.ToString(Properties.Resources.DateFormat),
                    installment.AmountDue.ToString("C", enCulture),
                    (amountPaid == null) ? "--" : ((uint)amountPaid).ToString("C", enCulture)
                ));
            }

            // history
            Panel history = MakeSectionPanel(Properties.Resources.HistoryTitle);
            history.CssClass += " cp-PaymentPlanHistoryPanel";
            table = MakeSectionTable();
            history.Controls.Add(table);
            table.Rows.Add(MakeHeaderRow("Date", "Amount"));
            altColor = false;
            foreach (PlanPayment payment in _plan.Payments)
            {
                table.Rows.Add(MakeDataRow(altColor,
                    payment.Date.ToString(Properties.Resources.DateFormat),
                    payment.Amount.ToString("C", enCulture)
                ));
                altColor = !altColor;
            }

            _container = new Panel();
            _container.CssClass = "PaymentPlanWebPart";
            _container.Controls.Add(overview);
            _container.Controls.Add(schedule);
            balances.CssClass += " cp-leftcolumn";
            _container.Controls.Add(balances);
            _container.Controls.Add(history);
            this.Controls.Add(_container);
        }

        #endregion

        #region table element builder methods

        /// <summary>
        /// Makes a CSS-classed section with a title.
        /// </summary>
        /// <param name="title">The section title.</param>
        /// <returns>A titled, CSS-classed <see cref="Panel"/>.</returns>
        private Panel MakeSectionPanel(string title)
        {
            Panel p = new Panel();
            p.CssClass = "cp-detailsection";
            Literal html = new Literal();
            html.Text = string.Format("<h2>{0}</h2>", title);
            p.Controls.Add(html);
            return p;
        }

        /// <summary>
        /// Makes a table header row with the provided headers.
        /// </summary>
        /// <param name="headers">The names of the headers desired, in left-to-right order.</param>
        /// <returns>A <see cref="TableHeaderRow"/> containing the headers specified by <paramref name="headers"/>.</returns>
        private TableHeaderRow MakeHeaderRow(params string[] headers)
        {
            TableHeaderRow row = new TableHeaderRow();
            foreach (string header in headers)
            {
                TableHeaderCell cell = new TableHeaderCell();
                cell.Text = header;
                row.Cells.Add(cell);
            }
            return row;
        }

        /// <summary>
        /// Makes a table row containing data.
        /// </summary>
        /// <param name="values">A <see cref="String"/> array of the data values for the row, in left-to-right order.</param>
        /// <returns>A <see cref="TableRow"/> containing the values specified by <paramref name="values"/>.</returns>
        private TableRow MakeDataRow(params string[] values)
        {
            return MakeDataRow(false, values);
        }

        /// <summary>
        /// Makes a table row containing data with support for alternate row colors.
        /// </summary>
        /// <param name="useAltColor">Whether or not the row will be CSS-classed for an alternate color.</param>
        /// <param name="values">A <see cref="String"/> array of data values for the row, in left-to-right order.</param>
        /// <returns>A <see cref="TableRow"/> containting the values specified and CSS-classed for an alternate color, if so indicated.</returns>
        private TableRow MakeDataRow(bool useAltColor, params string[] values)
        {
            TableRow row = new TableRow();
            if (useAltColor)
                row.CssClass = "cp-altcolor";
            foreach (string value in values)
            {
                TableCell cell = new TableCell();
                cell = MakeDataCell(value);
                row.Cells.Add(cell);
            }
            return row;
        }

        /// <summary>
        /// Makes a table row containing a label cell and a data cell.
        /// </summary>
        /// <param name="label">The label value.</param>
        /// <param name="data">The data value.</param>
        /// <returns>A <see cref="TableRow"/> containing the specified label and data.</returns>
        private TableRow MakeLabeledDataRow(string label, string data)
        {
            TableRow row = new TableRow();
            row.Cells.Add(MakeLabelCell(label));
            row.Cells.Add(MakeDataCell(data));
            return row;
        }

        /// <summary>
        /// Makes a CSS-classed label cell.
        /// </summary>
        /// <param name="label">The label value.</param>
        /// <returns>A prepopulated, CSS-classed label cell.</returns>
        private TableCell MakeLabelCell(string label)
        {
            return MakeCssCell("cp-infolabel", label);
        }

        /// <summary>
        /// Makes a CSS-classed data cell.
        /// </summary>
        /// <param name="data">The data value.</param>
        /// <returns>A prepopulated, CSS-classed data cell.</returns>
        private TableCell MakeDataCell(string data)
        {
            return MakeCssCell("cp-infodata", data);
        }

        /// <summary>
        /// Makes a CSS-classed table for placement within a section.
        /// </summary>
        /// <returns>A CSS-classed <see cref="Table"/>.</returns>
        private Table MakeSectionTable()
        {
            Table table = new Table();
            table.CssClass = "cp-sectiontable";
            return table;
        }

        /// <summary>
        /// Makes a CSS-classed cell containing specific text.
        /// </summary>
        /// <param name="cssClass">The desired CSS class of the cell.</param>
        /// <param name="text">The desired cell text.</param>
        /// <returns>A prepopulated, CSS-classed <see cref="TableCell"/>.</returns>
        private TableCell MakeCssCell(string cssClass, string text)
        {
            TableCell cell = new TableCell();
            cell.CssClass = cssClass;
            cell.Text = text;
            return cell;
        }

        #endregion
    }
}
