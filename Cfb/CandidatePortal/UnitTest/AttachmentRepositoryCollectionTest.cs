using Cfb.CandidatePortal.Cmo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration.Provider;

namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for AttachmentRepositoryCollectionTest and is intended
    ///to contain all AttachmentRepositoryCollectionTest Unit Tests
    ///</summary>
	[TestClass()]
	public class AttachmentRepositoryCollectionTest
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
		///A test for Item
		///</summary>
		[TestMethod()]
		public void ItemTest()
		{
			AttachmentRepositoryCollection target = new AttachmentRepositoryCollection(); // TODO: Initialize to an appropriate value
			string name = string.Empty; // TODO: Initialize to an appropriate value
			AttachmentRepository actual;
			actual = target[name];
			Assert.Inconclusive("Verify the correctness of this test method.");
		}

		/// <summary>
		///A test for CopyTo
		///</summary>
		[TestMethod()]
		public void CopyToTest()
		{
			AttachmentRepositoryCollection target = new AttachmentRepositoryCollection(); // TODO: Initialize to an appropriate value
			AttachmentRepository[] array = null; // TODO: Initialize to an appropriate value
			int index = 0; // TODO: Initialize to an appropriate value
			target.CopyTo(array, index);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for Add
		///</summary>
		[TestMethod()]
		public void AddTest()
		{
			AttachmentRepositoryCollection target = new AttachmentRepositoryCollection(); // TODO: Initialize to an appropriate value
			ProviderBase provider = null; // TODO: Initialize to an appropriate value
			target.Add(provider);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}

		/// <summary>
		///A test for AttachmentRepositoryCollection Constructor
		///</summary>
		[TestMethod()]
		public void AttachmentRepositoryCollectionConstructorTest()
		{
			AttachmentRepositoryCollection target = new AttachmentRepositoryCollection();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
