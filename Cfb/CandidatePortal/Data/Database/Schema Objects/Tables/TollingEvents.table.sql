CREATE TABLE [dbo].[TollingEvents] (
    [EventId]     TINYINT        IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (6)    NOT NULL,
    [Description] NVARCHAR (256) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingEvents', @level2type = N'COLUMN', @level2name = N'EventId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A code representing the event in CFIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingEvents', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly description of the event.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingEvents', @level2type = N'COLUMN', @level2name = N'Description';

