using Cfb.CandidatePortal.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for ProfilePhotoHandlerTest and is intended
    ///to contain all ProfilePhotoHandlerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ProfilePhotoHandlerTest
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
		///A test for IsReusable
		///</summary>
		[TestMethod()]
		public void IsReusableTest()
		{
			ProfilePhotoHandler target = new ProfilePhotoHandler(); // TODO: Initialize to an appropriate value
			bool actual;
			actual = target.IsReusable;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ProcessRequest
		///</summary>
		[TestMethod()]
		public void ProcessRequestTest()
		{
			ProfilePhotoHandler target = new ProfilePhotoHandler(); // TODO: Initialize to an appropriate value
			HttpContext context = null; // TODO: Initialize to an appropriate value
			target.ProcessRequest(context);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for GetPhotoRepositoryPath
		///</summary>
		[TestMethod()]
		public void GetPhotoRepositoryPathTest()
		{
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = ProfilePhotoHandler.GetPhotoRepositoryPath();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetPhotoFilePath
		///</summary>
		[TestMethod()]
		public void GetPhotoFilePathTest1()
		{
			string path = string.Empty; // TODO: Initialize to an appropriate value
			string cid = string.Empty; // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = ProfilePhotoHandler.GetPhotoFilePath(path, cid);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for GetPhotoFilePath
		///</summary>
		[TestMethod()]
		public void GetPhotoFilePathTest()
		{
			string cid = string.Empty; // TODO: Initialize to an appropriate value
			string expected = string.Empty; // TODO: Initialize to an appropriate value
			string actual;
			actual = ProfilePhotoHandler.GetPhotoFilePath(cid);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CustomPhotoExists
		///</summary>
		[TestMethod()]
		public void CustomPhotoExistsTest()
		{
			string cid = string.Empty; // TODO: Initialize to an appropriate value
			bool expected = false; // TODO: Initialize to an appropriate value
			bool actual;
			actual = ProfilePhotoHandler.CustomPhotoExists(cid);
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for ProfilePhotoHandler Constructor
		///</summary>
		[TestMethod()]
		public void ProfilePhotoHandlerConstructorTest()
		{
			ProfilePhotoHandler target = new ProfilePhotoHandler();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
