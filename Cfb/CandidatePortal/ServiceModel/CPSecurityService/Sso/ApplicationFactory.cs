using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService.Sso
{
	/// <summary>
	/// Factory for creating C-Access Security single sign-on application instances.
	/// </summary>
	internal class ApplicationFactory
	{
		/// <summary>
		/// Hidden constructor.
		/// </summary>
		private ApplicationFactory() { }

		/// <summary>
		/// Creates a new <see cref="Application"/> instance using C-Access Security single sign-on application data.
		/// </summary>
		/// <param name="application">The single sign-on application to use.</param>
		/// <returns>A new application instance using the specified single sign-on data if valid; otherwise, null.</returns>
		public static Application CreateApplication(SecuritySsoApplication application)
		{
			if (application == null)
				return null;
			return new Application(application.ApplicationId, application.ApplicationName, (CPUserRights)application.UserRights)
			{
				Description = application.Description,
				Url = application.Url,
				RedirectUrl = application.RedirectUrl,
				TokenParameter = application.TokenParameter,
				LoginPagePath = application.LoginPagePath
			};
		}
	}
}
