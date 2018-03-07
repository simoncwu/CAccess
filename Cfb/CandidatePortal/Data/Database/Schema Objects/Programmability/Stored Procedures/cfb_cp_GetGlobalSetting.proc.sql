/*
 * Retrieves a C-Access global setting.
 */
CREATE PROCEDURE dbo.cfb_cp_GetGlobalSetting
(
	@name nvarchar(256)
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	Name, Value
FROM	GlobalSettings
WHERE	(Name = @name)

