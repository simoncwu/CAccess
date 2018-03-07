CREATE TABLE [dbo].[CmoTollingLetters] (
    [CandidateId] VARCHAR (5) NOT NULL,
    [MessageId]   INT         NOT NULL,
    [EventNumber] INT         NOT NULL,
    [LetterId]    TINYINT     NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the tolling letter message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoTollingLetters', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the tolling letter message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoTollingLetters', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The tolling event number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoTollingLetters', @level2type = N'COLUMN', @level2name = N'EventNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the tolling letter designation.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoTollingLetters', @level2type = N'COLUMN', @level2name = N'LetterId';

