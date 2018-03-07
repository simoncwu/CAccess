ALTER TABLE [dbo].[SecuritySsoTokens]
    ADD CONSTRAINT [DF_SecuritySsoTokens_Created] DEFAULT (getdate()) FOR [Created];

