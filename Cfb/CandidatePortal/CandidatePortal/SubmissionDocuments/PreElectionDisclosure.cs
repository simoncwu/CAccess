using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a pre-election disclosure statement for both primary and general elections.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the pre-election disclosure statement and not the pre-election disclosure statement itself. Therefore, it will not contain any data actually submitted in a pre-election disclosure statement.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class PreElectionDisclosure : SubmissionDocument
	{
		/// <summary>
		/// The CFIS ID of the committee that submitted the pre-election disclosure statement.
		/// </summary>
		[DataMember(Name = "CommitteeID")]
		private readonly char _committeeID;

		/// <summary>
		/// Gets the CFIS ID of the committee that submitted the pre-election disclosure statement.
		/// </summary>
		public char CommitteeID
		{
			get { return _committeeID; }
		}

		/// <summary>
		/// Gets or sets the name of the committee that submitted the pre-election disclosure statement.
		/// </summary>
		[DataMember]
		public string CommitteeName { get; set; }

		/// <summary>
		/// Creates a pre-election disclosure statement with a specific document number.
		/// </summary>
		/// <param name="number">The pre-election disclosure statement's document number.</param>
		/// <param name="committeeID">The CFIS ID of the committee submitting the pre-election disclosure statement.</param>
		/// <param name="lastUpdated">The date when the pre-election disclosure statement was last updated in CFIS.</param>
		/// <param name="isGeneral">true if the disclosure is for the general election, false otherwise.</param>
		public PreElectionDisclosure(int number, char committeeID, DateTime lastUpdated, bool isGeneral)
			: base(isGeneral ? DocumentType.PreGeneralDisclosure : DocumentType.PrePrimaryDisclosure, number, lastUpdated)
		{
			_committeeID = committeeID;
		}
	}
}
