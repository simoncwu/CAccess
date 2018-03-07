using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Cfb.CandidatePortal.CampaignContacts;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Security.Sso;
using Cfb.CandidatePortal.SubmissionDocuments;
using Cfb.CandidatePortal.TrainingTracking;
using Cfb.CandidatePortal.Web.Properties;

namespace Cfb.CandidatePortal.Web
{
    /// <summary>
    /// A C-Access user's account profile.
    /// </summary>
    public static class CPProfile
    {
        /// <summary>
        /// Whether or not to cache data in session state.
        /// </summary>
        private static readonly bool _cacheData = Settings.Default.CacheData;

        /// <summary>
        /// Gets the logged-in user's candidate ID.
        /// </summary>
        public static string Cid
        {
            get
            {
                string cid = HttpContext.Current.Session[CacheKeys.Cid] as string;
                if (string.IsNullOrEmpty(cid))
                    HttpContext.Current.Session[CacheKeys.Cid] = cid = CPSecurity.Provider.GetCid(UserName);
                return cid;
            }
        }

        /// <summary>
        /// Gets or sets the logged-in user's current election cycle.
        /// </summary>
        public static string ElectionCycle
        {
            get
            {
                return Election == null ? null : Election.Cycle;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Elections cycles = CPProfile.Elections;
                    if (cycles != null)
                    {
                        Election e = cycles[value];
                        if (e != null)
                            Election = e;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the logged-in user's current election.
        /// </summary>
        public static Election Election
        {
            get
            {
                Election e = HttpContext.Current.Session[CacheKeys.Election] as Election;
                if (e == null)
                {
                    Elections cycles = CPProfile.Elections;
                    HttpContext.Current.Session[CacheKeys.Election] = e = CPApplication.GetElection(cycles != null ? cycles.GetCurrentElectionCycle() : null);
                }
                return e;
            }
            private set
            {
                HttpContext.Current.Session[CacheKeys.Election] = value;
            }
        }

        /// <summary>
        /// Gets the logged-in user's election cycles.
        /// </summary>
        public static Elections Elections
        {
            get
            {
                string cacheKey = CacheKeys.ElectionCycles;
                Elections ec = _cacheData ? HttpContext.Current.Session[cacheKey] as Elections : null;
                if (ec == null)
                {
                    ec = Elections.GetElections();
                    ec.ApplyFilter(CPSecurity.Provider.GetAuthorizedElectionCycles(UserName));
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = ec;
                }
                return ec;
            }
        }

        /// <summary>
        /// Gets the logged-in user's active candidate information for the current election cycle.
        /// </summary>
        public static ActiveCandidate ActiveCandidate
        {
            get
            {
                string cacheKey = typeof(ActiveCandidate).FullName;
                ActiveCandidate c = _cacheData ? HttpContext.Current.Session[cacheKey] as ActiveCandidate : null;
                if (c == null)
                {
                    c = ActiveCandidate.GetActiveCandidate(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = c;
                }
                return c;
            }
        }

        /// <summary>
        /// Gets the logged-in user's authorized committees for the current election cycle.
        /// </summary>
        public static AuthorizedCommittees AuthorizedCommittees
        {
            get
            {
                string cacheKey = typeof(AuthorizedCommittees).FullName;
                AuthorizedCommittees acs = _cacheData ? HttpContext.Current.Session[cacheKey] as AuthorizedCommittees : null;
                if (acs == null)
                {
                    acs = AuthorizedCommittees.GetAuthorizedCommittees(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = acs;
                }
                return acs;
            }
        }

        /// <summary>
        /// Gets the logged-in user's filing deadlines for the current election cycle.
        /// </summary>
        public static FilingDeadlines FilingDeadlines
        {
            get
            {
                string cacheKey = typeof(FilingDeadlines).FullName;
                FilingDeadlines fd = _cacheData ? HttpContext.Current.Session[cacheKey] as FilingDeadlines : null;
                if (fd == null)
                {
                    fd = FilingDeadline.GetFilingDeadlines(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = fd;
                }
                return fd;
            }
        }

        /// <summary>
        /// Gets the logged-in user's filer registration history for the current election cycle.
        /// </summary>
        public static FilerRegistrationHistory FilerRegistrationHistory
        {
            get
            {
                string cacheKey = typeof(FilerRegistrationHistory).FullName;
                FilerRegistrationHistory frh = _cacheData ? HttpContext.Current.Session[cacheKey] as FilerRegistrationHistory : null;
                if (frh == null)
                {
                    frh = FilerRegistrationHistory.GetFilerRegistrationDocuments(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = frh;
                }
                return frh;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any filer registrations for the current election cycle.
        /// </summary>
        public static bool HasFilerRegistrations
        {
            get
            {
                FilerRegistrationHistory frh = CPProfile.FilerRegistrationHistory;
                return frh != null && frh.Documents.Count > 0;
            }
        }

        /// <summary>
        /// Gets the logged-in user's termination history for the current election cycle.
        /// </summary>
        public static TerminationHistory TerminationHistory
        {
            get
            {
                string cacheKey = typeof(TerminationHistory).FullName;
                TerminationHistory th = _cacheData ? HttpContext.Current.Session[cacheKey] as TerminationHistory : null;
                if (th == null)
                {
                    th = TerminationHistory.GetTerminationVerifications(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = th;
                }
                return th;
            }
        }

        /// <summary>
        /// Gets the logged-in user's threshold status history for the current election cycle.
        /// </summary>
        public static ThresholdHistory ThresholdHistory
        {
            get
            {
                string cacheKey = typeof(ThresholdHistory).FullName;
                ThresholdHistory th = _cacheData ? HttpContext.Current.Session[cacheKey] as ThresholdHistory : null;
                if (th == null)
                {
                    th = ThresholdHistory.GetThresholdHistory(Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = th;
                }
                return th;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any terminations for the current election cycle.
        /// </summary>
        public static bool HasTerminations
        {
            get
            {
                TerminationHistory th = CPProfile.TerminationHistory;
                return !object.Equals(th, null) && (th.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any threshold history for the current election cycle.
        /// </summary>
        public static bool HasThresholdHistory
        {
            get
            {
                ThresholdHistory th = CPProfile.ThresholdHistory;
                return !object.Equals(th, null) && (th.History.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's certification history for the current election cycle.
        /// </summary>
        public static CertificationHistory CertificationHistory
        {
            get
            {
                string cacheKey = typeof(CertificationHistory).FullName;
                CertificationHistory ch = _cacheData ? HttpContext.Current.Session[cacheKey] as CertificationHistory : null;
                if (ch == null)
                {
                    ch = CertificationHistory.GetCertificationDocuments(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = ch;
                }
                return ch;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any certifications for the current election cycle.
        /// </summary>
        public static bool HasCertifications
        {
            get
            {
                CertificationHistory ch = CPProfile.CertificationHistory;
                return !object.Equals(ch, null) && (ch.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's COIB receipt history for the current election cycle.
        /// </summary>
        public static CoibReceiptHistory CoibReceiptHistory
        {
            get
            {
                string cacheKey = typeof(CoibReceiptHistory).FullName;
                CoibReceiptHistory crh = _cacheData ? HttpContext.Current.Session[cacheKey] as CoibReceiptHistory : null;
                if (crh == null)
                {
                    crh = CoibReceiptHistory.GetCoibReceipts(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = crh;
                }
                return crh;
            }
        }

        /// <summary>
        /// Gets a collection of all upcoming reminder events for the current user in the current election cycle.
        /// </summary>
        public static IEnumerable<CPCalendarItem> ReminderEvents
        {
            get
            {
                IEnumerable<CPCalendarItem> items = _cacheData ? HttpContext.Current.Session[CacheKeys.ReminderEvents] as IEnumerable<CPCalendarItem> : null;
                if (items == null)
                {
                    CPCalendarItemCollection<CPCalendarItem> c = new CPCalendarItemCollection<CPCalendarItem>();
                    ComplianceVisits cv = CPProfile.ComplianceVisits;
                    c.AddRange(cv.Visits);

                    // upcoming deadlines
                    CPCalendarItemCollection<ResponseDeadlineBase> deadlines = new CPCalendarItemCollection<ResponseDeadlineBase>();
                    deadlines.AddRange(cv.GetUpcomingDeadlines());
                    deadlines.AddRange(CPProfile.StatementReviews.ResponseDeadlines);
                    deadlines.AddRange(CPProfile.DoingBusinessReviews.ResponseDeadlines);
                    deadlines.AddRange(CPProfile.FilingDeadlines.GetRequiredDeadlines());

                    // post election deadlines
                    AuditReportBase report = CPProfile.DraftAuditReport ?? (AuditReportBase)CPProfile.InitialDocumentationRequest;
                    ResponseDeadlineBase ped = report == null ? null : report.LastReponseDeadline;
                    if (ped != null)
                        deadlines.Add(ped);

                    // combine and sort
                    c.AddRange(deadlines.Where(d => !d.ResponseReceived));
                    bool isElectionYear = CPProfile.Election != null && CPProfile.Election.Year == DateTime.Now.Year;
                    items = from i in c
                            where isElectionYear ? i.IsElectionYearUpcoming : i.IsUpcoming
                            orderby i.Date
                            select i;
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.ReminderEvents] = items;
                }
                return items;
            }
        }

        /// <summary>
        /// Gets a collection of all calendar events for the current user in the current election cycle.
        /// </summary>
        public static IEnumerable<CPCalendarItem> CalendarEvents
        {
            get
            {
                List<CPCalendarItem> items = _cacheData ? HttpContext.Current.Session[CacheKeys.CalendarEvents] as List<CPCalendarItem> : null;
                if (items == null)
                {
                    items = new List<CPCalendarItem>();
                    ComplianceVisits cv = CPProfile.ComplianceVisits;
                    items.AddRange(cv.Visits);
                    items.AddRange(cv.ResponseDeadlines);
                    items.AddRange(CPProfile.StatementReviews.ResponseDeadlines);
                    items.AddRange(CPProfile.DoingBusinessReviews.ResponseDeadlines);
                    items.AddRange(CPProfile.FilingDeadlines.GetRequiredDeadlines());

                    // post election items
                    AuditReportBase report = CPProfile.InitialDocumentationRequest;
                    TollingEventCollection events;
                    if (report != null)
                    {
                        items.AddRange(report.GetCalendarEvents());
                        items.AddRange(report.TollingEvents.GetCalendarEvents());
                    }
                    // DAR
                    report = CPProfile.DraftAuditReport;
                    if (report != null)
                    {
                        items.AddRange(report.GetCalendarEvents());
                    }
                    // FAR
                    report = CPProfile.FinalAuditReport;
                    if (report == null)
                    {
                        events = CPProfile.FarTollingEvents;
                        if (events != null)
                            items.AddRange(events.GetCalendarEvents());
                    }
                    else
                    {
                        items.AddRange(report.GetCalendarEvents());
                        items.AddRange(report.TollingEvents.GetCalendarEvents());
                    }
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.CalendarEvents] = items;
                }
                return items;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any COIB receipts for the current election cycle.
        /// </summary>
        public static bool HasCoibReceipts
        {
            get
            {
                CoibReceiptHistory crh = CPProfile.CoibReceiptHistory;
                return !object.Equals(crh, null) && (crh.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's C-SMART/IDS request history for the current election cycle.
        /// </summary>
        public static CsmartIdsRequestHistory CsmartIdsRequestHistory
        {
            get
            {
                string cacheKey = typeof(CsmartIdsRequestHistory).FullName;
                CsmartIdsRequestHistory cirh = _cacheData ? HttpContext.Current.Session[cacheKey] as CsmartIdsRequestHistory : null;
                if (cirh == null)
                {
                    cirh = CsmartIdsRequestHistory.GetCsmartIdsRequests(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = cirh;
                }
                return cirh;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any C-SMART/IDS requests for the current election cycle.
        /// </summary>
        public static bool HasCsmartIdsRequests
        {
            get
            {
                CsmartIdsRequestHistory cirh = CPProfile.CsmartIdsRequestHistory;
                return !object.Equals(cirh, null) && (cirh.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's pre-election disclosure history for the current election cycle.
        /// </summary>
        public static PreElectionDisclosureHistory PreElectionDisclosureHistory
        {
            get
            {
                string cacheKey = typeof(PreElectionDisclosureHistory).FullName;
                PreElectionDisclosureHistory pedh = _cacheData ? HttpContext.Current.Session[cacheKey] as PreElectionDisclosureHistory : null;
                if (pedh == null)
                {
                    pedh = PreElectionDisclosureHistory.GetPreElectionDisclosures(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = pedh;
                }
                return pedh;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any pre-election disclosure statements for the current election cycle.
        /// </summary>
        public static bool HasPreElectionDisclosures
        {
            get
            {
                PreElectionDisclosureHistory pedh = CPProfile.PreElectionDisclosureHistory;
                return !object.Equals(pedh, null) && (pedh.Primary.Documents.Count + pedh.General.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's statements of need history for the current election cycle.
        /// </summary>
        public static StatementOfNeedHistory StatementOfNeedHistory
        {
            get
            {
                string cacheKey = typeof(StatementOfNeedHistory).FullName;
                StatementOfNeedHistory sonh = _cacheData ? HttpContext.Current.Session[cacheKey] as StatementOfNeedHistory : null;
                if (sonh == null)
                {
                    sonh = StatementOfNeedHistory.GetStatementsOfNeed(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = sonh;
                }
                return sonh;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any statements of need for the current election cycle.
        /// </summary>
        public static bool HasStatementsOfNeed
        {
            get
            {
                StatementOfNeedHistory sonh = CPProfile.StatementOfNeedHistory;
                return !object.Equals(sonh, null) && (sonh.Primary.Documents.Count + sonh.General.Documents.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's compliance visits for the current election cycle.
        /// </summary>
        public static ComplianceVisits ComplianceVisits
        {
            get
            {
                string cacheKey = typeof(ComplianceVisits).FullName;
                ComplianceVisits cv = _cacheData ? HttpContext.Current.Session[cacheKey] as ComplianceVisits : null;
                if (cv == null)
                {
                    cv = ComplianceVisits.GetComplianceVisits(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = cv;
                }
                return cv;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any compliance visits for the current election cycle.
        /// </summary>
        public static bool HasComplianceVisits
        {
            get
            {
                ComplianceVisits cv = CPProfile.ComplianceVisits;
                return !object.Equals(cv, null) && cv.Count > 0;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any disclosure statements for the current election cycle.
        /// </summary>
        public static bool HasDisclosureStatements
        {
            get
            {
                DisclosureStatementHistories dsh = CPProfile.DisclosureStatementHistories;
                return !object.Equals(dsh, null) && dsh.Count > 0;
            }
        }

        /// <summary>
        /// Gets the logged-in user's disclosure histories for the current election cycle.
        /// </summary>
        public static DisclosureStatementHistories DisclosureStatementHistories
        {
            get
            {
                string cacheKey = typeof(DisclosureStatementHistories).FullName;
                DisclosureStatementHistories dsh = _cacheData ? HttpContext.Current.Session[cacheKey] as DisclosureStatementHistories : null;
                if (dsh == null)
                {
                    dsh = DisclosureStatementHistories.GetDisclosureStatements(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = dsh;
                }
                return dsh;
            }
        }

        /// <summary>
        /// Gets the logged-in user's statement reviews for the current election cycle.
        /// </summary>
        public static StatementReviews StatementReviews
        {
            get
            {
                string cacheKey = typeof(StatementReviews).FullName;
                StatementReviews sr = _cacheData ? HttpContext.Current.Session[cacheKey] as StatementReviews : null;
                if (sr == null)
                {
                    sr = StatementReviews.GetStatementReviews(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = sr;
                }
                return sr;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any statement reviews for the current election cycle.
        /// </summary>
        public static bool HasStatementReviews
        {
            get
            {
                StatementReviews sr = CPProfile.StatementReviews;
                return !object.Equals(sr, null) && (sr.Count > 0);
            }
        }

        /// <summary>
        /// Gets the logged-in user's payment plan for the current election cycle.
        /// </summary>
        public static PaymentPlan PaymentPlan
        {
            get
            {
                string cacheKey = typeof(PaymentPlan).FullName;
                PaymentPlan p = _cacheData ? HttpContext.Current.Session[cacheKey] as PaymentPlan : null;
                if (p == null)
                {
                    p = PaymentPlan.GetPaymentPlan(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = p;
                }
                return p;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has a payment plan for the current election cycle.
        /// </summary>
        public static bool HasPaymentPlan
        {
            get { return CPProfile.PaymentPlan != null; }
        }

        /// <summary>
        /// Gets the e-mail address of the currently logged-in user.
        /// </summary>
        /// <returns>The e-mail address of the currently logged-in user.</returns>
        public static MailAddress GetMailAddress()
        {
            CPUser user = CPSecurity.Provider.GetUser(UserName);
            return user == null ? null : new MailAddress(user.Email, user.DisplayName);
        }

        /// <summary>
        /// Gets the logged-in user's Doing Business reviews for the current election cycle.
        /// </summary>
        public static DoingBusinessReviews DoingBusinessReviews
        {
            get
            {
                string cacheKey = typeof(DoingBusinessReviews).FullName;
                DoingBusinessReviews dbr = _cacheData ? HttpContext.Current.Session[cacheKey] as DoingBusinessReviews : null;
                if (dbr == null)
                {
                    dbr = DoingBusinessReviews.GetDoingBusinessReviews(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = dbr;
                }
                return dbr;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any Doing Business reviews for the current election cycle.
        /// </summary>
        public static bool HasDoingBusinessReviews
        {
            get
            {
                DoingBusinessReviews dbr = CPProfile.DoingBusinessReviews;
                return dbr != null && dbr.Count > 0;
            }
        }

        /// <summary>
        /// Gets the logged-in user's public funds disbursements for the current election cycle.
        /// </summary>
        public static PublicFundsHistory PublicFundsHistory
        {
            get
            {
                string cacheKey = typeof(PublicFundsHistory).FullName;
                PublicFundsHistory fph = _cacheData ? HttpContext.Current.Session[cacheKey] as PublicFundsHistory : null;
                if (fph == null)
                {
                    fph = PublicFundsHistory.GetPublicFundsHistory(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = fph;
                }
                return fph;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any public funds disbursement history for the current election cycle.
        /// </summary>
        public static bool HasPublicFundsHistory
        {
            get
            {
                PublicFundsHistory fph = CPProfile.PublicFundsHistory;
                return fph != null && fph.Determinations.Count > 0;
            }
        }

        /// <summary>
        /// Gets the logged-in user's training tracking history for the current election cycle.
        /// </summary>
        public static TrainingStatus TrainingStatus
        {
            get
            {
                string cacheKey = typeof(InitialDocumentationRequest).FullName;
                TrainingStatus ts = _cacheData ? HttpContext.Current.Session[cacheKey] as TrainingStatus : null;
                if (ts == null)
                {
                    ts = TrainingStatus.GetTrainingStatus(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = ts;
                }
                return ts;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any training tracking history for the current election cycle.
        /// </summary>
        public static bool HasTrainingStatus
        {
            get
            {
                TrainingStatus ts = CPProfile.TrainingStatus;
                return ts != null && ts.Sessions.Sessions.Count > 0;
            }
        }

        /// <summary>
        /// Gets the logged-in user's initial documentation request for the current election cycle.
        /// </summary>
        public static InitialDocumentationRequest InitialDocumentationRequest
        {
            get
            {
                string cacheKey = typeof(InitialDocumentationRequest).FullName;
                InitialDocumentationRequest idr = _cacheData ? HttpContext.Current.Session[cacheKey] as InitialDocumentationRequest : null;
                if (idr == null)
                {
                    idr = InitialDocumentationRequest.GetInitialDocumentationRequest(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = idr;
                }
                return idr;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has been issued an initial documentation request in the current election cycle.
        /// </summary>
        public static bool HasInitialDocumentationRequest
        {
            get { return CPProfile.InitialDocumentationRequest != null; }
        }

        /// <summary>
        /// Gets the logged-in user's draft audit report for the current election cycle.
        /// </summary>
        public static DraftAuditReport DraftAuditReport
        {
            get
            {
                string cacheKey = typeof(DraftAuditReport).FullName;
                DraftAuditReport dar = _cacheData ? HttpContext.Current.Session[cacheKey] as DraftAuditReport : null;
                if (dar == null)
                {
                    dar = DraftAuditReport.GetDraftAuditReport(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = dar;
                }
                return dar;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has been issued a draft audit report in the current election cycle.
        /// </summary>
        public static bool HasDraftAuditReport
        {
            get { return CPProfile.DraftAuditReport != null; }
        }

        /// <summary>
        /// Gets the logged-in user's draft audit report tolling events for the current election cycle.
        /// </summary>
        public static TollingEventCollection DarTollingEvents
        {
            get
            {
                string cacheKey = typeof(DraftAuditReport).FullName + typeof(TollingEventCollection).Name;
                TollingEventCollection events = _cacheData ? HttpContext.Current.Session[cacheKey] as TollingEventCollection : null;
                if (events == null)
                {
                    events = DraftAuditReport.GetTollingEvents(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = events;
                }
                return events;
            }
        }

        /// <summary>
        /// Gets the logged-in user's final audit report for the current election cycle.
        /// </summary>
        public static FinalAuditReport FinalAuditReport
        {
            get
            {
                string cacheKey = typeof(FinalAuditReport).FullName;
                FinalAuditReport far = _cacheData ? HttpContext.Current.Session[cacheKey] as FinalAuditReport : null;
                if (far == null)
                {
                    far = FinalAuditReport.GetFinalAuditReport(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = far;
                }
                return far;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has been issued a final audit report in the current election cycle.
        /// </summary>
        public static bool HasFinalAuditReport
        {
            get { return CPProfile.FinalAuditReport != null; }
        }

        /// <summary>
        /// Gets the logged-in user's final audit report tolling events for the current election cycle.
        /// </summary>
        public static TollingEventCollection FarTollingEvents
        {
            get
            {
                string cacheKey = typeof(FinalAuditReport).FullName + typeof(TollingEventCollection).Name;
                TollingEventCollection events = _cacheData ? HttpContext.Current.Session[cacheKey] as TollingEventCollection : null;
                if (events == null)
                {
                    events = FinalAuditReport.GetTollingEvents(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = events;
                }
                return events;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user has any post election audit status.
        /// </summary>
        public static bool HasPostElectionAudit
        {
            get { return CPProfile.HasInitialDocumentationRequest || CPProfile.HasDraftAuditReport; }
        }

        /// <summary>
        /// Gets the logged-in user's final audit report issuance date for the current election cycle.
        /// </summary>
        public static DateTime? FarIssuanceDate
        {
            get
            {
                string cacheKey = typeof(FinalAuditReport).FullName + typeof(DateTime).Name;
                DateTime? date = _cacheData ? HttpContext.Current.Session[cacheKey] as DateTime? : null;
                if (date == null)
                {
                    date = FinalAuditReport.GetOriginalIssuanceDate(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[cacheKey] = date;
                }
                return date;
            }
        }

        /// <summary>
        /// Gets the logged-in user's post election audit suspension information.
        /// </summary>
        public static Suspension PESuspension
        {
            get
            {
                Suspension s = _cacheData ? HttpContext.Current.Session[CacheKeys.PESuspension] as Suspension : null;
                if (s == null)
                {
                    s = Suspension.GetSuspension(CPProfile.Cid, CPProfile.ElectionCycle);
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.PESuspension] = s;
                }
                return s;
            }
        }

        /// <summary>
        /// Gets whether or not the logged-in user is authorized for C-Access.
        /// </summary>
        public static CPUserRights UserRights
        {
            get
            {
                HttpContext context = HttpContext.Current;
                HttpSessionState session = context.Session;
                CPUserRights? val = _cacheData ? session[CacheKeys.UserRights] as CPUserRights? : null;
                if (!val.HasValue)
                {
                    val = CPSecurity.Provider.GetUserRights(CPProfile.UserName);
                    if (_cacheData)
                        session[CacheKeys.UserRights] = val;
                }
                return val.Value;
            }
        }

        /// <summary>
        /// Gets an analysis of all current and potential C-Access user accounts for the current campaign.
        /// </summary>
        public static AnalysisResults AnalysisResults
        {
            get
            {
                return AccountAnalysis.Analyze(CPProfile.ActiveCandidate, CPProfile.ElectionCycle);
            }
        }

        /// <summary>
        /// Gets the username of the currently logged-in user.
        /// </summary>
        public static string UserName
        {
            get
            {
                string username = _cacheData ? HttpContext.Current.Session[CacheKeys.UserName] as string : null;
                if (username == null)
                {
                    username = HttpContext.Current.User.Identity.Name;
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.UserName] = username;
                }
                return username;
            }
        }

        /// <summary>
        /// Gets the user-friendly display name of the currently logged-in user.
        /// </summary>
        public static string DisplayName
        {
            get
            {
                string displayName = _cacheData ? HttpContext.Current.Session[CacheKeys.DisplayName] as string : null;
                if (displayName == null)
                {
                    displayName = CPSecurity.Provider.GetDisplayName(UserName);
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.DisplayName] = displayName;
                }
                return displayName;
            }
        }

        /// <summary>
        /// Gets a collection of SSO applications that the currently logged-in user is authorized to access.
        /// </summary>
        public static IEnumerable<Application> Applications
        {
            get
            {
                IEnumerable<Application> apps = _cacheData ? HttpContext.Current.Session[CacheKeys.Applications] as IEnumerable<Application> : null;
                if (apps == null)
                {
                    CPUser user = CPSecurity.Provider.GetUser(CPProfile.UserName);
                    apps = user != null ? user.Applications : new Application[0].AsEnumerable();
                    if (_cacheData)
                        HttpContext.Current.Session[CacheKeys.Applications] = apps;
                }
                return apps;
            }
        }
    }
}
