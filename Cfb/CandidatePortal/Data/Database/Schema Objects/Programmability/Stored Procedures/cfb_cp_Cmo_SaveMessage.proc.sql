/*
 * Saves a C-Access campaign message by insertion if no message ID is supplied, otherwise by update.
 * Locking behavior: Serializable for inserts, Repeatable Read for updates
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_SaveMessage]
(
	@candidateId varchar(5),
	@messageId int,
	@electionCycle varchar(5),
	@title nvarchar(255),
	@body ntext,
	@creatorADUserName varchar(32),
	@openReceiptEmail nvarchar(256),
	@category tinyint,
	@version timestamp
)
AS

DECLARE @returnId int

SET NOCOUNT OFF
IF (@messageId < 1)
BEGIN -- add new entry
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

	BEGIN TRANSACTION

	SET @returnId = 
	(
		SELECT MAX(MessageId) 
		FROM CmoMessages 
		WHERE (CandidateId = @candidateId) 
		GROUP BY CandidateId
	) + 1
	IF @returnId IS NULL SET @returnId = 1

	INSERT INTO CmoMessages
	(CandidateId, MessageId, ElectionCycle, Title, Body, CreatorADUserName, OpenReceiptEmail, Category)
	VALUES
	(@candidateId,@returnId,@electionCycle,@title,@body,@creatorADUserName, @openReceiptEmail, @category)

	IF @@rowcount = 0 SET @returnId = 0
END
ELSE
BEGIN -- update existing entry
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	BEGIN TRANSACTION

	UPDATE	CmoMessages
	SET		ElectionCycle = @electionCycle, Title = @title, Body = @body, CreatorADUserName = @creatorADUserName, 
			OpenReceiptEmail = @openReceiptEmail, Category = @category
	WHERE	(CandidateId = @candidateId) AND 
			(MessageId = @messageId) AND 
			(Version = @version) AND 
			(PostDate IS NULL)

	IF @@rowcount > 0 SET @returnId = @messageId ELSE SET @returnId = 0
END

COMMIT TRANSACTION

SELECT @returnId


