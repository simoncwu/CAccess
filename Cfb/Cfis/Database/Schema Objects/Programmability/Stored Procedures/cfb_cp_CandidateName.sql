IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_CandidateName')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_CandidateName]
	END

GO

CREATE Procedure [dbo].[cfb_cp_CandidateName]
(
	@candidateID char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	Last_Name AS LastName, First_Name AS FirstName, MI AS MiddleInitial
FROM	Candidate
WHERE	Cand_ID = @candidateID AND
		Deleted_Ind <> 'Y'

GO

GRANT EXEC ON [dbo].[cfb_cp_CandidateName] TO cfis

GO

