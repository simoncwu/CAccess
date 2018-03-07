using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cfb.CandidatePortal.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PhoneNumberTest and is intended
    ///to contain all PhoneNumberTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhoneNumberTest
    {
        private const string TestPhoneNumber = "2124091800";

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
        ///A test for Number
        ///</summary>
        [TestMethod()]
        public void NumberTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Number;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValid;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Extension
        ///</summary>
        [TestMethod()]
        public void ExtensionTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Extension = expected;
            actual = target.Extension;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AreaCode
        ///</summary>
        [TestMethod()]
        public void AreaCodeTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.AreaCode;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PhoneNumber Constructor
        ///</summary>
        [TestMethod()]
        public void PhoneNumberConstructorTest1()
        {
            string number = string.Empty; // TODO: Initialize to an appropriate value
            PhoneNumber target = new PhoneNumber(number);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for PhoneNumber Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Cfb.CandidatePortal.dll")]
        public void PhoneNumberConstructorTest()
        {
            PhoneNumber target = new PhoneNumber(TestPhoneNumber);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
