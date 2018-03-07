using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient;
using Cfb.Cfis.CampaignContacts;
using Cfb.CandidatePortal.SharePoint;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    /// <summary>
    /// Provides access to C-Access data from back-end SQL Server databases via WCF service methods.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = int.MaxValue, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed partial class DataService : IDataService, IDataProvider, ICmoDataProvider
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DataService"/> class.
        /// </summary>
        public DataService()
        {
            GC.KeepAlive(CPProviders.DataProvider = this);
            GC.KeepAlive(CmoProviders.DataProvider = this);
            GC.KeepAlive(CPProviders.SettingsProvider = new CPSettingsProvider());
            GC.KeepAlive(CPSecurity.Provider = new SecurityProvider());
        }

        /// <summary>
        /// Attempts a fast handshake with the CFIS database to verify that it is online.
        /// </summary>
        public void PingCfis()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Cfb.Cfis.Data.Properties.Settings.CfispubConnectionString"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("cfb_cp_Ping", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Parses a committee ID string for the committee ID character.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to parse.</param>
        /// <returns>The character value of <paramref name="value"/> if it is not empty; otherwise, <see cref="Char.MinValue"/>.</returns>
        private char ParseCommitteeID(string value)
        {
            if (value != null)
                value = value.Trim();
            return string.IsNullOrEmpty(value) || value.Trim().Length < 1 ? char.MinValue : value[0];
        }

        #region IDataProvider Members

        /// <summary>
        /// Retrieves a campaign entity registered with the CFB.
        /// </summary>
        /// <param name="candidateID">The ID for a candidate.</param>
        /// <param name="committeeID">The ID for a committee.</param>
        /// <param name="electionCycle">The election cycle for which a treasurer's committee is authorized.</param>
        /// <param name="liaisonID">The committee-relative ID for a liaison.</param>
        /// <returns>The entity that matches the specified criteria if found; otherwise, null.</returns>
        /// <remarks>If <paramref name="liaisonID"/> is provided (not null), a liaison is targeted. If <paramref name="committeeID"/> is not provided (null), a candidate is targeted.</remarks>
        public Entity GetEntity(string candidateID, char? committeeID = null, string electionCycle = null, byte? liaisonID = null)
        {
            if (!committeeID.HasValue)
                return GetCandidate(candidateID);
            char commID = committeeID.Value;
            if (liaisonID.HasValue)
            {
                var liaisons = GetLiaisonsByCommittee(candidateID, commID);
                if (liaisons != null && liaisons.ContainsKey(liaisonID.Value))
                {
                    return liaisons[liaisonID.Value];
                }
            }
            else
            {
                var comms = GetAuthorizedCommittees(candidateID, electionCycle);
                if (comms != null && comms.Committees.ContainsKey(commID))
                {
                    return comms.Committees[commID].Treasurer;
                }
            }
            return null;
        }

        public SubmissionDocuments.FilerRegistrationHistory GetFilerRegistrationDocuments(string candidateID, string electionCycle)
        {
            return this.GetFilerRegistrations(candidateID, electionCycle);
        }

        public StatementReviews GetPrincipalStatementReviews(string candidateID, Election election)
        {
            return this.GetPrincipalStatementReviews(candidateID, election.Cycle);
        }

        public StatementReviews GetStatementReviews(string candidateID, Election election)
        {
            return this.GetStatementReviews(candidateID, election.Cycle);
        }

        System.Collections.Generic.List<PostElection.ITollingEvent> IDataProvider.GetTollingEvents(string candidateID, string electionCycle, bool far)
        {
            return new System.Collections.Generic.List<PostElection.ITollingEvent>(this.GetTollingEvents(candidateID, electionCycle, far));
        }

        public System.Collections.Generic.Dictionary<byte, Liaison> GetLiaisonsByCommittee(string candidateID, char committeeID)
        {
            return this.GetLiaisons(candidateID, committeeID);
        }

        public System.Collections.Generic.Dictionary<byte, BankAccount> GetBankAccountsByCommittee(string candidateID, string electionCycle, char committeeID)
        {
            return this.GetBankAccounts(candidateID, electionCycle, committeeID);
        }

        public System.Collections.Generic.IEnumerable<Announcement> GetAnnouncements(string electionCycle)
        {
            return AnnouncementsClient.GetAnnouncements(electionCycle);
        }

        public Announcement GetAnnouncement(string id)
        {
            return AnnouncementsClient.GetAnnouncement(id);
        }

        public Announcement GetFilingDateAnnouncement(byte statementNumber, string electionCycle)
        {
            return AnnouncementsClient.GetFilingDateAnnouncement(statementNumber, electionCycle);
        }

        public bool IsOfflineDataException(Exception e)
        {
            return e as FaultException<OfflineDataException> != null;
        }

        #endregion

        #region ICmoDataProvider Members


        public bool Open(CmoMessage message, string username)
        {
            return this.OpenCmoMessage(message.CandidateID, message.ID, username, message.Version);
        }

        public bool SetArchiveStatus(CmoMessage message, bool archived, string username)
        {
            return this.SetCmoMessageArchiveStatus(message.CandidateID, message.ID, archived, username, message.Version);
        }

        public bool SetFlagStatus(CmoMessage message, bool flagged, string username)
        {
            return this.SetCmoMessageFlagStatus(message.CandidateID, message.ID, flagged, username, message.Version);
        }

        public CmoAttachment Attach(CmoMessage message, byte[] data, string name)
        {
            return this.AddCmoAttachment(message.CandidateID, message.ID, data, name);
        }

        public bool Detach(CmoMessage message, byte attachmentID)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CmoMessage message)
        {
            return this.Delete(message.CandidateID, message.ID, message.Version);
        }

        public CmoMessage GetCmoMessageByCandidateMessage(string candidateID, int messageID)
        {
            return this.GetCmoMessage(candidateID, messageID);
        }

        #endregion

        #region IGlobalSettingsProvider Members

        public DateTime LastReminderDistributionDate
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string MessageSenderDisplayName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string AdministratorsGroupName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string AccountManagersGroupName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}
