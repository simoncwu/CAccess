using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// An analysis worksheet for a response to a post election audit initial documentation request.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class IdrResponseAnalysis
	{
		/// <summary>
		/// Gets whether or not a Post Election Initial Documentation Request response analysis worksheet exists for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>true if an IDR response analysis exists for the candidate and election cycle specified; otherwise, false.</returns>
		public static bool HasIdrResponseAnalysis(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.HasIdrResponseAnalysis(candidateID, electionCycle);
		}
	}
}
