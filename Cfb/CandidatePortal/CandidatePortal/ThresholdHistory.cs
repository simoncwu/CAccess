using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a candidate's threshold status history, indexed by statement number.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ThresholdHistory
	{
		/// <summary>
		/// The inner collection of threshold histories.
		/// </summary>
		[DataMember(Name = "History")]
		private Dictionary<byte, ThresholdStatus> _history;

		/// <summary>
		/// Gets the inner collection of threshold histories.
		/// </summary>
		public Dictionary<byte, ThresholdStatus> History
		{
			get { return _history; }
		}

		/// <summary>
		/// Gets the most recent cumulative threshold status totals.
		/// </summary>
		public ThresholdRevision Current
		{
			get
			{
				if ((_history.Count > 0) && (_history.Values.First().Count > 0))
					return _history.Values.First().First();
				else
					return null;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThresholdHistory"/> class.
		/// </summary>
		internal ThresholdHistory()
		{
			_history = new Dictionary<byte, ThresholdStatus>();
		}

		/// <summary>
		/// Retrieves a collection of the specified candidate's complete threshold status history for the specified election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of the specified candidate's complete threshold status history for the specified election cycle.</returns>
		public static ThresholdHistory GetThresholdHistory(string candidateID, string electionCycle)
		{
			return CPProviders.DataProvider.GetThresholdHistory(candidateID, electionCycle);
		}
	}
}
