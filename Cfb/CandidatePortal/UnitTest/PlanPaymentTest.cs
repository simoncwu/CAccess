using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PlanPaymentTest and is intended
    ///to contain all PlanPaymentTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PlanPaymentTest
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
		///A test for Type
		///</summary>
		[TestMethod()]
		public void TypeTest()
		{
			PlanPayment target = new PlanPayment(); // TODO: Initialize to an appropriate value
			PaymentType expected = new PaymentType(); // TODO: Initialize to an appropriate value
			PaymentType actual;
			target.Type = expected;
			actual = target.Type;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Date
		///</summary>
		[TestMethod()]
		public void DateTest()
		{
			PlanPayment target = new PlanPayment(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.Date = expected;
			actual = target.Date;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CheckNumber
		///</summary>
		[TestMethod()]
		public void CheckNumberTest()
		{
			PlanPayment target = new PlanPayment(); // TODO: Initialize to an appropriate value
			ushort expected = 0; // TODO: Initialize to an appropriate value
			ushort actual;
			target.CheckNumber = expected;
			actual = target.CheckNumber;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Amount
		///</summary>
		[TestMethod()]
		public void AmountTest()
		{
			PlanPayment target = new PlanPayment(); // TODO: Initialize to an appropriate value
			uint expected = 0; // TODO: Initialize to an appropriate value
			uint actual;
			target.Amount = expected;
			actual = target.Amount;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PlanPayment Constructor
		///</summary>
		[TestMethod()]
		public void PlanPaymentConstructorTest()
		{
			PlanPayment target = new PlanPayment();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
