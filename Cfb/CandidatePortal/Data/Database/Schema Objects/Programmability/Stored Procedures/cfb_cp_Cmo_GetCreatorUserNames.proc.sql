/*
 * Retrieves the usernames of all existing CMO message creators.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetCreatorUserNames
AS

SET NOCOUNT ON

SELECT	DISTINCT CreatorADUserName
FROM	CmoMessages
ORDER BY CreatorADUserName ASC

