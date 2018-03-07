using System;
using System.Collections.Generic;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a schedule of payment installments in a payment plan.
	/// </summary>
	public class PaymentSchedule : SortedList<int, PaymentInstallment>
	{
		/// <summary>
		/// The grace period extended to the candidate for late plan payments.
		/// </summary>
		private byte _gracePeriod;

		/// <summary>
		/// Gets or sets the grace period extended to the candidate for late plan payments.
		/// </summary>
		public byte GracePeriod
		{
			get { return _gracePeriod; }
			set { _gracePeriod = value; }
		}

		/// <summary>
		/// The upcoming scheduled payment installment.
		/// </summary>
		private PaymentInstallment _current;

		/// <summary>
		/// Gets the upcoming scheduled payment installment.
		/// </summary>
		public PaymentInstallment Current
		{
			get { return _current; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentSchedule"/> class that is empty and has a specific initial capacity.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="PaymentSchedule"/> can contain.</param>
		internal PaymentSchedule(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentSchedule"/> class that is empty.
		/// </summary>
		private PaymentSchedule()
		{
		}

		/// <summary>
		/// Adds a <see cref="PaymentInstallment"/> to the history.
		/// </summary>
		/// <param name="value">The <see cref="PaymentInstallment"/> to add.</param>
		/// <remarks>The <see cref="PaymentInstallment"/> will be inserted so that the resulting schedule will remain sorted in increasing order by payment number.</remarks>
		public void Add(PaymentInstallment value)
		{
			if (value != null)
				this.Add(value.Number, value);
		}

		/// <summary>
		/// Gets the current past due balance for the schedule from a history of payments.
		/// </summary>
		/// <param name="history">A <see cref="PlanPaymentHistory"/> collection of payment transactions.</param>
		/// <returns>The current past due balance as of the last payment installment.</returns>
		internal long GetPastDueBalance(PlanPaymentHistory history)
		{
			DateTime periodStart = DateTime.MinValue;
			long pastDueBalance = 0;
			IEnumerator<PlanPayment> payment = history.Values.GetEnumerator();
			payment.MoveNext();
			foreach (PaymentInstallment installment in this.Values) {
				DateTime actualDueDate = installment.DueDate.AddDays(_gracePeriod);
				if (DateTime.Today.CompareTo(installment.DueDate.AddDays(_gracePeriod)) <= 0) {
					_current = installment;
					break;
				}
				// [balance] = [payment due] + [past due balance] - [amount paid in period]
				uint amountPaid = 0;
				DateTime paymentDate;
				paymentDate = (payment.Current == null) ? DateTime.MaxValue : payment.Current.Date;
				while ((payment.Current != null) && installment.IncludesDate(paymentDate, _gracePeriod)) {
					amountPaid += payment.Current.Amount;
					payment.MoveNext();
					paymentDate = (payment.Current == null) ? DateTime.MaxValue : payment.Current.Date;
				}
				installment.AmountPaid = amountPaid;
				pastDueBalance = installment.AmountDue + pastDueBalance - amountPaid;
			}
			return pastDueBalance;
		}
	}
}
