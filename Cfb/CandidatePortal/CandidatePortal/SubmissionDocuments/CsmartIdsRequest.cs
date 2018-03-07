using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a C-SMART/IDS request document.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the C-SMART/IDS request and not the request itself. Therefore, it will not contain any data actually submitted in a C-SMART/IDS request form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CsmartIdsRequest : SubmissionDocument
	{
		/// <summary>
		/// Creates a C-SMART/IDS request document with a specific document number.
		/// </summary>
		/// <param name="number">The C-SMART/IDS request's document number.</param>
		/// <param name="lastUpdated">The date when the C-SMART/IDS request document was last updated in CFIS.</param>
		public CsmartIdsRequest(int number, DateTime lastUpdated)
			: base(DocumentType.CsmartIdsRequest, number, lastUpdated)
		{
		}
	}
}
