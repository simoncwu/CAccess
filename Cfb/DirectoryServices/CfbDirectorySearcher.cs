using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace Cfb.DirectoryServices
{
	public class CfbDirectorySearcher
	{
		/// <summary>
		/// Retrieves a collection of all CFB staff entries found in Active Directory at the given LDAP path.
		/// </summary>
		/// <returns>A collection of all CFB staff entries found in Active Directory.</returns>
		public static List<User> GetCfbStaff()
		{
			return GetCfbStaff(Properties.Settings.Default.CfbStaffADPath, null, null);
		}

		/// <summary>
		/// Retrieves a collection of all CFB staff entries found in Active Directory at the given LDAP path.
		/// </summary>
		/// <param name="path">The path to start searching from.</param>
		/// <param name="filter">The search filter string to use for narrowing results, in LDAP format, or null to use no filter.</param>
		/// <param name="users">The collection of employees to fill.</param>
		/// <returns>A collection of all CFB staff entries found in Active Directory at the given LDAP path.</returns>
		private static List<User> GetCfbStaff(string path, string filter, List<User> users)
		{
			if (users == null)
				users = new List<User>();
			uint rangeStep = 1000;
			uint rangeLow = 0;
			uint rangeHigh = rangeLow + (rangeStep - 1);
			bool lastQuery = false;
			bool quitLoop = false;
			SearchResult result;
			do
			{
				// perform ranged retrieval to safely retrieve all results without exceeding LDAP query limit
				string attributeWithRange = lastQuery ? String.Format("member;range={0}-*", rangeLow) : String.Format("member;range={0}-{1}", rangeLow, rangeHigh);
				using (DirectorySearcher searcher = new DirectorySearcher(new DirectoryEntry(path)))
				{
					searcher.Filter = filter ?? "(!(objectClass=group)(objectClass=person))";
					searcher.PropertiesToLoad.Clear();
					searcher.PropertiesToLoad.Add(attributeWithRange);
					searcher.PropertiesToLoad.Add("objectClass");
					result = searcher.FindOne();
				}

				// add to results if the entry is a user; otherwise recursively iterate through group members
				if (result.Properties["objectClass"].Contains("person"))
				{
					DirectoryEntry entry = result.GetDirectoryEntry();
					if (entry != null)
						users.Add(entry.AsUser());
					quitLoop = true;
				}
				else if (result.Properties["objectClass"].Contains("group"))
				{
					if (result.Properties.Contains(attributeWithRange)) // check if search range exceeded limit
					{
						foreach (object obj in result.Properties[attributeWithRange])
						{
							if (obj is string)
							{
								GetCfbStaff(string.Format("LDAP://{0}", obj.ToString()), filter, users);
							}
						}
						if (lastQuery)
						{
							quitLoop = true;
						}
					}
					else
					{
						lastQuery = true;
					}
					if (!lastQuery)
					{
						rangeLow = rangeHigh + 1;
						rangeHigh = rangeLow + (rangeStep - 1);
					}
				}
			}
			while (!quitLoop);
			return users;
		}

		/// <summary>
		/// Retrieves a user from Active Directory.
		/// </summary>
		/// <param name="username">The network logon name of the user to retrieve.</param>
		/// <returns>A <see cref="User"/> object representing the user if found; otherwise, null.</returns>
		public static User GetUser(string username)
		{
			using (DirectorySearcher searcher = new DirectorySearcher(string.Format("(&(objectClass=user)(sAMAccountName={0}))", username)))
			{
				try
				{
					SearchResult result = searcher.FindOne();
					if (result != null)
					{
						using (DirectoryEntry entry = result.GetDirectoryEntry())
						{
							return entry.AsUser();
						}
					}
				}
				catch (ArgumentException)
				{
				}
				return new User() { DisplayName = username, Username = username };
			}
		}

	}

}
