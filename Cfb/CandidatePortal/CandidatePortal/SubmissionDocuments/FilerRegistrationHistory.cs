using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's filer registration document history.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the filer registration and not the ceritification itself. Therefore, it will not contain any data actually submitted in a filer registration form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class FilerRegistrationHistory : SubmissionHistory<FilerRegistrationDocument>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="FilerRegistrationDocument"/> records.
		/// </summary>
		public FilerRegistrationHistory()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="FilerRegistrationDocument"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of filer registration documents that the collection can initially store.</param>
		/// </summary>
		public FilerRegistrationHistory(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Retrieves a generic collection of all filer registration documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filer registration documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all filer registration documents on record for the specified candidate and election cycle.</returns>
		public static FilerRegistrationHistory GetFilerRegistrationDocuments(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetFilerRegistrationDocuments(candidateID, electionCycle);
		}
	}
}
