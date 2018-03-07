using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PaymentPlanSummaryTest and is intended
    ///to contain all PaymentPlanSummaryTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PaymentPlanSummaryTest
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
		///A test for PeriodPaymentAmount
		///</summary>
		[TestMethod()]
		public void PeriodPaymentAmountTest()
		{
			PaymentPlanSummary target = new PaymentPlanSummary(); // TODO: Initialize to an appropriate value
			uint expected = 0; // TODO: Initialize to an appropriate value
			uint actual;
			target.PeriodPaymentAmount = expected;
			actual = target.PeriodPaymentAmount;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PaymentCount
		///</summary>
		[TestMethod()]
		public void PaymentCountTest()
		{
			PaymentPlanSummary target = new PaymentPlanSummary(); // TODO: Initialize to an appropriate value
			ushort expected = 0; // TODO: Initialize to an appropriate value
			ushort actual;
			target.PaymentCount = expected;
			actual = target.PaymentCount;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for FirstPaymentDate
		///</summary>
		[TestMethod()]
		public void FirstPaymentDateTest()
		{
			PaymentPlanSummary target = new PaymentPlanSummary(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.FirstPaymentDate = expected;
			actual = target.FirstPaymentDate;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for FinalPaymentDueDate
		///</summary>
		[TestMethod()]
		public void FinalPaymentDueDateTest()
		{
			PaymentPlanSummary target = new PaymentPlanSummary(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.FinalPaymentDueDate = expected;
			actual = target.FinalPaymentDueDate;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PaymentPlanSummary Constructor
		///</summary>
		[TestMethod()]
		public void PaymentPlanSummaryConstructorTest()
		{
			PaymentPlanSummary target = new PaymentPlanSummary();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
