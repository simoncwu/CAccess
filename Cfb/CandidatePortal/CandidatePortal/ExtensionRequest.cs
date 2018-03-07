using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// A request for an extension to an audit review response deadline.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ExtensionRequest
	{
		/// <summary>
		/// The ID of the candidate requesting the extension.
		/// </summary>
		[DataMember(Name = "CandidateID")]
		private string _candidateID;

		/// <summary>
		/// Gets the ID of the candidate requesting the extension.
		/// </summary>
		public string CandidateID
		{
			get { return _candidateID; }
		}

		/// <summary>
		/// The election cycle in which the extension is being requested.
		/// </summary>
		[DataMember(Name = "ElectionCycle")]
		private readonly string _electionCycle;

		/// <summary>
		/// Gets the election cycle in which the extension is being requested.
		/// </summary>
		public string ElectionCycle
		{
			get { return _electionCycle; }
		}

		/// <summary>
		/// The extension type.
		/// </summary>
		[DataMember(Name = "ExtensionType")]
		private readonly ExtensionType _extensionType;

		/// <summary>
		/// Gets the extension type.
		/// </summary>
		public ExtensionType ExtensionType
		{
			get { return _extensionType; }
		}

		/// <summary>
		/// The number of the audit review or visit for which the response extension is being requested.
		/// </summary>
		[DataMember(Name = "ReviewNumber")]
		private readonly byte _reviewNumber;

		/// <summary>
		/// Gets the number of the audit review or visit for which the response extension is being requested.
		/// </summary>
		public byte ReviewNumber
		{
			get { return _reviewNumber; }
		}

		/// <summary>
		/// The iteration of the extension request.
		/// </summary>
		[DataMember(Name = "Iteration")]
		private readonly byte _iteration;

		/// <summary>
		/// Gets the iteration of the extension request.
		/// </summary>
		public byte Iteration
		{
			get { return _iteration; }
		}

		/// <summary>
		/// Gets or sets the date of the extension request.
		/// </summary>
		[DataMember]
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the requested response due date.
		/// </summary>
		[DataMember]
		public DateTime RequestedDueDate { get; set; }

		/// <summary>
		/// Gets or sets the candidate-specified reason for an extension.
		/// </summary>
		[DataMember]
		public string Reason { get; set; }

		/// <summary>
		/// Gets the version of the message.
		/// </summary>
		[DataMember]
		public byte[] Version { get; private set; }

		/// <summary>
		/// Gets the default maximum extension length in days.
		/// </summary>
		public static byte DefaultMaxExtensionLength
		{
			get { return Properties.Settings.Default.MaxExtensionLength; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtensionRequest"/> class.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review or visit for which the extension is being requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		internal ExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration)
			: this(candidateID, electionCycle, type, reviewNumber, iteration, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtensionRequest"/> class.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="type">The type of extension requested.</param>
		/// <param name="reviewNumber">The number of the audit review or visit for which the extension is being requested.</param>
		/// <param name="iteration">The iteration of the extension request.</param>
		/// <param name="version">The extension request version.</param>
		internal ExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration, byte[] version)
		{
			_candidateID = candidateID;
			_electionCycle = electionCycle;
			_extensionType = type;
			_reviewNumber = reviewNumber;
			_iteration = iteration;
			this.Version = version;
		}

		/// <summary>
		/// Reloads the extension request from the persistence storage medium.
		/// </summary>
		/// <returns>The current version of the extension request if found; otherwise, null.</returns>
		public ExtensionRequest Reload()
		{
			return CPProviders.DataProvider.GetExtensionRequest(_candidateID, _electionCycle, _extensionType, _reviewNumber, _iteration);
		}

		/// <summary>
		/// Gets the maximum allowable extension length in days for a specific statement number.
		/// </summary>
		/// <param name="number">The number of the statement for which an extension is being requested.</param>
		/// <returns>The maximum extension length for the specified statement in business days.</returns>
		public static byte GetMaxExtensionLength(byte number)
		{
			byte cutoff = Properties.Settings.Default.OptInStatementNumber;
			return number < cutoff ? Properties.Settings.Default.MaxExtensionLength : number == cutoff ? Properties.Settings.Default.MaxExtensionLengthOptIn : Properties.Settings.Default.MaxExtensionLengthPostOptIn;
		}

		/// <summary>
		/// Creates a new extension request and adds it to the persistence storage medium.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate requesting the extension.</param>
		/// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
		/// <param name="extensionType">The type of extension requested.</param>
		/// <param name="number">The number of the audit review for which the extension is being requested.</param>
		/// <param name="dateTime">The date of the extension request.</param>
		/// <param name="requestedDate">The requested extension due date.</param>
		/// <param name="reason">The reason for the extension.</param>
		/// <returns>A new <see cref="ExtensionRequest"/> object if the extension request was added successfully; otherwise, null.</returns>
		public static ExtensionRequest Add(string candidateID, string electionCycle, ExtensionType extensionType, byte number, DateTime dateTime, DateTime requestedDate, string reason)
		{
			return CPProviders.DataProvider.AddExtensionRequest(candidateID, electionCycle, extensionType, number, dateTime, requestedDate, reason);
		}
	}
}
