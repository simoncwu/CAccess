﻿ALTER TABLE [dbo].[CmoTollingLetters]
    ADD CONSTRAINT [FK_CmoTollingLetters_TollingLetters] FOREIGN KEY ([LetterId]) REFERENCES [dbo].[TollingLetters] ([LetterId]) ON DELETE NO ACTION ON UPDATE CASCADE;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key: LetterId from TollingLetters', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoTollingLetters', @level2type = N'CONSTRAINT', @level2name = N'FK_CmoTollingLetters_TollingLetters';
