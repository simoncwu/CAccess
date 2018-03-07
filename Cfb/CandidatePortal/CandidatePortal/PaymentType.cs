namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for payment transaction types.
	/// </summary>
	public enum PaymentType : byte
	{
		/// <summary>
		/// Unknown transaction.
		/// </summary>
		Unknown,
		/// <summary>
		/// Automatic penalty payment.
		/// </summary>
		AutomaticPenalty,
		/// <summary>
		/// Candidate payment.
		/// </summary>
		Candidate,
		/// <summary>
		/// Manual penalty payment.
		/// </summary>
		ManualPenalty,
		/// <summary>
		/// Penalty payment.
		/// </summary>
		Penalty,
		/// <summary>
		/// Returned funds.
		/// </summary>
		ReturnedFunds,
		/// <summary>
		/// Reimbursed returned funds.
		/// </summary>
		ReimbursedReturn
	}
}
