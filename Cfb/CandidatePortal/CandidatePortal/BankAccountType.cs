namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different bank account types.
	/// </summary>
	public enum BankAccountType : byte
	{
		/// <summary>
		/// A non-standard bank account.
		/// </summary>
		Other,
		/// <summary>
		/// A checking account.
		/// </summary>
		Checking,
		/// <summary>
		/// A credit card account.
		/// </summary>
		CreditCard,
		/// <summary>
		/// A custodial account.
		/// </summary>
		Custodial,
		/// <summary>
		/// A savings account.
		/// </summary>
		Savings,
		/// <summary>
		/// A money market account.
		/// </summary>
		MoneyMarket
	}
}
