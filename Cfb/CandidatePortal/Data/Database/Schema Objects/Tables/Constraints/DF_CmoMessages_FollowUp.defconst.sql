ALTER TABLE [dbo].[CmoMessages]
    ADD CONSTRAINT [DF_CmoMessages_FollowUp] DEFAULT ((0)) FOR [FollowUp];

