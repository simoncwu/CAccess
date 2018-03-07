using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.DoingBusinessReviewTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves a collection of all completed Doing Business reviews on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Doing Business reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed Doing Business reviews for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        DoingBusinessReviews GetDoingBusinessReviews(string candidateID, string electionCycle);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves a collection of all completed Doing Business reviews on record for the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose Doing Business reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed Doing Business reviews for the specified candidate and election cycle.</returns>
        public DoingBusinessReviews GetDoingBusinessReviews(string candidateID, string electionCycle)
        {
            using (DoingBusinessReviewTds ds = new DoingBusinessReviewTds())
            {
                using (DoingBusinessReviewsTableAdapter ta = new DoingBusinessReviewsTableAdapter())
                {
                    ta.Fill(ds.DoingBusinessReviews, candidateID, electionCycle);
                }
                Election election = GetElections(CPProviders.SettingsProvider.MinimumElectionCycle)[electionCycle];
                DoingBusinessReviews dbr = new DoingBusinessReviews();
                if (election == null)
                    return dbr;
                var s = this.GetStatements(electionCycle);
                foreach (DoingBusinessReviewTds.DoingBusinessReviewsRow row in ds.DoingBusinessReviews.Rows)
                {
                    byte number = row.StatementNumber;
                    if (!object.Equals(s, null) && s.ContainsKey(number))
                    {
                        DoingBusinessReview review = new DoingBusinessReview(row.ElectionCycle.Trim(), ParseCommitteeID(row.CommitteeID), s[number])
                        {
                            StartDate = row.IsStartDateNull() ? DateTime.MinValue : row.StartDate,
                            EndDate = row.IsCompletionDateNull() ? DateTime.MinValue : row.CompletionDate,
                            CommitteeName = row.CommitteeName.Trim(),
                            LastUpdated = row.LastUpdated,
                            SentDate = row.IsSentDateNull() ? null : row.SentDate as DateTime?
                        };
                        // response due if due date is after letter sent date
                        if (!row.IsResponseDueDateNull() && review.SentDate.HasValue && (review.SentDate.Value < row.ResponseDueDate))
                        {
                            DbrResponseDeadline deadline = new DbrResponseDeadline(row.ResponseDueDate.Date, review);
                            if (!row.IsResponseReceivedDateNull())
                                deadline.ResponseReceivedDate = row.ResponseReceivedDate;
                            review.ResponseDeadline = deadline;
                            dbr.ResponseDeadlines.Add(deadline);
                        }
                        dbr.Reviews.Add(review);
                    }
                }
                return dbr;
            }
        }
    }
}
