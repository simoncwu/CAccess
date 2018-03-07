using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.Security;
using Cfb.Cfis.CampaignContacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cfb.CandidatePortal.UnitTest
{


    /// <summary>
    ///This is a test class for CmoPostedMessageNoticeTest and is intended
    ///to contain all CmoPostedMessageNoticeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CmoPostedMessageNoticeTest
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
        ///A test for SendFor
        ///</summary>
        [TestMethod()]
        public void SendForTest()
        {
            string cid = "811";
            int usersCount = 5;
            string googleAccount = "simoncwu";
            CmoMessage message = new CmoMessage(cid, 1)
            {
                Title = "Unit Test Title",
                Body = "Body for unit test",
                PostDate = DateTime.Now,
                Category = new CmoCategory(1) { Name = "Test Category" }
            };
            LoadCandidateEventHandler loadCandidate = (string id) =>
            {
                System.Threading.Thread.Sleep(10000);
                return new Candidate(id) { FirstName = "CFB", LastName = "Installation" };
            };
            var recipients = from i in Enumerable.Range(1, usersCount)
                             select new CPUser("testuser" + i)
                             {
                                 Cid = cid,
                                 DisplayName = "Test User " + i,
                                 Email = googleAccount + "+caccess" + i + "@gmail.com",
                                 Enabled = true,
                                 SourceType = EntityType.Generic
                             };
            CPProviders.SettingsProvider = new MockSettingsProvider();
            bool expected = true;
            bool actual;
            actual = CmoPostedMessageNotice.SendFor(message, loadCandidate, recipients);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SendForAsyncTest()
        {
            string cid = "811";
            int usersCount = 5;
            string email = "caccess@nyccfb.info";
            CmoMessage message = new CmoMessage(cid, 1)
            {
                Title = "Unit Test Title",
                Body = "Body for unit test",
                PostDate = DateTime.Now,
                Category = new CmoCategory(1) { Name = "Test Category" }
            };
            LoadCandidateEventHandler loadCandidate = (string id) =>
            {
                return new Candidate(id) { FirstName = "CFB", LastName = "Installation" };
            };
            var recipients = from i in Enumerable.Range(1, usersCount)
                             select new CPUser("testuser" + i)
                             {
                                 Cid = cid,
                                 DisplayName = "Test User " + i,
                                 Email = email,
                                 Enabled = true,
                                 SourceType = EntityType.Generic
                             };
            CPProviders.SettingsProvider = new MockSettingsProvider();
            bool expected = true;
            bool actual;
            Func<CmoMessage, LoadCandidateEventHandler, IEnumerable<CPUser>, bool> caller = CmoPostedMessageNotice.SendFor;
            IAsyncResult result = caller.BeginInvoke(message, loadCandidate, recipients, null, null);
            actual = caller.EndInvoke(result);
            Assert.AreEqual(expected, actual);
        }

        private class MockSettingsProvider : ISettingsProvider
        {
            #region ISettingsProvider Members

            public DateTime LastReminderDistributionDate
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string MessageSenderDisplayName
            {
                get
                {
                    return "CFB Notice";
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string MessageSenderEmail
            {
                get
                {
                    return "caccess@nyccfb.info";
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string AdministratorsGroupName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string AccountManagersGroupName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string AgencyName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string ApplicationName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string ApplicationUrl
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string AgencyPhoneNumber
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public byte MinimumPasswordLength
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string CandidatesSecurityGroupName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string MinimumElectionCycle
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            #endregion
        }
    }
}
