using Cfb.CandidatePortal.SubmissionDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cfb.CandidatePortal.Web.MvcApplication.Areas.Filings.Models
{
    public sealed class FilingsViewModelFactory
    {
        public static DocumentStatusListViewModel DocumentStatusListFrom<T>(SubmissionHistory<T> source) where T : SubmissionDocument
        {
            return new DocumentStatusListViewModel
            {
                Documents = from s in source.Documents
                            select DocumentStatusFrom(s)
            };
        }

        private static DocumentStatusViewModel DocumentStatusFrom<T>(T source) where T : SubmissionDocument
        {
            return source == null ? new DocumentStatusViewModel() : new DocumentStatusViewModel
            {
                DeliveryType = source.DeliveryType,
                PageCount = source.PageCount,
                PostmarkDate = source.PostmarkDate,
                ReceivedDate = source.ReceivedDate.HasValue ? source.ReceivedDate.Value : DateTime.MinValue,
                Status = CPConvert.ToString(source.Status),
                StatusDate = source.StatusDate,
                SubmissionType = source.SubmissionType
            };
        }

        public static PrimaryGeneralStatusListViewModel PrimaryGeneralStatusListFrom<T>(SubmissionHistory<T> primarySource, SubmissionHistory<T> generalSource) where T : SubmissionDocument
        {
            return new PrimaryGeneralStatusListViewModel
            {
                PrimaryElection = DocumentStatusListFrom(primarySource).Documents,
                GeneralElection = DocumentStatusListFrom(generalSource).Documents
            };
        }

        private static DisclosureStatementViewModel DisclosureStatementFrom(DisclosureStatement source)
        {
            return source == null ? new DisclosureStatementViewModel() : new DisclosureStatementViewModel
            {
                DeliveryType = source.DeliveryType,
                PageCount = source.PageCount,
                PostmarkDate = source.PostmarkDate,
                ReceivedDate = source.ReceivedDate.HasValue ? source.ReceivedDate.Value : DateTime.MinValue,
                Status = CPConvert.ToString(source.Status),
                StatusDate = source.StatusDate,
                SubmissionType = source.SubmissionType,
                IsSmallCampaign = source.SmallCampaign,
                Deferred = source.DeferredFiling,
                BackupDeliveryType = source.BackupDeliveryType,
                BackupPostmarkDate = source.BackupPostmarkDate,
                BackupReceivedDate = source.BackupReceivedDate,
                DataFormat = GetDataFormat(source)
            };
        }

        public static DisclosureStatementListViewModel DisclosureStatementListFrom(DisclosureStatementHistories source, char committeeID, byte statementNumber)
        {
            var model = new DisclosureStatementListViewModel
            {
                CommitteeID = committeeID,
                StatementNumber = statementNumber
            };
            if (source != null)
            {
                var committees = source.CommitteeNames;
                model.Committees = committees.Select(c => new SelectListItem { Value = c.Key.ToString(), Text = c.Value });
                if (committees.ContainsKey(committeeID))
                {
                    var statements = source.CommitteeStatements[committeeID];
                    model.Statements = statements.Select(s => new SelectListItem { Value = s.Key.ToString(), Text = s.Value.ToDetailString() });
                    var key = DisclosureStatementHistories.ToKey(committeeID, statementNumber);
                    if (source.Submissions.ContainsKey(key))
                        model.Documents = source.Submissions[key].Documents.Select(d => DisclosureStatementFrom(d));
                }
            }
            return model;
        }

        private static string GetDataFormat(DisclosureStatement statement)
        {
            if (statement == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (SubmissionFormat f in Enum.GetValues(typeof(SubmissionFormat)))
            {
                if (f == SubmissionFormat.None)
                    continue;
                if (statement.SubmissionFormats.HasFlag(f))
                {
                    sb.AppendFormat(f.ToString<SubmissionFormat>());
                    sb.Append(", ");
                }
            }
            return sb.ToString().Trim().Trim(',');
        }
    }
}