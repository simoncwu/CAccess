using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.Security.Sso
{
	/// <summary>
	/// A one-time use single sign-on token for authenticating a C-Access user account to an external web application.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class Token
	{
		/// <summary>
		/// The token's unique.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly Guid _id;

		/// <summary>
		/// Gets the token's unique identifier.
		/// </summary>
		public Guid ID
		{
			get { return _id; }
		}

		/// <summary>
		/// The application requesting a token.
		/// </summary>
		[DataMember(Name = "Application")]
		private readonly Application _application;

		/// <summary>
		/// Gets the application requesting a token.
		/// </summary>
		public Application Application
		{
			get { return _application; }
		}

		/// <summary>
		/// The login name of the user requesting a token.
		/// </summary>
		[DataMember(Name = "UserName")]
		private readonly string _username;

		/// <summary>
		/// Gets the login name of the user requesting a token.
		/// </summary>
		public string UserName
		{
			get { return _username; }
		}

		/// <summary>
		/// The time the token was generated.
		/// </summary>
		[DataMember(Name = "Created")]
		private DateTime _created;

		/// <summary>
		/// Gets the time the token was generated.
		/// </summary>
		public DateTime Created
		{
			get { return _created; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Token"/> class.
		/// </summary>
		/// <param name="id">The unique token identifier.</param>
		/// <param name="app">The application requesting a token.</param>
		/// <param name="username">The login name of the user requesting a token.</param>
		/// <param name="created">The date the token was generated.</param>
		public Token(Guid id, Application app, string username, DateTime created)
		{
			if (id == null || app == null || string.IsNullOrEmpty(username))
				throw new ArgumentNullException(id == null ? "id" : app == null ? "app" : "username");
			_id = id;
			_application = app;
			_username = username;
			_created = created;
		}
	}
}
