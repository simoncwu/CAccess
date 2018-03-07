using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a diclsosure statement during an election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class Statement
	{
		/// <summary>
		/// The statement number.
		/// </summary>
		[DataMember(Name = "Number")]
		private readonly byte _number;

		/// <summary>
		/// Gets the statement number;
		/// </summary>
		public byte Number
		{
			get { return _number; }
		}

		/// <summary>
		/// Gets or sets the start date of the statement period.
		/// </summary>
		[DataMember]
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date of the statement period.
		/// </summary>
		[DataMember]
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Gets or sets the statement filing due date.
		/// </summary>
		[DataMember]
		public DateTime DueDate { get; set; }

		/// <summary>
		/// Gets or sets whether or not the statement is a primary or general pre-election statement.
		/// </summary>
		[DataMember]
		public bool IsPrimaryGeneral { get; set; }

		/// <summary>
		/// Gets or sets whether or not the statement is required for candidates who have opted in.
		/// </summary>
		[DataMember]
		public bool PostOptInRequired { get; set; }

		/// <summary>
		/// Gets or sets whether or not the statement is a catch-up statement.
		/// </summary>
		[DataMember]
		public bool IsCatchUp { get; set; }

		/// <summary>
		/// Gets or sets whether or not the statement can be referred.
		/// </summary>
		[DataMember]
		public bool Deferrable { get; set; }

		/// <summary>
		/// Gets the statement number and due date details as a <see cref="String"/>.
		/// </summary>
		/// <returns>The statement number and due date as a <see cref="String"/>.</returns>
		public string ToDetailString()
		{
			return string.Format("{0} ({1:M/d/yyyy})", _number, this.DueDate);
		}

		/// <summary>
		/// Creates a new <see cref="Statement"/> obect for a specific statement number.
		/// </summary>
		/// <param name="number">The number of the statement.</param>
		public Statement(byte number)
		{
			_number = number;
		}

		/// <summary>
		/// Retrieves a collection of statement dates for an election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>A collection of statement dates for the election cycle specified, sorted and indexed by statement number.</returns>
		public static Dictionary<byte, Statement> GetStatementDatesByElectionCycle(string electionCycle)
		{
			return CPProviders.DataProvider.GetStatements(electionCycle);
		}
	}
}
