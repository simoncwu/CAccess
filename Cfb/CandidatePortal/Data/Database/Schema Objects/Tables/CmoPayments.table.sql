CREATE TABLE [dbo].[CmoPayments] (
    [CandidateId] VARCHAR (5) NOT NULL,
    [MessageId]   INT         NOT NULL,
    [Run]         TINYINT     NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the payment letter message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPayments', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the payment letter message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPayments', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The payment run number.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPayments', @level2type = N'COLUMN', @level2name = N'Run';

