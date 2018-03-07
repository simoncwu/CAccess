CREATE NONCLUSTERED INDEX [IX_CmoPostElectionRequests_CandidateIdRepost]
    ON [dbo].[CmoPostElectionRequests]([CandidateId] ASC, [Repost] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Index for searches on Candidate ID, Repost', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPostElectionRequests', @level2type = N'INDEX', @level2name = N'IX_CmoPostElectionRequests_CandidateIdRepost';

