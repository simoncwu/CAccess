using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's Conflict of Interest Board receipt history.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CoibReceiptHistory : SubmissionHistory<CoibReceipt>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="CoibReceipt"/> records.
		/// </summary>
		public CoibReceiptHistory()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="CoibReceipt"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of Conflict of Interest Board receipts that the collection can initially store.</param>
		/// </summary>
		public CoibReceiptHistory(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Retrieves a generic collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.</returns>
		public static CoibReceiptHistory GetCoibReceipts(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetCoibReceipts(candidateID, electionCycle);
		}
	}
}
