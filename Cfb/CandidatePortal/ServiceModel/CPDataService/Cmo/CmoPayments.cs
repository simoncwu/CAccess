using System;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Gets the unique message ID for a specific payment letter's corresponding CMO message.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate context.</param>
		/// <param name="paymentRun">The payment run number.</param>
		/// <returns>The unique ID of the CMO message for the specified payment run if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		string GetPaymentMessageID(string candidateID, byte paymentRun);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Gets the unique message ID for a specific payment letter's corresponding CMO message.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate context.</param>
		/// <param name="paymentRun">The payment run number.</param>
		/// <returns>The unique ID of the CMO message for the specified payment run if found; otherwise, null.</returns>
		public string GetPaymentMessageID(string candidateID, byte paymentRun)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var message = context.CmoMessages.OrderByDescending(m => m.PostDate).FirstOrDefault(m => m.CandidateId == candidateID && m.PostDate.HasValue && m.CmoAuditReview.ReviewNumber == paymentRun);
				return message == null ? null : CmoMessage.ToUniqueID(message.CandidateId, message.MessageId);
			}
		}
	}
}
