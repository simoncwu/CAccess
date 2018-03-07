namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the types of payment plan periods.
	/// </summary>
	public enum PaymentPeriod : byte
	{
		/// <summary>
		/// Unknown period.
		/// </summary>
		Unknown,
		/// <summary>
		/// Monthly payment periods.
		/// </summary>
		Monthly,
		/// <summary>
		/// Quarterly payment periods.
		/// </summary>
		Quarterly,
		/// <summary>
		/// Semi-annual payment periods.
		/// </summary>
		SemiAnnual,
		/// <summary>
		/// Annual payment periods.
		/// </summary>
		Annual
	}
}
