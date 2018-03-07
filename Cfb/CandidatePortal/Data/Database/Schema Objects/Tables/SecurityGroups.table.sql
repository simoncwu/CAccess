CREATE TABLE [dbo].[SecurityGroups] (
    [GroupId]    TINYINT        IDENTITY (1, 1) NOT NULL,
    [GroupName]  NVARCHAR (256) NOT NULL,
    [UserRights] INT            NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier for the group.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityGroups', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique name for the group.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityGroups', @level2type = N'COLUMN', @level2name = N'GroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A numeric equivalent of a bit mask that defines the rights granted to the group.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecurityGroups', @level2type = N'COLUMN', @level2name = N'UserRights';

