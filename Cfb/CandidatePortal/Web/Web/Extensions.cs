using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Cfb.CandidatePortal.Security;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web
{
	/// <summary>
	/// Web-related extensions.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Evaluates the current HTTP web session for pit stop conditions and executes them if found.
		/// </summary>
		/// <param name="username">The name of the current user.</param>
		/// <param name="context">The <see cref="HttpContext"/> to examine.</param>
		public static void CheckPitStops(this HttpContext context, string username = null)
		{
			if (username == null)
				username = context.User.Identity.Name;
			CPUser user = CPSecurity.Provider.GetUser(username);
			if (user != null && user.PasswordExpired && !PageUrlManager.ChangePasswordPageUrl.Equals(context.Request.Path, StringComparison.CurrentCultureIgnoreCase))
				context.Response.Pit(PageUrlManager.ChangePasswordPageUrl);
			// redirection check for CMO settings, disabled pending policy change
			//else if ((CmoSettings.GetSettings(UserManagement.GetCfisId(context.User.Identity.Name)) == null) && !Properties.Settings.Default.ChangeCmoSettingsPage.Equals(context.Request.Path))
			//    Pit(context, Properties.Settings.Default.ChangeCmoSettingsPage);
		}

		/// <summary>
		/// Temporarily redirects the user of a web session to an intermediary page for additional processing before continuing on. Specifies whether execution of the current page should terminate.
		/// </summary>
		/// <param name="response">The <see cref="HttpResponse"/> object for sending data to a client.</param>
		/// <param name="url">The intermediary page URL.</param>
		public static void Pit(this HttpResponse response, string url)
		{
			HttpContext context = HttpContext.Current;
			response.Redirect(url, QueryStringManager.MakeQueryString(returnUrl: context.Server.UrlEncode(context.Request.Url.PathAndQuery)));
		}

		/// <summary>
		/// Redirects the user to the originally requested page and query string if this is an intermediary page for a temporary redirection.
		/// </summary>
		/// <param name="response">The <see cref="HttpResponse"/> object for sending data to a client.</param>
		public static void ExitPit(this HttpResponse response)
		{
			HttpRequest request = HttpContext.Current.Request;
			if (request.IsPitStop())
			{
				response.Redirect(request.GetReturnUrl());
			}
		}

		/// <summary>
		/// Gets whether or not a web request is an intermediary page for a temporary redirection.
		/// </summary>
		/// <param name="request">The <see cref="HttpRequest"/> object for the current HTTP request.</param>
		public static bool IsPitStop(this HttpRequest request)
		{
			return !string.IsNullOrWhiteSpace(request.GetReturnUrl());
		}

		/// <summary>
		/// Redirects a client to an page indicating that access was not authorized.
		/// </summary>
		/// <param name="response">The <see cref="HttpResponse"/> object for sending data to a client.</param>
		public static void RedirectUnauthorized(this HttpResponse response)
		{
			response.Redirect(PageUrlManager.UnauthorizedPageUrl, HttpContext.Current.Request.QueryString);
		}
	}
}
