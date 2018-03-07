using System;
using System.Configuration;
using System.Web;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient;

namespace Cfb.CandidatePortal.Web
{
    /// <summary>
    /// Provides access to Candidate Portal application-wide settings.
    /// </summary>
    public static class CPApplication
    {
        #region Application Cache Keys

        /// <summary>
        /// The application cache key for retrieving the URL of the web site.
        /// </summary>
        private const string SiteUrlKey = "CPSiteUrlApplicationCacheKey";

        /// <summary>
        /// The application cache key for retrieving the contact e-mail address for Auditing and Account department.
        /// </summary>
        private const string AuditEmailKey = "CPAuditEmailApplicationCacheKey";

        /// <summary>
        /// The application cache key for retrieving the contact e-mail address for Candidate Services Unit.
        /// </summary>
        private const string CsuEmailKey = "CPCsuEmailApplicationCacheKey";

        /// <summary>
        /// The application cache key for retrieving the general contact e-mail address for the web site.
        /// </summary>
        private const string GeneralEmailKey = "CPGeneralEmailApplicationCacheKey";

        /// <summary>
        /// The application cache key for retrieving information about elections.
        /// </summary>
        private const string ElectionsKey = "CPElectionsApplicationCacheKey";

        /// <summary>
        /// The application cache key for retrieving the contact e-mail address for audit extension requests.
        /// </summary>
        private const string AuditExtensionRequestsEmailKey = "CPAuditExtensionRequestsCacheKey";

        #endregion

        /// <summary>
        /// Gets or sets the current ASP.NET web application.
        /// </summary>
        public static HttpApplicationState Application { get; set; }

        /// <summary>
        /// Gets the URL of the web site.
        /// </summary>
        public static string SiteUrl
        {
            get { return CPApplication.Application[SiteUrlKey] as string; }
        }

        /// <summary>
        /// Gets the contact e-mail address for Auditing and Account department.
        /// </summary>
        public static string AuditEmail
        {
            get { return CPApplication.Application[AuditEmailKey] as string; }
        }

        /// <summary>
        /// Gets the contact e-mail address for Candidate Services Unit.
        /// </summary>
        public static string CsuEmail
        {
            get { return CPApplication.Application[CsuEmailKey] as string; }
        }

        /// <summary>
        /// Gets the general contact e-mail address for the web site.
        /// </summary>
        public static string GeneralEmail
        {
            get { return CPApplication.Application[GeneralEmailKey] as string; }
        }

        /// <summary>
        /// Gets a collection of information about elections.
        /// </summary>
        public static Elections Elections
        {
            get
            {
                Elections elections = CPApplication.Application[ElectionsKey] as Elections;
                if (elections == null)
                    elections = Refresh(ElectionsKey) as Elections;
                return elections;
            }
        }

        /// <summary>
        /// Gets the contact e-mail address for audit extension requests.
        /// </summary>
        public static string AuditExtensionRequestsEmail
        {
            get { return CPApplication.Application[AuditExtensionRequestsEmailKey] as string; }
        }

        /// <summary>
        /// Gets an election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to get.</param>
        /// <returns>An <see cref="Election"/> containing information about the requested election.</returns>
        public static Election GetElection(string electionCycle)
        {
            // BUGFIX #19: reload election cycles from database if requested election is not found to account for an outdated cache
            if (electionCycle == null)
                return null;
            Election e = Elections[electionCycle];
            if (e == null)
            {
                Refresh(ElectionsKey);
                e = Elections[electionCycle];
                if (e == null)
                    e = new Election(electionCycle);
            }
            return e;
        }

        /// <summary>
        /// Refreshes an object in the application cache.
        /// </summary>
        /// <param name="cacheKey">The cache key of the object to reload.</param>
        private static object Refresh(string cacheKey)
        {
            switch (cacheKey)
            {
                case ElectionsKey:
                    return CPApplication.Application[cacheKey] = Elections.GetElections(CPProviders.SettingsProvider.MinimumElectionCycle);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes <see cref="CPApplication"/> with an <see cref="HttpApplicationState"/>.
        /// </summary>
        /// <param name="application">The <see cref="HttpApplicationState"/> to associate with.</param>
        public static void Initialize(HttpApplicationState application, IDataProvider dataProvider = null, ICmoDataProvider cmoProvider = null, ISettingsProvider settingsProvider = null, ISecurityProvider securityProvider = null)
        {
            CPApplication.Application = application;
            CPApplication.Application[SiteUrlKey] = ConfigurationManager.AppSettings["CPSiteUrl"];
            CPApplication.Application[AuditEmailKey] = ConfigurationManager.AppSettings["CPAuditEmail"];
            CPApplication.Application[CsuEmailKey] = ConfigurationManager.AppSettings["CPCsuEmail"];
            CPApplication.Application[GeneralEmailKey] = ConfigurationManager.AppSettings["CPGeneralEmail"];
            CPApplication.Application[AuditExtensionRequestsEmailKey] = ConfigurationManager.AppSettings["CPAuditExtensionRequestsEmail"];
            // initialize providers
            CPDataProvider defaultProvider = new CPDataProvider();
            GC.KeepAlive(CPProviders.DataProvider = dataProvider ?? defaultProvider);
            GC.KeepAlive(CmoProviders.DataProvider = cmoProvider ?? defaultProvider);
            GC.KeepAlive(CPProviders.SettingsProvider = settingsProvider ?? new CPSettingsProvider());
            GC.KeepAlive(CPSecurity.Provider = securityProvider ?? new SecurityProvider());
        }

        /// <summary>
        /// Releases resources consumed by the application.
        /// </summary>
        /// <param name="application"></param>
        public static void Dispose()
        {
            var provider = CPProviders.DataProvider as IDisposable;
            if (provider != null)
                provider.Dispose();
        }
    }
}
