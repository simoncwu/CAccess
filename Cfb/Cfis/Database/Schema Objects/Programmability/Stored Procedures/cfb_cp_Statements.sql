IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_Statements')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_Statements]
	END

GO

CREATE Procedure [dbo].[cfb_cp_Statements]
(
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Election_Cycle, Statement_No, Start_Date, End_Date, Due_Date, Pri_Gen_Ind, Post_Opt_In_Required_Statement, Catch_Up_Statement_Ind, 
		Deferable_Ind
FROM	Statement_Date
WHERE	Election_Cycle = @electionCycle

GO

GRANT EXEC ON [dbo].[cfb_cp_Statements] TO cfis

GO

