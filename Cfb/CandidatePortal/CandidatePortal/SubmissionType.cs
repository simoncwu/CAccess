namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different document submission revision types.
	/// </summary>
	public enum SubmissionType : byte
	{
		/// <summary>
		/// An unrecognized submission.
		/// </summary>
		Unknown,
		/// <summary>
		/// A regular/original submission.
		/// </summary>
		Regular, //REG
		/// <summary>
		/// An amendment to a previous submission.
		/// </summary>
		Amendment, //AMEND
		/// <summary>
		/// A resubmission of a previous submission.
		/// </summary>
		Resubmission, //RESUB
		/// <summary>
		/// An amendment to a previous submission generated internally.
		/// </summary>
		InternalAmendment, //IAMEND
		/// <summary>
		/// A record for documentation purposes only.
		/// </summary>
		Documentation //DOCONL
	}
}
