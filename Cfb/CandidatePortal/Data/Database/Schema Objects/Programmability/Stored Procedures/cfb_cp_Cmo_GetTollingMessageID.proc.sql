/*
 * Retrieves all unopened (posted) messages for a single candidate.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetTollingMessageID
(
	@candidateId varchar(5),
	@eventNumber int
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	MessageId 
FROM	CmoTollingLetters AS l 
WHERE	CandidateId = @candidateId AND 
		EventNumber = @eventNumber

