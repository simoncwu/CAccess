ALTER TABLE [dbo].[SecuritySsoTokens]
    ADD CONSTRAINT [FK_SecuritySsoTokens_SecuritySsoApplications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[SecuritySsoApplications] ([ApplicationId]) ON DELETE CASCADE ON UPDATE CASCADE;

