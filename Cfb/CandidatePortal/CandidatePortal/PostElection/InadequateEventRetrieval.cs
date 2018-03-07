namespace Cfb.CandidatePortal.PostElection
{
	/// <summary>
	/// Specifies the type of retrieval criteria to be used by inadequate event retrieval methods.
	/// </summary>
	public enum InadequateEventRetrieval
	{
		/// <summary>
		/// Retrieve only the initial inadequate event.
		/// </summary>
		InitialEvent,
		/// <summary>
		/// Retrieve only additional inadequate events.
		/// </summary>
		AdditionalEvents,
		/// <summary>
		/// Retrieve both initial and additional inadequate events.
		/// </summary>
		AllEvents
	}
}
