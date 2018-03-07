using System;
using System.IO;
using System.ServiceModel;
using Cfb.CandidatePortal.Cmo;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Net;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.DirectoryServices;

namespace Cfb.CandidatePortal.ServiceModel.CPCmoService
{
	/// <summary>
	/// WCF service for high-level interaction with Campaign Messages Online messages in C-Access.
	/// </summary>
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = int.MaxValue, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public sealed class CmoService : ICmoService
	{
		/// <summary>
		/// Creates a new instance of the <see cref="CmoService"/> class.
		/// </summary>
		public CmoService()
		{
			CPDataProvider provider = new CPDataProvider();
			CPProviders.DataProvider = provider;
			CmoProviders.DataProvider = provider;
			GC.KeepAlive(provider);
			GC.KeepAlive(CPProviders.SettingsProvider = new CPSettingsProvider());
		}

		#region ICmoService Members

		/// <summary>
		/// Directly posts a generic CMO message to C-Access.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the message to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the message to post.</param>
		/// <param name="creator">The network username of the user posting the message.</param>
		/// <param name="title">The message subject.</param>
		/// <param name="body">The message body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this message.</param>
		/// <param name="data">The raw binary data contents of a message attachment if desired; otherwise, null to post without an attachment.</param>
		/// <param name="name">The filename of a message attachment if desired; otherwise, null to post without an attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <remarks>To properly post a message without any attachments, set either <paramref name="data"/> or <paramref name="name"/> to null.</remarks>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>, <paramref name="data"/>, and <paramref name="name"/>.</exception>
		public int PostMessage(string electionCycle, string candidateID, string creator, string title, string body, string receiptEmail, byte[] data, string name, bool notify)
		{
			return Convert.ToInt32(CmoServiceError.MessageGeneralFailure);
		}

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
		/// <param name="data">The raw binary data contents of the payment letter attachment.</param>
		/// <param name="name">The filename of the payment letter.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		public int PostPaymentLetter(string electionCycle, string candidateID, byte run, string creator, string title, string body, string receiptEmail, byte[] data, string name, bool notify)
		{
			return PostPaymentLetter(electionCycle, candidateID, run, creator, title, body, receiptEmail, notify, delegate(CmoMessage message)
			{
				return message != null && CmoAttachment.Add(candidateID, message.ID, data, name) != null;
			});
		}

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
		/// <param name="path">The full UNC file path to the payment letter attachment.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <exception cref="FileNotFoundException"><paramref name="path"/> does not point to a file that can be accessed.</exception>
		public int PostPaymentLetter(string electionCycle, string candidateID, byte run, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			return PostPaymentLetter(electionCycle, candidateID, run, creator, title, body, receiptEmail, notify, GetAddAttachmentEventHandler(candidateID, path));
		}

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
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <param name="addAttachment">The delegate method to use for adding the attachment.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		private int PostPaymentLetter(string electionCycle, string candidateID, byte run, string creator, string title, string body, string receiptEmail, bool notify, Func<CmoMessage, bool> addAttachment)
		{
			return PostLetter(electionCycle, candidateID, creator, title, body, receiptEmail, CmoCategory.PaymentCategoryID, notify, addAttachment: addAttachment, addPayment: delegate(CmoMessage message)
			{
				if (message == null)
					return false;
				message.AuditReviewNumber = run;
				return message.Update();
			});
		}

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
		public int PostTollingLetter(string electionCycle, string candidateID, int eventNumber, string source, string eventCode, string type, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			TollingLetter letter = CPProviders.DataProvider.GetTollingLetter(source, eventCode, type);
			if (letter == null)
				return Convert.ToInt32(CmoServiceError.UnsupportedTollingLetter);
			return PostLetter(electionCycle, candidateID, creator, title, body, receiptEmail, letter.GetCmoCategoryID(), notify, addAttachment: GetAddAttachmentEventHandler(candidateID, path), addPostElection: GetAddPostElectionEventHandler(isSecondRequest, isRepost), addTolling: delegate(CmoMessage message)
			{
				if (message == null || letter == null)
					return false;
				message.TollingLetter = letter;
				message.TollingEventNumber = eventNumber;
				return message.Update();
			});
		}

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
		public int PostInitialDocumentRequest(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			return PostLetter(electionCycle, candidateID, creator, title, body, receiptEmail, CmoCategory.IdrCategoryID, notify, addAttachment: GetAddAttachmentEventHandler(candidateID, path), addPostElection: GetAddPostElectionEventHandler(isSecondRequest, isRepost));
		}

		/// <summary>
		/// Directly posts a Draft Audit Report to C-Access as a CMO message.
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
		public int PostDraftAuditReport(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			return PostDarFar(electionCycle, candidateID, false, isSecondRequest, isRepost, creator, title, body, receiptEmail, notify, GetAddAttachmentEventHandler(candidateID, path));
		}

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
		public int PostFinalAuditReport(string electionCycle, string candidateID, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			return PostDarFar(electionCycle, candidateID, true, isSecondRequest, isRepost, creator, title, body, receiptEmail, notify, GetAddAttachmentEventHandler(candidateID, path));
		}

		/// <summary>
		/// Directly posts a Draft or Final Audit Report to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the report to post.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the report to post.</param>
		/// <param name="isFar">true to post a Final Audit Report; otherwise, false to post a Draft Audit Report.</param>
		/// <param name="isSecondRequest">Whether or not the report is a second request.</param>
		/// <param name="isRepost">Whether or not the report is a repost.</param>
		/// <param name="creator">The network username of the user posting the report.</param>
		/// <param name="title">The report subject.</param>
		/// <param name="body">The report body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <param name="addAttachment">The delegate method to use for adding the attachment.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		/// <exception cref="ArgumentNullException">Any parameters are null, except <paramref name="receiptEmail"/>.</exception>
		/// <remarks>If <paramref name="receiptEmail"/> is null, whitespace, or empty, the default e-mail address for the user specified by <paramref name="creator"/> will be retrieved from the network and used instead.</remarks>
		private int PostDarFar(string electionCycle, string candidateID, bool isFar, bool isSecondRequest, bool isRepost, string creator, string title, string body, string receiptEmail, bool notify, Func<CmoMessage, bool> addAttachment)
		{
			return PostLetter(electionCycle, candidateID, creator, title, body, receiptEmail, isFar ? CmoCategory.FarCategoryID : CmoCategory.DarCategoryID, notify, addAttachment: addAttachment, addPostElection: GetAddPostElectionEventHandler(isSecondRequest, isRepost));
		}

		/// <summary>
		/// Directly posts a statement and Doing Business reviews to C-Access as a CMO message.
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
		public int PostStatementReview(string electionCycle, string candidateID, byte statementNumber, bool doingBusiness, string creator, string title, string body, string receiptEmail, string path, bool notify)
		{
			return PostLetter(electionCycle, candidateID, creator, title, body, receiptEmail, doingBusiness ? CmoCategory.DoingBusinessReviewCategoryID : CmoCategory.StatementReviewCategoryID, notify, addAttachment: GetAddAttachmentEventHandler(candidateID, path), addStatementReview: delegate(CmoMessage message)
			{
				if (message == null)
					return false;
				message.AuditReviewNumber = statementNumber;
				return message.Update();
			});
		}

		#endregion

		/// <summary>
		/// Directly posts a letter to C-Access as a CMO message.
		/// </summary>
		/// <param name="electionCycle">The election cycle context of the letter.</param>
		/// <param name="candidateID">The ID of the candidate receipient of the letter.</param>
		/// <param name="creator">The network username of the user posting the letter.</param>
		/// <param name="title">The letter subject.</param>
		/// <param name="body">The letter body.</param>
		/// <param name="receiptEmail">The e-mail address for the recipient of open receipt e-mails if desired; otherwise, null to decline open receipts for this letter.</param>
		/// <param name="categoryID">The letter category ID.</param>
		/// <param name="notify">Indicates whether to generate an e-mail notification to the campaign's C-Access accounts upon post.</param>
		/// <param name="addAttachment">The delegate method to use for adding the attachment.</param>
		/// <param name="addPayment">The delegate method to use for adding a payment run to the message.</param>
		/// <param name="addTolling">The delegate method to use for adding tolling details to the message.</param>
		/// <param name="addStatementReview">The delegate method to use for adding a statement review to the message.</param>
		/// <param name="addPostElection">The delegate method to use for adding a DAR/FAR to the message.</param>
		/// <returns>The ID of the message if succesfully posted; otherwise, a negative value representing an error code.</returns>
		private int PostLetter(string electionCycle, string candidateID, string creator, string title, string body, string receiptEmail, byte categoryID, bool notify,
			Func<CmoMessage, bool> addAttachment = null,
			Func<CmoMessage, bool> addPayment = null,
			Func<CmoMessage, bool> addTolling = null,
			Func<CmoMessage, bool> addStatementReview = null,
			Func<CmoMessage, bool> addPostElection = null)
		{
			CmoMessage message = CmoMessage.Add(candidateID, electionCycle, title, body, creator, GetEmail(creator, receiptEmail), categoryID);
			if (message == null)
				return Convert.ToInt32(CmoServiceError.MessageAddFailure);
			bool success = false;
			try
			{
				// set category-specific properties
				if (addPayment != null)
				{
					if (!addPayment(message))
						return Convert.ToInt32(CmoServiceError.PaymentRunFailure);
				}
				if (addTolling != null)
				{
					if (!addTolling(message))
						return Convert.ToInt32(CmoServiceError.TollingFailure);
				}
				if (addStatementReview != null)
				{
					if (!addStatementReview(message))
						return Convert.ToInt32(CmoServiceError.StatementNumberFailure);
				}
				if (addPostElection != null)
				{
					if (!addPostElection(message))
						return Convert.ToInt32(CmoServiceError.PostElectionRequestTypeFailure);
				}
				if (addAttachment == null || !addAttachment(message))
				{
					return Convert.ToInt32(CmoServiceError.AttachmentFailure);
				}

				// post message
				if (message.Reload())
				{
					return (success = message.Post() != null) ? message.ID : Convert.ToInt32(CmoServiceError.MessagePostFailure);
				}
				return Convert.ToInt32(CmoServiceError.MessageGeneralFailure);
			}
			finally
			{
				if (!success)
				{
					RollBack(message);
				}
			}
		}

		/// <summary>
		/// Retrieves the best e-mail address available given a preferred e-mail address and a username.
		/// </summary>
		/// <param name="username">The network logon username of a domain user.</param>
		/// <param name="preferred">The preferred e-mail address.</param>
		/// <returns><paramref name="preferred"/> if defined; otherwise, the e-mail address for <paramref name="username"/>.</returns>
		private static string GetEmail(string username, string preferred)
		{
			return string.IsNullOrWhiteSpace(preferred) ? CfbDirectorySearcher.GetUser(username).Email : preferred;
		}

		/// <summary>
		/// Obtains a <see cref="AddAttachmentEventHandler"/> to handle events for adding attachments to CMO messages.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose attachment is being added.</param>
		/// <param name="path">The full UNC file path to the attachment.</param>
		/// <returns>A <see cref="AddAttachmentEventHandler"/> for adding the attachment located at <paramref name="path"/>.</returns>
		/// <exception cref="FileNotFoundException"><paramref name="path"/> is invalid or does not exist.</exception>
		private static Func<CmoMessage, bool> GetAddAttachmentEventHandler(string candidateID, string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException(string.Format("Unable to access the specified file from UNC path \"{0}\". Please make sure the path is valid, the file exists, and the service has sufficient permissions to read it.", path), path);
			string name = Path.GetFileName(path);
			return delegate(CmoMessage message)
			{
				return message != null && CmoAttachment.Add(candidateID, message.ID, File.ReadAllBytes(path), name) != null;
			};
		}

		/// <summary>
		/// Obtains a <see cref="AddPostElectionEventHandler"/> to handle events for adding post-election report properties to a CMO message.
		/// </summary>
		/// <param name="isSecondRequest">Whether or not the letter is a second request.</param>
		/// <param name="isRepost">Whether or not the letter is a repost.</param>
		/// <returns>A <see cref="AddPostElectionEventHandler"/> for adding the post-election report properties specified.</returns>
		private static Func<CmoMessage, bool> GetAddPostElectionEventHandler(bool isSecondRequest, bool isRepost)
		{
			return delegate(CmoMessage message)
			{
				if (message == null)
					return false;
				if (message.IsTollingLetter && !message.IsPostElectionAudit)
					return true; // skip setting post-election report properties for non-post-election report tolling letters
				if (!isSecondRequest)
					message.PostElectionAuditRequestType = isRepost ? AuditRequestType.FirstRepost : AuditRequestType.FirstRequest;
				else
					message.PostElectionAuditRequestType = isRepost ? AuditRequestType.SecondRepost : AuditRequestType.SecondRequest;
				return message.Update();
			};
		}

		/// <summary>
		/// Rolls back a CMO message.
		/// </summary>
		/// <param name="message">The <see cref="CmoMessage"/> to roll back.</param>
		private static void RollBack(CmoMessage message)
		{
			if (message != null)
			{
				for (int i = 0; i < Properties.Settings.Default.MaxCleanupRetryCount; i++)
				{
					message.Reload();
					if (message.Delete())
						return;
				}
			}
		}
	}
}
