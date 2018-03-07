using Cfb.CandidatePortal.Cmo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for CmoTest and is intended
    ///to contain all CmoTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CmoTest
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
		///A test for Repository
		///</summary>
		[TestMethod()]
		public void RepositoryTest()
		{
			AttachmentRepository actual;
			actual = Cmo.Cmo.Repository;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for Repositories
		///</summary>
		[TestMethod()]
		public void RepositoriesTest()
		{
			AttachmentRepositoryCollection actual;
			actual = Cmo.Cmo.Repositories;
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
