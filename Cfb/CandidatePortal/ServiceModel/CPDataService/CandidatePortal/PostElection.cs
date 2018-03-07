using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using Cfb.CandidatePortal.PostElection;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.PostElectionTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
	public partial interface IDataService
	{
		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle, bool published);

		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		IdrInadequateEvent GetIdrInadequateEvent(string candidateID, string electionCycle, bool published);

		/// <summary>
		/// Gets whether or not a Post Election Initial Documentation Request response analysis worksheet exists for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>true if an IDR response analysis exists for the candidate and election cycle specified; otherwise, false.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		bool HasIdrResponseAnalysis(string candidateID, string electionCycle);

		/// <summary>
		/// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle, bool published);

		/// <summary>
		/// Retrieves the Post Election Draft Audit Report Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		DarInadequateEvent GetDarInadequateEvent(string candidateID, string electionCycle, bool published);

		/// <summary>
		/// Retrieves the Post Election Audit Final Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Final Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		FinalAuditReport GetFinalAuditReport(string candidateID, string electionCycle);

		/// <summary>
		/// Counts the number of post election audit tolling days incurred by a candidate for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <param name="isFar">true to count Final Audit Report tolling days; otherwise, false to count Draft Audit Report tolling days.</param>
		/// <returns>The number of tolling days incurred that match the criteria specified.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		int CountTollingDaysIncurred(string candidateID, string electionCycle, bool isFar);

		/// <summary>
		/// Retrieves a collection of post election events that affect tolling for the Draft Audit Report or Final Audit Report.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="far">true to retrieve events for the Final Audit Report; otherwise, false to retreive events for the Draft Audit Report.</param>
		/// <returns>A collection of tolling events that affect Draft Audit Report tolling.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		List<TollingEvent> GetTollingEvents(string candidateID, string electionCycle, bool far);

		/// <summary>
		/// Retrieves a collection of Post Election Initial Documentation Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Initial Documentation Inadequate Response events matching the specified criteria.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		List<IdrInadequateEvent> GetIdrInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType);

		/// <summary>
		/// Retrieves a collection of Post Election Draft Audit Response Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Draft Audit Response Inadequate Response events matching the specified criteria.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		List<DarInadequateEvent> GetDarInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType);

		/// <summary>
		/// Retrieves Post Election Audit suspension information for a specfic candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose suspension information is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>Suspension information matching the specified criteria if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		Suspension GetSuspension(string candidateID, string electionCycle);

		/// <summary>
		/// Retrieves the date when post-election tolling events begin for a specific election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The date when post-election tolling events begin for the specified election cycle if found; otherwise, null.</returns>
		[OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
		[FaultContract(typeof(OfflineDataException))]
		DateTime? GetTollingStartDate(string electionCycle);
	}

	public partial class DataService : IDataService
	{
		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve only published data.</param>
		/// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
		public InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle, bool published)
		{
			InitialDocumentationRequest idr = null;
			using (PostElectionTds ds = new PostElectionTds())
			{
				if (published)
				{
					using (InitialDocumentationRequestTableAdapter ta = new InitialDocumentationRequestTableAdapter())
					{
						ta.Fill(ds.InitialDocumentationRequest, candidateID, electionCycle);
					}
					foreach (PostElectionTds.InitialDocumentationRequestRow row in ds.InitialDocumentationRequest)
					{
						idr = new InitialDocumentationRequest(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate
						};
						idr.ResponseDeadline = new IdrResponseDeadline(row.DueDate, idr)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
				else
				{
					using (InitialDocumentationRequestDirectTableAdapter ta = new InitialDocumentationRequestDirectTableAdapter())
					{
						ta.Fill(ds.InitialDocumentationRequestDirect, candidateID, electionCycle);
					}
					foreach (PostElectionTds.InitialDocumentationRequestDirectRow row in ds.InitialDocumentationRequestDirect)
					{
						idr = new InitialDocumentationRequest(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate
						};
						idr.ResponseDeadline = new IdrResponseDeadline(row.DueDate, idr)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
			}
			return idr;
		}

		/// <summary>
		/// Retrieves the Post Election Initial Documentation Request Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response event is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Initial Documentation Request Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public IdrInadequateEvent GetIdrInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			IdrInadequateEvent inadequate = null;
			using (PostElectionTds ds = new PostElectionTds())
			{
				if (published)
				{
					DateTime? minStartDate = GetTollingStartDate(electionCycle);
					using (InadequateEventsTableAdapter ta = new InadequateEventsTableAdapter())
					{
						ta.Fill(ds.InadequateEvents, candidateID, electionCycle, false, false);
					}
					foreach (PostElectionTds.InadequateEventsRow row in ds.InadequateEvents)
					{
						TollingLetter letter = GetTollingLetter(row.SourceCode.Trim(), row.EventCode.Trim(), row.TypeCode.Trim()) ?? new TollingLetter(byte.MinValue)
						{
							SourceCode = new TollingCode(TollingCodeType.SourceCode, byte.MinValue) { Code = row.SourceCode.Trim() },
							EventCode = new TollingCode(TollingCodeType.EventCode, byte.MinValue) { Code = row.EventCode.Trim() },
							TypeCode = new TollingCode(TollingCodeType.TypeCode, byte.MinValue) { Code = row.TypeCode.Trim() },
							Description = row.Description,
							Title = row.Description
						};
						// first fetch end reason to determine end date inclusion in tolling days
						TollingEndReason reason = CPConvert.ToTollingEndReason(row.EndReasonCode.Trim());
						inadequate = new IdrInadequateEvent(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate,
							// tolling event properties
							Description = letter.Description,
							Title = letter.Title,
							TollingEventNumber = row.EventNumber,
							TollingLetter = letter,
							TollingStartDate = minStartDate.HasValue && row.StartDate < minStartDate ? minStartDate.Value : row.StartDate,
							TollingEndDate = row.IsEndDateNull() ? null : (DateTime?)row.EndDate,
							TollingEndReason = reason
						};
						inadequate.ResponseDeadline = new IdrInadequateDeadline(row.DueDate, inadequate)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
				else
				{
					using (InadequateEventsDirectTableAdapter ta = new InadequateEventsDirectTableAdapter())
					{
						ta.Fill(ds.InadequateEventsDirect, candidateID, electionCycle, false, false);
					}
					foreach (PostElectionTds.InadequateEventsDirectRow row in ds.InadequateEventsDirect)
					{
						inadequate = new IdrInadequateEvent(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate
						};
						inadequate.ResponseDeadline = new IdrInadequateDeadline(row.DueDate, inadequate)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
			}
			return inadequate;
		}

		/// <summary>
		/// Gets whether or not a Post Election Initial Documentation Request response analysis worksheet exists for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>true if an IDR response analysis exists for the candidate and election cycle specified; otherwise, false.</returns>
		public bool HasIdrResponseAnalysis(string candidateID, string electionCycle)
		{
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (ResponseAnalysisDirectTableAdapter ta = new ResponseAnalysisDirectTableAdapter())
				{
					ta.Fill(ds.ResponseAnalysisDirect, candidateID, electionCycle);
				}
				return ds.ResponseAnalysisDirect.Count > 0;
			}
		}

		/// <summary>
		/// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle, bool published)
		{
			DraftAuditReport dar = null;
			using (PostElectionTds ds = new PostElectionTds())
			{
				if (published)
				{
					using (DraftAuditReportTableAdapter ta = new DraftAuditReportTableAdapter())
					{
						ta.Fill(ds.DraftAuditReport, candidateID, electionCycle);
					}
					foreach (PostElectionTds.DraftAuditReportRow row in ds.DraftAuditReport)
					{
						if (row.IsSentDateNull())
							continue;
						dar = new DraftAuditReport(row.SentDate);
						if (!row.IsReceiptDateNull())
							dar.ReceiptDate = row.ReceiptDate;
						if (!row.IsResponseSubmittedDateNull())
							dar.ResponseSubmittedDate = row.ResponseSubmittedDate;
						if (!row.IsSecondReceiptDateNull())
							dar.SecondReceiptDate = row.SecondReceiptDate;
						if (!row.IsSecondSentDateNull())
							dar.SecondSentDate = row.SecondSentDate;
						dar.ResponseDeadline = new DarResponseDeadline(row.DueDate, dar)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
				else
				{
					using (DraftAuditReportDirectTableAdapter ta = new DraftAuditReportDirectTableAdapter())
					{
						ta.Fill(ds.DraftAuditReportDirect, candidateID, electionCycle);
					}
					foreach (PostElectionTds.DraftAuditReportDirectRow row in ds.DraftAuditReportDirect)
					{
						if (row.IsSentDateNull())
							continue;
						dar = new DraftAuditReport(row.SentDate);
						if (!row.IsReceiptDateNull())
							dar.ReceiptDate = row.ReceiptDate;
						if (!row.IsResponseSubmittedDateNull())
							dar.ResponseSubmittedDate = row.ResponseSubmittedDate;
						if (!row.IsSecondReceiptDateNull())
							dar.SecondReceiptDate = row.SecondReceiptDate;
						if (!row.IsSecondSentDateNull())
							dar.SecondSentDate = row.SecondSentDate;
						dar.ResponseDeadline = new DarResponseDeadline(row.DueDate, dar)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
			}
			return dar;
		}

		/// <summary>
		/// Retrieves the Post Election Draft Audit Report Inadequate Response event for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response event is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="published">Whether or not to retrieve published data.</param>
		/// <returns>The Post Election Draft Audit Report Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
		public DarInadequateEvent GetDarInadequateEvent(string candidateID, string electionCycle, bool published)
		{
			DarInadequateEvent inadequate = null;
			using (PostElectionTds ds = new PostElectionTds())
			{
				if (published)
				{
					DateTime? minStartDate = GetTollingStartDate(electionCycle);
					using (InadequateEventsTableAdapter ta = new InadequateEventsTableAdapter())
					{
						ta.Fill(ds.InadequateEvents, candidateID, electionCycle, true, false);
					}
					foreach (PostElectionTds.InadequateEventsRow row in ds.InadequateEvents)
					{
						TollingLetter letter = GetTollingLetter(row.SourceCode.Trim(), row.EventCode.Trim(), row.TypeCode.Trim()) ?? new TollingLetter(byte.MinValue)
						{
							SourceCode = new TollingCode(TollingCodeType.SourceCode, byte.MinValue) { Code = row.SourceCode.Trim() },
							EventCode = new TollingCode(TollingCodeType.EventCode, byte.MinValue) { Code = row.EventCode.Trim() },
							TypeCode = new TollingCode(TollingCodeType.TypeCode, byte.MinValue) { Code = row.TypeCode.Trim() },
							Description = row.Description,
							Title = row.Description
						};
						// first fetch end reason to determine end date inclusion in tolling days
						TollingEndReason reason = CPConvert.ToTollingEndReason(row.EndReasonCode.Trim());
						inadequate = new DarInadequateEvent(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate,
							// tolling event properties
							Description = letter.Description,
							Title = letter.Title,
							TollingEventNumber = row.EventNumber,
							TollingLetter = letter,
							TollingStartDate = minStartDate.HasValue && row.StartDate < minStartDate ? minStartDate.Value : row.StartDate,
							TollingEndDate = row.IsEndDateNull() ? null : (DateTime?)row.EndDate,
							TollingEndReason = reason
						};
						inadequate.ResponseDeadline = new DarInadequateDeadline(row.DueDate, inadequate)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
				else
				{
					using (InadequateEventsDirectTableAdapter ta = new InadequateEventsDirectTableAdapter())
					{
						ta.Fill(ds.InadequateEventsDirect, candidateID, electionCycle, true, false);
					}
					foreach (PostElectionTds.InadequateEventsDirectRow row in ds.InadequateEventsDirect)
					{
						inadequate = new DarInadequateEvent(row.SentDate)
						{
							ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
							SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
							SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
							ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate,
						};
						inadequate.ResponseDeadline = new DarInadequateDeadline(row.DueDate, inadequate)
						{
							ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
							SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
						};
						break;
					}
				}
			}
			return inadequate;
		}

		/// <summary>
		/// Retrieves the Post Election Audit Final Audit Report for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>The Post Election Final Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
		public FinalAuditReport GetFinalAuditReport(string candidateID, string electionCycle)
		{
			FinalAuditReport far = null;
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (FinalAuditReportTableAdapter ta = new FinalAuditReportTableAdapter())
				{
					ta.Fill(ds.FinalAuditReport, candidateID, electionCycle);
				}
				foreach (PostElectionTds.FinalAuditReportRow row in ds.FinalAuditReport)
				{
					if (row.IsSentDateNull())
						continue;
					far = new FinalAuditReport(row.SentDate);
					break;
				}
			}
			return far;
		}

		/// <summary>
		/// Counts the number of post election audit tolling days incurred by a candidate for a specific election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the desired candidate.</param>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <param name="isFar">true to count Final Audit Report tolling days; otherwise, false to count Draft Audit Report tolling days.</param>
		/// <returns>The number of tolling days incurred that match the criteria specified.</returns>
		public int CountTollingDaysIncurred(string candidateID, string electionCycle, bool isFar)
		{
			using (PostElectionTableAdapter ta = new PostElectionTableAdapter())
			{
				int? tollingDays = null;
				ta.GetTollingDays(candidateID, electionCycle, isFar, ref tollingDays);
				return tollingDays ?? 0;
			}
		}

		/// <summary>
		/// Retrieves a collection of post election events that affect tolling for the Draft Audit Report or Final Audit Report.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="far">true to retrieve events for the Final Audit Report; otherwise, false to retreive events for the Draft Audit Report.</param>
		/// <returns>A collection of tolling events that affect Draft Audit Report tolling.</returns>
		public List<TollingEvent> GetTollingEvents(string candidateID, string electionCycle, bool far)
		{
			DateTime? minStartDate = GetTollingStartDate(electionCycle);
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (TollingEventsTableAdapter ta = new TollingEventsTableAdapter())
				{
					ta.Fill(ds.TollingEvents, candidateID, electionCycle, far);
				}
				List<TollingEvent> events = new List<TollingEvent>(ds.TollingEvents.Count);
				foreach (PostElectionTds.TollingEventsRow row in ds.TollingEvents)
				{
					if (row.IsStartDateNull())
						continue;
					TollingLetter letter = GetTollingLetter(row.SourceCode.Trim(), row.EventCode.Trim(), row.TypeCode.Trim()) ?? new TollingLetter(byte.MinValue)
					{
						SourceCode = new TollingCode(TollingCodeType.SourceCode, byte.MinValue) { Code = row.SourceCode.Trim() },
						EventCode = new TollingCode(TollingCodeType.EventCode, byte.MinValue) { Code = row.EventCode.Trim() },
						TypeCode = new TollingCode(TollingCodeType.TypeCode, byte.MinValue) { Code = row.TypeCode.Trim() },
						Description = row.Description,
						Title = row.Description
					};
					// first fetch end reason to determine end date inclusion in tolling days
					TollingEndReason reason = CPConvert.ToTollingEndReason(row.EndReasonCode.Trim());
					events.Add(new TollingEvent(row.StartDate, row.IsEndDateNull() ? DateTime.Now : row.EndDate)
					{
						Description = letter.Description,
						Title = letter.Title,
						TollingEventNumber = row.EventNumber,
						TollingLetter = letter,
						TollingStartDate = minStartDate.HasValue && row.StartDate < minStartDate ? minStartDate.Value : row.StartDate,
						TollingEndDate = row.IsEndDateNull() ? null : (DateTime?)row.EndDate,
						TollingDueDate = row.IsDueDateNull() ? null : (DateTime?)row.DueDate,
						TollingEndReason = reason,
						ReferenceEventNumber = row.RefEventNumber
					});
				}
				return events;
			}
		}

		/// <summary>
		/// Retrieves a collection of Post Election Initial Documentation Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Initial Documentation Inadequate Response events matching the specified criteria.</returns>
		public List<IdrInadequateEvent> GetIdrInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType)
		{
			// interpret retrieval type
			bool? additionals;
			switch (retrievalType)
			{
				case InadequateEventRetrieval.InitialEvent:
					additionals = false;
					break;
				case InadequateEventRetrieval.AdditionalEvents:
					additionals = true;
					break;
				default:
					additionals = null;
					break;
			}

			// determine minimum start date
			DateTime? minStartDate = GetTollingStartDate(electionCycle);

			// retrieve and parse data
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (InadequateEventsTableAdapter ta = new InadequateEventsTableAdapter())
				{
					ta.Fill(ds.InadequateEvents, candidateID, electionCycle, false, additionals);
				}
				List<IdrInadequateEvent> events = new List<IdrInadequateEvent>();
				foreach (PostElectionTds.InadequateEventsRow row in ds.InadequateEvents)
				{
					TollingLetter letter = GetTollingLetter(row.SourceCode.Trim(), row.EventCode.Trim(), row.TypeCode.Trim()) ?? new TollingLetter(byte.MinValue)
					{
						SourceCode = new TollingCode(TollingCodeType.SourceCode, byte.MinValue) { Code = row.SourceCode.Trim() },
						EventCode = new TollingCode(TollingCodeType.EventCode, byte.MinValue) { Code = row.EventCode.Trim() },
						TypeCode = new TollingCode(TollingCodeType.TypeCode, byte.MinValue) { Code = row.TypeCode.Trim() },
						Description = row.Description,
						Title = row.Description
					};
					// first fetch end reason to determine end date inclusion in tolling days
					TollingEndReason reason = CPConvert.ToTollingEndReason(row.EndReasonCode.Trim());
					IdrInadequateEvent inadequate = new IdrInadequateEvent(row.SentDate, row.IsAdditionalFlagNull() ? false : row.AdditionalFlag)
					{
						ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
						SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
						SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
						ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate,
						// tolling event properties
						Description = letter.Description,
						Title = letter.Title,
						TollingEventNumber = row.EventNumber,
						TollingLetter = letter,
						TollingStartDate = minStartDate.HasValue && row.StartDate < minStartDate ? minStartDate.Value : row.StartDate,
						TollingEndDate = row.IsEndDateNull() ? null : (DateTime?)row.EndDate,
						TollingEndReason = reason
					};
					inadequate.ResponseDeadline = new IdrInadequateDeadline(row.DueDate, inadequate)
					{
						ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
						SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
					};
					events.Add(inadequate);
				}
				return events;
			}
		}

		/// <summary>
		/// Retrieves a collection of Post Election Draft Audit Response Inadequate Response events for a specific candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events are to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
		/// <returns>A collection of Post Election Draft Audit Response Inadequate Response events matching the specified criteria.</returns>
		public List<DarInadequateEvent> GetDarInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType)
		{
			// interpret retrieval type
			bool? additionals;
			switch (retrievalType)
			{
				case InadequateEventRetrieval.InitialEvent:
					additionals = false;
					break;
				case InadequateEventRetrieval.AdditionalEvents:
					additionals = true;
					break;
				default:
					additionals = null;
					break;
			}

			// determine minimum start date
			DateTime? minStartDate = GetTollingStartDate(electionCycle);

			// retrieve and parse data
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (InadequateEventsTableAdapter ta = new InadequateEventsTableAdapter())
				{
					ta.Fill(ds.InadequateEvents, candidateID, electionCycle, true, additionals);
				}
				List<DarInadequateEvent> events = new List<DarInadequateEvent>();
				foreach (PostElectionTds.InadequateEventsRow row in ds.InadequateEvents)
				{
					TollingLetter letter = GetTollingLetter(row.SourceCode.Trim(), row.EventCode.Trim(), row.TypeCode.Trim()) ?? new TollingLetter(byte.MinValue)
					{
						SourceCode = new TollingCode(TollingCodeType.SourceCode, byte.MinValue) { Code = row.SourceCode.Trim() },
						EventCode = new TollingCode(TollingCodeType.EventCode, byte.MinValue) { Code = row.EventCode.Trim() },
						TypeCode = new TollingCode(TollingCodeType.TypeCode, byte.MinValue) { Code = row.TypeCode.Trim() },
						Description = row.Description,
						Title = row.Description
					};
					// first fetch end reason to determine end date inclusion in tolling days
					TollingEndReason reason = CPConvert.ToTollingEndReason(row.EndReasonCode.Trim());
					DarInadequateEvent inadequate = new DarInadequateEvent(row.SentDate, row.IsAdditionalFlagNull() ? false : row.AdditionalFlag)
					{
						ReceiptDate = row.IsReceiptDateNull() ? null : (DateTime?)row.ReceiptDate,
						SecondSentDate = row.IsSecondSentDateNull() ? null : (DateTime?)row.SecondSentDate,
						SecondReceiptDate = row.IsSecondReceiptDateNull() ? null : (DateTime?)row.SecondReceiptDate,
						ResponseSubmittedDate = row.IsResponseSubmittedDateNull() ? null : (DateTime?)row.ResponseSubmittedDate,
						// tolling event properties
						Description = letter.Description,
						Title = letter.Title,
						TollingEventNumber = row.EventNumber,
						TollingLetter = letter,
						TollingStartDate = minStartDate.HasValue && row.StartDate < minStartDate ? minStartDate.Value : row.StartDate,
						TollingEndDate = row.IsEndDateNull() ? null : (DateTime?)row.EndDate,
						TollingEndReason = reason
					};
					inadequate.ResponseDeadline = new DarInadequateDeadline(row.DueDate, inadequate)
					{
						ResponseReceivedDate = row.IsResponseReceivedDateNull() ? null : (DateTime?)row.ResponseReceivedDate,
						SecondDueDate = row.IsSecondDueDateNull() ? null : (DateTime?)row.SecondDueDate
					};
					events.Add(inadequate);
				}
				return events;
			}
		}

		/// <summary>
		/// Retrieves Post Election Audit suspension information for a specfic candidate and election cycle.
		/// </summary>
		/// <param name="candidateID">The ID of the candidate whose suspension information is to be retrieved.</param>
		/// <param name="electionCycle">The election cycle in which to search.</param>
		/// <returns>Suspension information matching the specified criteria if found; otherwise, null.</returns>
		public Suspension GetSuspension(string candidateID, string electionCycle)
		{
			using (PostElectionTds ds = new PostElectionTds())
			{
				using (SuspensionTableAdapter ta = new SuspensionTableAdapter())
				{
					ta.Fill(ds.Suspension, candidateID, electionCycle);
				}
				foreach (PostElectionTds.SuspensionRow row in ds.Suspension)
				{
					return row.IsSuspensionDateNull() ? null : new Suspension(row.SuspensionDate)
					{
						SuspenderName = row.IsSuspenderNameNull() ? null : row.SuspenderName.Trim(),
						SuspensionReason = CPConvert.ToSuspensionReason(row.SuspensionReasonCode.Trim())
					};
				}
			}
			return null;
		}

		/// <summary>
		/// Retrieves the date when post-election tolling events begin for a specific election cycle.
		/// </summary>
		/// <param name="electionCycle">The desired election cycle.</param>
		/// <returns>The date when post-election tolling events begin for the specified election cycle if found; otherwise, null.</returns>
		public DateTime? GetTollingStartDate(string electionCycle)
		{
			using (PostElectionTableAdapter ta = new PostElectionTableAdapter())
			{
				DateTime? startDate = null;
				ta.GetTollingStartDate(electionCycle, ref startDate);
				return startDate;
			}
		}
	}
}
