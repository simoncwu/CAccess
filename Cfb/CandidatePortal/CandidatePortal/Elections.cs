using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A collection of <see cref="Election"/> objects, indexed by election cycle.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public sealed class Elections : IEnumerable
	{
		/// <summary>
		/// Back-end data storage collection of all elections.
		/// </summary>
		[DataMember(Name = "Elections")]
		private readonly Dictionary<string, Election> _elections;

		/// <summary>
		/// Gets a collection containing the keys in the collection.
		/// </summary>
		public Dictionary<string, Election>.KeyCollection Keys
		{
			get { return _elections.Keys; }
		}

		/// <summary>
		/// Gets a collection containing the elections in the collection.
		/// </summary>
		public Dictionary<string, Election>.ValueCollection Values
		{
			get { return _elections.Values; }
		}

		/// <summary>
		/// Retrieves an election cycle from the collection by name.
		/// </summary>
		/// <param name="electionCycle">The name of the election to get.</param>
		/// <returns>An <see cref="Election"/> object for the requested election cycle.</returns>
		public Election this[string electionCycle]
		{
			get
			{
				Election e;
				return _elections.TryGetValue(electionCycle, out e) ? e : null;
			}
		}

		/// <summary>
		/// Gets the number of elections contained in the collection.
		/// </summary>
		public int Count
		{
			get { return _elections.Count; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Elections"/> class.
		/// </summary>
		internal Elections()
		{
			_elections = new Dictionary<string, Election>();
		}

		/// <summary>
		/// Adds the specified election to the collection.
		/// </summary>
		/// <param name="election">The election to add.</param>
		internal void Add(Election election)
		{
			if (election != null)
				_elections.Add(election.Cycle, election);
		}

		/// <summary>
		/// Creates a <see cref="List{Election}"/> from the values in the collection.
		/// </summary>
		/// <returns>A <see cref="List{Election}"/> collection of the <see cref="Election"/> objects in this collection.</returns>
		public List<Election> ToList()
		{
			return _elections.Values.ToList();
		}

		#region IEnumerable Members

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>An enumerator structure for the collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return _elections.GetEnumerator();
		}

		#endregion

		/// <summary>
		/// Retrieves the most current election cycle in the collection.
		/// </summary>
		/// <returns>The most current election cycle in the collection that has not already passed if found; otherwise, the latest election cycle in the collection. If the collection is empty, returns null.</returns>
		public string GetCurrentElectionCycle()
		{
			if (_elections.Count == 0)
				return null;
			var current = from f in _elections.Values
						  where f.ElectionDate.HasValue && f.ElectionDate.Value >= DateTime.Today
						  orderby f.ElectionDate.Value
						  select f;
			return current.Count() > 0 ? current.First().Cycle : _elections.Values.First().Cycle;
		}

		/// <summary>
		/// Applies a filter to the values in the collection.
		/// </summary>
		/// <param name="filter">A collection of election cycles to filter the current values by. If the collection is null, no filtering is performed.</param>
		public void ApplyFilter(IEnumerable<string> filter)
		{
			if (filter == null) return;
			var deletes = _elections.Keys.Except(filter).ToList();
			if (deletes.Count() > 0)
			{
				foreach (string e in deletes)
				{
					_elections.Remove(e);
				}
			}
		}

		/// <summary>
		/// Retrieves a collection of all election cycles in which a candidate is active.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all election cycles on record in which the candidate is active.</returns>
		public static HashSet<string> GetActiveElectionCycles(string candidateID, string cutoff = null)
		{
			return CPProviders.DataProvider.GetActiveElectionCycles(candidateID, cutoff ?? CPProviders.SettingsProvider.MinimumElectionCycle);
		}

		/// <summary>
		/// Gets a collection of all supported election cycles.
		/// </summary>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all supported election cycles.</returns>
		public static Elections GetElections(string cutoff = null)
		{
			return CPProviders.DataProvider.GetElections(cutoff ?? CPProviders.SettingsProvider.MinimumElectionCycle);
		}
	}
}
