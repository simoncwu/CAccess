using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate filer registration.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the filer registration and not the ceritification itself. Therefore, it will not contain any data actually submitted in a filer registration form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class FilerRegistrationDocument : SubmissionDocument
	{
		/// <summary>
		/// Creates a filer registration document with a specific document number.
		/// </summary>
		/// <param name="number">The filer registration's document number.</param>
		/// <param name="lastUpdated">The date when the filer registration document was last updated in CFIS.</param>
		public FilerRegistrationDocument(int number, DateTime lastUpdated)
			: base(DocumentType.FilerRegistration, number, lastUpdated)
		{
		}
	}
}
