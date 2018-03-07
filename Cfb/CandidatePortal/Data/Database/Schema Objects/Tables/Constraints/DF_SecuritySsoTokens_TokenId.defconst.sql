ALTER TABLE [dbo].[SecuritySsoTokens]
    ADD CONSTRAINT [DF_SecuritySsoTokens_TokenId] DEFAULT (newid()) FOR [TokenId];

