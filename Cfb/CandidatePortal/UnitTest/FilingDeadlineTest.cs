using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for FilingDeadlineTest and is intended
    ///to contain all FilingDeadlineTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FilingDeadlineTest
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
		///A test for SubmissionReceivedDate
		///</summary>
		[TestMethod()]
		public void SubmissionReceivedDateTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
			Nullable<DateTime> actual;
			target.SubmissionReceivedDate = expected;
			actual = target.SubmissionReceivedDate;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for SubmissionReceived
		///</summary>
		[TestMethod()]
		public void SubmissionReceivedTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.SubmissionReceived;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for StatementNumber
		///</summary>
		[TestMethod()]
		public void StatementNumberTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			byte actual;
			actual = target.StatementNumber;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsUpcomingOutstanding
		///</summary>
		[TestMethod()]
		public void IsUpcomingOutstandingTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsUpcomingOutstanding;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsRequired
		///</summary>
		[TestMethod()]
		public void IsRequiredTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			target.IsRequired = expected;
			actual = target.IsRequired;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsMissing
		///</summary>
		[TestMethod()]
		public void IsMissingTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsMissing;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsLate
		///</summary>
		[TestMethod()]
		public void IsLateTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsLate;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Date
		///</summary>
		[TestMethod()]
		public void DateTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber); // TODO: Initialize to an appropriate value
			DateTime actual;
			actual = target.Date;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for FilingDeadline Constructor
		///</summary>
		[TestMethod()]
		public void FilingDeadlineConstructorTest()
		{
			DateTime date = new DateTime(); // TODO: Initialize to an appropriate value
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			FilingDeadline target = new FilingDeadline(date, statementNumber);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
