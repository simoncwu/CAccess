ALTER TABLE [dbo].[TollingLetters]
    ADD CONSTRAINT [FK_TollingLetters_TollingTypes] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TollingTypes] ([TypeId]) ON DELETE NO ACTION ON UPDATE CASCADE;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key: TypeId from TollingTypes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'CONSTRAINT', @level2name = N'FK_TollingLetters_TollingTypes';

