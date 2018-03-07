

CREATE Procedure [dbo].[cfb_cp_Cmo_GetSettings]
(
	@candidateID varchar(5)
)

AS

SET NOCOUNT ON

SELECT	CandidateId, IsPaperless, UpdaterUserName, UpdatedDate, Version
FROM	CmoSettings
WHERE	CandidateId = @candidateID


