ALTER TABLE [dbo].[CmoMessages]
    ADD CONSTRAINT [CK_CmoMessages_EnforceLifeCycle] CHECK ([PostDate] IS NULL AND [OpenDate] IS NULL AND [ArchiveDate] IS NULL OR [PostDate] IS NOT NULL AND ([ArchiveDate] IS NULL AND ([OpenDate] IS NULL OR [OpenDate] IS NOT NULL AND [PostDate]<[OpenDate]) OR [OpenDate] IS NOT NULL AND [ArchiveDate] IS NOT NULL AND [PostDate]<[OpenDate] AND [OpenDate]<[ArchiveDate]));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enforces the life cycle of a message (draft, posted, opened, archived)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'CONSTRAINT', @level2name = N'CK_CmoMessages_EnforceLifeCycle';

