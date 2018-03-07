IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionSuspension')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionSuspension]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionSuspension]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1

SELECT	[Suspended_Date] AS [SuspensionDate],
		[Suspended_Reason_CD] AS [SuspensionReasonCode],
		[Long_Description] AS [SuspenderName]
FROM	[Final_Audit_Tracking] AS [f]
		LEFT OUTER JOIN Code AS [c]
		ON	[Suspended_Rev_Ind] = 'Y' AND
			[f].[Election_Cycle] = [c].[Election_Cycle] AND
			[c].[Election_Cycle] = @electionCycle AND
			[Category] = 'SUSPER' AND
			[Suspended_By_CD] = [Code]
WHERE	[f].[Cand_ID] = @candidateID AND
		[f].[Election_Cycle] = @electionCycle
GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionSuspension] TO cfis

GO

