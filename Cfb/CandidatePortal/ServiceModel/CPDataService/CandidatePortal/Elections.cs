using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.ActiveElectionCycleTdsTableAdapters;
using Cfb.Cfis.Data.ElectionCycleTdsTableAdapters;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Gets a collection of all supported election cycles.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <returns>A collection of all supported election cycles.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Elections GetElections(string cutoff);

        /// <summary>
        /// Gets a collection of all election cycles in which a candidate is active.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
        /// <returns>A collection of all election cycles in which the candidate is active.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Elections GetActiveElections(string cutoff, string candidateID);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Gets a collection of all supported election cycles.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <returns>A collection of all supported election cycles.</returns>
        public Elections GetElections(string cutoff)
        {
            Elections elections = new Elections();
            using (ElectionCycleTds ds = new ElectionCycleTds())
            {
                using (ElectionCyclesTableAdapter ta = new ElectionCyclesTableAdapter())
                {
                    ta.ExecuteWithBackupSource(delegate()
                    {
                        ta.Fill(ds.ElectionCycles, cutoff);
                    });
                }
                foreach (ElectionCycleTds.ElectionCyclesRow row in ds.ElectionCycles.Rows)
                {
                    int year;
                    Election election = new Election(row.ElectionCycle.Trim().ToUpper())
                    {
                        Year = int.TryParse(row.Year, out year) ? year : -1,
                        ElectionDate = row.IsElectionDateNull() ? null : row.ElectionDate as DateTime?,
                        IsTIE = !row.IsIsTIENull() && row.IsTIE
                    };
                    foreach (KeyValuePair<byte, Statement> pair in this.GetStatements(election.Cycle))
                    {
                        election.Statements.Add(pair.Key, pair.Value);
                    }
                    elections.Add(election);
                }
            }
            return elections;
        }

        /// <summary>
        /// Gets a collection of all election cycles in which a candidate is active.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
        /// <returns>A collection of all election cycles in which the candidate is active.</returns>
        public Elections GetActiveElections(string cutoff, string candidateID)
        {
            Elections elections = new Elections();
            using (ActiveElectionCycleTds ds = new ActiveElectionCycleTds())
            {
                using (ActiveElectionCyclesTableAdapter ta = new ActiveElectionCyclesTableAdapter())
                {
                    ta.ExecuteWithBackupSource(delegate()
                    {
                        ta.Fill(ds.ActiveElectionCycles, candidateID);
                    });
                }
                foreach (ActiveElectionCycleTds.ActiveElectionCyclesRow row in ds.ActiveElectionCycles.Rows)
                {
                    string ec = row.ElectionCycle.Trim().ToUpper();
                    if (cutoff.CompareTo(ec) <= 0)
                    {
                        Election election = new Election(ec);
                        foreach (KeyValuePair<byte, Statement> pair in this.GetStatements(election.Cycle))
                        {
                            election.Statements.Add(pair.Key, pair.Value);
                        }
                        elections.Add(election);
                    }
                }
            }
            return elections;
        }
    }
}
