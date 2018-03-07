CREATE TABLE [dbo].[SecuritySsoApplications] (
    [ApplicationId]   TINYINT        IDENTITY (1, 1) NOT NULL,
    [ApplicationName] NVARCHAR (256) NOT NULL,
    [UserRights]      INT            NOT NULL,
    [Description]     NVARCHAR (256) NULL,
    [Url]             NVARCHAR (256) NULL,
    [RedirectUrl]     NVARCHAR (256) NULL,
    [TokenParameter]  NVARCHAR (8)   NULL,
    [LoginPagePath]   NVARCHAR (256) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A unique identifier for the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'ApplicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A user-friendly name for the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'ApplicationName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A user rights value that represents the user permissions necessary to access this application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'UserRights';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A user-friendly description of the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A URL indicating the address of the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'Url';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A URL indicating the post-login redirection address of the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'RedirectUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A name for the query string parameter that will hold the token value when passed back to the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'TokenParameter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An absolute path to a custom login page for the application.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SecuritySsoApplications', @level2type = N'COLUMN', @level2name = N'LoginPagePath';

