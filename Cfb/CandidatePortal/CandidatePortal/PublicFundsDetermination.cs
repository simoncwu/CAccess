using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A public funds determination indicating whether or not a candidate was paid and the details of the payment.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class PublicFundsDetermination
	{
		/// <summary>
		/// Whether or not a payment was issued.
		/// </summary>
		[DataMember(Name = "PaymentIssued")]
		private readonly bool _paymentIssued;

		/// <summary>
		/// Gets whether or not a payment was issued.
		/// </summary>
		public bool PaymentIssued
		{
			get { return _paymentIssued; }
		}

		/// <summary>
		/// The date of the determination.
		/// </summary>
		[DataMember(Name = "Date")]
		private readonly DateTime _date;

		/// <summary>
		/// Gets the date of the determination.
		/// </summary>
		public DateTime Date
		{
			get { return _date; }
		}

		/// <summary>
		/// Gets or sets the amount of the payment, in U.S. dollars and cents.
		/// </summary>
		[DataMember]
		public decimal PaymentAmount { get; set; }

		/// <summary>
		/// Gets or sets the method by which the payment was made.
		/// </summary>
		[DataMember]
		public PaymentMethod PaymentMethod { get; set; }

		/// <summary>
		/// Gets or sets the type of election for which the payment was made.
		/// </summary>
		[DataMember]
		public ElectionType ElectionType { get; set; }

		/// <summary>
		/// Gets or sets the payment run number.
		/// </summary>
		[DataMember]
		public byte Run { get; set; }

		/// <summary>
		/// Gets or sets the ID for the payment's corresponding CMO message. A non-positive value indicates that there is no matching message.
		/// </summary>
		[DataMember]
		public int MessageID { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PublicFundsDetermination"/> class.
		/// </summary>
		/// <param name="date">The date of the determination.</param>
		/// <param name="paymentIssued">Indicates whether a payment was issued.</param>
		public PublicFundsDetermination(DateTime date, bool paymentIssued)
		{
			_date = date;
			_paymentIssued = paymentIssued;
			this.MessageID = -1;
		}
	}
}
