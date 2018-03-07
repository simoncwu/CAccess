IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ElectionCycles')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ElectionCycles]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ElectionCycles]
(
	@cutoff char(5)
)

AS

SET NOCOUNT ON;

SELECT	[Election_Cycle] AS [ElectionCycle], 
		[Year] AS [Year], 
		[Election_Date] AS [ElectionDate], 
		CAST(CASE [Transition_Inaugural_Ind] WHEN 'Y' THEN 1 ELSE 0 END AS bit) AS [IsTIE]
FROM	[Election] 
WHERE	([Election_Cycle] >= @cutoff) AND
		([Election_Cycle] NOT LIKE '%X') AND
		([Deleted_Ind] <> 'Y') AND
		([Transition_Inaugural_Ind] <> 'Y' OR [Year] >= '2013')
ORDER BY [Year] DESC, [Election_Date] DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_ElectionCycles] TO caccess

GO

