ALTER TABLE [dbo].[TollingLetters]
    ADD CONSTRAINT [FK_TollingLetters_TollingEvents] FOREIGN KEY ([EventId]) REFERENCES [dbo].[TollingEvents] ([EventId]) ON DELETE NO ACTION ON UPDATE CASCADE;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key: EventId from TollingEvents', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'CONSTRAINT', @level2name = N'FK_TollingLetters_TollingEvents';

