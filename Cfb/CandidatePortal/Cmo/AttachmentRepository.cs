using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Security;
using System.Security.AccessControl;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A file-based repository for message attachments.
	/// </summary>
	public class AttachmentRepository : ProviderBase
	{
		/// <summary>
		/// The system file path to the attachment repository.
		/// </summary>
		private readonly string _filePath;

		/// <summary>
		/// Represents the method that will handle an attachment write event.
		/// </summary>
		/// <param name="path">The full system file path to an attachment.</param>
		private delegate void WriteAttachmentEventHandler(string path);

		/// <summary>
		/// Gets the system file path to the attachment repository.
		/// </summary>
		public string FilePath
		{
			get { return _filePath; }
		}

		/// <summary>
		/// Initializes a new <see cref="AttachmentRepository"/> instance.
		/// </summary>
		/// <param name="name">A friendly name used to refer to the repository during configuration.</param>
		/// <param name="filePath">A system file path to the attachment repository.</param>
		/// <exception cref="ArgumentException"><paramref name="filePath"/> is not a valid file path or does not exist.</exception>
		public AttachmentRepository(string name, string filePath)
			: this(name, filePath, null)
		{
		}

		/// <summary>
		/// Initializes a new <see cref="AttachmentRepository"/> instance.
		/// </summary>
		/// <param name="name">A friendly name used to refer to the repository during configuration.</param>
		/// <param name="filePath">A system file path to the attachment repository.</param>
		/// <param name="description">A brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).</param>
		/// <exception cref="ArgumentException"><paramref name="filePath"/> is not a valid file path or does not exist.</exception>
		public AttachmentRepository(string name, string filePath, string description)
		{
			if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath)) throw new ArgumentException("The repository file path is not valid.", "filePath");
			_filePath = filePath.TrimEnd('\\');
			NameValueCollection config = new NameValueCollection();
			config["description"] = description;
			base.Initialize(name, config);
		}

		/// <summary>
		/// Saves a message attachment to the attachment repository.
		/// </summary>
		/// <param name="attachment">The attachment to save.</param>
		/// <param name="data">The attachment data to save.</param>
		/// <returns>true if the attachment was attached successfully; otherise, false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
		public bool Save(CmoAttachment attachment, byte[] data)
		{
			return Save(attachment, new WriteAttachmentEventHandler(delegate(string path)
			{
				/* due to Microsoft Connect bug #486256, we have to upload in chunks smaller than 64MB
				 * (http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=486256
				 */
				if (data == null)
					throw new ArgumentNullException("data", "Attachment data cannot be null.");
				using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					int offset = 0;
					int count = 33554432; // 32MB chunks
					int remaining = data.Length;
					while (remaining > 0)
					{
						count = Math.Min(count, remaining);
						fs.Write(data, offset, count);
						offset += count;
						remaining -= count;
					}
				}
			}));
		}

		/// <summary>
		/// Saves a message attachment to the attachment repository.
		/// </summary>
		/// <param name="attachment">The attachment to save.</param>
		/// <param name="contents">The attachment data to save.</param>
		/// <returns>true if the attachment was attached successfully; otherise, false.</returns>
		/// <exception cref="ArgumentException"><paramref name="attachment"/> has an invalid name.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="attachment"/> is null or has a null file name.</exception>
		/// <exception cref="DirectoryNotFoundException">The attachment file path is invalid.</exception>
		/// <exception cref="FileNotFoundException">The attachment file was not found.</exception>
		/// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="NotSupportedException">The attachment file path is in an invalid format.</exception>
		/// <exception cref="PathTooLongException">The path to the attachment exceeds the system-defined maximum length.</exception>
		/// <exception cref="SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="UnauthorizedAccessException">The attachment path specifies a read-only file or directory, or the operation is not supported on the current platform, or the caller does not have the required permission.</exception>
		public bool Save(CmoAttachment attachment, string contents)
		{
			return Save(attachment, new WriteAttachmentEventHandler(delegate(string path) { File.WriteAllText(path, contents); }));
		}

		/// <summary>
		/// Saves a message attachment to the attachment repository.
		/// </summary>
		/// <param name="attachment">The attachment to save.</param>
		/// <param name="writeAttachment">The delegate method to use for writing the attachment to disk.</param>
		/// <returns>true if the attachment was attached successfully; otherise, false.</returns>
		/// <exception cref="ArgumentException"><paramref name="attachment"/> has an invalid name.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="attachment"/> is null or has a null file name, or <paramref name="writeAttachment"/> is not defined.</exception>
		/// <exception cref="DirectoryNotFoundException">The attachment file path is invalid.</exception>
		/// <exception cref="FileNotFoundException">The attachment file was not found.</exception>
		/// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="NotSupportedException">The attachment file path is in an invalid format.</exception>
		/// <exception cref="PathTooLongException">The path to the attachment exceeds the system-defined maximum length.</exception>
		/// <exception cref="SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="UnauthorizedAccessException">The attachment path specifies a read-only file or directory, or the operation is not supported on the current platform, or the caller does not have the required permission.</exception>
		private bool Save(CmoAttachment attachment, WriteAttachmentEventHandler writeAttachment)
		{
			if (attachment == null || writeAttachment == null) return false;
			string path = GetPath(attachment);
			if (File.Exists(path)) return false;
			string folder = new FileInfo(path).DirectoryName;
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);
			writeAttachment(path);
			File.SetAttributes(path, File.GetAttributes(path) & FileAttributes.ReadOnly);
			return true;
		}

		/// <summary>
		/// Retrieves the full system file path to a file attachment.
		/// </summary>
		/// <param name="attachment">The attachment to find.</param>
		/// <returns>The full system file path to the attachment.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="attachment"/> is null.</exception>
		internal string GetPath(CmoAttachment attachment)
		{
			if (attachment == null) throw new ArgumentNullException("attachment", "The attachment cannot be null.");
			return string.Format("{0}\\{1}_{2}", GetMessageFolderName(attachment.CandidateID, attachment.MessageID), attachment.ID, attachment.FileName);
		}

		/// <summary>
		/// Retrieves the full system file path to a CMO message's file attachments folder.
		/// </summary>
		/// <param name="candidateID">The CFIS ID of the candidate recipient of the message to find.</param>
		/// <param name="messageID">The ID of the message to find.</param>
		/// <returns>The full system file path to the message's attachments folder.</returns>
		private string GetMessageFolderName(string candidateID, int messageID)
		{
			return string.Format("{0}\\{1}\\{2}", this.FilePath, candidateID, messageID);
		}

		/// <summary>
		/// Retrieves the full system file path to a CMO message's file attachments folder.
		/// </summary>
		/// <param name="message">The message to find.</param>
		/// <returns>The full system file path to the message's attachments folder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="message"/> is null.</exception>
		internal string GetMessageFolderName(CmoMessage message)
		{
			if (message == null) throw new ArgumentNullException("message", "The message cannot be null.");
			return GetMessageFolderName(message.CandidateID, message.ID);
		}

		/// <summary>
		/// Retrieves a message attachment as a stream sequence of raw bytes.
		/// </summary>
		/// <param name="attachment">The attachment to get.</param>
		/// <returns>A <see cref="Stream"/> object representing the raw attachment data.</returns>
		/// <exception cref="ArgumentException"><paramref name="attachment"/> has an invalid name.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="attachment"/> is null or has a null file name.</exception>
		/// <exception cref="DirectoryNotFoundException">The attachment file path is invalid.</exception>
		/// <exception cref="FileNotFoundException">The attachment file was not found.</exception>
		/// <exception cref="NotSupportedException">The attachment file path is in an invalid format.</exception>
		/// <exception cref="PathTooLongException">The path to the attachment exceeds the system-defined maximum length.</exception>
		/// <exception cref="UnauthorizedAccessException">The attachment path specifies a directory, or the caller does not have the required permission.</exception>
		internal Stream GetData(CmoAttachment attachment)
		{
			return File.OpenRead(GetPath(attachment));
		}

		/// <summary>
		/// Deletes a message attachment.
		/// </summary>
		/// <param name="attachment">The attachment to delete.</param>
		/// <returns>true if the attachment was removed successfully; otherwise, false.</returns>
		/// <exception cref="ArgumentException"><paramref name="attachment"/> has an invalid name.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="attachment"/> is null or has a null file name.</exception>
		/// <exception cref="DirectoryNotFoundException">The attachment file path is invalid.</exception>
		/// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="NotSupportedException">The attachment file path is in an invalid format.</exception>
		/// <exception cref="PathTooLongException">The path to the attachment exceeds the system-defined maximum length.</exception>
		/// <exception cref="UnauthorizedAccessException">The attachment path specifies a read-only file or directory, or the operation is not supported on the current platform, or the caller does not have the required permission.</exception>
		public bool Delete(CmoAttachment attachment)
		{
			if (attachment != null)
			{
				string path = this.GetPath(attachment);
				if (File.Exists(path))
				{
					try
					{
						// unmark as read-only
						FileAttributes attributes = File.GetAttributes(path);
						if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
							File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
						File.Delete(path);
						return true;
					}
					catch
					{
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Locks down the attachments folder for a message to prevent any further modifications as a records-retention/integrity measure.
		/// </summary>
		/// <param name="message">The message to lock down.</param>
		internal void LockDown(CmoMessage message)
		{
			string path = GetMessageFolderName(message);
			if (Directory.Exists(path))
			{
				DirectorySecurity ds = Directory.GetAccessControl(path);
				FileSystemRights rights = FileSystemRights.Write | FileSystemRights.ChangePermissions | FileSystemRights.Delete | FileSystemRights.TakeOwnership | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.CreateDirectories | FileSystemRights.CreateFiles;
				ds = Directory.GetAccessControl(path);
				// needs two rules - first is for "This folder"
				ds.AddAccessRule(new FileSystemAccessRule("Everyone", rights, InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Deny));
				// second rule is for "Subfolders and files"
				ds.AddAccessRule(new FileSystemAccessRule("Everyone", rights, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Deny));
				Directory.SetAccessControl(path, ds);
			}
		}
	}
}
