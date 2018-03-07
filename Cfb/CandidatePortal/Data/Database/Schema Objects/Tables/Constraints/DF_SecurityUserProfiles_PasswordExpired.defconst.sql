ALTER TABLE [dbo].[SecurityUserProfiles]
    ADD CONSTRAINT [DF_SecurityUserProfiles_PasswordExpired] DEFAULT ((1)) FOR [PasswordExpired];

