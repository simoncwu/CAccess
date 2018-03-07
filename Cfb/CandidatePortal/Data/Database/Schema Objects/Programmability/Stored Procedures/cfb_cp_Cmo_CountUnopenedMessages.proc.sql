/*
 * Counts the number of unopened (posted) messages for a specific candidate.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_CountUnopenedMessages]
(
	@candidateId varchar(5)
)
AS

SET NOCOUNT OFF

SELECT	COUNT(MessageId)
FROM	CmoMessages 
WHERE	(CandidateId = @candidateId) AND 
		(PostDate IS NOT NULL) AND 
		(OpenDate IS NULL)


