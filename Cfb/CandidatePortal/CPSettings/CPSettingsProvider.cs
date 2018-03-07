using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal;

namespace Cfb.CandidatePortal.CPSettings
{
    /// <summary>
    /// Provides access to global C-Access settings data.
    /// </summary>
    public class CPSettingsProvider : ISettingsProvider
    {
        private Cfb.CandidatePortal.CPSettings.Properties.Settings _settings = Properties.Settings.Default;

        #region ISettingsProvider Members

        /// <summary>
        /// Gets or sets the date when reminder e-mails were last distributed.
        /// </summary>
        public DateTime LastReminderDistributionDate
        {
            get
            {
                long binary;
                if (long.TryParse(CPProviders.DataProvider.GetGlobalSetting(_settings.LastReminderDistributionDate), out binary))
                {
                    try
                    {
                        return DateTime.FromBinary(binary);
                    }
                    catch (ArgumentException)
                    {
                        return DateTime.MinValue;
                    }
                }
                return DateTime.MinValue;
            }
            set
            {
                CPProviders.DataProvider.SetGlobalSetting(_settings.LastReminderDistributionDate, value.ToBinary().ToString());
            }
        }

        /// <summary>
        /// Gets or sets the outgoing e-mail message sender display name.
        /// </summary>
        public string MessageSenderDisplayName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.MessageSenderDisplayName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.MessageSenderDisplayName, value); }
        }

        /// <summary>
        /// Gets or sets the outgoing e-mail message sender address.
        /// </summary>
        public string MessageSenderEmail
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.MessageSenderEmail); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.MessageSenderEmail, value); }
        }

        /// <summary>
        /// Gets or sets the name of the C-Access Administrators security group.
        /// </summary>
        public string AdministratorsGroupName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.AdministratorsGroupName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.AdministratorsGroupName, value); }
        }

        /// <summary>
        /// Gets or sets the name of the C-Access Account Managers security group.
        /// </summary>
        public string AccountManagersGroupName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.AccountManagersGroupName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.AccountManagersGroupName, value); }
        }

        /// <summary>
        /// Gets or sets the name of the CFB agency.
        /// </summary>
        public string AgencyName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.AgencyName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.AgencyName, value); }
        }

        /// <summary>
        /// Gets or sets the name of the C-Access candidate portal application.
        /// </summary>
        public string ApplicationName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.ApplicationName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.ApplicationName, value); }
        }

        /// <summary>
        /// Gets or sets the URL of the C-Access candidate portal application.
        /// </summary>
        public string ApplicationUrl
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.ApplicationUrl); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.ApplicationUrl, value); }
        }

        /// <summary>
        /// Gets or sets the contact phone number of the CFB agency.
        /// </summary>
        public string AgencyPhoneNumber
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.AgencyPhoneNumber); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.AgencyPhoneNumber, value); }
        }

        /// <summary>
        /// Gets or sets the minimum required password length for C-Access accounts.
        /// </summary>
        public byte MinimumPasswordLength
        {
            get
            {
                byte length;
                return byte.TryParse(CPProviders.DataProvider.GetGlobalSetting(_settings.MinimumPasswordLength), out length) ? length : byte.MinValue;
            }
            set
            {
                CPProviders.DataProvider.SetGlobalSetting(_settings.MinimumPasswordLength, value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the name of the Candidates C-Access Security group.
        /// </summary>
        public string CandidatesSecurityGroupName
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.CandidatesSecurityGroupName); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.CandidatesSecurityGroupName, value); }
        }

        /// <summary>
        /// Gets or sets the earliest supported election cycle.
        /// </summary>
        public string MinimumElectionCycle
        {
            get { return CPProviders.DataProvider.GetGlobalSetting(_settings.MinimumElectionCycle); }
            set { CPProviders.DataProvider.SetGlobalSetting(_settings.MinimumElectionCycle, value); }
        }

        #endregion
    }
}
