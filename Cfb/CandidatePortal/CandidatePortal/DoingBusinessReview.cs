using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a Doing Business review.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(DbrResponseDeadline))]
	public class DoingBusinessReview : AuditReviewBase
	{
		/// <summary>
		/// Gets the number of the disclosure statement being reviewed.
		/// </summary>
		public override byte Number
		{
			get { return _statement == null ? byte.MinValue : _statement.Number; }
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
		/// Creates a new record of a completed Doing Business review.
		/// </summary>
		/// <param name="electionCycle">The election cycle under review.</param>
		/// <param name="committeeID">The ID of the committee under review.</param>
		/// <param name="statement">The disclosure statement under review.</param>
		/// <exception cref="ArgumentNullException"><paramref name="statement"/> is null.</exception>
		public DoingBusinessReview(string electionCycle, char committeeID, Statement statement)
			: base(CPCalendarItemType.DoingBusinessReview, electionCycle, committeeID)
		{
			if (statement == null)
				throw new ArgumentNullException("statement", "Statement cannot be null.");
			_statement = statement;
			this.IsAllDayEvent = true;
			byte stnum = _statement.Number;
			this.Title = string.Format(Properties.CPCalendarItemResources.DbrTitle, stnum);
			this.Description = string.Format(Properties.CPCalendarItemResources.DbrDescription, stnum);
		}
	}
}
