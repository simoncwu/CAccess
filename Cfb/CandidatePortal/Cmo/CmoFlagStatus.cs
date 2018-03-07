namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Enumeration of possible follow-up flagging statuses for campaign messages.
	/// </summary>
	public enum CmoFlagStatus : byte
	{
		/// <summary>
		/// A message requiring no follow-up.
		/// </summary>
		None,
		/// <summary>
		/// A message that has been flagged for follow-up.
		/// </summary>
		Flagged,
		/// <summary>
		/// A message that was flagged for follow-up and has been completed.
		/// </summary>
		Completed
	}
}
