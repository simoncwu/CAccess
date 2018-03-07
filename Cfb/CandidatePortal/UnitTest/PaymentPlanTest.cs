using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PaymentPlanTest and is intended
    ///to contain all PaymentPlanTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PaymentPlanTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for Total
		///</summary>
		[TestMethod()]
		public void TotalTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			uint expected = 0; // TODO: Initialize to an appropriate value
			uint actual;
			target.Total = expected;
			actual = target.Total;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Summaries
		///</summary>
		[TestMethod()]
		public void SummariesTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			List<PaymentPlanSummary> actual;
			actual = target.Summaries;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Schedule
		///</summary>
		[TestMethod()]
		public void ScheduleTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			PaymentSchedule expected = null; // TODO: Initialize to an appropriate value
			PaymentSchedule actual;
			target.Schedule = expected;
			actual = target.Schedule;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Period
		///</summary>
		[TestMethod()]
		public void PeriodTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			PaymentPeriod expected = new PaymentPeriod(); // TODO: Initialize to an appropriate value
			PaymentPeriod actual;
			target.Period = expected;
			actual = target.Period;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Payments
		///</summary>
		[TestMethod()]
		public void PaymentsTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			IList<PlanPayment> actual;
			actual = target.Payments;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PastDueBalance
		///</summary>
		[TestMethod()]
		public void PastDueBalanceTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			long expected = 0; // TODO: Initialize to an appropriate value
			long actual;
			target.PastDueBalance = expected;
			actual = target.PastDueBalance;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Installments
		///</summary>
		[TestMethod()]
		public void InstallmentsTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			IList<PaymentInstallment> actual;
			actual = target.Installments;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for History
		///</summary>
		[TestMethod()]
		public void HistoryTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			PlanPaymentHistory expected = null; // TODO: Initialize to an appropriate value
			PlanPaymentHistory actual;
			target.History = expected;
			actual = target.History;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GracePeriod
		///</summary>
		[TestMethod()]
		public void GracePeriodTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			byte expected = 0; // TODO: Initialize to an appropriate value
			byte actual;
			target.GracePeriod = expected;
			actual = target.GracePeriod;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Summarize
		///</summary>
		[TestMethod()]
		public void SummarizeTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			target.Summarize();
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for GetPaymentPlan
		///</summary>
		[TestMethod()]
		public void GetPaymentPlanTest()
		{
			string candidateID = string.Empty; // TODO: Initialize to an appropriate value
			string electionCycle = string.Empty; // TODO: Initialize to an appropriate value
			PaymentPlan expected = null; // TODO: Initialize to an appropriate value
			PaymentPlan actual;
			actual = PaymentPlan.GetPaymentPlan(candidateID, electionCycle);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetBalance
		///</summary>
		[TestMethod()]
		public void GetBalanceTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			uint expected = 0; // TODO: Initialize to an appropriate value
			uint actual;
			actual = target.GetBalance();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ComputeBalances
		///</summary>
		[TestMethod()]
		public void ComputeBalancesTest()
		{
			PaymentPlan target = new PaymentPlan(); // TODO: Initialize to an appropriate value
			target.ComputeBalances();
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for PaymentPlan Constructor
		///</summary>
		[TestMethod()]
		public void PaymentPlanConstructorTest()
		{
			PaymentPlan target = new PaymentPlan();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
