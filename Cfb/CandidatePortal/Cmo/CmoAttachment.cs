using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A file attachment attached to a Campaign Messages Online message.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CmoAttachment
	{
		/// <summary>
		/// The CFIS ID of the candidate recipient of the associated CMO message.
		/// </summary>
		[DataMember(Name = "CandidateID")]
		private readonly string _candidateID;

		/// <summary>
		/// Gets the CFIS ID of the candidate recipient of the associated CMO message.
		/// </summary>
		public string CandidateID
		{
			get { return _candidateID; }
		}

		/// <summary>
		/// The ID of the associated CMO message.
		/// </summary>
		[DataMember(Name = "MessageID")]
		private readonly int _messageID;

		/// <summary>
		/// Gets the ID of the associated CMO message.
		/// </summary>
		public int MessageID
		{
			get { return _messageID; }
		}

		/// <summary>
		/// The attachment identifier.
		/// </summary>
		[DataMember(Name = "ID")]
		private readonly byte _id;

		/// <summary>
		/// Gets the attachment identifier.
		/// </summary>
		public byte ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets or sets the attachment file name.
		/// </summary>
		[DataMember]
		public string FileName { get; set; }

		/// <summary>
		/// Gets the full system file path to the attachment.
		/// </summary>
		public string Path
		{
			get { return Cmo.Repository == null ? CmoProviders.DataProvider.GetCmoAttachmentPath(_candidateID, _messageID, _id) : Cmo.Repository.GetPath(this); }
		}

		/// <summary>
		/// Gets a unique download name for the attachment.
		/// </summary>
		public string DownloadName
		{
			get { return string.Format("{0}-{1}-{2}", _candidateID, _messageID, _id); }
		}

		/// <summary>
		/// Gets the filename extension for the attachment.
		/// </summary>
		public string Extension
		{
			get
			{
				string filename = this.FileName;
				return filename.Contains('.') ? filename.Substring(filename.LastIndexOf('.') + 1).ToLower() : string.Empty;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CmoAttachment"/> class.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate recipient of the message that owns the attachment.</param>
		/// <param name="messageID">The ID of the message that owns the attachment.</param>
		/// <param name="id">The attachment's candidate/message-scope identifier.</param>
		internal CmoAttachment(string candidateID, int messageID, byte id)
		{
			_candidateID = candidateID;
			_messageID = messageID;
			_id = id;
		}

		/// <summary>
		/// Retrieves the attachment data as a stream sequence of raw bytes.
		/// </summary>
		/// <returns>A <see cref="Stream"/> object representing the raw attachment data.</returns>
		/// <exception cref="NullReferenceException">No default repository has been defined.</exception>
		public Stream GetData()
		{
			return Cmo.Repository == null ? null : Cmo.Repository.GetData(this);
		}

		/// <summary>
		/// Gets an array containing the characters that are not allowed in attachment download names.
		/// </summary>
		/// <returns>An array containing the characters that are not allowed in attachment download names.</returns>
		private char[] GetInvalidDownloadNameChars()
		{
			return System.IO.Path.GetInvalidFileNameChars().Union(new char[] { ' ', '&', '#' }).ToArray();
		}

		/// <summary>
		/// Adds a CMO message attachment.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate recipient of the message.</param>
		/// <param name="messageID">The message ID.</param>
		/// <param name="data">The raw binary attachment data.</param>
		/// <param name="name">The attachment file name to be use for display.</param>
		/// <returns>A <see cref="CmoAttachment"/> that represents the newly added attachment.</returns>
		public static CmoAttachment Add(string candidateID, int messageID, byte[] data, string name)
		{
			return CmoProviders.DataProvider.AddCmoAttachment(candidateID, messageID, data, name);
		}

		/// <summary>
		/// Retrieves an attachment.
		/// </summary>
		/// <param name="uniqueID">The unique ID of the attachment to retrieve.</param>
		/// <returns>The requested <see cref="CmoAttachment"/> if found; otherwise, null.</returns>
		public static CmoAttachment GetAttachment(string uniqueID)
		{
			return CmoProviders.DataProvider.GetCmoAttachment(uniqueID);
		}
	}
}
