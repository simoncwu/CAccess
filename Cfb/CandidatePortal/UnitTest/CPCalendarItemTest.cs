using Cfb.CandidatePortal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CPCalendarItemTest and is intended
    ///to contain all CPCalendarItemTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CPCalendarItemTest
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
		///A test for LastUpdated
		///</summary>
		[TestMethod()]
		public void LastUpdatedTest()
		{
			CPCalendarItem target = new CPCalendarItem(); // TODO: Initialize to an appropriate value
			DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime actual;
			target.LastUpdated = expected;
			actual = target.LastUpdated;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsUpcoming
		///</summary>
		[TestMethod()]
		public void IsUpcomingTest()
		{
			CPCalendarItem target = new CPCalendarItem(); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsUpcoming;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Date
		///</summary>
		[TestMethod()]
		public void DateTest()
		{
			CPCalendarItem target = new CPCalendarItem(); // TODO: Initialize to an appropriate value
			DateTime actual;
			actual = target.Date;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CalendarItemType
		///</summary>
		[TestMethod()]
		public void CalendarItemTypeTest()
		{
			CPCalendarItem target = new CPCalendarItem(); // TODO: Initialize to an appropriate value
			CPCalendarItemType actual;
			actual = target.CalendarItemType;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CPCalendarItem Constructor
		///</summary>
		[TestMethod()]
		public void CPCalendarItemConstructorTest4()
		{
			DateTime start = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime end = new DateTime(); // TODO: Initialize to an appropriate value
			CPCalendarItem target = new CPCalendarItem(start, end);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for CPCalendarItem Constructor
		///</summary>
		[TestMethod()]
		public void CPCalendarItemConstructorTest3()
		{
			CPCalendarItem target = new CPCalendarItem();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for CPCalendarItem Constructor
		///</summary>
		[TestMethod()]
		public void CPCalendarItemConstructorTest2()
		{
			DateTime dt = new DateTime(); // TODO: Initialize to an appropriate value
			CPCalendarItem target = new CPCalendarItem(dt);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for CPCalendarItem Constructor
		///</summary>
		[TestMethod()]
		public void CPCalendarItemConstructorTest1()
		{
			DateTime start = new DateTime(); // TODO: Initialize to an appropriate value
			DateTime end = new DateTime(); // TODO: Initialize to an appropriate value
			CPCalendarItemType type = new CPCalendarItemType(); // TODO: Initialize to an appropriate value
			CPCalendarItem target = new CPCalendarItem(start, end, type);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for CPCalendarItem Constructor
		///</summary>
		[TestMethod()]
		public void CPCalendarItemConstructorTest()
		{
			DateTime dt = new DateTime(); // TODO: Initialize to an appropriate value
			CPCalendarItemType type = new CPCalendarItemType(); // TODO: Initialize to an appropriate value
			CPCalendarItem target = new CPCalendarItem(dt, type);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
