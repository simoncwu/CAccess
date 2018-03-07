using System;
using System.ServiceModel;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient.SecurityService;

namespace Cfb.CandidatePortal.ServiceModel.CPSecurityClient
{
	/// <summary>
	/// A proxy for accessing C-Access security data by means of WCF client-server communication.
	/// </summary>
	internal class SecurityClient : SecurityServiceClient, IDisposable
	{
		/// <summary>
		/// Initializes a new instace of the <see cref="SecurityClient"/> class that is ready for retrieving data.
		/// </summary>
		internal SecurityClient()
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
