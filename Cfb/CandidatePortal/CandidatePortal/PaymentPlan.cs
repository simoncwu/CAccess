using System;
using System.Collections.Generic;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object represenation of a candidate's payment plan.
	/// </summary>
	public class PaymentPlan : PaymentPlanSummary
	{
		/// <summary>
		/// Gets or sets the total amount assessed for repayment.
		/// </summary>
		public uint Total { get; set; }

		/// <summary>
		/// Gets or sets the period of occurrence for each payment installment in the plan.
		/// </summary>
		public PaymentPeriod Period { get; set; }

		/// <summary>
		/// Gets or sets the grace period extended to the candidate for late plan payments.
		/// </summary>
		public byte GracePeriod { get; set; }

		/// <summary>
		/// Gets or sets the current past due balance for the plan as of the most recent payment installment.
		/// </summary>
		public long PastDueBalance { get; set; }

		/// <summary>
		/// Gets or sets the history of plan payments made to date.
		/// </summary>
		public PlanPaymentHistory History { get; set; }

		/// <summary>
		/// The schedule of payment installments in the plan.
		/// </summary>
		private PaymentSchedule _schedule;

		/// <summary>
		/// Gets or sets the schedule of payment installments in the plan.
		/// </summary>
		public PaymentSchedule Schedule
		{
			get { return _schedule; }
			set
			{
				_schedule = value;
				_schedule.GracePeriod = this.GracePeriod;
			}
		}

		/// <summary>
		/// Gets a collection of the payment installments in the plan.
		/// </summary>
		public IList<PaymentInstallment> Installments
		{
			get { return _schedule.Values; }
		}

		/// <summary>
		/// Gets a collection of the plan payments made to date.
		/// </summary>
		public IList<PlanPayment> Payments
		{
			get { return History.Values; }
		}

		/// <summary>
		/// Gets the collection of summaries of the plan payment schedule.
		/// </summary>
		public List<PaymentPlanSummary> Summaries { get; private set; }

		/// <summary>
		/// Gets the due date of the final payment in the plan.
		/// </summary>
		public override DateTime FinalPaymentDueDate
		{
			get { return _schedule.Count > 0 ? _schedule[_schedule.Count - 1].DueDate : base.FinalPaymentDueDate; }
		}

		/// <summary>
		/// Gets the remaining unpaid balance of penalties in the plan.
		/// </summary>
		public uint GetBalance()
		{
			return this.Total - this.History.PaymentTotal;
		}

		/// <summary>
		/// Computes the past due balance and amount paid totals for the plan.
		/// </summary>
		internal void ComputeBalances()
		{
			this.PastDueBalance = _schedule.GetPastDueBalance(this.History);
		}

		/// <summary>
		/// Generates summaries of the plan payment schedule.
		/// </summary>
		internal void Summarize()
		{
			List<PaymentPlanSummary> summaries = new List<PaymentPlanSummary>();
			PaymentPlanSummary summary = new PaymentPlanSummary()
			{
				PeriodPaymentAmount = 0
			};
			foreach (PaymentInstallment installment in this.Installments)
			{
				if (installment.AmountDue != summary.PeriodPaymentAmount)
				{
					summaries.Add(summary);
					summary = new PaymentPlanSummary()
					{
						FirstPaymentDate = installment.DueDate,
						PaymentCount = 1,
						PeriodPaymentAmount = installment.AmountDue
					};
				}
				else
				{
					summary.PaymentCount++;
					summary.FinalPaymentDueDate = installment.DueDate;
				}
			}
			summaries.Add(summary);
			// discard first empty summary instance
			summaries.RemoveAt(0);
			this.Summaries = summaries;
		}

		/// <summary>
		/// Retrieves the payment plan on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		public static PaymentPlan GetPaymentPlan(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetPaymentPlan(candidateID, electionCycle);
		}
	}
}
