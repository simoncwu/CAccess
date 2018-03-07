using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for FinancialSummaryTest and is intended
    ///to contain all FinancialSummaryTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FinancialSummaryTest
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
		///A test for PublicFundsReturned
		///</summary>
		[TestMethod()]
		public void PublicFundsReturnedTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.PublicFundsReturned = expected;
			actual = target.PublicFundsReturned;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PublicFundsReceived
		///</summary>
		[TestMethod()]
		public void PublicFundsReceivedTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.PublicFundsReceived = expected;
			actual = target.PublicFundsReceived;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PrivateFundsReceived
		///</summary>
		[TestMethod()]
		public void PrivateFundsReceivedTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.PrivateFundsReceived = expected;
			actual = target.PrivateFundsReceived;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for OutstandingBills
		///</summary>
		[TestMethod()]
		public void OutstandingBillsTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.OutstandingBills = expected;
			actual = target.OutstandingBills;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for NetExpenditures
		///</summary>
		[TestMethod()]
		public void NetExpendituresTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.NetExpenditures = expected;
			actual = target.NetExpenditures;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for NetContributions
		///</summary>
		[TestMethod()]
		public void NetContributionsTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.NetContributions = expected;
			actual = target.NetContributions;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for MiscellaneousReceipts
		///</summary>
		[TestMethod()]
		public void MiscellaneousReceiptsTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.MiscellaneousReceipts = expected;
			actual = target.MiscellaneousReceipts;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for MatchingClaims
		///</summary>
		[TestMethod()]
		public void MatchingClaimsTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.MatchingClaims = expected;
			actual = target.MatchingClaims;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for LoansReceived
		///</summary>
		[TestMethod()]
		public void LoansReceivedTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.LoansReceived = expected;
			actual = target.LoansReceived;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for LoansPaid
		///</summary>
		[TestMethod()]
		public void LoansPaidTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.LoansPaid = expected;
			actual = target.LoansPaid;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for LastStatementSubmitted
		///</summary>
		[TestMethod()]
		public void LastStatementSubmittedTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			byte expected = 0; // TODO: Initialize to an appropriate value
			byte actual;
			target.LastStatementSubmitted = expected;
			actual = target.LastStatementSubmitted;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ContributorCount
		///</summary>
		[TestMethod()]
		public void ContributorCountTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			int expected = 0; // TODO: Initialize to an appropriate value
			int actual;
			target.ContributorCount = expected;
			actual = target.ContributorCount;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CampaignSpending
		///</summary>
		[TestMethod()]
		public void CampaignSpendingTest()
		{
			FinancialSummary target = new FinancialSummary(); // TODO: Initialize to an appropriate value
			Decimal expected = new Decimal(); // TODO: Initialize to an appropriate value
			Decimal actual;
			target.CampaignSpending = expected;
			actual = target.CampaignSpending;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetFinancialSummary
		///</summary>
		[TestMethod()]
		public void GetFinancialSummaryTest()
		{
			string candidateID = string.Empty; // TODO: Initialize to an appropriate value
			string electionCycle = string.Empty; // TODO: Initialize to an appropriate value
			FinancialSummary expected = null; // TODO: Initialize to an appropriate value
			FinancialSummary actual;
			actual = FinancialSummary.GetFinancialSummary(candidateID, electionCycle);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for FinancialSummary Constructor
		///</summary>
		[TestMethod()]
		[DeploymentItem("Cfb.CandidatePortal.dll")]
		public void FinancialSummaryConstructorTest()
		{
			FinancialSummary target = new FinancialSummary();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
