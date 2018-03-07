CREATE TABLE [dbo].[CmoSettings] (
    [CandidateId]     VARCHAR (5)    NOT NULL,
    [IsPaperless]     BIT            NOT NULL,
    [UpdaterUserName] NVARCHAR (256) NOT NULL,
    [UpdatedDate]     DATETIME       NOT NULL,
    [Version]         TIMESTAMP      NOT NULL
);

