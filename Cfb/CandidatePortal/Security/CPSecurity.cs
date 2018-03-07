using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Collections.Specialized;

namespace Cfb.CandidatePortal.Security
{
	/// <summary>
	/// Provides access to C-Access Security functionality.
	/// </summary>
	public static class CPSecurity
	{
		/// <summary>
		/// Gets or sets the C-Access Security provider.
		/// </summary>
		public static ISecurityProvider Provider { get; set; }

		/// <summary>
		/// Gets the absolute path to the C-Access login page.
		/// </summary>
		public static string LoginPath
		{
			get { return Properties.Settings.Default.LoginPath; }
		}

		/// <summary>
		/// Gets the absolute path to the C-Access logout page.
		/// </summary>
		public static string LogoutPath
		{
			get { return Properties.Settings.Default.LogoutPath; }
		}

		/// <summary>
		/// Ensures that a <see cref="CPUserRights"/> value consists only of valid individual C-Access user account rights.
		/// </summary>
		/// <param name="rights">The <see cref="CPUserRights"/> value to sanitize.</param>
		/// <returns>A sanitized copy of <paramref name="rights"/>.</returns>
		public static CPUserRights Sanitize(CPUserRights rights)
		{
			CPUserRights cleanRights = CPUserRights.None;
			foreach (CPUserRights right in Enum.GetValues(typeof(CPUserRights)))
			{
				if ((rights & right) == right)
				{
					cleanRights |= right;
				}
			}
			return cleanRights;
		}

		/// <summary>
		/// Gets the name of the query string parameter for passing single sign-on application IDs.
		/// </summary>
		public static string QueryStringSsoAppIDParameter
		{
			get { return Properties.Settings.Default.SsoAppIDParameter; }
		}

		/// <summary>
		/// Gets the name of the query string parameter for passing single sign-on return URLs.
		/// </summary>
		public static string QueryStringSsoReturnUrlParameter
		{
			get { return Properties.Settings.Default.SsoReturnUrlParameter; }
		}

		/// <summary>
		/// Retrieves a URL target for user authentication via C-Access Security single sign-on.
		/// </summary>
		/// <param name="applicationID">The unique C-Access Security single-sign on identifier of the application requesting a login URL.</param>
		/// <returns>A URL target for authenticating users via C-Access to the specified application.</returns>
		public static string GetSsoLoginUrl(byte applicationID)
		{
			return GetSsoLoginUrl(applicationID, new NameValueCollection(0));
		}

		/// <summary>
		/// Retrieves a URL target for user authentication via C-Access Security single sign-on.
		/// </summary>
		/// <param name="applicationID">The unique C-Access Security single sign-on identifier of the application requesting a login URL.</param>
		/// <param name="queryString">A query string to preserve throughout the authentication process.</param>
		/// <returns>A URL target for authenticating users via C-Access to the specified application, preserving the query string <paramref name="queryString"/>.</returns>
		public static string GetSsoLoginUrl(byte applicationID, NameValueCollection queryString)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Properties.Settings.Default.SsoSite);
			sb.Append(CPSecurity.LoginPath);
			sb.Append('?');
			NameValueCollection qs = new NameValueCollection(queryString);
			qs[CPSecurity.QueryStringSsoAppIDParameter] = applicationID.ToString();
			foreach (string key in qs.Keys)
			{
				sb.AppendFormat("{0}={1}&", key, qs[key]);
			}
			return sb.ToString().TrimEnd('&', '?');
		}

		/// <summary>
		/// Retrieves a URL target for user de-authentication via C-Access Security single sign-on.
		/// </summary>
		/// <param name="applicationID">The unique C-Access Security single sign-on identifier of the application requesting a logout URL.</param>
		/// <returns>A URL target for de-authenticating users via C-Access.</returns>
		public static string GetSsoLogoutUrl(byte applicationID)
		{
			return GetSsoLogoutUrl(applicationID, null);
		}

		/// <summary>
		/// Retrieves a URL target for user de-authentication via C-Access Security single sign-on.
		/// </summary>
		/// <param name="applicationID">The unique C-Access Security single sign-on identifier of the application requesting a logout URL.</param>
		/// <param name="returnUrl">A preferred post-logout redirection target URL.</param>
		/// <returns>A URL target for de-authenticating users via C-Access.</returns>
		public static string GetSsoLogoutUrl(byte applicationID, string returnUrl)
		{
			return string.Format("{0}{1}?{2}={3}&{4}={5}", Properties.Settings.Default.SsoSite, CPSecurity.LogoutPath, CPSecurity.QueryStringSsoAppIDParameter, applicationID, CPSecurity.QueryStringSsoReturnUrlParameter, returnUrl);
		}

		/// <summary>
		/// Determines whether one or more C-Access Security user rights are set in the current instance.
		/// </summary>
		/// <param name="this">The current instance to examine.</param>
		/// <param name="rights">A user rights value.</param>
		/// <returns>true if the user rights that are set in <paramref name="rights"/> are also set in the current instance; otherwise, false.</returns>
		public static bool HasRights(this CPUserRights @this, CPUserRights rights)
		{
			return (@this & rights) == rights;
		}
	}
}
