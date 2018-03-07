/*
 * Retrieves a message by candidate ID and message ID.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetMessage]
(
	@candidateId varchar(5),
	@messageId int
)
AS

SET NOCOUNT ON
SET ROWCOUNT 1

SELECT	CandidateId, MessageId, ElectionCycle, Title, Body, CreatorADUserName, OpenReceiptEmail, Category, PostDate, OpenUserName, 
		OpenDate, ArchiveUserName, ArchiveDate, FollowUp, FollowUpUserName, FollowUpDate, Version 
FROM	CmoMessages 
WHERE	(CandidateId = @candidateId) AND 
		(MessageId = @messageId)


