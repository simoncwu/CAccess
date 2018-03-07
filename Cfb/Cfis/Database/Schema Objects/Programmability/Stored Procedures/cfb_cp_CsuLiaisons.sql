IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_CsuLiaisons')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_CsuLiaisons]
	END

GO

CREATE Procedure [dbo].[cfb_cp_CsuLiaisons]
(
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	CAST([Code] AS tinyint) AS [ID],
		[Update_Date] AS [LastUpdated],
		[Long_Description] AS [Name]
FROM	[Code]
WHERE	[Election_Cycle] = @electionCycle AND
		[Category] = 'CSUID'

GO

GRANT EXEC ON [dbo].[cfb_cp_CsuLiaisons] TO cfis

GO

