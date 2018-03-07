using System.Collections.Generic;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to bank account data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves all bank accounts on record for the specified candidate, election cycle, and committee.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose bank accounts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="committeeID">The ID of the committee whose bank accounts are to be retrieved.</param>
		/// <returns>A collection of all bank accounts on record for the specified candidate, election cycle, and committee, indexed by bank account ID.</returns>
		public Dictionary<byte, BankAccount> GetBankAccountsByCommittee(string candidateID, string electionCycle, char committeeID)
		{
			using (DataClient client = new DataClient()) { return client.GetBankAccounts(candidateID, electionCycle, committeeID); }
		}
	}
}