using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's statement of need history for both primary and general elections.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class StatementOfNeedHistory
	{
		/// <summary>
		/// Gets the date when the candidate's statement of need history was last updated in CFIS.
		/// </summary>
		public DateTime LastUpdated
		{
			get
			{
				DateTime pdt = _primary.LastUpdated;
				DateTime gdt = _general.LastUpdated;
				return pdt.CompareTo(gdt) < 0 ? gdt : pdt;
			}
		}

		/// <summary>
		/// The statements of need for a primary election.
		/// </summary>
		[DataMember(Name = "Primary")]
		private readonly SubmissionHistory<StatementOfNeed> _primary;

		/// <summary>
		/// Gets the statements of need for a primary election.
		/// </summary>
		public SubmissionHistory<StatementOfNeed> Primary
		{
			get { return _primary; }
		}

		/// <summary>
		/// The statements of need for a general election.
		/// </summary>
		[DataMember(Name = "General")]
		private readonly SubmissionHistory<StatementOfNeed> _general;

		/// <summary>
		/// Gets the statements of need for a general election.
		/// </summary>
		public SubmissionHistory<StatementOfNeed> General
		{
			get { return _general; }
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="StatementOfNeed"/> records.
		/// </summary>
		public StatementOfNeedHistory()
		{
			_general = new SubmissionHistory<StatementOfNeed>();
			_primary = new SubmissionHistory<StatementOfNeed>();
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="StatementOfNeed"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of statements of need that the collection can initially store.</param>
		/// </summary>
		public StatementOfNeedHistory(int capacity)
		{
			_general = new SubmissionHistory<StatementOfNeed>(capacity);
			_primary = new SubmissionHistory<StatementOfNeed>(capacity);
		}

		/// <summary>
		/// Adds a statement of need to the appropriate collection by election.
		/// </summary>
		/// <param name="statementOfNeed">The statement of need to add.</param>
		public void Add(StatementOfNeed statementOfNeed)
		{
			if (statementOfNeed != null)
			{
				if (statementOfNeed.DocumentType == DocumentType.StatementOfNeedGeneral)
					_general.Documents.Add(statementOfNeed);
				else
					_primary.Documents.Add(statementOfNeed);
			}
		}

		/// <summary>
		/// Retrieves a generic collection of all statements of need on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose statements of need are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all statements of need on record for the specified candidate and election cycle.</returns>
		public static StatementOfNeedHistory GetStatementsOfNeed(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetStatementsOfNeed(candidateID, electionCycle);
		}
	}
}
