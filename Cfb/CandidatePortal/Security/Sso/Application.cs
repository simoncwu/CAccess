using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.Security.Sso
{
	/// <summary>
	/// Represents an external web application that has enterprise-level single sign-on integration with C-Access Security.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class Application
	{
		/// <summary>
		/// The application's unique identifier.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly byte _id;

		/// <summary>
		/// Gets the application's unique identifier.
		/// </summary>
		public byte ID
		{
			get { return _id; }
		}

		/// <summary>
		/// The name of the application.
		/// </summary>
		[DataMember(Name = "Name")]
		private readonly string _name;

		/// <summary>
		/// Gets or sets the name of the application.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// The user account rights needed to access this application.
		/// </summary>
		[DataMember(Name = "AccessRights")]
		private readonly CPUserRights _accessRights;

		/// <summary>
		/// Gets the user account rights needed to access this application.
		/// </summary>
		public CPUserRights AccessRights
		{
			get { return _accessRights; }
		}

		/// <summary>
		/// Gets or sets a user-friendly description for the application.
		/// </summary>
		[DataMember]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the URL for the application.
		/// </summary>
		[DataMember]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the post-login redirect URL for the application.
		/// </summary>
		[DataMember]
		public string RedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the name of the query string parameter that will hold the token value when passed back to the application.
		/// </summary>
		[DataMember]
		public string TokenParameter { get; set; }

		/// <summary>
		/// Gets or sets the absolute path of the custom login page for the application.
		/// </summary>
		[DataMember]
		public string LoginPagePath { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Application"/> class.
		/// </summary>
		/// <param name="id">The application's unique identifier.</param>
		/// <param name="name">The application name.</param>
		/// <param name="accessRights">The user account rights needed to access the application.</param>
		public Application(byte id, string name, CPUserRights accessRights)
		{
			_id = id;
			_name = name;
			_accessRights = accessRights;
		}

		/// <summary>
		/// Creates a new login token for authenticating a specific user.
		/// </summary>
		/// <param name="userName">The login name of the user requesting authentication.</param>
		/// <returns>A new login token if the specified user has access to the application; otherwise, null.</returns>
		public Token CreateToken(string userName)
		{
			return CPSecurity.Provider.CreateToken(userName, _id);
		}
	}
}
