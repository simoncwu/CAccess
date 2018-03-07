using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cfb.CandidatePortal.Cmo;
using Cfb.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Cfb.CandidatePortal.Web.Controls
{
	/// <summary>
	/// A control for displaying a candidate's public funds determination history (payments and non-payments).
	/// </summary>
	[ToolboxData("<{0}:PublicFundsPaymentHistoryGrid runat=server></{0}:PublicFundsPaymentHistoryGrid>")]
	public class PublicFundsPaymentHistoryGrid : WebControl
	{
		/// <summary>
		/// HTML for the header of the history table.
		/// </summary>
		private const string HeaderHtml = @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""publicFundsHistory ms-listviewtable"">
	<tr class=""ms-viewheadertr"">
		<th class=""date"">
			<div>Payment Date</div>
		</th>
		<th class=""amount"">
			<div>Amount</div>
		</th>
		<th class=""payMethod"">
			<div>Method</div>
		</th>
		<th class=""electionType"">
			<div>Election</div>
		</th>
		<th class=""cp-DownloadLink"">
		</th>
	</tr>";

		/// <summary>
		/// HTML format template for a payment record.
		/// </summary>
		private const string PaymentRowHtmlTemplate = @"<tr class=""payment"">
		<td class=""date"">
			<div>{0:d}</div>
		</td>
		<td class=""amount"">
			<div>{1:C}</div>
		</td>
		<td class=""payMethod"">
			<div>{2}</div>
		</td>
		<td class=""electionType"">
			<div>{3}</div>
		</td>
		<td class=""cp-DownloadLink"">
			<div>{4}</div>
		</td>
	</tr>";

		/// <summary>
		/// HTML format template for a non-payment record.
		/// </summary>
		private const string NonPaymentRowHtmlTemplate = @"<tr class=""nonPayment"">
		<td class=""date"">
			<div>{0:d}</div>
		</td>
		<td class=""nonPayment amount"">
			<div>Payment Not Issued</div>
		</td>
		<td class=""payMethod"">
			<div></div>
		</td>
		<td class=""electionType"">
			<div>{1}</div>
		</td>
		<td class=""cp-DownloadLink"">
			<div>{2}</div>
		</td>
	</tr>";

		/// <summary>
		/// HTML format temlate for the footer of the history table.
		/// </summary>
		private const string FooterHtmlTemplate = @"</table>
<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""publicFundsSummary ms-listviewtable"">
	<tr><th>Total Number of Payments</th><td>{0}</td></tr>
	<tr><th>Total Amount Paid</th><td>{1:C}</td></tr>
</table>";

		/// <summary>
		/// The source public funds history to display.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Description("The source public funds payment history.")]
		public PublicFundsHistory DataSource { get; set; }

		/// <summary>
		/// Gets or sets the text to display when there is no public funds history data.
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[Description("The text to display when there is no public funds history data.")]
		public string EmptyDataText { get; set; }

		/// <summary>
		/// A collection of HTML strings for rendering each payment/non-payment in the source public funds history.
		/// </summary>
		private StringCollection _rowsArray;

		/// <summary>
		/// The HTML for the table footer.
		/// </summary>
		private string _footerHtml;

		/// <summary>
		/// Initializes a new instance of the <see cref="PublicFundsPaymentHistoryGrid"/> control.
		/// </summary>
		public PublicFundsPaymentHistoryGrid()
		{
			_rowsArray = new StringCollection();
		}

		/// <summary>
		/// Raises the <see cref="Control.DataBinding"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected override void OnDataBinding(EventArgs e)
		{
			_rowsArray.Clear();
			string cid = CPProfile.Cid;

			// generate HTML for each public funds determination record
			foreach (PublicFundsDetermination item in this.DataSource)
			{
				string downloadLink = item.MessageID > 0 ? string.Format("<a class=\"pdf-file\" href=\"{0}\" title=\"{1}\">{1}</a>", PageUrlManager.GetMessageUrl(CmoMessage.ToUniqueID(cid, item.MessageID)), "View Letter") : null;
				if (item.PaymentIssued)
					_rowsArray.Add(string.Format(PaymentRowHtmlTemplate, item.Date, item.PaymentAmount, CPConvert.ToString(item.PaymentMethod), CPConvert.ToString(item.ElectionType), downloadLink));
				else
					_rowsArray.Add(string.Format(NonPaymentRowHtmlTemplate, item.Date, CPConvert.ToString(item.ElectionType), downloadLink));
			}

			// generate HTML for public funds summary
			_footerHtml = string.Format(FooterHtmlTemplate, this.DataSource.PaymentCount, this.DataSource.TotalPaymentAmount);
		}

		/// <summary>
		/// Renders the HTML opening tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("div");
			writer.WriteAttribute("class", "cp-FundsPaymentHistoryGrid");
			writer.Write(HtmlTextWriter.TagRightChar);
		}

		/// <summary>
		/// Renders the HTML closing tag of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.WriteEndTag("div");
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer.
		/// </summary>
		/// <param name="writer">A <see cref="HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.DataSource == null || this.DataSource.Determinations.Count == 0)
			{
				// empty history
				writer.Write(this.EmptyDataText);
			}
			else
			{
				writer.WriteLine(HeaderHtml);
				foreach (string rowHtml in _rowsArray)
				{
					writer.WriteLine(rowHtml);
				}
				writer.WriteLine(_footerHtml);
			}
		}
	}
}
