/*
 * Retrieves a message attachment by candidate ID, message ID, and attachment ID.
 */
CREATE PROCEDURE [dbo].[cfb_cp_Cmo_GetAttachment]
(
	@candidateId varchar(5),
	@messageId int,
	@attachmentId tinyint
)
AS

SET NOCOUNT ON

IF (@attachmentId IS NULL)
BEGIN
	SELECT	CandidateId, MessageId, AttachmentId, AttachmentName 
	FROM	CmoAttachments 
	WHERE	(CandidateId = @candidateId) AND 
			(MessageId = @messageId)
END
ELSE
BEGIN
	SELECT	CandidateId, MessageId, AttachmentId, AttachmentName 
	FROM	CmoAttachments 
	WHERE	(CandidateId = @candidateId) AND 
			(MessageId = @messageId) AND
			(AttachmentId = @attachmentId)
END


