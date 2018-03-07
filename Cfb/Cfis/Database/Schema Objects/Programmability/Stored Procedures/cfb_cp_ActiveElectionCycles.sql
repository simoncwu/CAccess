IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ActiveElectionCycles')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ActiveElectionCycles]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ActiveElectionCycles]
(
	@candidateID char(5)
)

AS

SET NOCOUNT ON;

SELECT	[a].[Election_Cycle]
FROM	[Active_Candidate] AS [a]
		INNER JOIN [Election] AS [e]
		ON	[a].[Election_Cycle] = e.Election_Cycle
WHERE	[a].[Cand_ID] = @candidateID AND
		[a].[Deleted_Ind] <> 'Y' AND
		[e].[Election_Cycle] NOT LIKE '%X' AND
		[e].[Deleted_Ind] <> 'Y' AND
		([e].[Transition_Inaugural_Ind] <> 'Y' OR [e].[Year] >= '2013')
ORDER BY  [Year] DESC, [Election_Date] DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_ActiveElectionCycles] TO caccess

GO

