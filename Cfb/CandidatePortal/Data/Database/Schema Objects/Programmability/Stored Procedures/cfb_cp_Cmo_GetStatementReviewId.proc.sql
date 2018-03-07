/*
 * Retrieves the message ID for a statement review message by candidate, election cycle, and statement number.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetStatementReviewId
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
		@categoryId = 2, 
		@reviewNumber = @statementNumber 

