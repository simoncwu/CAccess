using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.AuthorizedCommitteeTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves all bank accounts on record for the specified candidate, election cycle, and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose bank accounts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="committeeID">The ID of the committee whose bank accounts are to be retrieved.</param>
        /// <returns>A collection of all bank accounts on record for the specified candidate, election cycle, and committee, indexed by account ID.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Dictionary<byte, BankAccount> GetBankAccounts(string candidateID, string electionCycle, char committeeID);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves all bank accounts on record for the specified candidate, election cycle, and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose bank accounts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="committeeID">The ID of the committee whose bank accounts are to be retrieved.</param>
        /// <returns>A collection of all bank accounts on record for the specified candidate, election cycle, and committee, indexed by account ID.</returns>
        public Dictionary<byte, BankAccount> GetBankAccounts(string candidateID, string electionCycle, char committeeID)
        {
            using (AuthorizedCommitteeTds ds = new AuthorizedCommitteeTds())
            {
                using (BankAccountsTableAdapter ta = new BankAccountsTableAdapter())
                {
                    ta.Fill(ds.BankAccounts, candidateID, electionCycle, committeeID.ToString());
                }
                Dictionary<byte, BankAccount> d = new Dictionary<byte, BankAccount>(ds.BankAccounts.Count);
                foreach (AuthorizedCommitteeTds.BankAccountsRow row in ds.BankAccounts.Rows)
                {
                    byte id;
                    if (byte.TryParse(row.BankAccountID, out id))
                    {
                        d.Add(id, new BankAccount(id)
                        {
                            BankName = row.BankName.Trim(),
                            City = row.City.Trim(),
                            State = row.State.Trim(),
                            Zip = new ZipCode(row.Zip.Trim()),
                            Number = row.Number.Trim(),
                            Name = row.Name.Trim(),
                            OpeningDate = row.IsOpeningDateNull() ? null : row.OpeningDate as DateTime?,
                            ClosingDate = row.IsClosingDateNull() ? null : row.ClosingDate as DateTime?,
                            CurrentBalanceDate = row.IsCurrentBalanceDateNull() ? null : row.CurrentBalanceDate as DateTime?,
                            CurrentBalance = row.CurrentBalance,
                            Type = CPConvert.ToBankAccountType(row.AccountTypeCode.Trim()),
                            OtherTypeSpecification = row.AccountTypeOther.Trim(),
                            Purpose = CPConvert.ToBankAccountPurpose(row.PurposeCode.Trim()),
                            OtherPurposeSpecification = row.PurposeOther.Trim()
                        });
                    }
                }
                return d;
            }
        }
    }
}
