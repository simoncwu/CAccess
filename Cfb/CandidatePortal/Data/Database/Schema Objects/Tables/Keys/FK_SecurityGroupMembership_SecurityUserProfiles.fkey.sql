ALTER TABLE [dbo].[SecurityGroupMembership]
    ADD CONSTRAINT [FK_SecurityGroupMembership_SecurityUserProfiles] FOREIGN KEY ([UserName]) REFERENCES [dbo].[SecurityUserProfiles] ([UserName]) ON DELETE CASCADE ON UPDATE CASCADE;

