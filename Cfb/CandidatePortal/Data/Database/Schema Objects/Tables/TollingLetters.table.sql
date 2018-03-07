CREATE TABLE [dbo].[TollingLetters] (
    [LetterId]    TINYINT        IDENTITY (1, 1) NOT NULL,
    [SourceId]    TINYINT        NOT NULL,
    [EventId]     TINYINT        NOT NULL,
    [TypeId]      TINYINT        NOT NULL,
    [Description] NVARCHAR (256) NULL,
    [Title]       NVARCHAR (32)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A source code for the tolling letter.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'COLUMN', @level2name = N'SourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An event code for the tolling letter.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'COLUMN', @level2name = N'EventId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A type code for the tolling letter.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'COLUMN', @level2name = N'TypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly description of the tolling letter.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A brief display name for the tolling letter.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'COLUMN', @level2name = N'Title';

