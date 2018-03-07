/*
 * Retrieves the message ID for a Doing Business review message by candidate, election cycle, and statement number.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetDoingBusinessReviewId
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@statementNumber tinyint
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

EXEC	dbo.cfb_cp_Cmo_GetAuditReviewId 
		@candidateId = @candidateId, 
		@electionCycle = @electionCycle, 
		@categoryId = 7, 
		@reviewNumber = @statementNumber 

