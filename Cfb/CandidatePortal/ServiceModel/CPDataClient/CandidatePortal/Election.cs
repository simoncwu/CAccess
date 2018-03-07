using System.Collections.Generic;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to election cycle data by means of WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Retrieves a collection of all election cycles in which a candidate is active.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all election cycles on record in which the candidate is active.</returns>
		public HashSet<string> GetActiveElectionCycles(string candidateID, string cutoff)
		{
			using (DataClient client = new DataClient())
			{
				return new HashSet<string>(client.GetActiveElectionCycles(candidateID, cutoff));
			}
		}

		/// <summary>
		/// Gets a collection of all supported election cycles.
		/// </summary>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all supported election cycles.</returns>
		public Elections GetElections(string cutoff)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetElections(cutoff);
			}
		}

		/// <summary>
		/// Gets a collection of all election cycles in which a candidate is active.
		/// </summary>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
		/// <returns>A collection of all election cycles in which the candidate is active.</returns>
		public Elections GetActiveElections(string cutoff, string candidateID)
		{
			using (DataClient client = new DataClient())
			{
				return client.GetActiveElections(cutoff, candidateID);
			}
		}
	}
}