namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's filing disclosure statement history for a single committee and statement.
	/// </summary>
	public class DisclosureStatementHistory : SubmissionHistory<DisclosureStatement>
	{
		/// <summary>
		/// Initializes a new, empty collection of <see cref="DisclosureStatement"/> records.
		/// </summary>
		public DisclosureStatementHistory()
		{
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="DisclosureStatement"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of filing disclosure statements that the collection can initially store.</param>
		/// </summary>
		public DisclosureStatementHistory(int capacity)
			: base(capacity)
		{
		}
	}
}
