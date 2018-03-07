namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the types of publicly elected governmental offices of New York City.
	/// </summary>
	public enum NycPublicOfficeType : byte
	{
		/// <summary>
		/// Undeclared (6)
		/// </summary>
		Undeclared,
		/// <summary>
		/// City-wide office of the Mayor (1)
		/// </summary>
		Mayor,
		/// <summary>
		/// City-wide office of the Public Advocate (2)
		/// </summary>
		PublicAdvocate,
		/// <summary>
		/// City-wide office of the Comptroller (3)
		/// </summary>
		Comptroller,
		/// <summary>
		/// Office of Borough President (4)
		/// </summary>
		BoroughPresident,
		/// <summary>
		/// Office of City District Council Member (5)
		/// </summary>
		CityCouncilMember
	}
}
