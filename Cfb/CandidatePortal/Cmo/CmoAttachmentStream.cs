using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A sequence of bytes representing an attachment to a CMO message.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class CmoAttachmentStream : MemoryStream
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
		/// Gets or sets the name of the attachment.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Creates a new instance of the <see cref="CmoAttachmentStream"/> class.
		/// </summary>
		/// <param name="message">The source <see cref="CmoMessage"/> that the current <see cref="CmoAttachmentStream"/> is for.</param>
		/// <param name="data">The sequence of bytes that the current <see cref="CmoAttachmentStream"/> will encapsulate.</param>
		/// <exception cref="ArgumentNullException"><paramref name="message"/> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is null.</exception>
		public CmoAttachmentStream(CmoMessage message, byte[] data)
			: base(data, 0, data.Length, false, true)
		{
			if (message == null)
				throw new ArgumentNullException("message", "Source message cannot be null.");
			//if (data == null)
			//    throw new ArgumentNullException("data", "Source data cannot be null.");
			_candidateID = message.CandidateID;
			_messageID = message.ID;
		}

		///// <summary>
		///// Returns the array of unsigned bytes from which this stream was created.
		///// </summary>
		///// <returns>The byte array from which this stream was created.</returns>
		//public byte[] GetBuffer()
		//{
		//    return _baseStream.GetBuffer();
		//}

		///// <summary>
		///// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
		///// </summary>
		//public override void Close()
		//{
		//    _baseStream.Close();
		//    base.Close();
		//}

		///// <summary>
		///// Releases all resources used by the Stream.
		///// </summary>
		///// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		//protected override void Dispose(bool disposing)
		//{
		//    _baseStream.Dispose();
		//    base.Dispose(disposing);
		//}

		///// <summary>
		///// Reads a byte from the current stream.
		///// </summary>
		///// <returns>The byte cast to a <see cref="Int32"/>, or -1 if the end of the stream has been reached.</returns>
		//public override int ReadByte()
		//{
		//    return _baseStream.ReadByte();
		//}

		///// <summary>
		///// Writes a byte to the current stream at the current position.
		///// </summary>
		///// <param name="value">The byte to write.</param>
		//public override void WriteByte(byte value)
		//{
		//    _baseStream.WriteByte(value);
		//}

		///// <summary>
		///// Gets a value indicating whether the current stream supports reading.
		///// </summary>
		//public override bool CanRead
		//{
		//    get { return _baseStream.CanRead; }
		//}

		///// <summary>
		///// Gets a value indicating whether the current stream supports seeking.
		///// </summary>
		//public override bool CanSeek
		//{
		//    get { return _baseStream.CanSeek; }
		//}

		///// <summary>
		///// Gets a value indicating whether the current stream supports writing.
		///// </summary>
		//public override bool CanWrite
		//{
		//    get { return _baseStream.CanWrite; }
		//}

		///// <summary>
		///// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		///// </summary>
		//public override void Flush()
		//{
		//    _baseStream.Flush();
		//}

		///// <summary>
		///// Gets the length in bytes of the stream.
		///// </summary>
		//public override long Length
		//{
		//    get { return _baseStream.Length; }
		//}

		///// <summary>
		///// Gets or sets the position within the current stream.
		///// </summary>
		//public override long Position
		//{
		//    get { return _baseStream.Position; }
		//    set { _baseStream.Position = value; }
		//}

		///// <summary>
		///// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		///// </summary>
		///// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> +<paramref name="count"/> - 1) replaced by the bytes read from the current source.</param>
		///// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.</param>
		///// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		///// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		//public override int Read(byte[] buffer, int offset, int count)
		//{
		//    return _baseStream.Read(buffer, offset, count);
		//}

		///// <summary>
		///// Sets the position within the current stream.
		///// </summary>
		///// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
		///// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
		///// <returns>The new position within the current stream.</returns>
		//public override long Seek(long offset, SeekOrigin origin)
		//{
		//    return _baseStream.Seek(offset, origin);
		//}

		///// <summary>
		///// Sets the length of the current stream.
		///// </summary>
		///// <param name="value">The desired length of the current stream in bytes.</param>
		//public override void SetLength(long value)
		//{
		//    _baseStream.SetLength(value);
		//}

		///// <summary>
		///// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		///// </summary>
		///// <param name="buffer">An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.</param>
		///// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.</param>
		///// <param name="count">The number of bytes to be written to the current stream.</param>
		//public override void Write(byte[] buffer, int offset, int count)
		//{
		//    _baseStream.Write(buffer, offset, count);
		//}
	}
}
