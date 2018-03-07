using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a filing date for a candidate.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class FilingDeadline : ResponseDeadlineBase
	{
		/// <summary>
		/// The statement number of this filing date.
		/// </summary>
		[DataMember(Name = "StatementNumber")]
		private readonly byte _statementNumber;

		/// <summary>
		/// Gets the statement number of this filing date.
		/// </summary>
		public byte StatementNumber
		{
			get { return _statementNumber; }
		}

		/// <summary>
		/// Gets or sets whether or not the filing date is required.
		/// </summary>
		[DataMember]
		public bool IsRequired { get; set; }

		/// <summary>
		/// Gets or sets the date the submission for this filing deadline was received.
		/// </summary>
		public DateTime? SubmissionReceivedDate
		{
			get { return this.ResponseReceivedDate; }
			set { this.ResponseReceivedDate = value; }
		}

		/// <summary>
		/// Gets whether or not a submission was received for the filing.
		/// </summary>
		public bool SubmissionReceived
		{
			get { return this.SubmissionReceivedDate.HasValue; }
		}

		/// <summary>
		/// Gets whether or not the submission for this filing deadline is late.
		/// </summary>
		public bool IsLate
		{
			get { return this.SubmissionReceivedDate.HasValue && this.SubmissionReceivedDate.Value > this.Date; }
		}

		/// <summary>
		/// Gets whether or not the submission for this filing deadline is missing.
		/// </summary>
		public bool IsMissing
		{
			get { return !this.SubmissionReceivedDate.HasValue && DateTime.Today > this.Date; }
		}

		/// <summary>
		/// Checks if a filing date is upcoming, required, and outstanding.
		/// </summary>
		public bool IsUpcomingOutstanding
		{
			get { return !this.SubmissionReceivedDate.HasValue && this.IsRequired && this.IsUpcoming; }
		}

		/// <summary>
		/// Grants an extension for a response to an audit review.
		/// </summary>
		/// <param name="extension">The new due date for the response.</param>
		/// <param name="grantDate">The date the extension was granted, if available.</param>
		/// <param name="number">The extension number, if available.</param>
		internal override void GrantExtension(DateTime extension, DateTime? grantDate, ushort? number)
		{
		}

		/// <summary>
		/// Creates a filing deadline by statement number.
		/// </summary>
		/// <param name="date">The filing deadline date.</param>
		/// <param name="statementNumber">The statement number of the filing deadline.</param>
		internal FilingDeadline(DateTime date, byte statementNumber)
			: base(date, CPCalendarItemType.FilingDeadline)
		{
			_statementNumber = statementNumber;
			this.Title = string.Format(Properties.CPCalendarItemResources.FilingDateTitle, _statementNumber);
			this.Description = string.Format(Properties.CPCalendarItemResources.FilingDateDescription, _statementNumber);
		}

		/// <summary>
		/// Retrieves filing dates and requirements for a particular candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose filing dates and requirements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of all filing dates and requirements on record for the specified candidate and election cycle.</returns>
		public static FilingDeadlines GetFilingDeadlines(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetFilingDeadlines(candidateID, electionCycle);
		}
	}
}
