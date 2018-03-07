using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Cfb.DirectoryServices
{
	/// <summary>
	/// An Active Directory user.
	/// </summary>
	public class User
	{
		/// <summary>
		/// The user's globally unique identifier (GUID).
		/// </summary>
		private readonly Guid? _guid;

		/// <summary>
		/// Gets the user's globally unique identifier (GUID).
		/// </summary>
		public Guid? Guid
		{
			get { return _guid; }
		}

		/// <summary>
		/// Gets or sets the user's display name.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the user's network logon name.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the user's e-mail address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets whether or not the user is an ad-hoc instance.
		/// </summary>
		public bool IsAdHoc
		{
			get { return !_guid.HasValue; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		public User()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <param name="principal">The user's security principal instance.</param>
		public User(UserPrincipal principal)
		{
			if (principal != null)
			{
				_guid = principal.Guid;
				this.DisplayName = principal.DisplayName;
				this.Username = principal.SamAccountName;
				this.Email = principal.EmailAddress;
				if (this.Username != null)
					this.Username = this.Username.ToLowerInvariant();
				if (this.Email != null)
					this.Email = this.Email.ToLowerInvariant();
			}
		}

		/// <summary>
		/// Returns a Boolean value that specifies whether the user is a member of the specified group.
		/// </summary>
		/// <param name="groupName">The namne of the group for which membership is determined.</param>
		/// <returns>true if the user is a member of the specified group; otherwise false.</returns>
		public bool IsMemberOf(string groupName)
		{
			using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain))
			{
				using (UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, IdentityType.Guid, _guid.HasValue ? _guid.ToString() : null))
				{
					return userPrincipal.GetAuthorizationGroups().Any(g => g.SamAccountName == groupName);
				}
			}
		}

		/// <summary>
		/// Gets a user object that represents the current user under which the thread is running.
		/// </summary>
		public static User Current
		{
			get { return new User(UserPrincipal.Current); }
		}
	}
}
