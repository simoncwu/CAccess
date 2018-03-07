using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.CPSettings;
using Cfb.DirectoryServices;

namespace Cfb.Camp.Settings
{
    /// <summary>
    /// Provides access to application-wide settings.
    /// </summary>
    public static class SettingsManager
    {
        /// <summary>
        /// Gets the global settings provider.
        /// </summary>
        public static ISettingsProvider GlobalSettings
        {
            get { return CPProviders.SettingsProvider; }
        }

        /// <summary>
        /// Gets or sets the default election cycle context for CAMP.
        /// </summary>
        public static string DefaultElectionCycle
        {
            get
            {
                string value = Properties.Settings.Default.DefaultElectionCycle;
                if (string.IsNullOrEmpty(value))
                {
                    SettingsManager.DefaultElectionCycle = value = Elections.GetElections(CPProviders.SettingsProvider.MinimumElectionCycle).GetCurrentElectionCycle();
                }
                return value;
            }
            set
            {
                Properties.Settings.Default.DefaultElectionCycle = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Gets the minimum application width in pixels.
        /// </summary>
        public static int MinApplicationWidth
        {
            get { return Properties.Settings.Default.MinCampWidth; }
        }

        /// <summary>
        /// Gets the minimum application height in pixels.
        /// </summary>
        public static int MinApplicationHeight
        {
            get { return Properties.Settings.Default.MinCampHeight; }
        }

        /// <summary>
        /// Whether or not Administrator mode is enabled.
        /// </summary>
        private static bool _adminstratorModeEnabled;

        /// <summary>
        /// Gets whether or not Administrator mode is enabled.
        /// </summary>
        public static bool AdministratorModeEnabled
        {
            get { return _adminstratorModeEnabled; }
        }

        /// <summary>
        /// The current user under which the thread is running.
        /// </summary>
        private static User _currentUser;

        /// <summary>
        /// Gets the current user under which the thread is running.
        /// </summary>
        public static User CurrentUser
        {
            get { return _currentUser; }
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static SettingsManager()
        {
        }

        /// <summary>
        /// Initializes the settings manager for subsequent use.
        /// </summary>
        public static void Initialize()
        {
            _currentUser = User.Current;
            if (_currentUser != null)
            {
                _adminstratorModeEnabled = _currentUser.IsMemberOf(CPProviders.SettingsProvider.AdministratorsGroupName);
            }
        }
    }
}
