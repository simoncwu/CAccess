namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Enumeration of the types of post election audit tolling events.
	/// </summary>
	public enum TollingEventType : byte
	{
		/// <summary>
		/// An extension event.
		/// </summary>
		Extension,
		/// <summary>
		/// An outstanding response vent.
		/// </summary>
		OutstandingResponse,
		/// <summary>
		/// An inadequate response event.
		/// </summary>
		InadequateResponse,
		/// <summary>
		/// An additional inadequate response event.
		/// </summary>
		AdditionalInadequate
	}
}
