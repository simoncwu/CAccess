using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Security.Sso;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService.Sso
{
	/// <summary>
	/// Factory for creating C-Access Security single sign-on token instances.
	/// </summary>
	internal class TokenFactory
	{
		/// <summary>
		/// Hidden constructor.
		/// </summary>
		private TokenFactory() { }

		/// <summary>
		/// Creates a new <see cref="Token"/> instance using C-Access Security single sign-on token data.
		/// </summary>
		/// <param name="token">The single sign-on token to use.</param>
		/// <returns>A new token instance using the specified single sign-on data if valid; otherwise, null.</returns>
		public static Token CreateToken(SecuritySsoToken token)
		{
			if (token == null)
				return null;
			return new Token(token.TokenId, ApplicationFactory.CreateApplication(token.SecuritySsoApplication), token.UserName, token.Created);
		}
	}
}
