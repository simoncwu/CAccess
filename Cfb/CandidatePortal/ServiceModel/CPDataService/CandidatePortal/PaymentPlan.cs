using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.PaymentPlanTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves the payment plan on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		PaymentPlan GetPaymentPlan(string candidateID, string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves the payment plan on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		public PaymentPlan GetPaymentPlan(string candidateID, string electionCycle)
		{
			using (PaymentPlanTds ds = new PaymentPlanTds())
			{
				using (PaymentPlanTableAdapter ta = new PaymentPlanTableAdapter())
				{
					ta.Fill(ds.PaymentPlan, candidateID, electionCycle);
				}
				PaymentPlan plan;
				foreach (PaymentPlanTds.PaymentPlanRow row in ds.PaymentPlan.Rows)
				{
					// payment schedule
					using (PaymentScheduleTableAdapter ta = new PaymentScheduleTableAdapter())
					{
						ta.Fill(ds.PaymentSchedule, candidateID, electionCycle);
					}
					// payment history
					using (PlanPaymentsTableAdapter ta = new PlanPaymentsTableAdapter())
					{
						ta.Fill(ds.PlanPayments, candidateID, electionCycle);
					}
					plan = new PaymentPlan()
					{
						// basic plan info
						FirstPaymentDate = row.FirstPaymentDate,
						Total = Convert.ToUInt32(row.TotalAmount),
						PaymentCount = Convert.ToUInt16(row.Installments),
						Period = CPConvert.ToPaymentPeriod(row.PeriodTypeCode.Trim()),
						PeriodPaymentAmount = Convert.ToUInt32(row.PeriodPaymentAmount),
						GracePeriod = Convert.ToByte(row.GracePeriod),
						// payment schedule
						Schedule = ParsePaymentSchedule(ds),
						// payment history
						History = ParsePlanPaymentHistory(ds)
					};
					// balances
					plan.ComputeBalances();
					// summaries
					plan.Summarize();
					return plan;
				}
			}
			return null;
		}

		/// <summary>
		/// Parses a typed data set of payment installments into a <see cref="PaymentSchedule"/> collection.
		/// </summary>
		/// <param name="ds">The typed data set to parse.</param>
		/// <returns>A collection representing the data contained in the data set.</returns>
		private PaymentSchedule ParsePaymentSchedule(PaymentPlanTds ds)
		{
			PaymentSchedule schedule = new PaymentSchedule(ds.PaymentSchedule.Count);
			foreach (PaymentPlanTds.PaymentScheduleRow row in ds.PaymentSchedule.Rows)
			{
				schedule.Add(new PaymentInstallment(Convert.ToUInt16(row.Number))
				{
					AmountDue = Convert.ToUInt32(row.Amount),
					DueDate = row.DueDate.Date,
					AmountPaid = null,
				});
			}
			// TODO: what the heck is up with start date?
			DateTime startDate = DateTime.MinValue.Date;
			foreach (PaymentInstallment installment in schedule.Values)
			{
				installment.StartDate = startDate;
				startDate = installment.DueDate.AddDays(1);
			}
			return schedule;
		}

		/// <summary>
		/// Parses a typed data set of plan payments into a <see cref="PlanPaymentHistory"/> collection.
		/// </summary>
		/// <param name="ds">The typed data set to parse.</param>
		/// <returns>A collection representing the data contained in the data set.</returns>
		private PlanPaymentHistory ParsePlanPaymentHistory(PaymentPlanTds ds)
		{
			PlanPaymentHistory history = new PlanPaymentHistory(ds.PlanPayments.Count);
			foreach (PaymentPlanTds.PlanPaymentsRow row in ds.PlanPayments.Rows)
			{
				ushort checkNumber;
				if (ushort.TryParse(row.CheckNumber, out checkNumber))
				{
					history.Add(new PlanPayment()
					{
						Amount = Convert.ToUInt32(row.Amount),
						CheckNumber = checkNumber,
						Date = row.Date,
						Type = CPConvert.ToPaymentType(row.TransactionCode.Trim())
					});
				}
			}
			return history;
		}
	}
}
