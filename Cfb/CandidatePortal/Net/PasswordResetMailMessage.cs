using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Net
{
	/// <summary>
	/// Represents an e-mail notification of a password reset to be sent from the Candidate Portal.
	/// </summary>
	public sealed class PasswordResetMailMessage
	{
		/// <summary>
		/// The e-mail message body format.
		/// </summary>
		private const string MessageBodyFormat =
@"Hello {0},

This e-mail is being sent to you from the {1}'s {2} in response to a request for a reset of a forgotten password or a reminder for a forgotten username.

Below you will find your username and a temporary password for your {2} account. Please use this username and password to sign into {2} at {3}. Upon signing in, you will be prompted to choose a new password before accessing any other part of {2}.

Your username is (no spaces):
	{4}

Your temporary password is (no spaces, case-sensitive):
	{5}

Please note that your {2} username and password are for your own personal use only and are not to be disclosed to any unauthorized individuals.

If you have any questions regarding your {2} account, please contact the Candidate Services Unit at {6}.

NOTE: Please do not reply to this message, as e-mail sent to this address will not be answered.
";

		/// <summary>
		/// Initializes a new instance of a password reset e-mail notification using configuration file settings.
		/// </summary>
		private PasswordResetMailMessage()
		{
		}

		/// <summary>
		/// Sends an e-mail notification of a password reset to a user.
		/// </summary>
		/// <param name="recipient">The user whose password is being reset.</param>
		/// <param name="password">The user's new password.</param>
		/// <exception cref="ArgumentNullException"><paramref name="recipient"/> or <paramref name="password"/> is null.</exception>
		public static void Send(CPUser recipient, SecureString password)
		{
			if (recipient == null)
				throw new ArgumentNullException("recipient", "Password reset message recipient cannot be null.");
			if (password == null)
				throw new ArgumentNullException("password", "Password cannot be null.");

			using (CPMailMessage message = new CPMailMessage()
			{
				Recipient = new MailAddress(recipient.Email, recipient.DisplayName),
				Subject = CPProviders.SettingsProvider.ApplicationName + @" Username/Password Reset",
				Body = string.Format(MessageBodyFormat, recipient.DisplayName, CPProviders.SettingsProvider.AgencyName, CPProviders.SettingsProvider.ApplicationName, CPProviders.SettingsProvider.ApplicationUrl, recipient.UserName, password.Decrypt(), CPProviders.SettingsProvider.AgencyPhoneNumber)
			})
			{
				message.Send();
				message.Body.Remove(0);
			}
		}
	}
}