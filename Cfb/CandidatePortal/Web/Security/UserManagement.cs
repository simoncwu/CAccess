using System;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Web.Profile;
using System.Web.Security;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.Web.Security.SPPeopleService;
using Cfb.CandidatePortal.Web.Security.SPUserGroupService;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web.Security
{
    /// <summary>
    /// Utility class for setting up user accounts for candidates on the C-Access portal.
    /// </summary>
    public static class UserManagement
    {
        static UserManagement()
        {
            SPContentSiteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["SPContentSiteUrl"];
        }

        #region Legacy

        /// <summary>
        /// Gets or sets the URL for the back-end content site.
        /// </summary>
        public static string SPContentSiteUrl { get; set; }

        /// <summary>
        /// Converts a regular user name to the login name format used by SharePoint.
        /// </summary>
        /// <param name="username">The user name to convert.</param>
        /// <returns>The equivalent login name as seen by SharePoint.</returns>
        public static string ToSPLoginName(string username)
        {
            return string.Format("{0}:{1}", Membership.Provider.Name, username);
        }

        /// <summary>
        /// Gets the CFIS candidate ID for a C-Access user.
        /// </summary>
        /// <param name="username">The user name of the account to examine.</param>
        /// <returns>The CFIS candidate ID associated with the account for <paramref name="username"/> if found, otherwise an empty <see cref="String"/>.</returns>
        public static string GetCfisId(string username)
        {
            return GetProperty(username, Properties.Settings.Default.CfisIdProfileProperty);
        }

        /// <summary>
        /// Gets the C-Access ID for a C-Access user.
        /// </summary>
        /// <param name="username">The user name of the account to examine.</param>
        /// <returns>The C-Access ID associated with the account for <paramref name="username"/> if found, otherwise an empty <see cref="String"/>.</returns>
        public static string GetCaid(string username)
        {
            return GetProperty(username, Properties.Settings.Default.CaidProfileProperty);
        }

        /// <summary>
        /// Retrieves the full name of a C-Access user.
        /// </summary>
        /// <param name="user">The C-Access user to retrieve.</param>
        /// <returns>The full name on record for C-Access user <paramref name="user"/> otherwise null.</returns>
        public static string GetFullName(MembershipUser user)
        {
            if (user != null)
            {
                string username = ToSPLoginName(user.UserName);
                using (People proxy = new People())
                {
                    if (!string.IsNullOrWhiteSpace(SPContentSiteUrl))
                        proxy.Url = SPContentSiteUrl + "/_vti_bin/People.asmx";
                    proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    PrincipalInfo[] matches = proxy.SearchPrincipals(username, 1, SPPrincipalType.User);
                    if (matches.Length > 0)
                    {
                        PrincipalInfo userPI = matches[0];
                        return userPI.DisplayName;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets whether or not the password for a user has expired.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <returns>true if the password for the specified user has expired; otherwise, false.</returns>
        public static bool IsPasswordExpired(MembershipUser user)
        {
            return user != null && Roles.IsUserInRole(user.UserName, "Expired Passwords");
        }

        /// <summary>
        /// Gets the value of a profile property for a C-Access user.
        /// </summary>
        /// <param name="username">The user name of the account to examine.</param>
        /// <param name="propertyName">The name of profile property to retrieve.</param>
        /// <returns>The value of the profile property associated with the account for <paramref name="username"/> if found; otherwise, null.</returns>
        private static string GetProperty(string username, string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                ProfileBase profile = ProfileBase.Create(ToSPLoginName(username));
                return profile[propertyName] as string;
            }
            return null;
        }

        #endregion

        /// <summary>
        /// Gets or sets whether or not log file tracing is enabled.
        /// </summary>
        public static bool EnableTracing { get; set; }

        /// <summary>
        /// The output path location of the log trace file.
        /// </summary>
        private static readonly string LogFilePath = Path.Combine(Environment.CurrentDirectory, string.Format(Properties.Settings.Default.LogFileNameFormat, DateTime.Today.ToString("yyyyMMdd")));

        /// <summary>
        /// Writes a formatted trace message to the log file.
        /// </summary>
        /// <param name="format">A <see cref="string"/> containing one or more format items.</param>
        /// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
        private static void Trace(string format, params object[] args)
        {
            Trace(string.Format(format, args));
        }

        /// <summary>
        /// Writes a trace message to the log file.
        /// </summary>
        /// <param name="message">The message to log.</param>
        private static void Trace(string message)
        {
            if (EnableTracing)
            {
                using (StreamWriter sw = File.AppendText(LogFilePath))
                {
                    sw.WriteLine("[{0:yyyy-MM-dd hh:mm:ss.fff}] {1}", DateTime.Now, message);
                    sw.Close();

                }
            }
        }

        /// <summary>
        /// Writes an exception trace message to the log file.
        /// </summary>
        /// <param name="e">The exception to log.</param>
        private static void Trace(Exception e)
        {
            if (e != null)
                Trace("***ERROR***\n{0}\n{1}", e.ToString(), "Critical errors were encountered during account creation. New user will be rolled back.");
        }

        /// <summary>
        /// Generates a unique username for a new C-Access user.
        /// </summary>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="middleInitial">The user's middle initial.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="candidateID">The user's candidate ID.</param>
        /// <returns>A unique C-Access username for the user. If <paramref name="firstName"/>, <paramref name="lastName"/>, or <paramref name="candidateID"/> is null, returns null.</returns>
        /// <remarks>
        /// Usernames now in the format of "(first initial)(middle initial)(first 4 characters of last name)(cfis ID)[-optional unique number]" for users with middle initials, and "(first initial)(first 5 characters of last name)(cfis ID)[-optional unique number]" for users without.
        /// </remarks>
        public static string GenerateUserName(string firstName, char? middleInitial, string lastName, string candidateID)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(candidateID))
                return null;
            ushort increment = 0;
            string username;
            char[] skipChars = Properties.Settings.Default.UserNameSkipCharacters.ToCharArray();
            do
            {
                //TODO: change username generation algorithm to include additional last/first name characters for uniqueness
                username = string.Format("{0}{1}{2}{3}{4}",
                    firstName.TrimAll(skipChars).Truncate(1),
                    middleInitial,
                    lastName.TrimAll(skipChars).Truncate(middleInitial.HasValue ? 4 : 5),
                    candidateID,
                    increment > 0 ? "-" + increment.ToString() : null);
                increment++;
            } while (Membership.GetUser(username) != null);
            return username;
        }

        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="creator">The account creator.</param>
        /// <returns>The user account if successfully created; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="username"/>, <paramref name="password"/>, or <paramref name="email"/> is null or whitespace.</exception>
        public static MembershipUser CreateUser(string username, string password, string email, string creator)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");
            Trace("Attempting to create user {0}.", username);
            try
            {
                // check if account already exists
                if (Membership.GetUser(username) != null)
                {
                    Trace("Unable to create user {0}, account already exists.", username);
                    return null;
                }

                // create .NET Membership user
                Trace("Creating Membership user account for {0}.", username);
                MembershipUser user = Membership.CreateUser(username, password, email);
                user.Comment = string.Format("Created {0:f} by {1}", DateTime.Now, creator);
                Membership.UpdateUser(user);
                Trace("Membership user account created.");
                return user;
            }
            catch (Exception e)
            {
                Trace(e);
                DeleteUser(Membership.GetUser(username));
            }
            return null;
        }

        /// <summary>
        /// Deletes a C-Access user account and all associated profile properties.
        /// </summary>
        /// <param name="user">The user account to be deleted.</param>
        /// <returns>true if account was successfully deleted, false otherwise.</returns>
        public static bool DeleteUser(MembershipUser user)
        {
            if (user == null)
                return true;
            string username = user.UserName;
            string spName = ToSPLoginName(username);
            Trace("Deleting {0} provider entry for user {1}.", Membership.Provider.Name, spName);
            try
            {
                Membership.DeleteUser(spName);
            }
            catch (Exception)
            {
            }
            Trace("Deleting user {0}.", username);
            try
            {
                Membership.DeleteUser(username);
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// Unlocks a C-Access user account that has been locked out.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> account to unlock.</param>
        /// <returns>true if the account was successfully unlocked; otherwise, false.</returns>
        public static bool UnlockUser(MembershipUser user)
        {
            if (user == null)
                return false;
            if (user.IsLockedOut)
            {
                user.UnlockUser();
                try
                {
                    Membership.UpdateUser(user);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sets the enabled status for a C-Access user account.
        /// </summary>
        /// <param name="user">The <see cref="MembershipUser"/> account to set.</param>
        /// <param name="enabled">Indicates whether or not the account is to be enabled.</param>
        /// <returns>true if the account status was set successfully; otherwise, false.</returns>
        public static bool SetStatus(MembershipUser user, bool enabled)
        {
            if (user == null)
                return false;
            try
            {
                user.IsApproved = enabled;
                Membership.UpdateUser(user);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Resets a C-Access user's password to a new randomly generated password.
        /// </summary>
        /// <param name="user">The user whose password is to be reset.</param>
        /// <returns>The new password for the user if successful; otherwise, null.</returns>
        unsafe public static SecureString ResetPassword(MembershipUser user)
        {
            if (user == null)
            {
                return null;
            }
            string username = user.UserName;
            try
            {
                UnlockUser(user);
                char[] chars = user.ResetPassword().ToCharArray();
                fixed (char* pChars = chars)
                {
                    SecureString s = new SecureString(pChars, chars.Length);
                    s.MakeReadOnly();
                    return s;
                }
            }
            catch (Exception e)
            {
                Trace(e);
                return null;
            }
        }
    }
}
