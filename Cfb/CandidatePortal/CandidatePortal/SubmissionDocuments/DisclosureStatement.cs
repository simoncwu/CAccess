using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a filing disclosure statementdocument.
	/// </summary>
	/// <remarks>Note that this represents only the document tracking record of the disclosure statement and not the disclosure statement itself. Therefore, it will not contain any schedule data actually submitted in a disclosure statement.</remarks>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class DisclosureStatement : SubmissionDocument
	{
		/// <summary>
		/// The CFIS ID of the committee that submitted the disclosure statement.
		/// </summary>
		[DataMember(Name = "CommitteeID")]
		private readonly char _committeeID;

		/// <summary>
		/// Gets the CFIS ID of the committee that submitted the disclosure statement.
		/// </summary>
		public char CommitteeID
		{
			get { return _committeeID; }
		}

		/// <summary>
		/// The disclosure statement period.
		/// </summary>
		[DataMember(Name = "Statement")]
		private readonly Statement _statement;

		/// <summary>
		/// Gets the disclosure statement being reviewed.
		/// </summary>
		public Statement Statement
		{
			get { return _statement; }
		}

		/// <summary>
		/// Gets the disclosure statement number.
		/// </summary>
		public byte StatementNumber
		{
			get
			{
				if (object.Equals(_statement, null))
					return 0;
				else
					return _statement.Number;
			}
		}

		/// <summary>
		/// Gets the due date for the disclosure statement.
		/// </summary>
		public DateTime? StatementDueDate
		{
			get
			{
				if (object.Equals(_statement, null))
					return null;
				else
					return _statement.DueDate as DateTime?;
			}
		}

		/// <summary>
		/// Gets or sets the name of the committee that submitted the disclosure statement.
		/// </summary>
		[DataMember]
		public string CommitteeName { get; set; }

		/// <summary>
		/// Gets or sets the date when receipt of the disclosure statement was completed.
		/// </summary>
		[DataMember]
		public DateTime? ReceiptCompleteDate { get; set; }

		/// <summary>
		/// Gets or sets the date the status of this document last changed.
		/// </summary>
		[DataMember]
		public override DateTime? StatusDate
		{
			get { return this.Status == DocumentStatus.Received ? this.ReceiptCompleteDate : base.StatusDate; }
			set { base.StatusDate = value; }
		}

		/// <summary>
		/// Gets or sets whether or not the disclosure statement is an audit response.
		/// </summary>
		[DataMember]
		public bool IsAuditResponse { get; set; }

		/// <summary>
		/// Gets or sets the format(s) of the submission.
		/// </summary>
		[DataMember]
		public SubmissionFormat SubmissionFormats;

		/// <summary>
		/// Gets whether or not the statement was submitted online.
		/// </summary>
		public bool SubmittedOnline
		{
			get { return this.SubmissionFormats.HasFlag(SubmissionFormat.CsmartWeb) || this.SubmissionFormats.HasFlag(SubmissionFormat.IDS); }
		}

		/// <summary>
		/// Gets or sets whether or not the disclosure statement is a small campaign statement.
		/// </summary>
		[DataMember]
		public bool SmallCampaign { get; set; }

		/// <summary>
		/// Gets or sets whether or not the disclosure statement is a deferred filing.
		/// </summary>
		[DataMember]
		public bool DeferredFiling { get; set; }

		/// <summary>
		/// Gets or sets the date the backup documentation was received, if submitted.
		/// </summary>
		[DataMember]
		public DateTime? BackupReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets the means by which the backup documentation was delivered.
		/// </summary>
		[DataMember]
		public DeliveryType BackupDeliveryType { get; set; }

		/// <summary>
		/// Gets or sets the date the backup documentation was postmarked for postal delivery, if applicable.
		/// </summary>
		[DataMember]
		public DateTime? BackupPostmarkDate { get; set; }

		/// <summary>
		/// Gets whether or not any backup documentation was received.
		/// </summary>
		public bool BackupReceived
		{
			get { return this.BackupReceivedDate.HasValue; }
		}

		/// <summary>
		/// Creates a disclosure statement document.
		/// </summary>
		/// <param name="statement">The disclosure statement period.</param>
		/// <param name="number">The disclosure statement's document number.</param>
		/// <param name="committeeID">The CFIS ID of the committee submitting the disclosure statement.</param>
		/// <param name="lastUpdated">The date when the disclosure statement document was last updated in CFIS.</param>
		public DisclosureStatement(Statement statement, int number, char committeeID, DateTime lastUpdated)
			: base(DocumentType.DisclosureStatement, number, lastUpdated)
		{
			_statement = statement;
			_committeeID = committeeID;
		}
	}
}
