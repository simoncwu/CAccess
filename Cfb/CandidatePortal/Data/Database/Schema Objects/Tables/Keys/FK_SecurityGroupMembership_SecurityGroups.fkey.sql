ALTER TABLE [dbo].[SecurityGroupMembership]
    ADD CONSTRAINT [FK_SecurityGroupMembership_SecurityGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SecurityGroups] ([GroupId]) ON DELETE NO ACTION ON UPDATE CASCADE;

