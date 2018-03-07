using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a disclosure statement review.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(SRResponseDeadline))]
	public class StatementReview : AuditReviewBase
	{
		/// <summary>
		/// Gets the number of the statement being reviewed.
		/// </summary>
		public override byte Number
		{
			get { return _statement == null ? (byte)0 : _statement.Number; }
		}

		/// <summary>
		/// The disclosure statement being reviewed.
		/// </summary>
		[DataMember(Name = "Statement")]
		private readonly Statement _statement;

		/// <summary>
		/// Gets the disclosure statement being reviewed.
		/// </summary>
		public Statement Statement
		{
			get { return _statement; }
		}

		/// <summary>
		/// Creates a new record of a completed statement review.
		/// </summary>
		/// <param name="electionCycle">The election cycle under review.</param>
		/// <param name="committeeID">The ID of the committee under review.</param>
		/// <param name="statement">The disclosure statement under review.</param>
		public StatementReview(string electionCycle, char committeeID, Statement statement)
			: base(CPCalendarItemType.StatementReview, electionCycle, committeeID)
		{
			_statement = statement;
			this.IsAllDayEvent = true;
			this.Title = string.Format(Properties.CPCalendarItemResources.SRTitle, _statement.Number);
			this.Description = string.Format(Properties.CPCalendarItemResources.SRDescription, _statement.Number);
		}
	}
}
