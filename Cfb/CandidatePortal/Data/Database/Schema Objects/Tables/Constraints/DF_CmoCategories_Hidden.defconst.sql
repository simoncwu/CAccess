ALTER TABLE [dbo].[CmoCategories]
    ADD CONSTRAINT [DF_CmoCategories_Hidden] DEFAULT ((0)) FOR [Hidden];

