using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a statement of need for both primary and general elections.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the statement of need and not the statement itself. Therefore, it will not contain any data actually submitted in a statement of need.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class StatementOfNeed : SubmissionDocument
	{
		/// <summary>
		/// Creates a statement of need with a specific document number.
		/// </summary>
		/// <param name="number">The statement of need's document number.</param>
		/// <param name="lastUpdated">The date when the statement of need document was last updated in CFIS.</param>
		/// <param name="isGeneral">true if the statement is for the general election, false otherwise.</param>
		public StatementOfNeed(int number, DateTime lastUpdated, bool isGeneral)
			: base(isGeneral ? DocumentType.StatementOfNeedGeneral : DocumentType.StatementOfNeedPrimary, number, lastUpdated)
		{
		}
	}
}
