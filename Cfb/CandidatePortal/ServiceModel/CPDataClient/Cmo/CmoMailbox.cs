using System;
using System.Collections.Generic;
using System.Linq;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Campaign Messages Online mailbox data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves messages from a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="view">The mailbox view type.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>A collection of <see cref="CmoMessage"/> objects for the specified mailbox sorted according to the properties of the mailbox.</returns>
		public List<CmoMessage> GetMailboxMessages(string candidateID, CmoMailboxView view, HashSet<string> elections)
		{
			using (DataClient client = new DataClient()) { return client.GetMailboxMessages(candidateID, view, elections == null ? null : elections.ToList()); }
		}

		/// <summary>
		/// Gets the total number of unread messages in a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>The total number of unread messges in the specified candidate's mailbox.</returns>
		public uint CountUnopenedMailboxMessages(string candidateID, HashSet<string> elections)
		{
			using (DataClient client = new DataClient()) { return client.CountUnopenedMailboxMessages(candidateID, elections == null ? null : elections.ToList()); }
		}
	}
}