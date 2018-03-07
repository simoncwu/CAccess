using System;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Summaric business object represenation of a candidate's penalty payment plan.
	/// </summary>
	public class PaymentPlanSummary
	{
		/// <summary>
		/// Gets or sets the due date of the first scheduled payment in the plan.
		/// </summary>
		public DateTime FirstPaymentDate { get; set; }

		/// <summary>
		/// Gets or sets the due date of the last scheduled payment in the plan.
		/// </summary>
		public virtual DateTime FinalPaymentDueDate { get; set; }

		/// <summary>
		/// Gets or sets the total number of payment installments in the plan.
		/// </summary>
		public ushort PaymentCount { get; set; }

		/// <summary>
		/// Gets or sets the arithmetic mean amount of all payment installments in the plan.
		/// </summary>
		public uint PeriodPaymentAmount { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentPlanSummary"/> class.
		/// </summary>
		public PaymentPlanSummary()
		{
			this.PaymentCount = 0;
		}
	}
}
