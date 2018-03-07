/*
 * Retrieves a message ID for a payment.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetPaymentMessageID]
(
	@candidateId varchar(5),
	@run tinyint
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	p.MessageId 
FROM	CmoPayments AS p 
		INNER JOIN CmoMessages  AS m 
		ON	p.CandidateId = m.CandidateId AND 
			p.MessageId = m.MessageId AND 
			p.CandidateId = @candidateId AND 
			p.Run = @run
ORDER BY m.PostDate DESC


