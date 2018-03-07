/*
 * Retrieves tolling details for a tolling letter message.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetTollingLetter
(
	@candidateId varchar(5),
	@messageId int
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	ctl.CandidateId, ctl.MessageId, ctl.EventNumber, ctl.LetterId 
FROM	CmoTollingLetters AS ctl INNER JOIN TollingLetters AS tl 
		ON	ctl.CandidateId = @candidateId AND 
			ctl.MessageId = @messageId AND 
			ctl.LetterId = tl.LetterId

