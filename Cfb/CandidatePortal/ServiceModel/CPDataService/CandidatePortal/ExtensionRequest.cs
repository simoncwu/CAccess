using System;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Data;
using Cfb.CandidatePortal.Data.ExtensionRequestsTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves a specific extension request.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate who requested the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension was requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension was requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		/// <returns>An <see cref="ExtensionRequest"/> representing the specified extension request if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		ExtensionRequest GetExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration);

		/// <summary>
		/// Creates a new extension request and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension is being requested.</param>
		/// <param name="date">The date of the extension request.</param>
		/// <param name="requestedDueDate">The requested extension due date.</param>
		/// <param name="reason">The reason for the extension.</param>
		/// <returns>A new <see cref="ExtensionRequest"/> object if the extension request was added successfully; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		ExtensionRequest AddExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, DateTime date, DateTime requestedDueDate, string reason);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves a specific extension request.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate who requested the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension was requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension was requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		/// <returns>An <see cref="ExtensionRequest"/> representing the specified extension request if found; otherwise, null.</returns>
		public ExtensionRequest GetExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration)
		{
			using (ExtensionRequestsTableAdapter ta = new ExtensionRequestsTableAdapter())
			{
				return GetRequest(ta, candidateID, electionCycle, type, reviewNumber, iteration);
			}
		}

		/// <summary>
		/// Retrieves a specific extension request.
		/// </summary>
		/// <param name="adapter">The <see cref="ExtensionRequestsTableAdapter"/> to use for access to the storage medium.</param>
		/// <param name="candidateID">The ID of the candidate who requested the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension was requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension was requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		/// <returns>An <see cref="ExtensionRequest"/> representing the specified extension request if found; otherwise, false.</returns>
		private ExtensionRequest GetRequest(ExtensionRequestsTableAdapter adapter, string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration)
		{
			using (ExtensionRequestsTds ds = new ExtensionRequestsTds())
			{
				if (adapter == null)
				{
					using (adapter = new ExtensionRequestsTableAdapter())
					{
						adapter.FillRequestBy(ds.ExtensionRequests, candidateID, electionCycle, Convert.ToByte(type), reviewNumber, iteration);
					}
				}
				else
				{
					adapter.FillRequestBy(ds.ExtensionRequests, candidateID, electionCycle, Convert.ToByte(type), reviewNumber, iteration);
				}
				foreach (ExtensionRequestsTds.ExtensionRequestsRow row in ds.ExtensionRequests.Rows)
				{
					return new ExtensionRequest(candidateID, electionCycle, type, reviewNumber, iteration, row.Version)
					{
						Date = row.Date,
						RequestedDueDate = row.RequestedDueDate,
						Reason = row.Reason
					};
				}
			}
			return null;
		}

		/// <summary>
		/// Creates a new extension request and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review for which the extension is being requested.</param>
		/// <param name="date">The date of the extension request.</param>
		/// <param name="requestedDueDate">The requested extension due date.</param>
		/// <param name="reason">The reason for the extension.</param>
		/// <returns>A new <see cref="ExtensionRequest"/> object if the extension request was added successfully; otherwise, null.</returns>
		public ExtensionRequest AddExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, DateTime date, DateTime requestedDueDate, string reason)
		{
			using (ExtensionRequestsTableAdapter ta = new ExtensionRequestsTableAdapter())
			{
				byte iteration = Convert.ToByte(ta.SaveExtensionRequest(candidateID, electionCycle, Convert.ToByte(type), reviewNumber, null, date, requestedDueDate, reason, new byte[] { }));
				return iteration > byte.MinValue ? GetRequest(ta, candidateID, electionCycle, type, reviewNumber, iteration) : null;
			}
		}
	}
}
