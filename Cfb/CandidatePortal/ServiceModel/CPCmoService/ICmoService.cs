using System;
using System.Data.SqlClient;
using System.IO;
using System.ServiceModel;

namespace Cfb.CandidatePortal.ServiceModel.CPCmoService
{
	/// <summary>
	/// Provides methods for posting Campaign Messages Online messages to the C-Access candidate portal.
	/// </summary>
	[ServiceContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public interface ICmoService
	{
		/// <summary>
		/// Directly posts a generic CMO message to C-Access.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the message to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the message to post.</param>
		/// <param name="creator">The network username of the user posting the message.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this message.</param>
		/// <param name="attachmentData">The raw binary data contents of a message attachment if desired; otherwise, null to post without an attachment.</param>
		/// <param name="attachmentName">The filename of a message attachment if desired; otherwise, null to post without an attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <remarks>To properly post a letter without any attachments, set either <paramref name="attachmentName"/> or <paramref name="attachmentData"/> to null.</remarks>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>, <paramref name="attachmentName"/>, and <paramref name="attachmentData"/>.</exception>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(SqlException))]
		int PostMessage(string electionCycle, string candidateID, string creator, string title, string body, string receiptEmail, byte[] attachmentData, string attachmentName, bool notify);

		/// <summary>
		/// Directly posts a payment letter to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the letter to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the letter to post.</param>
		/// <param name="run">The payment run number.</param>
		/// <param name="creator">The network username of the user posting the letter.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this letter.</param>
		/// <param name="attachmentData">The raw binary data contents of the payment letter attachment.</param>
		/// <param name="attachmentName">The filename of the payment letter.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(SqlException))]
		int PostPaymentLetter(string electionCycle, string candidateID, byte run, string creator, string title, string body, string receiptEmail, byte[] attachmentData, string attachmentName, bool notify);

		/// <summary>
		/// Directly posts a Campaign Messages Online payment letter to C-Access.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the letter to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the letter to post.</param>
		/// <param name="run">The payment run number.</param>
		/// <param name="creator">The network username of the user posting the letter.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this letter.</param>
		/// <param name="path">The full UNC file path to the payment letter attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		[OperationContract(Name = "PostPaymentLetterByPath")]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostPaymentLetter(string electionCycle, string candidateID, byte run, string creator, string title, string body, string receiptEmail, string path, bool notify);

		/// <summary>
		/// Directly posts a tolling letter to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the letter to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the letter to post.</param>
		/// <param name="eventNumber">The tolling event number.</param>
		/// <param name="source">The tolling event source code.</param>
		/// <param name="eventCode">The tolling event code.</param>
		/// <param name="type">The type of letter to post.</param>
		/// <param name="isSecondRequest">Whether or not the letter is a second request.</param>
		/// <param name="isRepost">Whether or not the letter is a repost.</param>
		/// <param name="creator">The network username of the user posting the letter.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails.</param>
		/// <param name="path">The full UNC file path to the tolling letter attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <remarks>If <paramref name="receiptEmail"/> is null, whitespace, or empty, the default e-mail address for the user specified by <paramref name="creator"/> will be retrieved from the network and used instead.</remarks>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostTollingLetter(string electionCycle, string candidateID, int eventNumber, string source, string eventCode, string type, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify);

		/// <summary>
		/// Directly posts an Initial Document Request to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the post.</param>
		/// <param name="isSecondRequest">Whether or not the IDR is a second request.</param>
		/// <param name="isRepost">Whether or not the IDR is a repost.</param>
		/// <param name="creator">The network username of the user posting the IDR.</param>
		/// <param name="title">The message subject.</param>
		/// <param name="body">The message body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails.</param>
		/// <param name="path">The full UNC file path to the message attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <remarks>If <paramref name="receiptEmail"/> is null, whitespace, or empty, the default e-mail address for the user specified by <paramref name="creator"/> will be retrieved from the network and used instead.</remarks>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostInitialDocumentRequest(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify);

		/// <summary>
		/// Directly posts a Campaign Messages Online Draft Audit Report to C-Access.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the report to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the report to post.</param>
		/// <param name="isSecondRequest">Whether or not the report is a second request.</param>
		/// <param name="isRepost">Whether or not the report is a repost.</param>
		/// <param name="creator">The network username of the user posting the report.</param>
		/// <param name="title">The report subject.</param>
		/// <param name="body">The report body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails.</param>
		/// <param name="path">The full UNC file path to the report attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <remarks>If <paramref name="receiptEmail"/> is null, whitespace, or empty, the default e-mail address for the user specified by <paramref name="creator"/> will be retrieved from the network and used instead.</remarks>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostDraftAuditReport(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify);

		/// <summary>
		/// Directly posts a Final Audit Report to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the report to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the report to post.</param>
		/// <param name="isSecondRequest">Whether or not the report is a second request.</param>
		/// <param name="isRepost">Whether or not the report is a repost.</param>
		/// <param name="creator">The network username of the user posting the report.</param>
		/// <param name="title">The report subject.</param>
		/// <param name="body">The report body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails.</param>
		/// <param name="path">The full UNC file path to the report attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <remarks>If <paramref name="receiptEmail"/> is null, whitespace, or empty, the default e-mail address for the user specified by <paramref name="creator"/> will be retrieved from the network and used instead.</remarks>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostFinalAuditReport(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify);

		/// <summary>
		/// Directly posts a statement or Doing Business review to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the letter to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the letter to post.</param>
		/// <param name="statementNumber">The number of the statement reviewed.</param>
		/// <param name="doingBusiness">true to post a Doing Business Review; otherwise, false to post a Statement Review.</param>
		/// <param name="creator">The network username of the user posting the letter.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this letter.</param>
		/// <param name="path">The full UNC file path to the review attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		[OperationContract]
		[FaultContract(typeof(Exception))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(FileNotFoundException))]
		[FaultContract(typeof(SqlException))]
		int PostStatementReview(string electionCycle, string candidateID, byte statementNumber, bool doingBusiness, string creator, string title, string body, string receiptEmail, string path, bool notify);
	}
}
