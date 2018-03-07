IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionIdr')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionIdr]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionIdr]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Request_Sent_Date])) AS [SentDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Request_Sent_Cert_Date])) AS [ReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Original_Reply_Due_Date])) AS [DueDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Request_2_Sent_Date])) AS [SecondSentDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Request_2_Sent_Cert_Date])) AS [SecondReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Reply_Due_Date_2])) AS [SecondDueDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Reply_Received])) AS [ResponseReceivedDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Mail_Reply_Postmark_Date])) AS [ResponseSubmittedDate]
FROM	[Final_Audit_Tracking]
WHERE	[Cand_ID] = @candidateID AND
		[Election_Cycle] = @electionCycle AND
		[Mail_Request_Sent_Date] IS NOT NULL AND
		[Mail_Original_Reply_Due_Date] IS NOT NULL

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionIdr] TO cfis

GO

