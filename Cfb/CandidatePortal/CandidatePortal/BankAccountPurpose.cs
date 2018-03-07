namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different bank account purposes.
	/// </summary>
	public enum BankAccountPurpose : byte
	{
		/// <summary>
		/// For a non-standard purpose.
		/// </summary>
		Other,
		/// <summary>
		/// For fundraising in the current election.
		/// </summary>
		Regular,
		/// <summary>
		/// For a run-off election or for rerunning.
		/// </summary>
		RunoffRerun,
		/// <summary>
		/// For soliciting non-matchable contributions.
		/// </summary>
		Segregated
	}
}
