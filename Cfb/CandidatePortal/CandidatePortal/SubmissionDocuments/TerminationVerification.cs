using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a verification of terminated candidacy document.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the verification of terminated candidacy and not the ceritification itself. Therefore, it will not contain any data actually submitted in a verification of terminated candidacy form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class TerminationVerification : SubmissionDocument
	{
		/// <summary>
		/// Creates a verification of terminated candidacy document with a specific document number.
		/// </summary>
		/// <param name="number">The verification of terminated candidacy's document number.</param>
		/// <param name="lastUpdated">The date when the verification of terminated candidacy document was last updated in CFIS.</param>
		public TerminationVerification(int number, DateTime lastUpdated)
			: base(DocumentType.Termination, number, lastUpdated)
		{
		}
	}
}
