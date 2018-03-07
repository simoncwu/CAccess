CREATE TABLE [dbo].[SecurityUserElectionCycles] (
    [UserName]      NVARCHAR (256) NOT NULL,
    [ElectionCycle] VARCHAR (5)    NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A C-Access account username.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserElectionCycles', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An election cycle which a C-Access user account is authorized to access.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityUserElectionCycles', @level2type = N'COLUMN', @level2name = N'ElectionCycle';

