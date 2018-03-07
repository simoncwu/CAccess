CREATE TABLE [dbo].[CmoMessages] (
    [CandidateId]       VARCHAR (5)    NOT NULL,
    [MessageId]         INT            NOT NULL,
    [ElectionCycle]     VARCHAR (5)    NOT NULL,
    [Title]             NVARCHAR (255) NOT NULL,
    [Body]              NTEXT          NOT NULL,
    [CreatorADUserName] VARCHAR (32)   NOT NULL,
    [OpenReceiptEmail]  NVARCHAR (256) NULL,
    [Category]          TINYINT        NULL,
    [PostDate]          DATETIME       NULL,
    [OpenUserName]      NVARCHAR (255) NULL,
    [OpenDate]          DATETIME       NULL,
    [ArchiveUserName]   NVARCHAR (255) NULL,
    [ArchiveDate]       DATETIME       NULL,
    [FollowUp]          BIT            NOT NULL,
    [FollowUpUserName]  NVARCHAR (255) NULL,
    [FollowUpDate]      DATETIME       NULL,
    [Version]           TIMESTAMP      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID for the targeted candidate recipient.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The unique message identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The relevant election cycle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'ElectionCycle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The message title.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The message body HTML.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'Body';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The username of the message creator.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'CreatorADUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The recipient address for open receipt e-mails.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'OpenReceiptEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The message category identifier.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'Category';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date when the message was sent.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'PostDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The username of the first user to open the message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'OpenUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date when the message was first opened.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'OpenDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The username of the user who archived the message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'ArchiveUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date when the message was archived.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'ArchiveDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Whether or not the message is flagged for follow-up.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'FollowUp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The username of the user who last set the message flag.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'FollowUpUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date when the message flag was last set.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'FollowUpDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The version of the message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoMessages', @level2type = N'COLUMN', @level2name = N'Version';

