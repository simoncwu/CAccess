using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using System;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    /// <summary>
    /// Defines WCF service methods for accessing C-Access data from SQL Server databases.
    /// </summary>
    [ServiceContract(Namespace = "http://caccess.nyccfb.info/schema/data", ProtectionLevel = ProtectionLevel.EncryptAndSign)]
    public partial interface IDataService
    {
        /// <summary>
        /// Attempts a fast handshake with the CFIS database to verify that it is online.
        /// </summary>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        void PingCfis();

        /// <summary>
        /// Retrieves a campaign entity registered with the CFB.
        /// </summary>
        /// <param name="candidateID">The ID for a candidate.</param>
        /// <param name="committeeID">The ID for a committee.</param>
        /// <param name="electionCycle">The election cycle for which a treasurer's committee is authorized.</param>
        /// <param name="liaisonID">The committee-relative ID for a liaison.</param>
        /// <returns>The entity that matches the specified criteria if found; otherwise, null.</returns>
        /// <remarks>If <paramref name="liaisonID"/> is provided (not null), a liaison is targeted. If <paramref name="committeeID"/> is not provided (null), a candidate is targeted.</remarks>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Entity GetEntity(string candidateID, char? committeeID = null, string electionCycle = null, byte? liaisonID = null);
    }
}
