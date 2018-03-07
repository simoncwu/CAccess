ALTER TABLE [dbo].[SecurityUserElectionCycles]
    ADD CONSTRAINT [FK_SecurityUserElectionCycles_SecurityUserProfiles] FOREIGN KEY ([UserName]) REFERENCES [dbo].[SecurityUserProfiles] ([UserName]) ON DELETE CASCADE ON UPDATE CASCADE;

