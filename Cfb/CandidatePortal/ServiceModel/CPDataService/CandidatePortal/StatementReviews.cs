using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.StatementReviewTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        StatementReviews GetPrincipalStatementReviews(string candidateID, string electionCycle);

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        StatementReviews GetStatementReviews(string candidateID, string electionCycle);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        public StatementReviews GetPrincipalStatementReviews(string candidateID, string electionCycle)
        {
            using (StatementReviewTds ds = new StatementReviewTds())
            {
                using (StatementReviewsTableAdapter ta = new StatementReviewsTableAdapter())
                {
                    ta.FillPrincipalBy(ds.StatementReviews, candidateID, electionCycle);
                }
                return ReadStatmentReviews(ds, electionCycle);
            }
        }

        /// <summary>
        /// Retrieves a collection of all completed statement reviews on record for the principal committee of the specified candidate and election cycle.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose statement reviews are to be retrieved.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <returns>A collection of all completed statement reviews for the specified candidate and election cycle.</returns>
        public StatementReviews GetStatementReviews(string candidateID, string electionCycle)
        {
            using (StatementReviewTds ds = new StatementReviewTds())
            {
                using (StatementReviewsTableAdapter ta = new StatementReviewsTableAdapter())
                {
                    ta.Fill(ds.StatementReviews, candidateID, electionCycle);
                }
                return ReadStatmentReviews(ds, electionCycle);
            }
        }

        /// <summary>
        /// Retrieves a collection of all statement reviews from a <see cref="StatementReviewTds"/> dataset.
        /// </summary>
        /// <param name="ds">The dataset to read.</param>
        /// <param name="electionCycle">The election cycle pertaining to the dataset.</param>
        /// <returns>A collection of all statement reviews in the dataset.</returns>
        private StatementReviews ReadStatmentReviews(StatementReviewTds ds, string electionCycle)
        {
            Election election = GetElections(CPProviders.SettingsProvider.MinimumElectionCycle)[electionCycle];
            StatementReviews sr = new StatementReviews();
            if (election == null)
                return sr;
            var s = this.GetStatements(electionCycle);
            foreach (StatementReviewTds.StatementReviewsRow row in ds.StatementReviews.Rows)
            {
                //fetch applicable dates and create a new StatementReview object as appropriate
                byte number = row.StatementNumber;
                if (!object.Equals(s, null) && s.ContainsKey(number))
                {
                    StatementReview review = new StatementReview(row.ElectionCycle.Trim(), ParseCommitteeID(row.CommitteeID), s[number])
                    {
                        StartDate = row.IsStartDateNull() ? DateTime.MinValue : row.StartDate,
                        EndDate = row.IsCompletionDateNull() ? DateTime.MinValue : row.CompletionDate,
                        LastUpdated = row.LastUpdated,
                        SentDate = row.IsSentDateNull() ? null : row.SentDate as DateTime?,
                        CommitteeName = row.CommitteeName.Trim()
                    };
                    // response due only if due date is after letter sent date
                    if (!row.IsResponseDueDateNull() && review.SentDate.HasValue && (review.SentDate.Value < row.ResponseDueDate))
                    {
                        SRResponseDeadline deadline = new SRResponseDeadline(row.ResponseDueDate.Date, review);
                        if (!row.IsResponseReceivedDateNull())
                            deadline.ResponseReceivedDate = row.ResponseReceivedDate;
                        if (!row.IsExtensionDueDateNull())
                            deadline.GrantExtension(row.ExtensionDueDate.Date, null, Convert.ToUInt16(row.ExtensionsCount));
                        review.ResponseDeadline = deadline;
                        sr.ResponseDeadlines.Add(deadline);
                    }
                    sr.Reviews.Add(review);
                }
            }
            return sr;
        }
    }
}
