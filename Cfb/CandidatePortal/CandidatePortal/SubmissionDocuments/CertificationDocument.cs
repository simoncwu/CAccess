using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate certification document.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the certification and not the ceritification itself. Therefore, it will not contain any data actually submitted in a certification form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CertificationDocument : SubmissionDocument
	{
		/// <summary>
		/// Creates a certification document with a specific document number.
		/// </summary>
		/// <param name="number">The certification's document number.</param>
		/// <param name="lastUpdated">The date when the certification document was last updated in CFIS.</param>
		public CertificationDocument(int number, DateTime lastUpdated)
			: base(DocumentType.Certification, number, lastUpdated)
		{
		}
	}
}
