namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Enumeration for the different versions of a post election audit documentation request that can be sent.
	/// </summary>
	public enum AuditRequestType : byte
	{
		/// <summary>
		/// A first request.
		/// </summary>
		FirstRequest = 0,
		/// <summary>
		/// A repost of a first request.
		/// </summary>
		FirstRepost = 1,
		/// <summary>
		/// A second request.
		/// </summary>
		SecondRequest = 2,
		/// <summary>
		/// A repost of a second request.
		/// </summary>
		SecondRepost = 3
	}
}
