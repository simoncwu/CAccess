using Cfb.CandidatePortal.Cmo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for AttachmentRepositoryTest and is intended
    ///to contain all AttachmentRepositoryTest Unit Tests
    ///</summary>
	[TestClass()]
	public class AttachmentRepositoryTest
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
		///A test for AttachmentRepository Constructor
		///</summary>
		[TestMethod()]
		public void AttachmentRepositoryConstructorTest1()
		{
			string name = string.Empty; // TODO: Initialize to an appropriate value
			string filePath = string.Empty; // TODO: Initialize to an appropriate value
			AttachmentRepository target = new AttachmentRepository(name, filePath);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}

		/// <summary>
		///A test for AttachmentRepository Constructor
		///</summary>
		[TestMethod()]
		public void AttachmentRepositoryConstructorTest()
		{
			string name = string.Empty; // TODO: Initialize to an appropriate value
			string filePath = string.Empty; // TODO: Initialize to an appropriate value
			string description = string.Empty; // TODO: Initialize to an appropriate value
			AttachmentRepository target = new AttachmentRepository(name, filePath, description);
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
