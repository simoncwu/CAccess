using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a compliance visit.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	[KnownType(typeof(CVResponseDeadline))]
	public class ComplianceVisit : AuditReviewBase
	{
		/// <summary>
		/// Gets the compliance visit number.
		/// </summary>
		public override byte Number
		{
			get { return _number; }
		}

		/// <summary>
		/// The compliance visit number.
		/// </summary>
		[DataMember(Name = "Number")]
		private readonly byte _number;

		/// <summary>
		/// Creates a new record of a compliance visit.
		/// </summary>
		/// <param name="electionCycle">The election cycle under review.</param>
		/// <param name="committeeID">The ID of the committee that has this appointment.</param>
		/// <param name="number">The compliance visit number.</param>
		/// <param name="appointment">The date of the appointment.</param>
		public ComplianceVisit(string electionCycle, char committeeID, byte number, DateTime appointment)
			: base(CPCalendarItemType.ComplianceVisit, electionCycle, committeeID)
		{
			this.SetDate(appointment);
			_number = number;
			this.IsAllDayEvent = true;
			this.Title = string.Format(Properties.CPCalendarItemResources.CVTitle, number);
			this.Description = Properties.CPCalendarItemResources.CVDescription;
		}
	}
}
