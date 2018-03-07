namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different statuses of a document.
	/// </summary>
	public enum DocumentStatus : byte
	{
		/// <summary>
		/// Default unknown document status.
		/// </summary>
		Unknown,
		/// <summary>
		/// Fully approved and accepted.
		/// </summary>
		Accepted, //ACCEPT
		/// <summary>
		/// On-hold or awaiting additional info.
		/// </summary>
		OnHold, //ON-HOLD, AWAIT
		/// <summary>
		/// Disregarded.
		/// </summary>
		Disregarded, //DISREG
		/// <summary>
		/// Received for further processing.
		/// </summary>
		Received, //RCVD
		/// <summary>
		/// Rejected.
		/// </summary>
		Rejected //REJECT
	}
}
