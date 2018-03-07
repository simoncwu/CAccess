using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.Security
{
	/// <summary>
	/// Represents a C-Access Candidate Portal security group.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CPGroup
	{
		/// <summary>
		/// The unique group identifier.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly byte _id;

		/// <summary>
		/// Gets the unique group identifier.
		/// </summary>
		public byte ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the group name.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the security rights granted to the group.
		/// </summary>
		[DataMember]
		public CPUserRights UserRights { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CPGroup"/> class.
		/// </summary>
		/// <param name="id">The unique group identifier.</param>
		/// <param name="name">The group name.</param>
		public CPGroup(byte id, string name)
		{
			_id = id;
			this.Name = name;
		}
	}
}
