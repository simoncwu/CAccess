
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetAuditReviewNumber
(
	@candidateId varchar(5),
	@messageId int
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	ReviewNumber
FROM	CmoAuditReviews
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId)

