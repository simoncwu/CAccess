﻿ALTER TABLE [dbo].[CmoPayments]
    ADD CONSTRAINT [FK_CmoPayments_CmoMessages] FOREIGN KEY ([CandidateId], [MessageId]) REFERENCES [dbo].[CmoMessages] ([CandidateId], [MessageId]) ON DELETE CASCADE ON UPDATE CASCADE;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key: CandidateId, MessageId from CmoMessages', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CmoPayments', @level2type = N'CONSTRAINT', @level2name = N'FK_CmoPayments_CmoMessages';
