using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to Campaign Messages Online audit review data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Gets a collection of IDs for all post election audit tolling messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <param name="far">true to specify Final Audit Report tolling; otherwise, false to specify Draft Audit Report tolling.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		public Dictionary<int, string> GetTollingMessageIDs(string candidateID, string electionCycle, bool far)
		{
			using (DataClient client = new DataClient()) { return client.GetTollingMessageIDs(candidateID, electionCycle, far); }
		}
	}
}
