using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Cfb.CandidatePortal.PostElection;
using Cfb.CandidatePortal.Web.Properties;
using Cfb.Extensions;

namespace Cfb.CandidatePortal.Web
{
    /// <summary>
    /// Provides strongly-typed access to C-Access page URLs.
    /// </summary>
    public sealed class PageUrlManager
    {
        /// <summary>
        /// The project settings.
        /// </summary>
        private static readonly Settings _settings = Properties.Settings.Default;

        #region Page URLs

        /// <summary>
        /// Gets the URL for the candidate profile page.
        /// </summary>
        public static string CandidateProfilePageUrl { get { return _settings.CandidateProfilePageUrl; } }

        /// <summary>
        /// Gets the URL for the committee profile page.
        /// </summary>
        public static string CommitteeProfilePageUrl { get { return _settings.CommitteeProfilePageUrl; } }

        /// <summary>
        /// Gets the URL for the compliance visits page.
        /// </summary>
        public static string ComplianceVisitsPageUrl { get { return _settings.ComplianceVisitsPageUrl; } }

        /// <summary>
        /// Gets the URL for the extension request page.
        /// </summary>
        public static string ExtensionRequestPageUrl { get { return _settings.ExtensionRequestPageUrl; } }

        /// <summary>
        /// Gets the URL for the statement reviews page.
        /// </summary>
        public static string StatementReviewsPageUrl { get { return _settings.StatementReviewsPageUrl; } }

        /// <summary>
        /// Gets the URL for the payment plan page.
        /// </summary>
        public static string PaymentPlanPageUrl { get { return _settings.PaymentPlanPageUrl; } }

        /// <summary>
        /// Gets the URL for the threshold status page.
        /// </summary>
        public static string ThresholdPageUrl { get { return _settings.ThresholdPageUrl; } }

        /// <summary>
        /// Gets the URL for the public funds history page.
        /// </summary>
        public static string PublicFundsPageUrl { get { return _settings.PublicFundsPageUrl; } }

        /// <summary>
        /// Gets the URL for the Doing Business reviews page.
        /// </summary>
        public static string DoingBusinessPageUrl { get { return _settings.DoingBusinessPageUrl; } }

        /// <summary>
        /// Gets the URL for the change password page.
        /// </summary>
        public static string ChangePasswordPageUrl { get { return _settings.ChangePasswordPageUrl; } }

        /// <summary>
        /// Gets the URL for the financial summary page.
        /// </summary>
        public static string FinancialSummaryPageUrl { get { return _settings.FinancialSummaryPageUrl; } }

        /// <summary>
        /// Gets the URL for the training tracking page.
        /// </summary>
        public static string TrainingTrackingPageUrl { get { return _settings.TrainingTrackingPageUrl; } }

        /// <summary>
        /// Gets the URL for the post election audit status page.
        /// </summary>
        public static string PostElectionPageUrl { get { return _settings.PostElectionAuditPageUrl; } }

        /// <summary>
        /// Gets the URL for the filer registrations page.
        /// </summary>
        public static string FilerRegistrationsPageUrl { get { return _settings.FilerRegistrationsPageUrl; } }

        /// <summary>
        /// Gets the URL for the candidate certifications page.
        /// </summary>
        public static string CertificationsPageUrl { get { return _settings.CertificationsPageUrl; } }

        /// <summary>
        /// Gets the URL for the COIB receipts page.
        /// </summary>
        public static string CoibReceiptsPageUrl { get { return _settings.CoibReceiptsPageUrl; } }

        /// <summary>
        /// Gets the URL for the disclosure statements page.
        /// </summary>
        public static string DisclosureStatmentsPageUrl { get { return _settings.DisclosureStatmentsPageUrl; } }

        /// <summary>
        /// Gets the URL for the C-SMART/IDS requests page.
        /// </summary>
        public static string CsmartIdsRequestsPageUrl { get { return _settings.CsmartIdsRequestsPageUrl; } }

        /// <summary>
        /// Gets the URL for the pre-election disclosure statements page.
        /// </summary>
        public static string PreElectionPageUrl { get { return _settings.PreElectionPageUrl; } }

        /// <summary>
        /// Gets the URL for the statements of need page.
        /// </summary>
        public static string StatementsOfNeedPageUrl { get { return _settings.StatementsOfNeedPageUrl; } }

        /// <summary>
        /// Gets the URL for the termination verification page.
        /// </summary>
        public static string TerminationPageUrl { get { return _settings.TerminationPageUrl; } }

        /// <summary>
        /// Gets the URL to the Campaign Messages Online mailbox.
        /// </summary>
        public static string CmoMailboxUrl { get { return _settings.CmoMailboxUrl; } }

        /// <summary>
        /// Gets the URL for the filing day reminder page.
        /// </summary>
        public static string FilingDayReminderPageUrl { get { return _settings.FilingDayReminderPageUrl; } }

        /// <summary>
        /// Gets the URL for the announcement page.
        /// </summary>
        public static string AnnouncementPageUrl { get { return _settings.AnnouncementPageUrl; } }

        /// <summary>
        /// Gets the URL for the unauthorized access page.
        /// </summary>
        public static string UnauthorizedPageUrl { get { return _settings.UnauthorizedPageUrl; } }

        /// <summary>
        /// Gets the URL for the server error page.
        /// </summary>
        public static string ErrorPageUrl { get { return _settings.ErrorPageUrl; } }

        /// <summary>
        /// Gets the URL for the minimal masterpage.
        /// </summary>
        public static string MinimalMasterPageUrl { get { return _settings.MinimalMasterPageUrl; } }

        #endregion

        /// <summary>
        /// Private constructor.
        /// </summary>
        private PageUrlManager() { }

        /// <summary>
        /// Gets the URL for viewing an announcement.
        /// </summary>
        /// <param name="announcement">The announcement to view.</param>
        /// <returns>A URL for viewing the specified announcement.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="announcement"/> is null.</exception>
        public static string GetAnnouncementUrl(Announcement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException("announcement", "Announcement must not be null.");
            return string.Format("{0}?ID={1}", PageUrlManager.AnnouncementPageUrl, announcement.ID);
        }

        /// <summary>
        /// Gets the URL for viewing a CMO message.
        /// </summary>
        /// <param name="uniqueID">The unique ID of the message to view.</param>
        /// <returns>A URL for viewing the specified message.</returns>
        public static string GetMessageUrl(string uniqueID)
        {
            return string.Format("{0}?id={1}", _settings.CmoMessageViewUrl, uniqueID);
        }

        /// <summary>
        /// Gets the URL for navigating to a post election audit report page.
        /// </summary>
        /// <param name="type">The type of post election audit report to navigate to.</param>
        /// <returns>The URL for the post election audit report indicated by <paramref name="type"/>.</returns>
        public static string GetPostElectionAuditReportUrl(AuditReportType type)
        {
            // group audit report types
            switch (type)
            {
                case AuditReportType.IdrInadequateResponse:
                case AuditReportType.IdrAdditionalInadequateResponse:
                    type = AuditReportType.InitialDocumentationRequest;
                    break;
                case AuditReportType.DarInadequateResponse:
                case AuditReportType.DarAdditionalInadequateResponse:
                    type = AuditReportType.DraftAuditReport;
                    break;
            }
            return string.Format("{0}?{1}", PageUrlManager.PostElectionPageUrl, QueryStringManager.MakeQueryString(auditReportType: type).ToQueryString(HttpContext.Current != null ? HttpContext.Current.Server : null));
        }

        /// <summary>
        /// Gets the item display URL for a <see cref="CPCalendarItem"/>.
        /// </summary>
        /// <param name="item">The <see cref="CPCalendarItem"/> whose item display URL is to be retrieved.</param>
        /// <returns>The item display URL for <paramref name="item"/>.</returns>
        public static string GetCalendarItemDisplayUrl(CPCalendarItem item)
        {
            if (item == null)
                return null;
            switch (item.CalendarItemType)
            {
                case CPCalendarItemType.InitialDocumentRequest:
                case CPCalendarItemType.IdrResponseDeadline:
                case CPCalendarItemType.IdrInadequateEvent:
                case CPCalendarItemType.IdrInadequateDeadline:
                case CPCalendarItemType.IdrAdditionalInadequateEvent:
                case CPCalendarItemType.IdrAdditionalInadequateDeadline:
                case CPCalendarItemType.DraftAuditReport:
                case CPCalendarItemType.DarResponseDeadline:
                case CPCalendarItemType.DarInadequateEvent:
                case CPCalendarItemType.DarInadequateDeadline:
                case CPCalendarItemType.DarAdditionalInadequateEvent:
                case CPCalendarItemType.DarAdditionalInadequateDeadline:
                case CPCalendarItemType.FinalAuditReport:
                    return PageUrlManager.PostElectionPageUrl;
                case CPCalendarItemType.StatementReview:
                case CPCalendarItemType.SRResponseDeadline:
                    return PageUrlManager.StatementReviewsPageUrl;
                case CPCalendarItemType.ComplianceVisit:
                case CPCalendarItemType.CVResponseDeadline:
                    return PageUrlManager.ComplianceVisitsPageUrl;
                case CPCalendarItemType.DoingBusinessReview:
                case CPCalendarItemType.DbrResponseDeadline:
                    return PageUrlManager.DoingBusinessPageUrl;
                case CPCalendarItemType.FilingDeadline:
                    return PageUrlManager.FilingDayReminderPageUrl;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the item ID for a <see cref="CPCalendarItem"/>.
        /// </summary>
        /// <param name="item">The <see cref="CPCalendarItem"/> whose item ID is to be retrieved.</param>
        /// <returns>The item ID for <paramref name="item"/>.</returns>
        public static string GetCalendarItemID(CPCalendarItem item)
        {
            if (item == null)
                return null;
            NameValueCollection id = null;
            switch (item.CalendarItemType)
            {
                case CPCalendarItemType.InitialDocumentRequest:
                case CPCalendarItemType.IdrResponseDeadline:
                case CPCalendarItemType.IdrInadequateEvent:
                case CPCalendarItemType.IdrInadequateDeadline:
                case CPCalendarItemType.IdrAdditionalInadequateEvent:
                case CPCalendarItemType.IdrAdditionalInadequateDeadline:
                    id = GetPostElectionAuditItemID(AuditReportType.InitialDocumentationRequest);
                    break;
                case CPCalendarItemType.DraftAuditReport:
                case CPCalendarItemType.DarResponseDeadline:
                case CPCalendarItemType.DarInadequateEvent:
                case CPCalendarItemType.DarInadequateDeadline:
                case CPCalendarItemType.DarAdditionalInadequateEvent:
                case CPCalendarItemType.DarAdditionalInadequateDeadline:
                    id = GetPostElectionAuditItemID(AuditReportType.DraftAuditReport);
                    break;
                case CPCalendarItemType.FinalAuditReport:
                    id = GetPostElectionAuditItemID(AuditReportType.FinalAuditReport);
                    break;
                case CPCalendarItemType.StatementReview:
                case CPCalendarItemType.ComplianceVisit:
                case CPCalendarItemType.DoingBusinessReview:
                    AuditReviewBase review = item as AuditReviewBase;
                    if (review != null)
                        id = GetAuditReviewItemID(review.CommitteeID, review.Number);
                    break;
                case CPCalendarItemType.SRResponseDeadline:
                case CPCalendarItemType.CVResponseDeadline:
                case CPCalendarItemType.DbrResponseDeadline:
                    ResponseDeadlineBase deadline = item as ResponseDeadlineBase;
                    if (deadline != null)
                        id = GetAuditReviewItemID(deadline.CommitteeID, deadline.ReviewNumber);
                    break;
                case CPCalendarItemType.FilingDeadline:
                    FilingDeadline fd = item as FilingDeadline;
                    if (fd != null)
                        id = GetStatementReviewItemID(fd.StatementNumber);
                    break;
                default:
                    return null;
            }
            return "&" + id.ToQueryString(HttpContext.Current != null ? HttpContext.Current.Server : null); // & is to terminate existing item ID parameter key/value pair
        }

        /// <summary>
        /// Gets the calendar item ID for a post election audit item.
        /// </summary>
        /// <param name="type">The item's associated post election audit report type.</param>
        /// <returns>The calendar item ID for the specified post election audit type.</returns>
        private static NameValueCollection GetPostElectionAuditItemID(AuditReportType type)
        {
            return QueryStringManager.MakeQueryString(auditReportType: type);
        }

        /// <summary>
        /// Gets the calendar item ID for a filing disclosure statement.
        /// </summary>
        /// <param name="statementNumber">The number of the statement.</param>
        /// <returns>The calendar item ID for the specified statement.</returns>
        private static NameValueCollection GetStatementReviewItemID(byte statementNumber)
        {
            return QueryStringManager.MakeQueryString(statementNumber: statementNumber);
        }

        /// <summary>
        /// Gets the calendar item ID for an audit review.
        /// </summary>
        /// <param name="committeeID">The ID of the audited committee.</param>
        /// <param name="reviewNumber">The number of the audit review.</param>
        /// <returns>The calendar item ID for the specified audit review.</returns>
        private static NameValueCollection GetAuditReviewItemID(char committeeID, byte reviewNumber)
        {
            return QueryStringManager.MakeQueryString(committeeID: committeeID, reviewNumber: reviewNumber);
        }

        /// <summary>
        /// Gets the URL for requesting an audit review extension.
        /// </summary>
        /// <param name="type">The type of extension being requested.</param>
        /// <param name="number">The number of the audit review response for which an extension is to be requested.</param>
        /// <returns>The URL for requesting an extension to the audit review type and number specified.</returns>
        public static string GetExtensionRequestUrl(ExtensionType type, byte number)
        {
            return string.Format("{0}?{1}", PageUrlManager.ExtensionRequestPageUrl, QueryStringManager.MakeQueryString(extensionType: type, reviewNumber: number).ToQueryString(HttpContext.Current != null ? HttpContext.Current.Server : null));
        }
    }
}
