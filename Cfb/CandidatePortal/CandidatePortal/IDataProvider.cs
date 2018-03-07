using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.CandidatePortal.TrainingTracking;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Defines methods for accessing C-Access data.
    /// </summary>
    public interface IDataProvider
    {
        #region Submission Document Methods

        /// <summary>
        /// Retrieves a generic collection of all certification documents on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose certification documents are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all certification documents on record for the specified candidate and election cycle.</returns>
        CertificationHistory GetCertificationDocuments(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all filer registration documents on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose filer registration documents are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all filer registration documents on record for the specified candidate and election cycle.</returns>
        FilerRegistrationHistory GetFilerRegistrationDocuments(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose verifications of terminated candidacy are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all verifications of terminated candidacy on record for the specified candidate and election cycle.</returns>
        TerminationHistory GetTerminationVerifications(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all pre-election disclosure statements on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose pre-election disclosure statements are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all pre-election disclosure statements on record for the specified candidate and election cycle.</returns>
        PreElectionDisclosureHistory GetPreElectionDisclosures(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="election">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        StatementReviews GetPrincipalStatementReviews(string candidateID, Election election);

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="election">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        StatementReviews GetStatementReviews(string candidateID, Election election);

        /// <summary>
        /// Retrieves a generic collection of all statements of need on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statements of need are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all statements of need on record for the specified candidate and election cycle.</returns>
        StatementOfNeedHistory GetStatementsOfNeed(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all filing disclosure statements on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose filing disclosure statements are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all filing disclosure statements on record for the specified candidate and election cycle.</returns>
        DisclosureStatementHistories GetDisclosureStatements(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose C-SMART/IDS requests are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all C-SMART/IDS requests on record for the specified candidate and election cycle.</returns>
        CsmartIdsRequestHistory GetCsmartIdsRequests(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a generic collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A generic List collection of all Conflict of Interest Board receipts on record for the specified candidate and election cycle.</returns>
        CoibReceiptHistory GetCoibReceipts(string candidateID, string electionCycle);

        #endregion

        #region Extension Request Methods

        /// <summary>
        /// Retrieves a specific extension request.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate who requested the extension.</param>
        /// <param name="electionCycle">The election cycle in which the extension was requested.</param>
        /// <param name="type">The type of extension requested.</param>
        /// <param name="reviewNumber">The number of the audit review for which the extension was requested.</param>
        /// <param name="iteration">The iteration of the extension request.</param>
        /// <returns>An <see cref="ExtensionRequest"/> representing the specified extension request if found; otherwise, false.</returns>
        ExtensionRequest GetExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, byte iteration);

        /// <summary>
        /// Creates a new extension request and adds it to the persistence storage medium.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate requesting the extension.</param>
        /// <param name="electionCycle">The election cycle in which the extension is being requested.</param>
        /// <param name="type">The type of extension requested.</param>
        /// <param name="reviewNumber">The number of the audit review for which the extension is being requested.</param>
        /// <param name="date">The date of the extension request.</param>
        /// <param name="requestedDueDate">The requested extension due date.</param>
        /// <param name="reason">The reason for the extension.</param>
        /// <returns>A new <see cref="ExtensionRequest"/> object if the extension request was added successfully; otherwise, null.</returns>
        ExtensionRequest AddExtensionRequest(string candidateID, string electionCycle, ExtensionType type, byte reviewNumber, DateTime date, DateTime requestedDueDate, string reason);

        #endregion

        #region Election Methods

        /// <summary>
        /// Retrieves a collection of all election cycles in which a candidate is active.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <returns>A collection of all election cycles on record in which the candidate is active.</returns>
        HashSet<string> GetActiveElectionCycles(string candidateID, string cutoff);

        /// <summary>
        /// Gets a collection of all supported election cycles.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <returns>A collection of all supported election cycles.</returns>
        Elections GetElections(string cutoff);

        /// <summary>
        /// Gets a collection of all election cycles in which a candidate is active.
        /// </summary>
        /// <param name="cutoff">The cutoff for supported election cycles.</param>
        /// <param name="candidateID">The ID of the candidate whose active election cycles are to be retrieved.</param>
        /// <returns>A collection of all election cycles in which the candidate is active.</returns>
        Elections GetActiveElections(string cutoff, string candidateID);

        #endregion

        #region Committee Methods

        /// <summary>
        /// Retrieves all authorized committees for a candidate in a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose authorized committees are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all authorized committees on record for the specified candidate and election cycle.</returns>
        AuthorizedCommittees GetAuthorizedCommittees(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves the CFIS ID of a candidate's primary committee for a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The CFIS ID of the candidate's primary committee if found, else null.</returns>
        char? GetPrimaryCommitteeID(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves committees for a candidate.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate desired.</param>
        /// <param name="committeeID">The ID of a committee to search for.</param>
        /// <returns>A collection of all committees on record for the specified candidate with the specified committee ID (if provided).</returns>
        List<Committee> GetCommittees(string candidateID, char? committeeID = null);

        #endregion

        #region Candidate Methods

        /// <summary>
        /// Retrieves basic profile information for all known candidates.
        /// </summary>
        /// <returns>A collection of <see cref="Candidate"/> objects representing all known candidates indexed by CFIS ID.</returns>
        Dictionary<string, Candidate> GetCandidates();

        /// <summary>
        /// Retrieves basic profile information for all candidates active in a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter by.</param>
        /// <returns>A collection of <see cref="ActiveCandidate"/> objects representing all candidates active in the <paramref name="electionCycle"/> election cycle, indexed by CFIS ID.</returns>
        Dictionary<string, ActiveCandidate> GetActiveCandidates(string electionCycle);

        /// <summary>
        /// Retrieves the full name of a candidate by CFIS ID.
        /// </summary>
        /// <param name="candidateID">The CFIS ID of the candidate to find.</param>
        /// <param name="formal">Whether or not to retrieve a formal name.</param>
        /// <returns>The full name of the specified candidate.</returns>
        string GetCandidateName(string candidateID, bool formal);

        /// <summary>
        /// Retrieves basic profile information for a candidate.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <returns>The requested candidate's profile information if found, otherwise null.</returns>
        Candidate GetCandidate(string candidateID);

        /// <summary>
        /// Returns profile information for a candidate who is active in an election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's profile for the specified election cycle.</returns>
        ActiveCandidate GetActiveCandidate(string candidateID, string electionCycle);

        #endregion

        #region Audit Review Methods

        /// <summary>
        /// Retrieves a collection of all completed Doing Business reviews on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Doing Business reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed Doing Business reviews for the specified candidate and election cycle.</returns>
        DoingBusinessReviews GetDoingBusinessReviews(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves all compliance visits on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose compliance visits are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A <see cref="ComplianceVisits"/> collection of all compliance visits on record for the specified candidate and election cycle.</returns>
        ComplianceVisits GetComplianceVisits(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        StatementReviews GetPrincipalStatementReviews(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        StatementReviews GetStatementReviews(string candidateID, string electionCycle);

        #endregion

        #region Post Election Methods

        /// <summary>
        /// Retrieves the Post Election Initial Documentation Request for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose IDR is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="published">Whether or not to retrieve published data.</param>
        /// <returns>The Post Election Initial Documentation Request for the candidate and election cycle specified if found; otherwise, null.</returns>
        InitialDocumentationRequest GetInitialDocumentationRequest(string candidateID, string electionCycle, bool published);

        /// <summary>
        /// Retrieves a tolling letter by tolling codes.
        /// </summary>
        /// <param name="sourceCode">The tolling source code to match.</param>
        /// <param name="eventCode">The tolling event code to match.</param>
        /// <param name="typeCode">The tolling type code to match.</param>
        /// <returns>The tolling letter designated by the specified tolling codes if one exists; otherwise, null.</returns>
        TollingLetter GetTollingLetter(string sourceCode, string eventCode, string typeCode);

        /// <summary>
        /// Retrieves a tolling letter by tolling letter ID.
        /// </summary>
        /// <param name="letterID">The ID of the tolling letter to retrieve.</param>
        /// <returns>The tolling letter designated by the specified tolling codes if found; otherwise, null.</returns>
        TollingLetter GetTollingLetter(int letterID);

        /// <summary>
        /// Retrieves a collection of all known tolling letters.
        /// </summary>
        /// <returns>A collection of all known tolling letter.</returns>
        List<TollingLetter> GetTollingLetters();

        /// <summary>
        /// Retrieves the Post Election Initial Documentation Request Inadequate Response event for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="published">Whether or not to retrieve published data.</param>
        /// <returns>The Post Election Initial Documentation Request Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
        IdrInadequateEvent GetIdrInadequateEvent(string candidateID, string electionCycle, bool published);

        /// <summary>
        /// Gets whether or not a Post Election Initial Documentation Request response analysis worksheet exists for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>true if an IDR response analysis exists for the candidate and election cycle specified; otherwise, false.</returns>
        bool HasIdrResponseAnalysis(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves the Post Election Audit Draft Audit Report for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="published">Whether or not to retrieve published data.</param>
        /// <returns>The Post Election Draft Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
        DraftAuditReport GetDraftAuditReport(string candidateID, string electionCycle, bool published);

        /// <summary>
        /// Retrieves the Post Election Draft Audit Report Inadequate Response event for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="published">Whether or not to retrieve published data.</param>
        /// <returns>The Post Election Draft Audit Report Inadequate Response event for the candidate and election cycle specified if found; otherwise, null.</returns>
        DarInadequateEvent GetDarInadequateEvent(string candidateID, string electionCycle, bool published);

        /// <summary>
        /// Retrieves the Post Election Audit Final Audit Report for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose DAR is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The Post Election Final Audit Report for the candidate and election cycle specified if found; otherwise, null.</returns>
        FinalAuditReport GetFinalAuditReport(string candidateID, string electionCycle);

        /// <summary>
        /// Counts the number of post election audit tolling days incurred by a candidate for a specific election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the desired candidate.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <param name="isFar">true to count Final Audit Report tolling days; otherwise, false to count Draft Audit Report tolling days.</param>
        /// <returns>The number of tolling days incurred that match the criteria specified.</returns>
        int CountTollingDaysIncurred(string candidateID, string electionCycle, bool isFar);

        /// <summary>
        /// Retrieves a collection of post election events that affect tolling for the Draft Audit Report or Final Audit Report.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose events are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="far">true to retrieve events for the Final Audit Report; otherwise, false to retreive events for the Draft Audit Report.</param>
        /// <returns>A collection of tolling events that affect Draft Audit Report tolling.</returns>
        List<ITollingEvent> GetTollingEvents(string candidateID, string electionCycle, bool far);

        /// <summary>
        /// Retrieves a collection of Post Election Initial Documentation Inadequate Response events for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose IDR Inadequate Response events is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
        /// <returns>A collection of Post Election Initial Documentation Inadequate Response events matching the specified criteria.</returns>
        List<IdrInadequateEvent> GetIdrInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType);

        /// <summary>
        /// Retrieves a collection of Post Election Draft Audit Response Inadequate Response events for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose DAR Inadequate Response events are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="retrievalType">The type of inadequate event criteria to use for retrieval.</param>
        /// <returns>A collection of Post Election Draft Audit Response Inadequate Response events matching the specified criteria.</returns>
        List<DarInadequateEvent> GetDarInadequateEvents(string candidateID, string electionCycle, InadequateEventRetrieval retrievalType);

        /// <summary>
        /// Retrieves Post Election Audit suspension information for a specfic candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose suspension information is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>Suspension information matching the specified criteria if found; otherwise, null.</returns>
        Suspension GetSuspension(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves the date when post-election tolling events begin for a specific election cycle.
        /// </summary>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The date when post-election tolling events begin for the specified election cycle if found; otherwise, null.</returns>
        DateTime? GetTollingStartDate(string electionCycle);

        #endregion

        /// <summary>
        /// Retrieves the current training tracking status for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose training tracking status is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The current training tracking status for the specified candidate and election cycle.</returns>
        TrainingStatus GetTrainingStatus(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a history of public funds determinations for a specific candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose public funds determination history is to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The history of public funds determinations for the specified candidate and election cycle.</returns>
        PublicFundsHistory GetPublicFundsHistory(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of statement dates for an election cycle.
        /// </summary>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>A collection of statement dates for the election cycle specified, sorted and indexed by statement number.</returns>
        Dictionary<byte, Statement> GetStatements(string electionCycle);

        /// <summary>
        /// Retrieves the payment plan on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        PaymentPlan GetPaymentPlan(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves all campaign liaisons on record for the specified candidate and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose liaisons are to be retrieved.</param>
        /// <param name="committeeID">The ID of the committee whose liaisons are to be retrieved.</param>
        /// <returns>A collection of all campaign liaisons on record for the specified candidate and committee, indexed by liaison ID.</returns>
        Dictionary<byte, Liaison> GetLiaisonsByCommittee(string candidateID, char committeeID);

        /// <summary>
        /// Returns a candidate's campaign financial summary for a given election cycle.
        /// </summary>
        /// <param name="candidateID">The desired candidate's CFIS ID.</param>
        /// <param name="electionCycle">The desired election cycle.</param>
        /// <returns>The specified candidate's campaign financial summary for the specified election cycle.</returns>
        FinancialSummary GetFinancialSummary(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves filing dates and requirements for a particular candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose filing dates and requirements are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all filing dates and requirements on record for the specified candidate and election cycle.</returns>
        FilingDeadlines GetFilingDeadlines(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves all bank accounts on record for the specified candidate, election cycle, and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose bank accounts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="committeeID">The ID of the committee whose bank accounts are to be retrieved.</param>
        /// <returns>A collection of all bank accounts on record for the specified candidate, election cycle, and committee, indexed by bank account ID.</returns>
        Dictionary<byte, BankAccount> GetBankAccountsByCommittee(string candidateID, string electionCycle, char committeeID);

        /// <summary>
        /// Retrieves a web transfer date for a specific election cycle.
        /// </summary>
        /// <returns>A web transfer date for a specific election cycle.</returns>
        DateTime? GetWebTransferDate(string electionCycle);

        /// <summary>
        /// Retrieves a collection of the specified candidate's complete threshold status history for the specified election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Conflict of Interest Board receipts are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of the specified candidate's complete threshold status history for the specified election cycle.</returns>
        ThresholdHistory GetThresholdHistory(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of unexpired announcements filtered by election cycle.
        /// </summary>
        /// <param name="electionCycle">The election cycle to filter results by, or an empty string to retrieve announcements across all elections.</param>
        /// <returns>A collection of unexpired announcements filtered by the election cycle specified.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="electionCycle"/> is null.</exception>
        IEnumerable<Announcement> GetAnnouncements(string electionCycle);

        /// <summary>
        /// Retrieves an announcement by unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the announcement to retrieve.</param>
        /// <returns>The announcement matching the ID specified if found; otherwise, null.</returns>
        Announcement GetAnnouncement(string id);

        /// <summary>
        /// Retrieves a filing date announcement by statement number and election cycle.
        /// </summary>
        /// <param name="statementNumber">The filing statement number to search for.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>The announcement matching the filing statement number and election cycle specified if found; otherwise, null.</returns>
        Announcement GetFilingDateAnnouncement(byte statementNumber, string electionCycle);

        /// <summary>
        /// Attempts a fast handshake with the CFIS database to verify that it is online.
        /// </summary>
        void PingCfis();

        /// <summary>
        /// Determines whether or not an exception indicates an offline data provider.
        /// </summary>
        /// <param name="e">The exception to analyze.</param>
        /// <returns>true if <paramref name="e"/> indicates that a data provider is offline.</returns>
        bool IsOfflineDataException(Exception e);

        /// <summary>
        /// Retrieves a global C-Access setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to retrieve.</param>
        /// <returns>The value of the setting if found; otherwise, null.</returns>
        string GetGlobalSetting(string settingName);

        /// <summary>
        /// Saves a global C-Access setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to save.</param>
        /// <param name="value">The desired value of the setting.</param>
        void SetGlobalSetting(string settingName, string value);

        /// <summary>
        /// Retrieves a campaign entity registered with the CFB.
        /// </summary>
        /// <param name="candidateID">The ID for a candidate.</param>
        /// <param name="committeeID">The ID for a committee.</param>
        /// <param name="electionCycle">The election cycle for which a treasurer's committee is authorized.</param>
        /// <param name="liaisonID">The committee-relative ID for a liaison.</param>
        /// <returns>The entity that matches the specified criteria if found; otherwise, null.</returns>
        /// <remarks>If <paramref name="liaisonID"/> is provided (not null), a liaison is targeted. If <paramref name="committeeID"/> is not provided (null), a candidate is targeted.</remarks>
        Entity GetEntity(string candidateID, char? committeeID = null, string electionCycle = null, byte? liaisonID = null);
    }
}
