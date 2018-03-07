IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionDar')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionDar]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionDar]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_Sent_Date])) AS [SentDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_Sent_Cert_Date])) AS [ReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_Original_Due_Date])) AS [DueDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_2_Sent_Date])) AS [SecondSentDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_2_Sent_Cert_Date])) AS [SecondReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_2_Due_Date])) AS [SecondDueDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_Reply_Received_Date])) AS [ResponseReceivedDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Draft_Postmark_Date])) AS [ResponseSubmittedDate]
FROM	[Final_Audit_Tracking]
WHERE	[Cand_ID] = @candidateID AND
		[Election_Cycle] = @electionCycle AND
		[Rpt_Draft_Sent_Date] IS NOT NULL AND
		[Rpt_Draft_Original_Due_Date] IS NOT NULL

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionDar] TO cfis

GO

