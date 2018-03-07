using System;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a remitted payment for a payment plan.
	/// </summary>
	public class PlanPayment
	{
		/// <summary>
		/// The type of the payment.
		/// </summary>
		private PaymentType _type;

		/// <summary>
		/// Gets or sets the type of the payment.
		/// </summary>
		public PaymentType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		/// <summary>
		/// The date the payment was made.
		/// </summary>
		private DateTime _date;

		/// <summary>
		/// Gets or sets the date the payment was made.
		/// </summary>
		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		/// <summary>
		/// The number of the check used for payment, if paid via check.
		/// </summary>
		/// <remarks>This is not used consistently and cannot yet be relied upon to determine the check number.</remarks>
		private ushort _checkNumber;

		/// <summary>
		/// Gets or sets the number of the check used for payment, if paid via check.
		/// </summary>
		/// <remarks>This is not used consistently and cannot yet be relied upon to determine the check number.</remarks>
		public ushort CheckNumber
		{
			get { return _checkNumber; }
			set { _checkNumber = value; }
		}

		/// <summary>
		/// The amount paid.
		/// </summary>
		private uint _amount;

		/// <summary>
		/// Gets or sets the amount paid.
		/// </summary>
		public uint Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

		/// <summary>
		/// Creates a new record of a plan payment.
		/// </summary>
		internal PlanPayment()
		{
		}
	}
}
