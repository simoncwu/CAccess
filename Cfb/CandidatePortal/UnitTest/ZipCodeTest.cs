using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cfb.CandidatePortal.UnitTest
{


    /// <summary>
    ///This is a test class for ZipCodeTest and is intended
    ///to contain all ZipCodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZipCodeTest
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
        ///A test for Zip5
        ///</summary>
        [TestMethod()]
        public void Zip5Test()
        {
            ZipCode target = new ZipCode("10006"); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Zip5;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Zip4
        ///</summary>
        [TestMethod()]
        public void Zip4Test()
        {
            ZipCode target = new ZipCode("10006"); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Zip4;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsZip4
        ///</summary>
        [TestMethod()]
        public void IsZip4Test()
        {
            ZipCode target = new ZipCode("10006"); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsZip4;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            ZipCode target = new ZipCode("10006"); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValid;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ZipCode target = new ZipCode("10006"); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ZipCode Constructor
        ///</summary>
        [TestMethod()]
        public void ZipCodeConstructorTest()
        {
            string zip = string.Empty; // TODO: Initialize to an appropriate value
            ZipCode target = new ZipCode(zip);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
