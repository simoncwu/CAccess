ALTER TABLE [dbo].[ExtensionRequests]
    ADD CONSTRAINT [PK_ExtensionRequests] PRIMARY KEY CLUSTERED ([CandidateID] ASC, [ElectionCycle] ASC, [ExtensionType] ASC, [ReviewNumber] ASC, [Iteration] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key: Candidate ID, Election Cycle, Extension Type, Review Number, Iteration', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'CONSTRAINT', @level2name = N'PK_ExtensionRequests';

