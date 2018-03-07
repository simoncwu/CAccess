/*
 * Archives an opened (posted) message.
 * Locking behavior: Repeatable Read.
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_ArchiveMessage]
(
	@candidateId varchar(5),
	@messageId int,
	@archiveUserName nvarchar(255),
	@archived bit,
	@version timestamp
)
AS

DECLARE @return int
DECLARE @archiveDate datetime

SET NOCOUNT OFF
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRANSACTION

IF @archived = 1
	SET @archiveDate = GETDATE()
ELSE
	SET @archiveDate = NULL
UPDATE	CmoMessages 
SET		ArchiveUserName = @archiveUserName, ArchiveDate = @archiveDate 
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId) AND 
		(Version = @version) AND 
		(PostDate IS NOT NULL) AND 
		(OpenDate IS NOT NULL) AND 
		(((ArchiveDate IS NULL) AND (@archiveDate IS NOT NULL)) OR ((ArchiveDate IS NOT NULL) AND (@archiveDate IS NULL)))

SET @return = @@rowcount

COMMIT TRANSACTION

SELECT @return


