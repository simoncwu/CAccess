CREATE TABLE [dbo].[SecurityUserProfiles] (
    [UserName]               NVARCHAR (256) NOT NULL,
    [DisplayName]            VARCHAR (256)  NULL,
    [CandidateId]            VARCHAR (5)    NOT NULL,
    [PasswordExpired]        BIT            NOT NULL,
    [CfisType]               TINYINT        NOT NULL,
    [CfisCommitteeID]        VARCHAR (1)    NULL,
    [CfisCommitteeContactID] VARCHAR (5)    NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A C-Access account username.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A friendly display name for the C-Access account.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'DisplayName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An identifier for the candidate represented by the C-Access account.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'CandidateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A type of CFIS contact that maps to the account.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'CfisType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of a committee context for the CFIS contact that maps to the account.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'CfisCommitteeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An election cycle or liaison ID for the CFIS committee contact that maps to the account.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserProfiles', @level2type = N'COLUMN', @level2name = N'CfisCommitteeContactID';

