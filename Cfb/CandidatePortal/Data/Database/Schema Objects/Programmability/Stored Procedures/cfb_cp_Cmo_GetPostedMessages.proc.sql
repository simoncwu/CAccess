

CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetPostedMessages]
AS

SET NOCOUNT ON

SELECT	CandidateId, MessageId, ElectionCycle, Title, Body, CreatorADUserName, OpenReceiptEmail, Category, PostDate, OpenUserName, 
		OpenDate, ArchiveUserName, ArchiveDate, FollowUp, FollowUpUserName, FollowUpDate, Version 
FROM	CmoMessages 
WHERE	PostDate IS NOT NULL
ORDER BY CandidateId, PostDate


