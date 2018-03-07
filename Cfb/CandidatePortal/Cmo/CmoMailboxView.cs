namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Enumeration of the types of Campaign Messages Online mailbox views.
	/// </summary>
	public enum CmoMailboxView : byte
	{
		/// <summary>
		/// A view showing only current (unarchived) messages.
		/// </summary>
		Current,
		/// <summary>
		/// A view showing only unopened messages.
		/// </summary>
		Unopened,
		/// <summary>
		/// A view showing only archived messages.
		/// </summary>
		Archived,
		/// <summary>
		/// A view showing only messages flagged for follow-up.
		/// </summary>
		FollowUp,
		/// <summary>
		/// A view showing all messages.
		/// </summary>
		All
	}
}
