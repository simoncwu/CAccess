﻿ALTER TABLE [dbo].[TollingLetters]
    ADD CONSTRAINT [PK_TollingLetters] PRIMARY KEY CLUSTERED ([LetterId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key: Letter ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingLetters', @level2type = N'CONSTRAINT', @level2name = N'PK_TollingLetters';

