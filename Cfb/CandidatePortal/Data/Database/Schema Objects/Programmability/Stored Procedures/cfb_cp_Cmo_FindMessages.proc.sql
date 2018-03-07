/*
 * Performs a search for messages across various properties.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_FindMessages]
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@creatorADUserName varchar(64)
)
AS

SET NOCOUNT ON

SELECT	CandidateId, MessageId, ElectionCycle, Title, Body, CreatorADUserName, OpenReceiptEmail, Category, PostDate, OpenUserName, 
		OpenDate, ArchiveUserName, ArchiveDate, FollowUp, FollowUpUserName, FollowUpDate, Version 
FROM	CmoMessages 
WHERE	((@candidateId IS NULL) OR (CandidateId = @candidateId)) AND 
		((@electionCycle IS NULL) OR (ElectionCycle = @electionCycle)) AND 
		((@creatorADUserName IS NULL) OR (CreatorADUserName = @creatorADUserName))
ORDER BY PostDate DESC, ElectionCycle DESC


