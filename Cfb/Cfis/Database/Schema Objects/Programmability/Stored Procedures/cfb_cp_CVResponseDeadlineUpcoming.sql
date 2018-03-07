IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_CVResponseDeadlineUpcoming')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_CVResponseDeadlineUpcoming]
	END

GO

CREATE Procedure [dbo].[cfb_cp_CVResponseDeadlineUpcoming]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Comm_ID, Compliance_Visit_No, Visit_Scheduled_Date, Visit_Start_Date, Visit_Complete_Date, Memo_Written_Date, 
		Letter_Sent_Date, Letter_Sent_Cert_Date, Letter_Original_Reply_Date, Letter_Extension_Date, Letter_Extension_Due_Date, Letter_Received_Date, 
		Update_Date, '' AS Committee_Name,
		CASE
			WHEN Letter_Extension_Due_Date IS NOT NULL THEN Letter_Extension_Due_Date
			ELSE Letter_Original_Reply_Date
		END AS Actual_Due_Date
FROM	Compliance_Tracking
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(	CASE
				WHEN Letter_Sent_Cert_Date IS NOT NULL THEN Letter_Sent_Cert_Date
				ELSE Letter_Sent_Date
			END < Letter_Original_Reply_Date) AND
		(	CASE
				WHEN Letter_Extension_Due_Date IS NOT NULL THEN Letter_Extension_Due_Date
				ELSE Letter_Original_Reply_Date
			END BETWEEN DATEADD(day, -7, GETDATE()) AND DATEADD(month, 6, GETDATE()))
ORDER BY Actual_Due_Date

GO

GRANT EXEC ON [dbo].[cfb_cp_CVResponseDeadlineUpcoming] TO cfis

GO

