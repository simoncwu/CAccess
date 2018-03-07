using Cfb.CandidatePortal.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cfb.CandidatePortal;
using System.Collections.Generic;

namespace Cfb.CandidatePortal.UnitTest
{
	
	
	/// <summary>
	///This is a test class for AnnouncementsClientTest and is intended
	///to contain all AnnouncementsClientTest Unit Tests
	///</summary>
	[TestClass()]
	public class AnnouncementsClientTest
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
		///A test for GetAnnouncement
		///</summary>
		[TestMethod()]
		public void GetAnnouncementTest()
		{
			string id = string.Empty; // TODO: Initialize to an appropriate value
			Announcement expected = null; // TODO: Initialize to an appropriate value
			Announcement actual;
			actual = AnnouncementsClient.GetAnnouncement(id);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetAnnouncements
		///</summary>
		[TestMethod()]
		public void GetAnnouncementsTest()
		{
			string electionCycle = string.Empty; // TODO: Initialize to an appropriate value
			IEnumerable<Announcement> expected = null; // TODO: Initialize to an appropriate value
			IEnumerable<Announcement> actual;
			actual = AnnouncementsClient.GetAnnouncements(electionCycle);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetFilingDateAnnouncement
		///</summary>
		[TestMethod()]
		public void GetFilingDateAnnouncementTest()
		{
			byte statementNumber = 0; // TODO: Initialize to an appropriate value
			string electionCycle = string.Empty; // TODO: Initialize to an appropriate value
			Announcement expected = null; // TODO: Initialize to an appropriate value
			Announcement actual;
			actual = AnnouncementsClient.GetFilingDateAnnouncement(statementNumber, electionCycle);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
