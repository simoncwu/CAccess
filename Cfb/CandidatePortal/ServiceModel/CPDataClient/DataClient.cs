using System;
using System.ServiceModel;
using Cfb.CandidatePortal.ServiceModel.CPDataClient.DataService;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
    /// <summary>
    /// A proxy for accessing C-Access and CFIS data by means of WCF client-server communication.
    /// </summary>
    internal class DataClient : DataServiceClient, IDisposable
    {
        /// <summary>
        /// Initializes a new instace of the <see cref="DataClient"/> class that is ready for retrieving data.
        /// </summary>
        internal DataClient()
        {
            this.Open();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // close connection
            switch (this.State)
            {
                case CommunicationState.Faulted:
                    this.Abort();
                    break;
                case CommunicationState.Closed:
                    break;
                default:
                    try
                    {
                        this.Close();
                    }
                    catch (CommunicationException)
                    {
                        this.Abort();
                    }
                    catch (TimeoutException)
                    {
                        this.Abort();
                    }
                    catch (Exception)
                    {
                        this.Abort();
                        throw;
                    }
                    break;
            }
        }

        #endregion
    }
}