using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cfb.CandidatePortal.Security
{
    /// <summary>
    /// Defines C-Access user account rights and privileges to CFB secured online applications.
    /// </summary>
    [Flags]
    public enum CPUserRights : int
    {
        /// <summary>
        /// Specifies no rights to any CFB applications.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies the right to access campaign profile and tracking information in the C-Access application.
        /// </summary>
        CAccess = 1,
        /// <summary>
        /// Specifies the right to access sensitive campaign profile and tracking information in C-Access.
        /// </summary>
        CAccessPrivate = 2,
        /// <summary>
        /// Specifies the right to add/modify campaign profile information in C-Access.
        /// </summary>
        CAccessModifyProfile = 4,
        /// <summary>
        /// Specifies the right to receive electronic mail alerts for major CFB events related to the campaign.
        /// </summary>
        EmailAlert = 8,
        /// <summary>
        /// Specifies the right to access the Voter Guide Submission Application.
        /// </summary>
        VoterGuide = 16,
        /// <summary>
        /// Specifies the right to submit Voter Guide profiles in the Voter Guide Submission Application.
        /// </summary>
        VoterGuideSubmit = 4096,
        /// <summary>
        /// Specifies the right to perform security administration tasks for the campaign's C-Access user accounts.
        /// </summary>
        SecurityAdmin = 32,
        /// <summary>
        /// Specifies the right to access the online C-SMART application.
        /// </summary>
        Csmart = 64,
        /// <summary>
        /// Specifies the right to access sensitive campaign information in the online C-SMART application.
        /// </summary>
        CsmartPrivate = 128,
        /// <summary>
        /// Specifies the right to add/modify data in the online C-SMART application.
        /// </summary>
        CsmartModify = 256,
        /// <summary>
        /// Specifies the right to delete data in the online C-SMART application.
        /// </summary>
        CsmartDelete = 512,
        /// <summary>
        /// Specifies the right to perform disclosure statement submissions and administrative tasks in the online C-SMART application.
        /// </summary>
        CsmartSubmit = 1024,
        /// <summary>
        /// Specifies the right to modify candidate encryption keys for the online C-SMART application.
        /// </summary>
        CsmartEncryptionKey = 2048
    }

    /// <summary>
    /// Extension support methods for <see cref="CPUserRights"/> enumeration.
    /// </summary>
    public static class CPUserRights_Extensions
    {
        /// <summary>
        /// Converts the value of this instance to a friendly string representation.
        /// </summary>
        /// <param name="right">The <see cref="CPUserRights"/> to convert.</param>
        /// <returns>The friendly string representation of the value of this instance.</returns>
        public static string ToDisplayString(this CPUserRights right)
        {
            switch (right)
            {
                case CPUserRights.CAccess:
                    return "C-Access";
                case CPUserRights.CAccessModifyProfile:
                    return "C-Access - Modify Profile";
                case CPUserRights.CAccessPrivate:
                    return "C-Access - Sensitive Data";
                case CPUserRights.Csmart:
                    return "C-SMART";
                case CPUserRights.CsmartDelete:
                    return "C-SMART - Delete Data";
                case CPUserRights.CsmartEncryptionKey:
                    return "C-SMART - Change Encryption Key";
                case CPUserRights.CsmartModify:
                    return "C-SMART - Modify Data";
                case CPUserRights.CsmartPrivate:
                    return "C-SMART - Sensitive Data";
                case CPUserRights.CsmartSubmit:
                    return "C-SMART - Administration and Submission";
                case CPUserRights.EmailAlert:
                    return "E-mail Alerts";
                case CPUserRights.SecurityAdmin:
                    return "C-Access - Security Administration";
                case CPUserRights.VoterGuide:
                    return "VGSA";
                case CPUserRights.VoterGuideSubmit:
                    return "VGSA - Submission";
                default:
                    return null;
            }
        }
    }
}
