using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to funds payment data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a history of public funds determinations for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose public funds determination history is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A history of public funds determinations for the specified candidate and election cycle.</returns>
		public PublicFundsHistory GetPublicFundsHistory(string candidateID, string electionCycle)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetPublicFundsHistory(candidateID, electionCycle);
			}
		}
	}
}
