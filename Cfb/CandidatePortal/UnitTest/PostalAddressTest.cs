using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cfb.CandidatePortal.UnitTest
{


    /// <summary>
    ///This is a test class for PostalAddressTest and is intended
    ///to contain all PostalAddressTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PostalAddressTest
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
        ///A test for Zip
        ///</summary>
        [TestMethod()]
        public void ZipTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            ZipCode expected = null; // TODO: Initialize to an appropriate value
            ZipCode actual;
            target.Zip = expected;
            actual = target.Zip;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StreetNumber
        ///</summary>
        [TestMethod()]
        public void StreetNumberTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.StreetNumber = expected;
            actual = target.StreetNumber;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StreetName
        ///</summary>
        [TestMethod()]
        public void StreetNameTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.StreetName = expected;
            actual = target.StreetName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for State
        ///</summary>
        [TestMethod()]
        public void StateTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.State = expected;
            actual = target.State;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsValid;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for City
        ///</summary>
        [TestMethod()]
        public void CityTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.City = expected;
            actual = target.City;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Apartment
        ///</summary>
        [TestMethod()]
        public void ApartmentTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Apartment = expected;
            actual = target.Apartment;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddressLine1
        ///</summary>
        [TestMethod()]
        public void AddressLine1Test()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.AddressLine1 = expected;
            actual = target.AddressLine1;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            PostalAddress target = new PostalAddress(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PostalAddress Constructor
        ///</summary>
        [TestMethod()]
        public void PostalAddressConstructorTest()
        {
            PostalAddress target = new PostalAddress();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
