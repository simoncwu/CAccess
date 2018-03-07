using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using CmoRepository = Cfb.CandidatePortal.Cmo.Cmo;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Adds a CMO message attachment via a stream representation of the attchment.
		/// </summary>
		/// <param name="stream">A stream representing an attachment for a CMO message.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "AddCmoAttachmentStream")]
		[FaultContract(typeof(OfflineDataException))]
		CmoAttachment AddCmoAttachment(CmoAttachmentStream stream);

		/// <summary>
		/// Adds a CMO message attachment.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the targeted message.</param>
		/// <param name="data">The raw binary attachment data.</param>
		/// <param name="name">The attachment file name to be used for display.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CmoAttachment AddCmoAttachment(string candidateID, int messageID, byte[] data, string name);

		/// <summary>
		/// Deletes a CMO message attachment instance from the persistence storage medium.
		/// </summary>
		/// <param name="attachment">The CMO message attachment to delete.</param>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "DeleteCmoAttachment")]
		[FaultContract(typeof(OfflineDataException))]
		bool Delete(CmoAttachment attachment);

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="uniqueID">The unique ID of the attachment to retrieve.</param>
		/// <returns>The requested <see cref="CmoAttachment"/> if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		CmoAttachment GetCmoAttachment(string uniqueID);

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the message the attachment is for.</param>
		/// <param name="id">The ID of the attachment to retrieve.</param>
		/// <returns>A <see cref="CmoAttachment"/> instance that matches the specified criteria if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "GetCmoAttachmentByIDs")]
		[FaultContract(typeof(OfflineDataException))]
		CmoAttachment GetCmoAttachment(string candidateID, int messageID, byte id);

		/// <summary>
		/// Retrieves the full system file path to an attachment.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the message the attachment is for.</param>
		/// <param name="id">The ID of the attachment to retrieve.</param>
		/// <returns>A system file path to the requested attachment if found; otherwise, null.</returns>
		/// <remarks>A UNC file path should be returned, provided the application configuration file defines the CMO repository location as a UNC file share location.</remarks>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		string GetCmoAttachmentPath(string candidateID, int messageID, byte id);

		/// <summary>
		/// Updates a CMO message attachment instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="attachment">The CMO message attachment to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign, Name = "UpdateCmoAttachment")]
		[FaultContract(typeof(OfflineDataException))]
		bool Update(CmoAttachment attachment);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Represents the method that will handle an attachment save event.
		/// </summary>
		/// <param name="attachment">The attachment to save.</param>
		/// <returns>true if the attachment save event completed successfully; otherwise, false.</returns>
		private delegate bool SaveAttachmentEventHandler(CmoAttachment attachment);

		/// <summary>
		/// Adds a CMO message attachment via a stream representation of the attchment.
		/// </summary>
		/// <param name="stream">A stream representing an attachment for a CMO message.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		public CmoAttachment AddCmoAttachment(CmoAttachmentStream stream)
		{
			return stream != null && stream.CanRead ? AddCmoAttachment(stream.CandidateID, stream.MessageID, stream.GetBuffer(), stream.Name) : null;
		}

		/// <summary>
		/// Adds an attachment to a CMO message.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the targeted message.</param>
		/// <param name="data">The raw binary attachment data.</param>
		/// <param name="name">The attachment file name to be used for display.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		public CmoAttachment AddCmoAttachment(string candidateID, int messageID, byte[] data, string name)
		{
			return AddCmoAttachment(candidateID, messageID, name, new SaveAttachmentEventHandler(delegate(CmoAttachment attachment) { return CmoRepository.Repository.Save(attachment, data); }));
		}

		/// <summary>
		/// Adds an attachment to a CMO message.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the targeted message.</param>
		/// <param name="name">The attachment file name to be used for display.</param>
		/// <param name="saveAttachment">The delegate method to use for saving the attachment.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		private CmoAttachment AddCmoAttachment(string candidateID, int messageID, string name, SaveAttachmentEventHandler saveAttachment)
		{
			CmoMessage owner = GetCmoMessage(candidateID, messageID);
			if ((owner != null) && (CmoRepository.Repository != null))
			{
				using (Data.CmoEntities context = new Data.CmoEntities())
				{
					var ret = context.CmoSaveAttachment(candidateID, messageID, byte.MinValue, name).FirstOrDefault();
					byte id;
					if (ret.HasValue && byte.TryParse(ret.Value.ToString(), out id) && id > byte.MinValue)
					{
						CmoAttachment attachment = new CmoAttachment(candidateID, messageID, id);
						attachment.FileName = name;
						try
						{
							if (saveAttachment != null && saveAttachment(attachment))
							{
								return attachment;
							}
							else
							{
								// rollback attachment if it could not be saved
								CmoRepository.Repository.Delete(attachment);
								Delete(attachment);
							}
						}
						catch (Exception e)
						{
							// rollback attachment if it could not be saved
							CmoRepository.Repository.Delete(attachment);
							Delete(attachment);
							throw e;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the message the attachment is for.</param>
		/// <param name="id">The ID of the attachment to retrieve.</param>
		/// <returns>A <see cref="CmoAttachment"/> instance that matches the specified criteria if found; otherwise, null.</returns>
		public CmoAttachment GetCmoAttachment(string candidateID, int messageID, byte id)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				return CreateCmoAttachment(context.CmoAttachments.FirstOrDefault(a => a.CandidateId == candidateID && a.MessageId == messageID && a.AttachmentId == id));
			}
		}

		/// <summary>
		/// Retrieves the full system file path to an attachment.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate the attachment is for.</param>
		/// <param name="messageID">The ID of the message the attachment is for.</param>
		/// <param name="id">The ID of the attachment to retrieve.</param>
		/// <returns>A system file path to the requested attachment if found; otherwise, null.</returns>
		/// <remarks>A UNC file path should be returned, provided the application configuration file defines the CMO repository location as a UNC file share location.</remarks>
		public string GetCmoAttachmentPath(string candidateID, int messageID, byte id)
		{
			return CmoRepository.Repository == null ? null : CmoRepository.Repository.GetPath(this.GetCmoAttachment(candidateID, messageID, id));
		}

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="uniqueID">The unique ID of the attachment to retrieve.</param>
		/// <returns>The requested <see cref="CmoAttachment"/> if found; otherwise, null.</returns>
		public CmoAttachment GetCmoAttachment(string uniqueID)
		{
			if (!string.IsNullOrEmpty(uniqueID))
			{
				string[] ids = uniqueID.Split(new char[] { '-' }, 3, StringSplitOptions.None);
				if (ids.Length == 3)
				{
					int messageID;
					if (int.TryParse(ids[1], out messageID))
					{
						byte attachmentID;
						if (byte.TryParse(ids[2], out attachmentID))
						{
							return GetCmoAttachment(ids[0], messageID, attachmentID);
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Updates a CMO message attachment instance in the persistence storage medium by overwriting the existing record.
		/// </summary>
		/// <param name="attachment">The CMO message attachment to update.</param>
		/// <returns>true if this instance was saved successfuly; otherwise, false.</returns>
		public bool Update(CmoAttachment attachment)
		{
			using (Data.CmoEntities context = new Data.CmoEntities())
			{
				var data = context.CmoAttachments.FirstOrDefault(a => a.CandidateId == attachment.CandidateID && a.MessageId == attachment.MessageID && a.AttachmentId == attachment.ID);
				if (data != null)
				{
					data.AttachmentName = attachment.FileName;
					try
					{
						return context.SaveChanges() > 0;
					}
					catch (OptimisticConcurrencyException) { }
				}
				return false;
			}
		}

		/// <summary>
		/// Deletes a CMO message attachment instance from the persistence storage medium.
		/// </summary>
		/// <param name="attachment">The CMO message attachment to delete.</param>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		public bool Delete(CmoAttachment attachment)
		{
			if (attachment != null)
			{
				using (Data.CmoEntities context = new Data.CmoEntities())
				{
					var data = context.CmoAttachments.FirstOrDefault(a => a.CandidateId == attachment.CandidateID && a.MessageId == attachment.MessageID && a.AttachmentId == attachment.ID);
					if (data != null)
					{
						context.DeleteObject(data);
						try
						{
							return CmoRepository.Repository != null && context.SaveChanges() > 0 && CmoRepository.Repository.Delete(attachment);
						}
						catch (OptimisticConcurrencyException)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Creates a new <see cref="CmoAttachment"/> instance using C-Access Messages Online message attachment data.
		/// </summary>
		/// <param name="data">The CMO message attachment to use.</param>
		/// <returns>A new CMO message attachment instance using the specified data if valid; otherwise, null.</returns>
		private static CmoAttachment CreateCmoAttachment(Data.CmoAttachment data)
		{
			return data == null ? null : new CmoAttachment(data.CandidateId, data.MessageId, data.AttachmentId)
			{
				FileName = data.AttachmentName
			};
		}
	}
}
