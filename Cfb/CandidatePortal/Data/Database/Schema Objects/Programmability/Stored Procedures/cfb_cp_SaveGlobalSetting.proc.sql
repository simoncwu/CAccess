/*
 * Saves a C-Access global setting by insertion if not yet defined, otherwise by update.
 * Locking behavior: Serializable for inserts, Repeatable Read for updates
 * Concurrency: Optimistic via timestamp
 */
CREATE PROCEDURE dbo.cfb_cp_SaveGlobalSetting
(
	@name nvarchar(128),
	@value nvarchar(256)
)
AS

DECLARE @return int

SET NOCOUNT OFF
SET ROWCOUNT 1

IF EXISTS(SELECT Name FROM GlobalSettings WHERE Name = @name)
BEGIN -- update existing entry
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

	BEGIN TRANSACTION

	UPDATE	GlobalSettings
	SET		Value = @value
	WHERE	Name = @name
	
	SET @return = @@rowcount
END
ELSE
BEGIN -- add new entry
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

	BEGIN TRANSACTION

	INSERT INTO GlobalSettings
	(Name, Value)
	VALUES
	(@name, @value)
	
	SET @return = @@rowcount
END

COMMIT TRANSACTION

RETURN @return

