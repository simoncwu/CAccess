using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.SubmissionDocuments
{
	/// <summary>
	/// Business object representation of a candidate's pre-election disclosure statement history for both primary and general elections.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class PreElectionDisclosureHistory
	{
		/// <summary>
		/// Gets the date when the candidate's pre-election disclosure statement history was last updated in CFIS.
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
		/// The pre-election disclosure statements for a primary election.
		/// </summary>
		[DataMember(Name = "Primary")]
		private readonly SubmissionHistory<PreElectionDisclosure> _primary;

		/// <summary>
		/// Gets the pre-election disclosure statements for a primary election.
		/// </summary>
		public SubmissionHistory<PreElectionDisclosure> Primary
		{
			get { return _primary; }
		}

		/// <summary>
		/// The pre-election disclosure statements for a general election.
		/// </summary>
		[DataMember(Name = "General")]
		private readonly SubmissionHistory<PreElectionDisclosure> _general;

		/// <summary>
		/// Gets the pre-election disclosure statements for a general election.
		/// </summary>
		public SubmissionHistory<PreElectionDisclosure> General
		{
			get { return _general; }
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="PreElectionDisclosure"/> records.
		/// </summary>
		public PreElectionDisclosureHistory()
		{
			_general = new SubmissionHistory<PreElectionDisclosure>();
			_primary = new SubmissionHistory<PreElectionDisclosure>();
		}

		/// <summary>
		/// Initializes a new, empty collection of <see cref="PreElectionDisclosure"/> records with a predefined initial capacity.
		/// <param name="capacity">The number of pre-election disclosure statements that the collection can initially store.</param>
		/// </summary>
		public PreElectionDisclosureHistory(int capacity)
		{
			_general = new SubmissionHistory<PreElectionDisclosure>(capacity);
			_primary = new SubmissionHistory<PreElectionDisclosure>(capacity);
		}

		/// <summary>
		/// Adds a pre-election disclosure statement to the appropriate collection by election.
		/// </summary>
		/// <param name="preDisclosure">The pre-election disclosure statement to add.</param>
		public void Add(PreElectionDisclosure preDisclosure)
		{
			if (preDisclosure != null)
			{
				if (preDisclosure.DocumentType == DocumentType.PreGeneralDisclosure)
					_general.Documents.Add(preDisclosure);
				else
					_primary.Documents.Add(preDisclosure);
			}
		}

		/// <summary>
		/// Retrieves a generic collection of all pre-election disclosure statements on record for the specified candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose pre-election disclosure statements are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A generic List collection of all pre-election disclosure statements on record for the specified candidate and election cycle.</returns>
		public static PreElectionDisclosureHistory GetPreElectionDisclosures(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetPreElectionDisclosures(candidateID, electionCycle);
		}
	}
}
