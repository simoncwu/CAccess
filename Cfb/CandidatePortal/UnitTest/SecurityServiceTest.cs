﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cfb.CandidatePortal.ServiceModel.CPSecurityService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cfb.CandidatePortal.UnitTest
{


	/// <summary>
	///This is a test class for SecurityServiceTest and is intended
	///to contain all SecurityServiceTest Unit Tests
	///</summary>
	[TestClass()]
	public class SecurityServiceTest
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
		///A test for GetAuthorizedElectionCycles
		///</summary>
		[TestMethod()]
		public void GetAuthorizedElectionCyclesTest()
		{
			SecurityService target = new SecurityService(); // TODO: Initialize to an appropriate value
			string userName = "cptest"; // TODO: Initialize to an appropriate value
			List<string> expected = new[] { "2009", "2013" }.ToList(); // TODO: Initialize to an appropriate value
			List<string> actual;
			actual = target.GetAuthorizedElectionCycles(userName);
			Assert.AreEqual(expected.Count, actual.Count);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
