namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// Enumeration of possible campaign message statues.
	/// </summary>
	public enum CmoMessageStatus : byte
	{
		/// <summary>
		/// A draft message that has not yet been sent.
		/// </summary>
		Draft,
		/// <summary>
		/// A message that has been sent but not opened.
		/// </summary>
		Posted,
		/// <summary>
		/// A message that has been both sent and opened.
		/// </summary>
		Opened,
		/// <summary>
		/// A message that has been archived.
		/// </summary>
		Archived
	}
}
