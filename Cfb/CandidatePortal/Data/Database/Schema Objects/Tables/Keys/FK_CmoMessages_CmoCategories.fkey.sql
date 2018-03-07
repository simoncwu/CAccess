ALTER TABLE [dbo].[CmoMessages]
    ADD CONSTRAINT [FK_CmoMessages_CmoCategories] FOREIGN KEY ([Category]) REFERENCES [dbo].[CmoCategories] ([CategoryId]) ON DELETE NO ACTION ON UPDATE CASCADE;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key: CategoryId from CmoCategories', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'CONSTRAINT', @level2name = N'FK_CmoMessages_CmoCategories';

