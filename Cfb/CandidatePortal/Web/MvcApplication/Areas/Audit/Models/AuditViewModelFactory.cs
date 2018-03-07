using Cfb.CandidatePortal.PostElection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Audit.Models
{
    public sealed class AuditViewModelFactory
    {
        private static IDictionary<char, string> GetNamesFrom(IEnumerable<AuditReviewBase> source)
        {
            var names = new Dictionary<char, string>();
            foreach (var r in source)
            {
                names[r.CommitteeID] = r.CommitteeName;
            }
            return names;
        }

        public static AuditReviewTrackingViewModel StatementReviewsFrom(StatementReviews source, IDictionary<byte, string> messageIDs)
        {
            return source == null ? new AuditReviewTrackingViewModel() : new AuditReviewTrackingViewModel
            {
                Reviews = (from r in source.Reviews
                           group r by r.CommitteeID).ToDictionary(g => g.Key, g => g.Select(r => AuditReviewFrom(r, messageIDs.ContainsKey(r.Number) ? messageIDs[r.Number] : null, r.Statement))),
                CommiteeNames = GetNamesFrom(source.Reviews)
            };
        }

        public static AuditReviewTrackingViewModel ComplianceVisitsFrom(ComplianceVisits source, IDictionary<byte, string> messageIDs)
        {
            return source == null ? new AuditReviewTrackingViewModel() : new AuditReviewTrackingViewModel
            {
                ReviewNumberName = "Visit",
                Reviews = (from r in source.Visits
                           group r by r.CommitteeID).ToDictionary(g => g.Key, g => g.Select(r => AuditReviewFrom(r, messageIDs.ContainsKey(r.Number) ? messageIDs[r.Number] : null))),
                CommiteeNames = GetNamesFrom(source.Visits),
                IsComplianceVisits = true
            };
        }

        public static AuditReviewTrackingViewModel DoingBusinessReviewsFrom(DoingBusinessReviews source, IDictionary<byte, string> messageIDs)
        {
            return source == null ? new AuditReviewTrackingViewModel() : new AuditReviewTrackingViewModel
            {
                SentNamePrefix = "Review",
                Reviews = (from r in source.Reviews
                           group r by r.CommitteeID).ToDictionary(g => g.Key, g => g.Select(r => AuditReviewFrom(r, messageIDs.ContainsKey(r.Number) ? messageIDs[r.Number] : null, r.Statement))),
                CommiteeNames = GetNamesFrom(source.Reviews)
            };
        }

        public static AuditReviewViewModel AuditReviewFrom(AuditReviewBase source, string messageID, Statement statement = null)
        {
            return source == null ? new AuditReviewViewModel() : new AuditReviewViewModel
            {
                AppointmentDate = source.Date,
                ExtensionGrantedDate = source.ResponseDeadline == null ? null : source.ResponseDeadline.ExtensionDate,
                ExtensionsGranted = source.ResponseDeadline == null ? (ushort)0 : source.ResponseDeadline.ExtensionNumber,
                ID = statement == null ? source.Number.ToString() : statement.ToDetailString(),
                MessageID = messageID,
                OriginalDueDate = source.ResponseDeadline == null || !source.ResponseDeadline.ExtensionGranted ? null : (DateTime?)source.ResponseDeadline.OriginalDueDate,
                ResponseDueDate = source.ResponseDeadline == null ? null : (DateTime?)source.ResponseDeadline.Date,
                ResponseReceivedDate = source.ResponseReceivedDate,
                SentDate = source.SentDate
            };
        }

        public static PublicFundsTrackingViewModel PublicFundsPaymentTrackingFrom(PublicFundsHistory source)
        {
            return source == null ? new PublicFundsTrackingViewModel() : new PublicFundsTrackingViewModel
            {
                Payments = source.Select(p => PublicFundsPaymentFrom(p))
            };
        }

        private static PublicFundsViewModel PublicFundsPaymentFrom(PublicFundsDetermination source)
        {
            return source == null ? new PublicFundsViewModel() : new PublicFundsViewModel
            {
                Amount = source.PaymentIssued ? (decimal?)source.PaymentAmount : null,
                Date = source.Date,
                ElectionType = source.ElectionType,
                MessageID = source.MessageID,
                Method = source.PaymentIssued ? (PaymentMethod?)source.PaymentMethod : null
            };
        }

        public static ThresholdStatusViewModel ThresholdStatusFrom(ThresholdHistory source)
        {
            return source == null ? new ThresholdStatusViewModel() : new ThresholdStatusViewModel
            {
                Revisions = source.History.ToDictionary(k => k.Key, v => v.Value.Select(r => ThresholdRevisionFrom(r)))
            };
        }

        private static ThresholdRevisionViewModel ThresholdRevisionFrom(ThresholdRevision source)
        {
            return source == null ? new ThresholdRevisionViewModel() : new ThresholdRevisionViewModel
            {
                Amount = source.Funds,
                AmountRequired = source.IsUndetermined ? null : (decimal?)source.FundsRequired,
                Date = source.Date,
                Number = source.IsUndetermined ? null : (ushort?)source.Number,
                NumberRequired = source.IsUndetermined ? null : (ushort?)source.NumberRequired,
                OfficeSought = source.Office.Type,
                Statement = source.Statement.ToDetailString(),
                Version = source.Type
            };
        }

        public static PostElectionViewModel PostElectionFrom(AuditReportBase idr, AuditReportBase dar, AuditReportBase far, string agencyPhone, int darTollingDays, int farTollingDays, DateTime? darIssueDate, DateTime? farIssueDate, int farOffsetMonths, bool suspended)
        {
            var current = far ?? dar ?? idr; // auto-select most recent one
            var model = new PostElectionViewModel
            {
                AgencyPhoneNumber = agencyPhone,
                DarOriginalDeadline = darIssueDate,
                DarTollingDays = darTollingDays,
                DarTargetDeadline = darIssueDate.HasValue ? (DateTime?)darIssueDate.Value.AddDays(darTollingDays) : null,
                FarOriginalDeadline = farIssueDate,
                FarTollingDays = farTollingDays,
                FarTargetDeadline = farIssueDate.HasValue ? (DateTime?)farIssueDate.Value.AddMonths(farOffsetMonths).AddDays(darTollingDays + farTollingDays) : null,
                HasTrainingCompliance = farOffsetMonths != 0,
                TotalTollingDays = darTollingDays + farTollingDays,
                Suspended = suspended,
                Stage = current != null ? current.AuditReportType : AuditReportType.InitialDocumentationRequest
            };
            if (suspended)
            {
                model.CurrentStatus = PostElectionStatus.Suspended;
            }
            else if (far != null)
            {
                model.CurrentStatus = PostElectionStatus.FinalAuditComplete;
            }
            else if (current != null)
            {
                if (current.InadequateNotice != null)
                    current = current.InadequateNotice;
                if (current.ResponseReceived)
                {
                    model.CurrentStatus = PostElectionStatus.UnderReview;
                }
                else
                {
                    switch (current.AuditReportType)
                    {
                        case AuditReportType.DarInadequateResponse:
                        case AuditReportType.DarAdditionalInadequateResponse:
                            model.CurrentStatus = PostElectionStatus.PendingDarInadequate;
                            break;
                        case AuditReportType.IdrInadequateResponse:
                        case AuditReportType.IdrAdditionalInadequateResponse:
                            model.CurrentStatus = PostElectionStatus.PendingIdrInadequate;
                            break;
                        case AuditReportType.DraftAuditReport:
                            model.CurrentStatus = PostElectionStatus.PendingDar;
                            break;
                        case AuditReportType.InitialDocumentationRequest:
                            model.CurrentStatus = PostElectionStatus.PendingIdr;
                            break;
                    }
                }
            }
            return model;
        }

        public static PostElectionSummaryViewModel PostElectionSummaryFrom(AuditReportBase source, IDictionary<AuditReportType, string> messageIDs)
        {
            var inadequate = source != null ? source.InadequateNotice : null;
            var model = new PostElectionSummaryViewModel
            {
                ReportResponse = source == null ? null : PostElectionResponseFrom(source, messageIDs),
                InadequateResponse = inadequate == null ? null : PostElectionResponseFrom(inadequate, messageIDs)
            };
            return model;
        }

        public static PostElectionResponseViewModel NoFindingsResponse()
        {
            return new PostElectionResponseViewModel
            {
                HasNoFindings = true
            };
        }

        private static PostElectionResponseViewModel PostElectionResponseFrom(AuditReportBase source, IDictionary<AuditReportType, string> messageIDs)
        {
            return source == null ? new PostElectionResponseViewModel() : new PostElectionResponseViewModel
            {
                ReportTypeText = source.IsInadequateNotice ? "Notice" : source.AuditReportType == AuditReportType.InitialDocumentationRequest ? "Request" : "Report",
                SentDate = source.SentDate,
                SecondSentDate = source.SecondSentDate,
                DueDate = source.DueDate,
                ReceivedDate = source.ResponseReceivedDate,
                PostmarkedDate = source.ResponseSubmittedDate,
                MessageID = messageIDs != null && messageIDs.ContainsKey(source.AuditReportType) ? messageIDs[source.AuditReportType] : null,
                Overdue = source.DueDate.HasValue && DateTime.Today > source.DueDate.Value,
                ReportType = source.AuditReportType,
                IsInadequate = source.IsInadequateNotice
            };
        }

        private static TollingEventViewModel TollingEventFrom(ITollingEvent source, string messageID)
        {
            return source == null ? new TollingEventViewModel() : new TollingEventViewModel
            {
                Description = source.Description,
                DueDate = source.TollingDueDate,
                EndDate = source.TollingEndDate,
                EndReason = source.TollingEndReason,
                MessageID = messageID,
                StartDate = source.TollingStartDate,
                TollingDays = source.GetTollingDays()
            };
        }

        public static TollingEventsListViewModel TollingEventsListFrom(AuditReportBase source, IDictionary<int, string> messageIDs, AuditReportBase secondSource = null)
        {
            if (secondSource == null)
            {
                return source == null ? new TollingEventsListViewModel() : new TollingEventsListViewModel
                {
                    ReportType = source.AuditReportType,
                    Events = from t in source.TollingEvents
                             select TollingEventFrom(t, messageIDs.ContainsKey(t.TollingEventNumber) ? messageIDs[t.TollingEventNumber] : null)
                };
            }
            var allEvents = TollingEventsListFrom(source, messageIDs);
            allEvents.Events = allEvents.Events.Concat(TollingEventsListFrom(secondSource, messageIDs).Events);
            allEvents.ReportType = null;
            return allEvents;
        }
    }
}