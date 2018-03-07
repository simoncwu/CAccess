using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;
using System.Web;

namespace Cfb.CandidatePortal.Web.Security
{
	/// <summary>
	/// Custom membership provider for the C-Access candidate portal.
	/// </summary>
	public class CfbMembershipProvider : SqlMembershipProvider
	{
		/// <summary>
		/// The duration of account lock-outs from too many incorrent logon attempts, in minutes.
		/// </summary>
		private int _lockoutDuration;

		/// <summary>
		/// Initializes the membership provider with the property values specified in the C-Access configuration file.
		/// </summary>
		/// <param name="name">The name of the <see cref="CfbMembershipProvider"/> to initialize.</param>
		/// <param name="config">A <see cref="NameValueCollection"/> that contains the names and values of configuration options for the membership provider.</param>
		public override void Initialize(string name, NameValueCollection config)
		{
			string lockout = config["cpLockoutDuration"];
			if (string.IsNullOrEmpty(lockout) || !int.TryParse(lockout, out _lockoutDuration))
			{
				_lockoutDuration = Properties.Settings.Default.LockoutDuration;
			}
			config.Remove("cpLockoutDuration");
			base.Initialize(name, config);
		}

		/// <summary>
		/// Unlocks a locked-out user account if the lockout duration has passed since the account was locked-out.
		/// </summary>
		/// <param name="username">The username of the account to unlock.</param>
		/// <returns>true if and only if the user account was locked out and the lockout duration has passed.</returns>
		private bool AutoUnlockUser(string username)
		{
			if (username != null && username.Length <= 0x100)
			{
				MembershipUser user = Membership.GetUser(username, false);
				if ((user != null) && (user.IsLockedOut) && (user.LastLockoutDate.ToUniversalTime().AddMinutes(_lockoutDuration) < DateTime.UtcNow))
					return user.UnlockUser();
			}
			return false;
		}

		/// <summary>
		/// Verifies that the specified user name and password exist in the Candidate Portal membership database.
		/// </summary>
		/// <param name="username">The name of the user to validate.</param>
		/// <param name="password">The password for the specified user.</param>
		/// <returns>true if the specified username and password are valid; otherwise, false. A value of false is also returned if the user does not exist in the database.</returns>
		public override bool ValidateUser(string username, string password)
		{
			if (base.ValidateUser(username, password))
			{
				// BUG #13 - clear session upon login
				HttpContext.Current.Session.Clear();
				return true;
			}
			else if (AutoUnlockUser(username))
			{
				return this.ValidateUser(username, password);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Resets a user's password to a new, automatically generated password.
		/// </summary>
		/// <param name="username">The user to reset the password for.</param>
		/// <param name="passwordAnswer">The password answer for the specified user.</param>
		/// <returns>The new password for the specified user.</returns>
		public override string ResetPassword(string username, string passwordAnswer)
		{
			try
			{
				return base.ResetPassword(username, passwordAnswer);
			}
			catch (MembershipPasswordException e)
			{
				if (AutoUnlockUser(username))
					return base.ResetPassword(username, passwordAnswer);
				throw e;
			}
		}

		/// <summary>
		/// Raises the <see cref="MembershipProvider.ValidatingPassword"/> event if an event handler has been defined.
		/// </summary>
		/// <param name="e">The <see cref="ValidatePasswordEventArgs"/> to pass to the ValidatingPassword event handler.</param>
		protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
		{
			// BUG #13 - clear session cache on login
			if (HttpContext.Current != null)
			{
				FormsAuthentication.SignOut();
				HttpContext.Current.Session.Clear();
			}
			base.OnValidatingPassword(e);
		}
	}
}
