using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's C-SMART/IDS request history.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CsmartIdsRequestHistory : SubmissionHistory<CsmartIdsRequest>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="CsmartIdsRequest"/> records.
		/// </summary>
		public CsmartIdsRequestHistory()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="CsmartIdsRequest"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of C-SMART/IDS requests that the collection can initially store.</param>
		/// </summary>
		public CsmartIdsRequestHistory(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Retrieves a generic collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose C-SMART/IDS requests are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.</returns>
		public static CsmartIdsRequestHistory GetCsmartIdsRequests(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetCsmartIdsRequests(candidateID, electionCycle);
		}
	}
}
