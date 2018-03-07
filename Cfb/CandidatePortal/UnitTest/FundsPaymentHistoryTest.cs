using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for FundsPaymentHistoryTest and is intended
    ///to contain all FundsPaymentHistoryTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FundsPaymentHistoryTest
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
		///A test for TotalPaymentAmount
		///</summary>
		[TestMethod()]
		public void TotalPaymentAmountTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			Decimal actual;
			actual = target.TotalPaymentAmount;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PaymentCount
		///</summary>
		[TestMethod()]
		public void PaymentCountTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			int actual;
			actual = target.PaymentCount;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Item
		///</summary>
		[TestMethod()]
		public void ItemTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			int index = 0; // TODO: Initialize to an appropriate value
			PublicFundsDetermination expected = null; // TODO: Initialize to an appropriate value
			PublicFundsDetermination actual;
			target[index] = expected;
			actual = target[index];
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Determinations
		///</summary>
		[TestMethod()]
		public void DeterminationsTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			List<PublicFundsDetermination> actual;
			actual = target.Determinations;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for System.Collections.IEnumerable.GetEnumerator
		///</summary>
		[TestMethod()]
		[DeploymentItem("Cfb.CandidatePortal.dll")]
		public void GetEnumeratorTest1()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			IEnumerable target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			IEnumerator expected = null; // TODO: Initialize to an appropriate value
			IEnumerator actual;
			actual = target.GetEnumerator();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetFundsPaymentHistory
		///</summary>
		[TestMethod()]
		public void GetFundsPaymentHistoryTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			CPProviders.DataProvider = new Cfb.CandidatePortal.ServiceModel.CPDataClient.CPDataProvider();
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			string candidateID = "DD"; // TODO: Initialize to an appropriate value
			string electionCycle = "2009"; // TODO: Initialize to an appropriate value
			PublicFundsHistory expected = null; // TODO: Initialize to an appropriate value
			PublicFundsHistory actual;
			actual = PublicFundsHistory.GetPublicFundsHistory(candidateID, electionCycle);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetEnumerator
		///</summary>
		[TestMethod()]
		public void GetEnumeratorTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity); // TODO: Initialize to an appropriate value
			IEnumerator<PublicFundsDetermination> expected = null; // TODO: Initialize to an appropriate value
			IEnumerator<PublicFundsDetermination> actual;
			actual = target.GetEnumerator();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for FundsPaymentHistory Constructor
		///</summary>
		[TestMethod()]
		public void FundsPaymentHistoryConstructorTest()
		{
			int capacity = 0; // TODO: Initialize to an appropriate value
			PublicFundsHistory target = new PublicFundsHistory(capacity);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
