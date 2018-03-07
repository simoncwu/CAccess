/*
 * Flags or unflags a posted message for follow-up.
 * Locking behavior: Repeatable Read.
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_FollowUpMessage]
(
	@candidateId varchar(5),
	@messageId int,
	@followUpUserName nvarchar(255),
	@followUp bit,
	@version timestamp
)
AS

DECLARE @return int

SET NOCOUNT OFF
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRANSACTION

UPDATE	CmoMessages 
SET		FollowUp = @followUp, FollowUpUserName = @followUpUserName, FollowUpDate = GETDATE() 
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId) AND 
		(Version = @version) AND 
		(PostDate IS NOT NULL)

SET @return = @@rowcount

COMMIT TRANSACTION

SELECT @return


