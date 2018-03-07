CREATE TABLE [dbo].[TollingSources] (
    [SourceId]    TINYINT        IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (6)    NOT NULL,
    [Description] NVARCHAR (256) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingSources', @level2type = N'COLUMN', @level2name = N'SourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A code representing the source in CFIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingSources', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly description of the source.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingSources', @level2type = N'COLUMN', @level2name = N'Description';

