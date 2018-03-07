IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_SRResponseDeadlineUpcoming')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_SRResponseDeadlineUpcoming]
	END

GO

CREATE Procedure [dbo].[cfb_cp_SRResponseDeadlineUpcoming]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Comm_ID, Statement_No, Start_Date, Complete_Date, Auditor_Assigned_CD, Review_Sent_Date, 
		Original_Response_Due_Date, Number_of_Extensions, Response_Due_Date, Response_Received_Date, Update_Date, '' AS Committee_Name,
		CASE
			WHEN Response_Due_Date IS NOT NULL THEN Response_Due_Date
			ELSE Original_Response_Due_Date
		END AS Actual_Due_Date
FROM	Statement_Review_Tracking
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(Complete_Date IS NOT NULL) AND
		(Review_Sent_Date < Original_Response_Due_Date) AND
		(	CASE
				WHEN Response_Due_Date IS NOT NULL THEN Response_Due_Date
				ELSE Original_Response_Due_Date
			END BETWEEN DATEADD(day, -7, GETDATE()) AND DATEADD(month, 6, GETDATE()))
ORDER BY Actual_Due_Date

GO

GRANT EXEC ON [dbo].[cfb_cp_SRResponseDeadlineUpcoming] TO cfis

GO

