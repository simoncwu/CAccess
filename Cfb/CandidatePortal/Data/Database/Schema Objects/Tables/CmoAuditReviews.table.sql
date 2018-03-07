CREATE TABLE [dbo].[CmoAuditReviews] (
    [CandidateId]  VARCHAR (5) NOT NULL,
    [MessageId]    INT         NOT NULL,
    [ReviewNumber] TINYINT     NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the audit review message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAuditReviews', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the audit review message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAuditReviews', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The audit review number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoAuditReviews', @level2type = N'COLUMN', @level2name = N'ReviewNumber';

