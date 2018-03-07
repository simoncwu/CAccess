CREATE TABLE [dbo].[CmoPostElectionRequests] (
    [CandidateId]   VARCHAR (5) NOT NULL,
    [MessageId]     INT         NOT NULL,
    [Repost]        BIT         DEFAULT ((0)) NOT NULL,
    [SecondRequest] BIT         DEFAULT ((0)) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the message for the request.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPostElectionRequests', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of the message for the request.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPostElectionRequests', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Whether or not the request is a repost.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPostElectionRequests', @level2type = N'COLUMN', @level2name = N'Repost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Whether or not the request is a second request.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPostElectionRequests', @level2type = N'COLUMN', @level2name = N'SecondRequest';

