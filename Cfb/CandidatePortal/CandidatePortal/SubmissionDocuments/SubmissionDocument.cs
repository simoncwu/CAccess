using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// A document submitted by a candidate.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public abstract class SubmissionDocument
	{
		/// <summary>
		/// The type of submisson document.
		/// </summary>
		[DataMember(Name = "DocumentType")]
		protected readonly DocumentType _documentType;

		/// <summary>
		/// Gets the submission document type.
		/// </summary>
		public DocumentType DocumentType
		{
			get { return _documentType; }
		}

		/// <summary>
		/// The document number.
		/// </summary>
		[DataMember(Name = "Number")]
		protected readonly int _number;

		/// <summary>
		/// Gets the document number.
		/// </summary>
		public int Number
		{
			get { return _number; }
		}

		/// <summary>
		/// The date when the document record was last updated in CFIS.
		/// </summary>
		[DataMember(Name = "LastUpdated")]
		protected readonly DateTime _lastUpdated;

		/// <summary>
		/// Gets the date when the document record was last updated in CFIS.
		/// </summary>
		public DateTime LastUpdated
		{
			get { return _lastUpdated; }
		}

		/// <summary>
		/// Gets or sets the document status.
		/// </summary>
		[DataMember]
		public DocumentStatus Status { get; set; }

		/// <summary>
		/// Gets the document status as a friendly string.
		/// </summary>
		public string StatusString
		{
			get { return CPConvert.ToString(this.Status); }
		}

		/// <summary>
		/// Gets or sets the reason for an exception status on the document.
		/// </summary>
		[DataMember]
		public DocumentStatusReason StatusReason { get; set; }

		/// <summary>
		/// Gets or sets the submission type of the document.
		/// </summary>
		[DataMember]
		public SubmissionType SubmissionType { get; set; }

		/// <summary>
		/// Gets or sets the means by which the document was delivered.
		/// </summary>
		[DataMember]
		public DeliveryType DeliveryType { get; set; }

		/// <summary>
		/// Gets or sets the date the document was received.
		/// </summary>
		[DataMember]
		public DateTime? ReceivedDate { get; set; }

		/// <summary>
		/// The date the document was postmarked for postal delivery, if applicable.
		/// </summary>
		[DataMember(Name = "PostmarkDate")]
		protected DateTime? _postmarkDate = null;

		/// <summary>
		/// Gets or sets the date the document was postmarked for postal delivery, if applicable.
		/// </summary>
		public DateTime? PostmarkDate
		{
			get
			{
				switch (this.DeliveryType)
				{
					case DeliveryType.Mail:
					case DeliveryType.RegisteredCertified:
						return _postmarkDate;
					default:
						return null;
				}
			}
			set
			{
				_postmarkDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the date the status of this document last changed.
		/// </summary>
		[DataMember]
		public virtual DateTime? StatusDate { get; set; }

		/// <summary>
		/// Gets or sets the number of pages submitted for this document.
		/// </summary>
		[DataMember]
		public short PageCount { get; set; }

		/// <summary>
		/// Gets whether or not the document was ever received.
		/// </summary>
		public bool Received
		{
			get { return this.Status == DocumentStatus.Received && this.ReceivedDate != null; }
		}

		/// <summary>
		/// Gets whether or not the document has been fully accepted.
		/// </summary>
		public bool Accepted
		{
			get { return this.Status == DocumentStatus.Accepted; }
		}

		/// <summary>
		/// Creates a new document of a specific type and with a specific document number.
		/// </summary>
		/// <param name="doctype">A <see cref="DocumentType"/> indicating the type of document to create.</param>
		/// <param name="number">The document's document number.</param>
		/// <param name="lastUpdated">The date when the document record was last updated in CFIS.</param>
		protected SubmissionDocument(DocumentType doctype, int number, DateTime lastUpdated)
		{
			_documentType = doctype;
			_number = number;
			_lastUpdated = lastUpdated;
			this.ReceivedDate = null;
			this.PageCount = 0;
		}
	}
}
