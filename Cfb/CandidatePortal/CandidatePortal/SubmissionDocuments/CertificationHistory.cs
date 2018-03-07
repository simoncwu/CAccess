using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's certification document history.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the certification and not the ceritification itself. Therefore, it will not contain any data actually submitted in a certification form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CertificationHistory : SubmissionHistory<CertificationDocument>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="CertificationDocument"/> records.
		/// </summary>
		public CertificationHistory()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="CertificationDocument"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of certification documents that the collection can initially store.</param>
		/// </summary>
		public CertificationHistory(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Retrieves a generic collection of all certification documents on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose certification documents are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all certification documents on record for the specified candidate and election cycle.</returns>
		public static CertificationHistory GetCertificationDocuments(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetCertificationDocuments(candidateID, electionCycle);
		}
	}
}
