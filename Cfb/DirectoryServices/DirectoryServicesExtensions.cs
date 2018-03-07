using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace Cfb.DirectoryServices
{
	/// <summary>
	/// Extensions to Active Directory Domain Services classes.
	/// </summary>
	public static class DirectoryServicesExtensions
	{
		/// <summary>
		/// Converts a <see cref="DirectoryEntry"/> to a <see cref="User"/>.
		/// </summary>
		/// <param name="entry">A directory entry to convert.</param>
		/// <returns>A <see cref="User"/> that represents the input sequence.</returns>
		public static User AsUser(this DirectoryEntry entry)
		{
			using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
			{
				User user = new User(UserPrincipal.FindByIdentity(context, IdentityType.Guid, entry.Guid.ToString()));
				return user;
			}
		}

		/// <summary>
		/// Gets the value of a property as a string.
		/// </summary>
		/// <param name="entry">A directory entry to examine.</param>
		/// <param name="propertyName">The name of the property to retrieve.</param>
		/// <returns>The first string value found in the property.</returns>
		public static string GetPropertyStringValue(this DirectoryEntry entry, string propertyName)
		{
			var props = entry.Properties[propertyName];
			foreach (string s in props)
			{
				return s;
			}
			return null;
		}
	}
}
