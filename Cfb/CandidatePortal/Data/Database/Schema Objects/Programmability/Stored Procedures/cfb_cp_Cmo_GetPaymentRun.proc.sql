/*
 * Retrieves a payment run number by message ID.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetPaymentRun]
(
	@candidateId varchar(5),
	@messageId int
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	Run
FROM	CmoPayments
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId)


