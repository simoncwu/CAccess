using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's verification of terminated candidacy document history.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TerminationHistory : SubmissionHistory<TerminationVerification>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="TerminationVerification"/> records.
		/// </summary>
		public TerminationHistory()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="TerminationVerification"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of verifications of terminated candidacy that the collection can initially store.</param>
		/// </summary>
		public TerminationHistory(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Retrieves a generic collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose verifications of terminated candidacy are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.</returns>
		public static TerminationHistory GetTerminationVerifications(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetTerminationVerifications(candidateID, electionCycle);
		}
	}
}
