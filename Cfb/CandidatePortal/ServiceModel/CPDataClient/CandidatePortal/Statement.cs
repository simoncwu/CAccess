using System;
using System.Collections.Generic;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to statement data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a collection of statement dates for an election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>A collection of statement dates for the election cycle specified, sorted and indexed by statement number.</returns>
		public Dictionary<byte, Statement> GetStatements(string electionCycle)
		{
			using (DataClient client = new DataClient()) { return client.GetStatements(electionCycle); }
		}
	}
}