using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.Web
{
    /// <summary>
    /// Defines keys for caching data in session state.
    /// </summary>
    internal sealed class CacheKeys
    {
        /// <summary>
        /// Hidden constructor.
        /// </summary>
        private CacheKeys() { }

        /// <summary>
        /// The prefix for all cache keys.
        /// </summary>
        private const string CacheKeyPrefix = "CACHEKEY_";

        /// <summary>
        /// The cache key for current candidate ID.
        /// </summary>
        public const string Cid = CacheKeyPrefix + "Cid";

        /// <summary>
        /// The cache key for current election.
        /// </summary>
        public const string Election = CacheKeyPrefix + "Election";

        /// <summary>
        /// The cache key for reminder events.
        /// </summary>
        public const string ReminderEvents = CacheKeyPrefix + "ReminderEvents";

        /// <summary>
        /// The cache key for calendar events.
        /// </summary>
        public const string CalendarEvents = CacheKeyPrefix + "CalendarEvents";

        /// <summary>
        /// The cache key for post-election audit suspection status.
        /// </summary>
        public const string PESuspension = CacheKeyPrefix + "Suspension";

        /// <summary>
        /// The cache key for granted C-Access Security user rights.
        /// </summary>
        public const string UserRights = CacheKeyPrefix + "UserRights";

        /// <summary>
        /// The cache key for current candidate's active election cycles.
        /// </summary>
        public const string ElectionCycles = CacheKeyPrefix + "ElectionCycles";

        /// <summary>
        /// The cache key for current campaign's user account analysis.
        /// </summary>
        public const string AccountAnalysis = CacheKeyPrefix + "AccountAnalysis";

        /// <summary>
        /// The cache key for current user name.
        /// </summary>
        public const string UserName = CacheKeyPrefix + "UserName";

        /// <summary>
        /// The cache key for current user.
        /// </summary>
        public const string User = CacheKeyPrefix + "User";

        /// <summary>
        /// The cache key for display name.
        /// </summary>
        public const string DisplayName = CacheKeyPrefix + "DisplayName";

        /// <summary>
        /// The cache key for page errors.
        /// </summary>
        public const string PageError = CacheKeyPrefix + "PageError";

        /// <summary>
        /// The cache key for page results.
        /// </summary>
        public const string PageResult = CacheKeyPrefix + "PageResult";

        /// <summary>
        /// The cache key for SSO applications.
        /// </summary>
        public const string Applications = CacheKeyPrefix + "Applications";
    }
}
