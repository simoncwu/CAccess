using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves messages from a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="view">The mailbox view type.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>A collection of <see cref="CmoMessage"/> objects for the specified mailbox sorted according to the properties of the mailbox.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		List<CmoMessage> GetMailboxMessages(string candidateID, CmoMailboxView view, HashSet<string> elections);

		/// <summary>
		/// Gets the total number of unread messages in a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>The total number of unread messges in the specified candidate's mailbox.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		uint CountUnopenedMailboxMessages(string candidateID, HashSet<string> elections);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves messages from a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="view">The mailbox view type.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>A collection of <see cref="CmoMessage"/> objects for the specified mailbox sorted according to the properties of the mailbox.</returns>
		public List<CmoMessage> GetMailboxMessages(string candidateID, CmoMailboxView view, HashSet<string> elections)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				IQueryable<Data.CmoMessage> matches = context.CmoMessages.Where(m => m.PostDate.HasValue && m.CandidateId == candidateID);
				switch (view)
				{
					case CmoMailboxView.All:
						break;
					case CmoMailboxView.Archived:
						matches = matches.Where(m => m.ArchiveDate.HasValue);
						break;
					case CmoMailboxView.FollowUp:
						matches = matches.Where(m => m.FollowUp);
						break;
					case CmoMailboxView.Unopened:
						matches = matches.Where(m => !m.OpenDate.HasValue);
						break;
					case CmoMailboxView.Current:
					default:
						matches = matches.Where(m => !m.ArchiveDate.HasValue);
						break;
				}
				if (elections != null && elections.Count > 0)
					matches = matches.Where(m => elections.Contains(m.ElectionCycle));
				return matches.AsEnumerable().Select(m => CreateCmoMessage(m)).ToList();
			}
		}

		/// <summary>
		/// Gets the total number of unread messages in a candidate's CMO mailbox.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the desired candidate.</param>
		/// <param name="elections">A collection of election cycles to filter against.</param>
		/// <returns>The total number of unread messges in the specified candidate's mailbox.</returns>
		public uint CountUnopenedMailboxMessages(string candidateID, HashSet<string> elections)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var messages = context.CmoMessages.Where(m => m.CandidateId == candidateID && m.PostDate.HasValue && !m.OpenDate.HasValue);
				if (elections != null && elections.Count > 0)
					messages = messages.Where(m => elections.Contains(m.ElectionCycle));
				return Convert.ToUInt32(messages.Count());
			}
		}
	}
}
