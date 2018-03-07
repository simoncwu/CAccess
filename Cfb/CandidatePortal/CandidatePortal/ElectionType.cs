namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different types of elections.
	/// </summary>
	public enum ElectionType : byte
	{
		/// <summary>
		/// A general election.
		/// </summary>
		General,
		/// <summary>
		/// A primary election.
		/// </summary>
		Primary,
		/// <summary>
		/// A runoff/rerun election.
		/// </summary>
		Runoff,
		/// <summary>
		/// A special election.
		/// </summary>
		Special
	}
}
