IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ComplianceVisitUpcoming')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ComplianceVisitUpcoming]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ComplianceVisitUpcoming]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Comm_ID, Compliance_Visit_No, Visit_Scheduled_Date, Visit_Start_Date, Visit_Complete_Date, Memo_Written_Date, 
		Letter_Sent_Date, Letter_Sent_Cert_Date, Letter_Original_Reply_Date, Letter_Extension_Date, Letter_Extension_Due_Date, Letter_Received_Date, 
		Update_Date, '' AS Committee_Name
FROM	Compliance_Tracking
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(Visit_Scheduled_Date BETWEEN DATEADD(day, -7, GETDATE()) AND DATEADD(month, 6, GETDATE()))
ORDER BY Visit_Scheduled_Date

GO

GRANT EXEC ON [dbo].[cfb_cp_ComplianceVisitUpcoming] TO cfis

GO

