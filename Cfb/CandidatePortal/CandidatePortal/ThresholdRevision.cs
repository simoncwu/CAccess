using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a single revision of a candidate's threshold status.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ThresholdRevision
	{
		/// <summary>
		/// The number of the statement reviewed that determines threshold status.
		/// </summary>
		[DataMember(Name = "Statement")]
		private readonly Statement _statement;

		/// <summary>
		/// Gets the number of the statement reviewed that determines threshold status.
		/// </summary>
		public Statement Statement
		{
			get { return _statement; }
		}

		/// <summary>
		/// Gets whether or not the threshold status is undetermined due to an office status of "undeclared".
		/// </summary>
		public bool IsUndetermined
		{
			get { return this.NumberRequired == 0; }
		}

		/// <summary>
		/// The type of threshold status revision.
		/// </summary>
		[DataMember(Name = "Type")]
		private readonly ThresholdRevisionType _type;

		/// <summary>
		/// Gets the type of threshold status revision.
		/// </summary>
		public ThresholdRevisionType Type
		{
			get { return _type; }
		}

		/// <summary>
		/// Gets or sets the date of the threshold status revision.
		/// </summary>
		[DataMember]
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the number of matching contributors valid for meeting threshold.
		/// </summary>
		[DataMember]
		public ushort Number { get; set; }

		/// <summary>
		/// Gets or sets the number of valid matching contributors required for meeting threshold.
		/// </summary>
		[DataMember]
		public ushort NumberRequired { get; set; }

		/// <summary>
		/// Gets or sets the amount of matching contribution dollars valid for meeting threshold.
		/// </summary>
		[DataMember]
		public decimal Funds { get; set; }

		/// <summary>
		/// Gets or sets the amount of valid matching contribution dolalrs required for meeting threshold.
		/// </summary>
		[DataMember]
		public decimal FundsRequired { get; set; }

		/// <summary>
		/// Gets or sets the public office upon which threshold figures are determined.
		/// </summary>
		[DataMember]
		public NycPublicOffice Office { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ThresholdRevision"/> class.
		/// </summary>
		/// <param name="statement">The number of the statement reviewed</param>
		/// <param name="type">The type of threshold status to create.</param>
		internal ThresholdRevision(Statement statement, ThresholdRevisionType type)
		{
			_statement = statement;
			_type = type;
		}
	}
}
