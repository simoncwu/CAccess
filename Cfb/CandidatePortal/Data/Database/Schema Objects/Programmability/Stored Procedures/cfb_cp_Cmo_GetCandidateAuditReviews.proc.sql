/*
 * Retrieves the review number for an audit review message.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetCandidateAuditReviews
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@categoryId tinyint
)
AS

SET NOCOUNT ON

SELECT	m.CandidateId, m.MessageId, r.ReviewNumber
FROM	CmoMessages AS m 
		INNER JOIN CmoAuditReviews AS r 
		ON	m.CandidateId = r.CandidateId AND 
			m.MessageId = r.MessageId AND 
			m.CandidateId = @candidateId AND 
			m.Category = @categoryId AND 
			m.PostDate IS NOT NULL AND 
			(m.ElectionCycle = @electionCycle OR @electionCycle IS NULL)
ORDER BY m.ElectionCycle DESC, r.ReviewNumber DESC

