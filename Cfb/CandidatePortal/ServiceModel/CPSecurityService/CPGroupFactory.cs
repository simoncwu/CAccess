using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Security;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityService
{
	/// <summary>
	/// Factory for creating C-Access Security user group instances.
	/// </summary>
	internal class CPGroupFactory
	{
		/// <summary>
		/// Hidden constructor.
		/// </summary>
		private CPGroupFactory() { }

		/// <summary>
		/// Creates a new <see cref="CPGroup"/> instance using C-Access Security data.
		/// </summary>
		/// <param name="group">The group to use.</param>
		/// <returns>A new group instance using the specified group data if valid; otherwise, null.</returns>
		public static CPGroup CreateGroup(SecurityGroup group)
		{
			if (group == null)
				return null;
			return new CPGroup(group.GroupId, group.GroupName)
			{
				UserRights = CPSecurity.Sanitize((CPUserRights)group.UserRights)
			};
		}
	}
}
