/*
 * Saves an audit extension request by insertion if no message ID is supplied, otherwise by update.
 * Locking behavior: Serializable for inserts, Repeatable Read for updates
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_SaveExtensionRequest]
(
	@candidateID varchar(5),
	@electionCycle varchar(5),
	@extensionType tinyint,
	@reviewNumber tinyint,
	@iteration tinyint,
	@date datetime,
	@requestedDueDate datetime,
	@reason ntext,
	@version timestamp
)
AS

DECLARE @return int

SET NOCOUNT OFF
IF (@iteration IS NULL) OR (@iteration < 1)
BEGIN -- add new entry
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

	BEGIN TRANSACTION

	SET @return = 
	(
		SELECT MAX(Iteration) 
		FROM ExtensionRequests 
		WHERE	(CandidateID = @candidateID) AND
				(ElectionCycle = @electionCycle) AND 
				(ExtensionType = @extensionType) AND 
				(ReviewNumber = @reviewNumber) 
		GROUP BY CandidateID
	) + 1
	IF @return IS NULL SET @return = 1

	INSERT INTO ExtensionRequests
	(CandidateID, ElectionCycle, ExtensionType, ReviewNumber, Iteration, Date, RequestedDueDate, Reason)
	VALUES
	(@candidateId,@electionCycle,@extensionType,@reviewNumber,@return,@date,@requestedDueDate,@reason)

	IF @@rowcount = 0 SET @return = 0
END
ELSE
BEGIN -- update existing entry
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	BEGIN TRANSACTION

	UPDATE	ExtensionRequests
	SET		Date = @date, RequestedDueDate = @requestedDueDate, Reason = @reason
	WHERE	(CandidateId = @candidateId) AND 
			(ElectionCycle = @electionCycle) AND 
			(ExtensionType = @extensionType) AND 
			(ReviewNumber = @reviewNumber) AND 
			(Iteration = @iteration) AND 
			(Version = @version)

	IF @@rowcount > 0 SET @return = @iteration ELSE SET @return = 0
END

COMMIT TRANSACTION

SELECT @return


