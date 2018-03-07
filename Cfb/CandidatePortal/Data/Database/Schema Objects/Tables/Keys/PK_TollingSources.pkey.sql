ALTER TABLE [dbo].[TollingSources]
    ADD CONSTRAINT [PK_TollingSources] PRIMARY KEY CLUSTERED ([SourceId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key: Source ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TollingSources', @level2type = N'CONSTRAINT', @level2name = N'PK_TollingSources';

