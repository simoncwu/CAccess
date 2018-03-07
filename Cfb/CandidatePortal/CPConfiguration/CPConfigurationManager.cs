using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Cfb.CandidatePortal.CPConfiguration
{
    /// <summary>
    /// Provides access to C-Access Candidate Portal application-wide settings from the application configuration file.
    /// </summary>
    public static class CPConfigurationManager
    {
        /// <summary>
        /// Path to C-Access configuration settings section in the configuration file.
        /// </summary>
        private const string RootSectionPath = "cAccess";

        /// <summary>
        /// Gets the configuration section for Campaign Messages Online settings.
        /// </summary>
        public static CmoConfigurationSection Cmo
        {
            get { return ConfigurationManager.GetSection(ToFullPath(CmoConfigurationSection.SectionName)) as CmoConfigurationSection; }
        }

        /// <summary>
        /// Gets the configuration section for C-Access user profile settings.
        /// </summary>
        public static CPProfileSection Profile
        {
            get { return ConfigurationManager.GetSection(ToFullPath(CPProfileSection.SectionName)) as CPProfileSection; }
        }

        /// <summary>
        /// Converts a child path for a C-Access configuration section to a full root-relative path.
        /// </summary>
        /// <param name="childPath">The child path to convert.</param>
        /// <returns>A full root-relative path for <paramref name="childPath"/>.</returns>
        public static string ToFullPath(string childPath)
        {
            return string.Format("{0}/{1}", RootSectionPath, childPath);
        }
    }
}
