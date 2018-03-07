/*
 * Saves a C-Access campaign message attachment by insertion if no message ID is supplied, otherwise by update.
 * Locking behavior: Serializable for inserts, Repeatable Read for updates
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_SaveAttachment]
(
	@candidateId varchar(5),
	@messageId int,
	@attachmentId tinyint,
	@attachmentName nvarchar(256)
)
AS

DECLARE @returnId int

SET NOCOUNT OFF
IF (@attachmentId < 1)
BEGIN -- add new entry
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

	BEGIN TRANSACTION

	SET @returnId = 
	(
		SELECT MAX(AttachmentId) 
		FROM CmoAttachments 
		WHERE (CandidateId = @candidateId) AND (MessageId = @messageId) 
		GROUP BY CandidateId, MessageId
	) + 1
	IF @returnId IS NULL SET @returnId = 1

	INSERT INTO CmoAttachments
	(CandidateId, MessageId, AttachmentId, AttachmentName)
	VALUES
	(@candidateId,@messageId,@returnId,@attachmentName)

	IF @@rowcount = 0 SET @returnId = 0
END
ELSE
BEGIN -- update existing entry
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	BEGIN TRANSACTION

	UPDATE	CmoAttachments
	SET		AttachmentName = @attachmentName
	WHERE	(CandidateId = @candidateId) AND 
			(MessageId = @messageId) AND 
			(AttachmentId = @attachmentId)

	IF @@rowcount > 0 SET @returnId = @messageId ELSE SET @returnId = 0
END

COMMIT TRANSACTION

SELECT @returnId


