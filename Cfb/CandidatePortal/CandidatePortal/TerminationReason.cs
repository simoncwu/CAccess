namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different reasons for a candidate termination.
	/// </summary>
	public enum TerminationReason
	{
		/// <summary>
		/// Other, miscellaneous reason.
		/// </summary>
		Other, //OTHER
		/// <summary>
		/// Terminated by the Board.
		/// </summary>
		ByBoard, //BOARD
		/// <summary>
		/// Ceased campaigning.
		/// </summary>
		CeasedCampaign, //CEASE
		/// <summary>
		/// Off the ballot.
		/// </summary>
		OffBallot //OFFBAL
	}
}
