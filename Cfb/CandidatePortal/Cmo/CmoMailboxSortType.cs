namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Enumeration for the mailbox columns available for sorting.
	/// </summary>
	public enum CmoMailboxSortType : byte
	{
		/// <summary>
		/// The message received/posted date.
		/// </summary>
		Date,
		/// <summary>
		/// The message election cycle context.
		/// </summary>
		ElectionCycle,
		/// <summary>
		/// The message subject.
		/// </summary>
		Subject,
		/// <summary>
		/// The message follow-up flag.
		/// </summary>
		FollowUp
	}
}
