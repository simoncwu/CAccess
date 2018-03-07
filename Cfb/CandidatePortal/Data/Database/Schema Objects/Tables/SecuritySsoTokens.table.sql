CREATE TABLE [dbo].[SecuritySsoTokens] (
    [TokenId]       UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] TINYINT          NOT NULL,
    [UserName]      NVARCHAR (256)   NOT NULL,
    [Created]       DATETIME         NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier for the temporary application token.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoTokens', @level2type = N'COLUMN', @level2name = N'TokenId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An identifier for the application requesting user authentication.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoTokens', @level2type = N'COLUMN', @level2name = N'ApplicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A username for the user attempting authentication.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoTokens', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A creation date/time for the token.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoTokens', @level2type = N'COLUMN', @level2name = N'Created';

