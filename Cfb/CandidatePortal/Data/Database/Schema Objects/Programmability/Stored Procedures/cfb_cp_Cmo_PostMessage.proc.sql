/*
 * Posts a saved draft message for viewing by C-Access users.
 * Locking behavior: Repeatable Read.
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_PostMessage]
(
	@candidateId varchar(5),
	@messageId int,
	@version timestamp
)
AS

DECLARE @return int

SET NOCOUNT OFF
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRANSACTION

UPDATE	CmoMessages 
SET		PostDate = GETDATE() 
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId) AND 
		(Version = @version) AND 
		(PostDate IS NULL) AND 
		(OpenDate IS NULL) AND 
		(ArchiveDate IS NULL)

SET @return = @@rowcount

COMMIT TRANSACTION

SELECT @return


