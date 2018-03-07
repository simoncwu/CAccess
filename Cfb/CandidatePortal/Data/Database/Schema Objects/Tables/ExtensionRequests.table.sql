CREATE TABLE [dbo].[ExtensionRequests] (
    [CandidateID]      VARCHAR (5) NOT NULL,
    [ElectionCycle]    VARCHAR (5) NOT NULL,
    [ExtensionType]    TINYINT     NOT NULL,
    [ReviewNumber]     TINYINT     NOT NULL,
    [Iteration]        TINYINT     NOT NULL,
    [Date]             DATETIME    NOT NULL,
    [RequestedDueDate] DATETIME    NOT NULL,
    [Reason]           NTEXT       NOT NULL,
    [Version]          TIMESTAMP   NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The candidate ID of the extension requestor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'CandidateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The relevant election cycle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'ElectionCycle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The code indicating the type of extension requested.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'ExtensionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The number of the relevant audit review.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'ReviewNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The extension request iteration.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'Iteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The date of the extension request.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The requested response due date.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'RequestedDueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reason given for the extension request.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'Reason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The version of the message.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ExtensionRequests', @level2type = N'COLUMN', @level2name = N'Version';

