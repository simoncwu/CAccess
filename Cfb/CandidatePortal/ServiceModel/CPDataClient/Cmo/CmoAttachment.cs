using System;
using System.IO;
using System.Linq;
using Cfb.CandidatePortal.Cmo;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.ServiceModel.CPDataClient
{
	/// <summary>
	/// Provides access to CMO attachment data via WCF client-server communication.
	/// </summary>
	partial class CPDataProvider
	{
		/// <summary>
		/// Deletes this instance from the persistence storage medium.
		/// </summary>
		/// <returns>true if this instance was deleted successfully; otherwise, false.</returns>
		public bool Delete(CmoAttachment attachment)
		{
			if (attachment == null)
				return false;
			using (DataClient client = new DataClient())
			{
				return client.DeleteCmoAttachment(attachment);
			}
		}

		/// <summary>
		/// Adds a CMO message attachment.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate recipient of the message.</param>
		/// <param name="messageID">The message ID.</param>
		/// <param name="data">The raw binary attachment data.</param>
		/// <param name="name">The attachment file name to be use for display.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		public CmoAttachment AddCmoAttachment(string candidateID, int messageID, byte[] data, string name)
		{
			using (DataClient client = new DataClient()) { return client.AddCmoAttachment(candidateID, messageID, data, name); }
		}

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="uniqueID">The unique ID of the attachment to retrieve.</param>
		/// <returns>The requested <see cref="CmoAttachment"/> if found; otherwise, null.</returns>
		public CmoAttachment GetCmoAttachment(string uniqueID)
		{
			using (DataClient client = new DataClient()) { return client.GetCmoAttachment(uniqueID); }
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
			using (DataClient client = new DataClient()) { return client.GetCmoAttachmentPath(candidateID, messageID, id); }
		}
	}
}
