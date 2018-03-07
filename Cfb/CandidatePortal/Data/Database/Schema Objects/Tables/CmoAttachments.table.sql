CREATE TABLE [dbo].[CmoAttachments] (
    [CandidateId]    VARCHAR (5)    NOT NULL,
    [MessageId]      INT            NOT NULL,
    [AttachmentId]   TINYINT        NOT NULL,
    [AttachmentName] NVARCHAR (256) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the attachment recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAttachments', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the message this attachment is for.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAttachments', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The unique attachment identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAttachments', @level2type = N'COLUMN', @level2name = N'AttachmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The name to display as the attachment file name.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAttachments', @level2type = N'COLUMN', @level2name = N'AttachmentName';

