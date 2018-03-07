CREATE NONCLUSTERED INDEX [IX_CmoAuditReviews_CandidateIdReviewNumber]
    ON [dbo].[CmoAuditReviews]([CandidateId] ASC, [ReviewNumber] DESC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Index for searches on Candidate ID, Review Number', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAuditReviews', @level2type = N'INDEX', @level2name = N'IX_CmoAuditReviews_CandidateIdReviewNumber';

