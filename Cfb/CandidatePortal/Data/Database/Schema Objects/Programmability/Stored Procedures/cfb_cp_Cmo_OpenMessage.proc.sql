/*
 * Marks a posted message as opened.
 * Locking behavior: Repeatable Read.
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_OpenMessage]
(
	@candidateId varchar(5),
	@messageId int,
	@openUserName nvarchar(255),
	@version timestamp
)
AS

DECLARE @return int

SET NOCOUNT OFF
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRANSACTION

UPDATE	CmoMessages 
SET		OpenUserName = @openUserName, OpenDate = GETDATE() 
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId) AND 
		(Version = @version) AND 
		(PostDate IS NOT NULL) AND 
		(OpenDate IS NULL) AND 
		(ArchiveDate IS NULL)

SET @return = @@rowcount

COMMIT TRANSACTION

SELECT @return


