/*
 * Counts the number of unopened (posted) messages for a specific candidate and election cycle.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_CountUnopenedMessagesByElection]
(
	@candidateId varchar(5),
	@electionCycle varchar(5)
)
AS

SET NOCOUNT OFF

SELECT	COUNT(MessageId)
FROM	CmoMessages 
WHERE	(CandidateId = @candidateId) AND 
		(ElectionCycle = @electionCycle) AND 
		(PostDate IS NOT NULL) AND 
		(OpenDate IS NULL) AND 
		(ArchiveDate IS NULL)


