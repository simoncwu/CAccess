/*
 * Retrieves request version details for a post election audit message.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetPostElectionRequest
(
	@candidateId varchar(5),
	@messageId int
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	CandidateId, MessageId, Repost, SecondRequest
FROM	CmoPostElectionRequests
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId)

