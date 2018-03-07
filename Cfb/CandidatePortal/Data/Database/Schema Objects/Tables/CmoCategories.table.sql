CREATE TABLE [dbo].[CmoCategories] (
    [CategoryId]       TINYINT        IDENTITY (1, 1) NOT NULL,
    [CategoryName]     NVARCHAR (256) NOT NULL,
    [Description]      NVARCHAR (256) NULL,
    [Hidden]           BIT            NOT NULL,
    [TemplateTitle]    NVARCHAR (256) NULL,
    [TemplateBody]     NTEXT          NULL,
    [TemplateEditable] BIT            NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly display name.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'CategoryName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly description of the category.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether or not the category is hidden from users.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'Hidden';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A templated subject for the category''s messages.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'TemplateTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A templated body for the category''s messages.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'TemplateBody';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether or not the template text can be overridden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoCategories', @level2type = N'COLUMN', @level2name = N'TemplateEditable';

