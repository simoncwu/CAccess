using System;
using System.DirectoryServices.AccountManagement;
using Cfb.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cfb.CandidatePortal.UnitTest
{


	/// <summary>
	///This is a test class for UserTest and is intended
	///to contain all UserTest Unit Tests
	///</summary>
	[TestClass()]
	public class UserTest
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
		///A test for User Constructor
		///</summary>
		[TestMethod()]
		public void UserConstructorTest()
		{
			UserPrincipal principal = null; // TODO: Initialize to an appropriate value
			User target = new User(principal);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for User Constructor
		///</summary>
		[TestMethod()]
		public void UserConstructorTest1()
		{
			User target = new User();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for DisplayName
		///</summary>
		[TestMethod()]
		public void DisplayNameTest()
		{
			User target = new User(); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			target.DisplayName = expected;
			actual = target.DisplayName;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Email
		///</summary>
		[TestMethod()]
		public void EmailTest()
		{
			User target = new User(); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			target.Email = expected;
			actual = target.Email;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Guid
		///</summary>
		[TestMethod()]
		public void GuidTest()
		{
			User target = new User(); // TODO: Initialize to an appropriate value
			Nullable<Guid> actual;
			actual = target.Guid;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsAdHoc
		///</summary>
		[TestMethod()]
		public void IsAdHocTest()
		{
			User target = new User(); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsAdHoc;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Username
		///</summary>
		[TestMethod()]
		public void UsernameTest()
		{
			User target = new User(); // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			target.Username = expected;
			actual = target.Username;
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for IsMemberOf
		///</summary>
		[TestMethod()]
		public void IsMemberOfTest()
		{
			User target = new User(UserPrincipal.Current); // TODO: Initialize to an appropriate value
			string identityValue = "C-Access Administrators"; // TODO: Initialize to an appropriate value
			bool expected = true; // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsMemberOf(identityValue);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
