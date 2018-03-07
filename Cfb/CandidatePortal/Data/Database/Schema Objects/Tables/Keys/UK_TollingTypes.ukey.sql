ALTER TABLE [dbo].[TollingTypes]
    ADD CONSTRAINT [UK_TollingTypes] UNIQUE NONCLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique key: Code', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingTypes', @level2type = N'CONSTRAINT', @level2name = N'UK_TollingTypes';

