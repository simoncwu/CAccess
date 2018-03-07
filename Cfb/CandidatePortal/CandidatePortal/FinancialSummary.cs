using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a candidate's campaign financial activity summary to date for a specific election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class FinancialSummary
	{
		/// <summary>
		/// Gets or sets the last statement submitted by the candidate for a specific election cycle.
		/// </summary>
		[DataMember]
		public byte LastStatementSubmitted { get; set; }

		/// <summary>
		/// Gets or sets the candidate's total campaign spending amount to date for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal CampaignSpending { get; set; }

		/// <summary>
		/// Gets or sets the total number of contributors to the candidate's campaign to date for a specific election cycle.
		/// </summary>
		[DataMember]
		public int ContributorCount { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of loans paid to date by the candidate for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal LoansPaid { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of loans received to date by the candidate for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal LoansReceived { get; set; }

		/// <summary>
		/// Gets or sets the amount of contributions received to date that qualify for matching claims for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal MatchingClaims { get; set; }

		/// <summary>
		/// Gets or sets the net amount of contributions received to date for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal NetContributions { get; set; }

		/// <summary>
		/// Gets or sets the amount of expenditures spent to date for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal NetExpenditures { get; set; }

		/// <summary>
		/// Gets or sets the candidate's total monetary amount of miscellaneous receipts to date for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal MiscellaneousReceipts { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of outstanding liabilities reported on the candidate's last disclosure statement for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal OutstandingBills { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of funds received to date by the candidate from private sources for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal PrivateFundsReceived { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of public funds  disbursed to date to the candidate for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal PublicFundsReceived { get; set; }

		/// <summary>
		/// Gets or sets the total monetary amount of public funds returned to date by the candidate to the CFB for a specific election cycle.
		/// </summary>
		[DataMember]
		public decimal PublicFundsReturned { get; set; }

		/// <summary>
		/// Creates a new campaign financial summary.
		/// </summary>
		internal FinancialSummary()
		{
		}

		/// <summary>
		/// Returns a candidate's campaign financial summary for a given election cycle.
		/// </summary>
		/// <param name="candidateID">The desired candidate's CFIS ID.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The specified candidate's campaign financial summary for the specified election cycle.</returns>
		public static FinancialSummary GetFinancialSummary(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetFinancialSummary(candidateID, electionCycle);
		}
	}
}
