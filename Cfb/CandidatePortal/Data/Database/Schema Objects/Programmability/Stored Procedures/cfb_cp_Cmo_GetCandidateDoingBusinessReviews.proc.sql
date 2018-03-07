/*
 * Retrieves all Doing Business review messages for a single candidate.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetCandidateDoingBusinessReviews
(
	@candidateId varchar(5),
	@electionCycle varchar(5)
)
AS

SET NOCOUNT ON

EXEC	dbo.cfb_cp_Cmo_GetCandidateAuditReviews
		@candidateId = @candidateId,
		@electionCycle = @electionCycle,
		@categoryId = 7

