using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.CsuLiaisonTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a collection of CSU liaisons indexed by ID.
		/// </summary>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of CSU liaisons for the specified election cycle, indexed by ID.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<byte, CsuLiaison> GetCsuLiaisons(string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a collection of CSU liaisons indexed by ID.
		/// </summary>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>A collection of CSU liaisons for the specified election cycle, indexed by ID.</returns>
		public Dictionary<byte, CsuLiaison> GetCsuLiaisons(string electionCycle)
		{
			using (CsuLiaisonTds ds = new CsuLiaisonTds())
			{
				using (CsuLiaisonsTableAdapter ta = new CsuLiaisonsTableAdapter())
				{
					ta.Fill(ds.CsuLiaisons, electionCycle);
				}
				Dictionary<byte, CsuLiaison> c = new Dictionary<byte, CsuLiaison>(ds.CsuLiaisons.Count);
				foreach (CsuLiaisonTds.CsuLiaisonsRow row in ds.CsuLiaisons)
				{
					c[row.ID] = new CsuLiaison(row.ID)
					{
						Name = row.Name.Trim(),
						LastUpdated = row.LastUpdated
					};
				}
				return c;
			}
		}
	}
}
