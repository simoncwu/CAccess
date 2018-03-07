namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different types of submission documents supported.
	/// </summary>
	public enum DocumentType : byte
	{
		/// <summary>
		/// Default unknown document type.
		/// </summary>
		Unknown,
		/// <summary>
		/// A candidate certification.
		/// </summary>
		Certification,
		/// <summary>
		/// A Conflict of Interest Board receipt.
		/// </summary>
		CoibReceipt,
		/// <summary>
		/// A filing disclosure statement submitted by a candidate.
		/// </summary>
		DisclosureStatement,
		/// <summary>
		/// A candidate filer registration.
		/// </summary>
		FilerRegistration,
		/// <summary>
		/// An Internet Delivery System password request.
		/// </summary>
		CsmartIdsRequest,
		/// <summary>
		/// A special pre-election disclosure statement for activity prior to a general election.
		/// </summary>
		PreGeneralDisclosure,
		/// <summary>
		/// A special pre-election disclosure statement for activity prior to a primary election.
		/// </summary>
		PrePrimaryDisclosure,
		/// <summary>
		/// A statement of need prior to a general election.
		/// </summary>
		StatementOfNeedGeneral,
		/// <summary>
		/// A statement of need prior to a primary election.
		/// </summary>
		StatementOfNeedPrimary,
		/// <summary>
		/// A verification of terminated candidacy.
		/// </summary>
		Termination
	}
}
