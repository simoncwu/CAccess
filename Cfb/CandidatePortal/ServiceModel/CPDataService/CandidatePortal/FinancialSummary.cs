using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.FinancialSummaryTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Returns a candidate's campaign financial summary for a given election cycle.
		/// </summary>
		/// <param name="candidateID">The desired candidate's CFIS ID.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The specified candidate's campaign financial summary for the specified election cycle.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		FinancialSummary GetFinancialSummary(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Returns a candidate's campaign financial summary for a given election cycle.
		/// </summary>
		/// <param name="candidateID">The desired candidate's CFIS ID.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The specified candidate's campaign financial summary for the specified election cycle.</returns>
		public FinancialSummary GetFinancialSummary(string candidateID, string electionCycle)
		{
			using (FinancialSummaryTds ds = new FinancialSummaryTds())
			{
				using (FinancialSummaryTableAdapter ta = new FinancialSummaryTableAdapter())
				{
					ta.Fill(ds.FinancialSummary, candidateID, electionCycle);
					foreach (FinancialSummaryTds.FinancialSummaryRow row in ds.FinancialSummary.Rows)
					{
						return new FinancialSummary()
						{
							LastStatementSubmitted = Convert.ToByte(row.LastStatementSubmitted),
							ContributorCount = row.ContributorCount,
							NetContributions = row.NetContributions,
							MatchingClaims = row.MatchingClaims,
							MiscellaneousReceipts = row.OtherReceipts,
							LoansReceived = row.LoansReceived,
							PublicFundsReceived = row.PublicFundsReceived,
							PublicFundsReturned = row.PublicFundsReturned,
							NetExpenditures = row.NetExpenditures,
							LoansPaid = row.LoansPaid,
							OutstandingBills = row.OutstandingBills,
							PrivateFundsReceived = row.PrivateFundsReceived,
							CampaignSpending = row.CampaignSpending
						};
					}
				}
			}
			return null;
		}
	}
}
