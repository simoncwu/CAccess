CREATE TABLE [dbo].[SecurityGroupMembership] (
    [UserName] NVARCHAR (256) NOT NULL,
    [GroupId]  TINYINT        NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A C-Access account username.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityGroupMembership', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The ID of a role assigned to the user.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityGroupMembership', @level2type = N'COLUMN', @level2name = N'GroupId';

