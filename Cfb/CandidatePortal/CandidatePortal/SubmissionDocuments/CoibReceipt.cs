using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a Conflict of Interest Board receipt document.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the COIB receipt and not the ceritification itself. Therefore, it will not contain any data actually submitted in a COIB receipt form.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CoibReceipt : SubmissionDocument
	{
		/// <summary>
		/// Gets or sets the date the candidate filed with the Conflict of Interest Board.
		/// </summary>
		[DataMember]
		public DateTime CoibFilingDate { get; set; }

		/// <summary>
		/// Creates a COIB receipt document with a specific document number.
		/// </summary>
		/// <param name="number">The COIB receipt's document number.</param>
		/// <param name="lastUpdated">The date when the COIB receipt document was last updated in CFIS.</param>
		public CoibReceipt(int number, DateTime lastUpdated)
			: base(DocumentType.CoibReceipt, number, lastUpdated)
		{
		}
	}
}
