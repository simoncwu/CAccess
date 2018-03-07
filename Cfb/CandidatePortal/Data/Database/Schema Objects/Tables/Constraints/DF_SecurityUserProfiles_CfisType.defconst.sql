ALTER TABLE [dbo].[SecurityUserProfiles]
    ADD CONSTRAINT [DF_SecurityUserProfiles_CfisType] DEFAULT ((0)) FOR [CfisType];

