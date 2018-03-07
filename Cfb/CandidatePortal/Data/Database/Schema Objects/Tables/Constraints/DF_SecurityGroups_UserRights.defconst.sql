ALTER TABLE [dbo].[SecurityGroups]
    ADD CONSTRAINT [DF_SecurityGroups_UserRights] DEFAULT ((0)) FOR [UserRights];

