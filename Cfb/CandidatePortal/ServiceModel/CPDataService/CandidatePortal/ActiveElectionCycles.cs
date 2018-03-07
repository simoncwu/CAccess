using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.Linq;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.ActiveElectionCycleTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a collection of all election cycles in which a candidate is active.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all election cycles on record in which the candidate is active.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		HashSet<string> GetActiveElectionCycles(string candidateID, string cutoff);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a collection of all election cycles in which a candidate is active.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
		/// <param name="cutoff">The cutoff for supported election cycles.</param>
		/// <returns>A collection of all election cycles on record in which the candidate is active.</returns>
		public HashSet<string> GetActiveElectionCycles(string candidateID, string cutoff)
		{
			return new HashSet<string>(GetActiveElections(cutoff, candidateID).Keys);
		}
	}
}
