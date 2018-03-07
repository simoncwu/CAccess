using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Defines methods for accessing C-Access global settings.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets or sets the date when reminder e-mails were last distributed.
        /// </summary>
        DateTime LastReminderDistributionDate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing e-mail message sender display name.
        /// </summary>
        string MessageSenderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the outgoing e-mail message sender address.
        /// </summary>
        string MessageSenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the C-Access Administrators security group.
        /// </summary>
        string AdministratorsGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the C-Access Account Managers security group.
        /// </summary>
        string AccountManagersGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the CFB agency.
        /// </summary>
        string AgencyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the C-Access candidate portal application.
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the C-Access candidate portal application.
        /// </summary>
        string ApplicationUrl { get; set; }

        /// <summary>
        /// Gets or sets the contact phone number of the CFB agency.
        /// </summary>
        string AgencyPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the minimum required password length for C-Access accounts.
        /// </summary>
        byte MinimumPasswordLength { get; set; }

        /// <summary>
        /// Gets or sets the name of the Candidates C-Access Security group.
        /// </summary>
        string CandidatesSecurityGroupName { get; set; }

        /// <summary>
        /// Gets or sets the earliest supported election cycle.
        /// </summary>
        string MinimumElectionCycle { get; set; }
    }
}
