using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PaymentInstallmentTest and is intended
    ///to contain all PaymentInstallmentTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PaymentInstallmentTest
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
		///A test for StartDate
		///</summary>
		[TestMethod()]
		public void StartDateTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.StartDate = expected;
			actual = target.StartDate;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Number
		///</summary>
		[TestMethod()]
		public void NumberTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			ushort actual;
			actual = target.Number;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for DueDate
		///</summary>
		[TestMethod()]
		public void DueDateTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.DueDate = expected;
			actual = target.DueDate;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for AmountPaid
		///</summary>
		[TestMethod()]
		public void AmountPaidTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			Nullable<uint> expected = new Nullable<uint>(); // TODO: Initialize to an appropriate value
			Nullable<uint> actual;
			target.AmountPaid = expected;
			actual = target.AmountPaid;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for AmountDue
		///</summary>
		[TestMethod()]
		public void AmountDueTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			uint expected = 0; // TODO: Initialize to an appropriate value
			uint actual;
			target.AmountDue = expected;
			actual = target.AmountDue;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IncludesDate
		///</summary>
		[TestMethod()]
		public void IncludesDateTest1()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IncludesDate(date);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IncludesDate
		///</summary>
		[TestMethod()]
		public void IncludesDateTest()
		{
			PaymentInstallment target = new PaymentInstallment(); // TODO: Initialize to an appropriate value
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte grace = 0; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IncludesDate(date, grace);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for PaymentInstallment Constructor
		///</summary>
		[TestMethod()]
		public void PaymentInstallmentConstructorTest1()
		{
			PaymentInstallment target = new PaymentInstallment();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for PaymentInstallment Constructor
		///</summary>
		[TestMethod()]
		public void PaymentInstallmentConstructorTest()
		{
			ushort number = 0; // TODO: Initialize to an appropriate value
			PaymentInstallment target = new PaymentInstallment(number);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
