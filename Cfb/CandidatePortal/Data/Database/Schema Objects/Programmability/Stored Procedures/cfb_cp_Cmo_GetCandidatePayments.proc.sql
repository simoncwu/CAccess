/*
 * Retrieves all public funds determination messages for a single candidate.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetCandidatePayments
(
	@candidateId varchar(5),
	@electionCycle varchar(5)
)
AS

SET NOCOUNT ON

SELECT	m.CandidateId, m.MessageId, p.Run, m.PostDate
FROM	CmoMessages AS m 
		INNER JOIN CmoPayments AS p 
		ON	m.CandidateId = p.CandidateId AND 
			m.MessageId = p.MessageId AND 
			m.CandidateId = @candidateId AND 
			m.Category = 8 AND 
			(m.ElectionCycle = @electionCycle OR @electionCycle IS NULL)
ORDER BY m.ElectionCycle DESC, p.Run DESC

