/*
 * Retrieves the message ID for an audit review message by candidate, election cycle, type, and review number.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetAuditReviewId
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@categoryId tinyint,
	@reviewNumber tinyint
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	m.MessageId
FROM	CmoMessages AS m 
		INNER JOIN CmoAuditReviews AS r 
		ON	m.CandidateId = r.CandidateId AND 
			m.MessageId = r.MessageId AND 
			m.CandidateId = @candidateId AND 
			m.ElectionCycle = @electionCycle AND 
			m.PostDate IS NOT NULL AND 
			m.Category = @categoryId AND 
			r.ReviewNumber = @reviewNumber
ORDER BY m.PostDate DESC

