using System;
using System.Collections.Generic;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a candidate's plan payment history, sorted in increasing order by payment date.
	/// </summary>
	public class PlanPaymentHistory : SortedList<DateTime, PlanPayment>
	{
		/// <summary>
		/// The total monetary amount of plan payments made to date.
		/// </summary>
		private uint _paymentTotal;

		/// <summary>
		/// Gets or sets the total monetary amount of plan payments made to date.
		/// </summary>
		public uint PaymentTotal
		{
			get { return _paymentTotal; }
			set { _paymentTotal = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlanPaymentHistory"/> class that is empty and has a specific initial capacity.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="PlanPaymentHistory"/> can contain.</param>
		internal PlanPaymentHistory(int capacity)
			: base(capacity)
		{
			_paymentTotal = 0;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlanPaymentHistory"/> class that is empty.
		/// </summary>
		private PlanPaymentHistory()
		{
		}

		/// <summary>
		/// Adds a <see cref="PlanPayment"/> to the history.
		/// </summary>
		/// <param name="value">The <see cref="PlanPayment"/> to add.</param>
		/// <remarks>The <see cref="PlanPayment"/> will be inserted so that the resulting history will remain sorted in increasing order by payment date.</remarks>
		public void Add(PlanPayment value)
		{
			if (value != null)
			{
				this.Add(value.Date, value);
				_paymentTotal += value.Amount;
			}
		}
	}
}
