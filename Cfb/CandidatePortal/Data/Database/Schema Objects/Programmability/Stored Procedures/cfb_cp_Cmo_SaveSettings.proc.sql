/*
 * Sets an enrollment setting for a candidate regardless of whether or not one already exists.
 * Locking behavior: Serializable for inserts, Repeatable Read for updates.
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_SaveSettings]
(
	@candidateId varchar(5),
	@isPaperless bit,
	@updaterUserName nvarchar(256),
	@version timestamp
)
AS

DECLARE @return int

SET NOCOUNT OFF
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

BEGIN TRANSACTION

-- determine if we are adding or updating
DECLARE @CmoSettingsCount TABLE(
	CandidateId varchar(5) not null
)
INSERT INTO @CmoSettingsCount
SELECT	CandidateId
FROM	CmoSettings
WHERE	(CandidateId = @candidateId)

IF (@@rowcount = 0)
BEGIN -- add new entry
	INSERT INTO CmoSettings
	(CandidateId, IsPaperless, UpdaterUserName, UpdatedDate)
	VALUES
	(@candidateId,@isPaperless,@updaterUserName,GETDATE())
END
ELSE
BEGIN -- update existing entry
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	UPDATE	CmoSettings
	SET		IsPaperless = @isPaperless, UpdaterUserName = @updaterUserName, UpdatedDate = GETDATE()
	WHERE	(CandidateId = @candidateId) AND (Version = @version)
END

SET @return = @@rowcount

COMMIT TRANSACTION

SELECT @return


