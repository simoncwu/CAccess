namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different reasons for a document's status.
	/// </summary>
	public enum DocumentStatusReason
	{
		/// <summary>
		/// Other, miscellaneous reason.
		/// </summary>
		Other, //OTHER
		/// <summary>
		/// Submission disk is corrupt.
		/// </summary>
		CorruptDisk, //CORDSK
		/// <summary>
		/// Cover sheet was not signed.
		/// </summary>
		CoverNotSigned, //CVSIGN
		/// <summary>
		/// Submitted information is illegible.
		/// </summary>
		Illegible, //ILLEGI
		/// <summary>
		/// Invalid bank account number.
		/// </summary>
		BankAccount, //INVBKA
		/// <summary>
		/// Missing submission disk.
		/// </summary>
		MissingDisk, //MISDSK
		/// <summary>
		/// Missing submission papers.
		/// </summary>
		MissingPapers, //MISPAP
		/// <summary>
		/// Missing summary pages.
		/// </summary>
		MissingSummary, //MISPGS
		/// <summary>
		/// Missing cover/control pages.
		/// </summary>
		MissingCover, //MSCCPG
		/// <summary>
		/// Missing current patch.
		/// </summary>
		MissingPatch, //MSPATC
		/// <summary>
		/// Missing cross-reference documentation.
		/// </summary>
		MissingReference, //MSREF
		/// <summary>
		/// Missing submission files.
		/// </summary>
		MissingFile, //MSSUB
		/// <summary>
		/// No files on disk.
		/// </summary>
		NoFilesOnDisk, //NOFILE
		/// <summary>
		/// No signature.
		/// </summary>
		NoSignature, //NOSIGN
		/// <summary>
		/// Everything is okay.
		/// </summary>
		Okay, //OK
		/// <summary>
		/// Software not updated.
		/// </summary>
		NotUpdated, //SOFTUP
		/// <summary>
		/// Split submission file.
		/// </summary>
		SplitFile //SPSUB
	}
}
