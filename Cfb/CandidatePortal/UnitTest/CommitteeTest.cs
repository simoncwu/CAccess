using Cfb.CandidatePortal;
using Cfb.Cfis.CampaignContacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cfb.CandidatePortal.UnitTest
{


    /// <summary>
    ///This is a test class for CommitteeTest and is intended
    ///to contain all CommitteeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommitteeTest
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
        ///A test for WebsiteUrl
        ///</summary>
        [TestMethod()]
        public void WebsiteUrlTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.WebsiteUrl = expected;
            actual = target.WebsiteUrl;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TerminationDate
        ///</summary>
        [TestMethod()]
        public void TerminationDateTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.TerminationDate = expected;
            actual = target.TerminationDate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MailingAdress
        ///</summary>
        [TestMethod()]
        public void MailingAdressTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id); // TODO: Initialize to an appropriate value
            PostalAddress expected = null; // TODO: Initialize to an appropriate value
            PostalAddress actual;
            target.MailingAdress = expected;
            actual = target.MailingAdress;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ID
        ///</summary>
        [TestMethod()]
        public void IDTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id); // TODO: Initialize to an appropriate value
            char actual;
            actual = target.ID;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BoeDate
        ///</summary>
        [TestMethod()]
        public void BoeDateTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id); // TODO: Initialize to an appropriate value
            Nullable<DateTime> expected = new Nullable<DateTime>(); // TODO: Initialize to an appropriate value
            Nullable<DateTime> actual;
            target.BoeDate = expected;
            actual = target.BoeDate;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Committee Constructor
        ///</summary>
        [TestMethod()]
        public void CommitteeConstructorTest()
        {
            char id = '\0'; // TODO: Initialize to an appropriate value
            Committee target = new Committee(id);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
