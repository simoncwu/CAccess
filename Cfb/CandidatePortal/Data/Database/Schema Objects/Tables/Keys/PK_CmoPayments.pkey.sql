ALTER TABLE [dbo].[CmoPayments]
    ADD CONSTRAINT [PK_CmoPayments] PRIMARY KEY CLUSTERED ([CandidateId] ASC, [MessageId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key: Candidate ID, MessageId', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPayments', @level2type = N'CONSTRAINT', @level2name = N'PK_CmoPayments';

