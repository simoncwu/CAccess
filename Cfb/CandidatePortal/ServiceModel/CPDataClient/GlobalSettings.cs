using System;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// Provides access to global C-Access settings data by means of WCF client-server communication.
    /// </summary>
    partial class CPDataProvider
    {
        /// <summary>
        /// Retrieves a global C-Access setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to retrieve.</param>
        /// <returns>The value of the setting if found; otherwise, null.</returns>
        public string GetGlobalSetting(string settingName)
        {
            this.EnsureClientState();
            return _client.GetGlobalSetting(settingName);
        }

        /// <summary>
        /// Saves a global C-Access setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to save.</param>
        /// <param name="value">The desired value of the setting.</param>
        public void SetGlobalSetting(string settingName, string value)
        {
            this.EnsureClientState();
            _client.SetGlobalSetting(settingName, value);
        }
    }
}