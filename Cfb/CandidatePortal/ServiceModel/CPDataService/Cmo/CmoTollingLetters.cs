using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.PostElection;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Sets tolling information for a tolling letter CMO message.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate context.</param>
		/// <param name="messageID">The ID of the message to update.</param>
		/// <param name="eventNumber">The tolling letter event number.</param>
		/// <param name="letter">The tolling letter to set or clear.</param>
		/// <returns>true if the tolling letter codes were set successfully; otherwise, false.</returns>
		/// <remarks>The tolling letter codes can only be set if the message already exists and is of the tolling letter category.</remarks>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		bool SetCmoMessageTolling(string candidateID, int messageID, int eventNumber, TollingLetter letter);

		/// <summary>
		/// Gets a collection of IDs for all post election audit tolling messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <param name="far">true to specify Final Audit Report tolling; otherwise, false to specify Draft Audit Report tolling.</param>
		/// <returns>A collection of unique IDs for all statement review CMO messages found.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Dictionary<int, string> GetTollingMessageIDs(string candidateID, string electionCycle, bool far);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Sets tolling information for a tolling letter CMO message.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate context.</param>
		/// <param name="messageID">The ID of the message to update.</param>
		/// <param name="eventNumber">The tolling letter event number.</param>
		/// <param name="letter">The tolling letter to set or clear.</param>
		/// <returns>true if the tolling letter codes were set successfully; otherwise, false.</returns>
		/// <remarks>The tolling letter codes can only be set if the message already exists and is of the tolling letter category.</remarks>
		public bool SetCmoMessageTolling(string candidateID, int messageID, int eventNumber, TollingLetter letter)
		{
			CmoMessage message = GetCmoMessage(candidateID, messageID);
			message.TollingLetter = letter;
			message.TollingEventNumber = eventNumber;
			return SetCmoMessageTolling(message);
		}

		/// <summary>
		/// Gets a collection of IDs for all post election audit tolling messages for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the reviewed candidate.</param>
		/// <param name="electionCycle">The election cycle of the reviews.</param>
		/// <param name="far">true to specify Final Audit Report tolling; otherwise, false to specify Draft Audit Report tolling.</param>
		/// <returns>A collection of unique IDs for all tolling event CMO messages found, indexed by tolling event ID.</returns>
		public Dictionary<int, string> GetTollingMessageIDs(string candidateID, string electionCycle, bool far)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var letters = GetTollingLetters();
				Dictionary<int, string> ids = new Dictionary<int, string>();
				foreach (var evt in context.CmoGetCandidateTollingEvents(candidateID, electionCycle, far))
				{
					ids[letters.Any(l => l.ID == evt.LetterId && l.IsInadequate) ? -evt.EventNumber : evt.EventNumber] = CmoMessage.ToUniqueID(candidateID, evt.MessageId);
				}
				return ids;
			}
		}
	}
}
