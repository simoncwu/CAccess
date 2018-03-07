using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A Candidate Services Unit liaison.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CsuLiaison
	{
		/// <summary>
		/// The CSU liaison ID.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly byte _id;

		/// <summary>
		/// Gets the CSU liaison ID.
		/// </summary>
		public byte ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the name of the CSU liaison.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the date and time the CSU liaison was last updated.
		/// </summary>
		[DataMember]
		public DateTime LastUpdated { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CsuLiaison"/> class.
		/// </summary>
		/// <param name="id">The ID of the CSU liaison.</param>
		public CsuLiaison(byte id)
		{
			_id = id;
		}
	}
}
