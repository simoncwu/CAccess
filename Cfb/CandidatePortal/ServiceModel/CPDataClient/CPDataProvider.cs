using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.ServiceModel.CPDataClient.DataService;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// Provides access to C-Access candidate portal data.
    /// </summary>
    public partial class CPDataProvider : IDataProvider, ICmoDataProvider, IDisposable
    {
        private DataServiceClient _client;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_client == null)
                return;
            // close connection
            switch (_client.State)
            {
                case CommunicationState.Faulted:
                    _client.Abort();
                    break;
                case CommunicationState.Closed:
                    break;
                default:
                    try
                    {
                        _client.Close();
                    }
                    catch (CommunicationException)
                    {
                        _client.Abort();
                    }
                    catch (TimeoutException)
                    {
                        _client.Abort();
                    }
                    catch (Exception)
                    {
                        _client.Abort();
                        throw;
                    }
                    break;
            }
        }

        /// <summary>
        /// Ensures that the service proxy client exists and is in an Open state.
        /// </summary>
        protected void EnsureClientState()
        {
            if (_client == null)
            {
                _client = new DataServiceClient();
            }
            switch (_client.State)
            {
                case CommunicationState.Opened:
                    break;
                case CommunicationState.Created:
                    _client.Open();
                    break;
                default:
                    this.Dispose();
                    _client = null;
                    EnsureClientState();
                    break;
            }
        }

        /// <summary>
        /// Attempts a fast handshake with the CFIS database to verify that it is online.
        /// </summary>
        public void PingCfis()
        {
            this.EnsureClientState();
            _client.PingCfis();
        }

        /// <summary>
        /// Determines whether or not an exception indicates an offline data provider.
        /// </summary>
        /// <param name="e">The exception to analyze.</param>
        /// <returns>true if <paramref name="e"/> indicates that a data provider is offline.</returns>
        public bool IsOfflineDataException(Exception e)
        {
            return e as FaultException<OfflineDataException> != null;
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
            this.EnsureClientState();
            return _client.GetEntity(candidateID, committeeID, electionCycle, liaisonID);
        }

        #endregion
    }
}