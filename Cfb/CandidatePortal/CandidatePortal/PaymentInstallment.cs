using System;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a scheduled installment for a payment plan.
	/// </summary>
	public class PaymentInstallment
	{
		/// <summary>
		/// The payment installment number.
		/// </summary>
		private readonly ushort _number;

		/// <summary>
		/// Gets the payment installment number.
		/// </summary>
		public ushort Number
		{
			get { return _number; }
		}

		/// <summary>
		/// The amount due for this payment installment.
		/// </summary>
		private uint _amountDue;

		/// <summary>
		/// Gets or sets the amount due for this payment installment.
		/// </summary>
		public uint AmountDue
		{
			get { return _amountDue; }
			set { _amountDue = value; }
		}

		/// <summary>
		/// The amount paid for this payment installment.
		/// </summary>
		private uint? _amountPaid;

		/// <summary>
		/// Gets or sets the amount paid for this payment installment.
		/// </summary>
		public uint? AmountPaid
		{
			get { return _amountPaid; }
			set { _amountPaid = value; }
		}

		/// <summary>
		/// The payment installment due date.
		/// </summary>
		private DateTime _dueDate;

		/// <summary>
		/// Gets or sets the payment installment due date.
		/// </summary>
		public DateTime DueDate
		{
			get { return _dueDate; }
			set { _dueDate = value.Date; }
		}

		/// <summary>
		/// The starting date of the payment installment period.
		/// </summary>
		private DateTime _startDate;

		/// <summary>
		/// Gets or sets the starting date of the payment installment period.
		/// </summary>
		public DateTime StartDate
		{
			get { return _startDate; }
			set { _startDate = value.Date; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentInstallment"/> class with zeroed-out amounts.
		/// </summary>
		internal PaymentInstallment()
		{
			_amountDue = 0;
			_amountPaid = 0;
		}

		/// <summary>
		/// Creates a new record of a payment plan installment.
		/// </summary>
		/// <param name="number"></param>
		internal PaymentInstallment(ushort number)
		{
			this._number = number;
		}

		/// <summary>
		/// Checks whether or not a date falls within the payment installment period.
		/// </summary>
		/// <param name="date">The date to check.</param>
		/// <returns>True if the period includes <paramref name="date"/>, otherwise false.</returns>
		public bool IncludesDate(DateTime date)
		{
			return IncludesDate(date, 0);
		}

		/// <summary>
		/// Checks whether or not a date falls within the grace-extended payment installment period.
		/// </summary>
		/// <param name="date">The date to check.</param>
		/// <param name="grace">The grace period allowed, in days.</param>
		/// <returns>True if the grace-extended period includes <paramref name="date"/>, otherwise false.</returns>
		public bool IncludesDate(DateTime date, byte grace)
		{
			return (_startDate.AddDays(grace).CompareTo(date.Date) <= 0) && (date.Date.CompareTo(_dueDate.AddDays(grace)) <= 0);
		}
	}
}
