/*
 * Retrieves the message ID for a compliance visit message by candidate, election cycle, and visit number.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetComplianceVisitId
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@visitNumber tinyint
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

EXEC	dbo.cfb_cp_Cmo_GetAuditReviewId 
		@candidateId = @candidateId, 
		@electionCycle = @electionCycle, 
		@categoryId = 3, 
		@reviewNumber = @visitNumber 

