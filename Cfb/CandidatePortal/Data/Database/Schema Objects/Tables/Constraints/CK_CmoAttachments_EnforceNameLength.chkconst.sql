ALTER TABLE [dbo].[CmoAttachments]
    ADD CONSTRAINT [CK_CmoAttachments_EnforceNameLength] CHECK (len([AttachmentName])>(0));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enforces non-empty attachment names.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAttachments', @level2type = N'CONSTRAINT', @level2name = N'CK_CmoAttachments_EnforceNameLength';

