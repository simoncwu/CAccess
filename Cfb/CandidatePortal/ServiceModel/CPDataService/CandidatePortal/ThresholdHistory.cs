using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.ThresholdTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves a collection of the specified candidate's complete threshold status history for the specified election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of the specified candidate's complete threshold status history for the specified election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        ThresholdHistory GetThresholdHistory(string candidateID, string electionCycle);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves a collection of the specified candidate's complete threshold status history for the specified election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of the specified candidate's complete threshold status history for the specified election cycle.</returns>
        public ThresholdHistory GetThresholdHistory(string candidateID, string electionCycle)
        {
            Election election = GetElections(CPProviders.SettingsProvider.MinimumElectionCycle)[electionCycle];
            if (election == null || string.IsNullOrEmpty(candidateID))
                return null;
            using (ThresholdTds ds = new ThresholdTds())
            {
                using (ThresholdTableAdapter ta = new ThresholdTableAdapter())
                {
                    ta.Fill(ds.Threshold, candidateID, election.Cycle);
                }
                var statements = this.GetStatements(election.Cycle);
                ThresholdHistory history = new ThresholdHistory();
                ThresholdStatus status;
                foreach (ThresholdTds.ThresholdRow row in ds.Threshold.Rows)
                {
                    try
                    {
                        Statement statement;
                        if (!statements.TryGetValue((byte)row.Statement, out statement))
                            continue;
                        // if a new statement is being added, create a new status statement group
                        if (!history.History.TryGetValue(statement.Number, out status))
                        {
                            status = new ThresholdStatus(statement.Number);
                            history.History.Add(statement.Number, status);
                        }
                        NycBorough borough = CPConvert.ToNycBorough(row.BoroughCode.Trim());
                        status.Add(new ThresholdRevision(statement, CPConvert.ToThresholdRevisionType(row.Type.Trim()))
                        {
                            Date = row.Date,
                            Number = (ushort)row.Number,
                            NumberRequired = (ushort)row.NumberRequired,
                            Funds = row.Funds,
                            FundsRequired = row.FundsRequired,
                            Office = (borough != NycBorough.Unknown) ? new NycPublicOffice(borough) : new NycPublicOffice(CPConvert.ToNycPublicOfficeType(row.OfficeCode.Trim()))
                        });
                    }
                    catch (InvalidCastException)
                    {
                    }
                }
                return history;
            }
        }
    }
}
