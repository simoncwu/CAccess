IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionResponseAnalysis')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionResponseAnalysis]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionResponseAnalysis]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	[Status_CD] AS [StatusCode] 
FROM	[Post_Election_Response_Analysis] 
WHERE	[Cand_ID] = @candidateID AND 
		[Election_Cycle] = @electionCycle AND 
		[Status_CD] = 'FINAL'

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionResponseAnalysis] TO cfis

GO

