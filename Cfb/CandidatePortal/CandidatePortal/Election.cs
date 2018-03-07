using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of an election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class Election
	{
		/// <summary>
		/// The election cycle name.
		/// </summary>
		[DataMember(Name = "Cycle")]
		private readonly string _cycle;

		/// <summary>
		/// Gets the election cycle name.
		/// </summary>
		public string Cycle
		{
			get { return _cycle; }
		}

		/// <summary>
		/// The collection of statements for the election.
		/// </summary>
		[DataMember(Name = "Statements")]
		private readonly Dictionary<byte, Statement> _statements;

		/// <summary>
		/// Gets a collection of statements for the election.
		/// </summary>
		public Dictionary<byte, Statement> Statements
		{
			get { return _statements; }
		}

		/// <summary>
		/// Gets or sets the election year.
		/// </summary>
		[DataMember]
		public int Year { get; set; }

		/// <summary>
		/// Gets or sets the election date.
		/// </summary>
		[DataMember]
		public DateTime? ElectionDate { get; set; }

		/// <summary>
		/// Gets or sets whether or not the election cycle is for transititonal/inaugural purposes.
		/// </summary>
		[DataMember]
		public bool IsTIE { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Election"/> class.
		/// </summary>
		/// <param name="electionCycle">The election cycle name.</param>
		public Election(string electionCycle)
		{
			_cycle = electionCycle;
			_statements = new Dictionary<byte, Statement>();
		}
	}
}
