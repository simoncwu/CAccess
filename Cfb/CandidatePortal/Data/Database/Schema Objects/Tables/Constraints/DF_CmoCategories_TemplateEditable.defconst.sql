ALTER TABLE [dbo].[CmoCategories]
    ADD CONSTRAINT [DF_CmoCategories_TemplateEditable] DEFAULT ((0)) FOR [TemplateEditable];

