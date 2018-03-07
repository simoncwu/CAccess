namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for candidate classifications with respect to "the Program".
	/// </summary>
	public enum CfbClassification : byte
	{
		/// <summary>
		/// Undetermined classification
		/// </summary>
		Undetermined,
		/// <summary>
		/// Limited Participant classification
		/// </summary>
		LimitedParticipant,
		/// <summary>
		/// Non-Participant classification
		/// </summary>
		NonParticipant,
		/// <summary>
		/// Participant classification
		/// </summary>
		Participant
	}
}
