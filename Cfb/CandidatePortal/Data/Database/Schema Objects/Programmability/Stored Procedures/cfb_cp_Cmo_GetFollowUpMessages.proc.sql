/*
 * Retrieves all (posted) flagged messages for a specific candidate and election cycle.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetFollowUpMessages]
(
	@candidateId varchar(5),
	@electionCycle varchar(5)
)
AS

SET NOCOUNT ON

SELECT	CandidateId, MessageId, ElectionCycle, Title, Body, CreatorADUserName, OpenReceiptEmail, Category, PostDate, OpenUserName, 
		OpenDate, ArchiveUserName, ArchiveDate, FollowUp, FollowUpUserName, FollowUpDate, Version 
FROM	CmoMessages 
WHERE	(CandidateId = @candidateId) AND 
		((@electionCycle IS NULL) OR (@electionCycle = '') OR (@electionCycle = ElectionCycle)) AND 
		(PostDate IS NOT NULL) AND 
		(FollowUp = 1)


