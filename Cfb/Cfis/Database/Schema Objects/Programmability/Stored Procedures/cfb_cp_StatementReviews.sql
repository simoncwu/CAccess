SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_StatementReviews')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_StatementReviews]
	END

GO

CREATE Procedure [dbo].[cfb_cp_StatementReviews]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	sr.Cand_ID, sr.Election_Cycle, sr.Comm_ID, sr.Statement_No, sr.Start_Date, sr.Complete_Date, sr.Auditor_Assigned_CD, sr.Review_Sent_Date, 
		sr.Original_Response_Due_Date, sr.Number_of_Extensions, sr.Response_Due_Date, sr.Response_Received_Date, sr.Update_Date, 
		c.Name AS Committee_Name
FROM	Statement_Review_Tracking AS sr
		INNER JOIN Committee AS c
		ON	sr.Cand_ID = c.Cand_ID AND
			sr.Comm_ID = c.Comm_ID AND
			sr.Cand_ID = @candidateID AND
			sr.Election_Cycle = @electionCycle AND
			sr.Complete_Date IS NOT NULL AND
			c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y'
ORDER BY sr.Comm_ID, Statement_No DESC
-- sort by committee ID, then actual due date, then letter sent date, then statement number
--		CASE
--			WHEN Response_Due_Date IS NOT NULL THEN Response_Due_Date
--			WHEN Review_Sent_Date < Original_Response_Due_Date THEN Original_Response_Due_Date
--		END DESC, Review_Sent_Date DESC, Statement_No DESC


GO

GRANT EXEC ON [dbo].[cfb_cp_StatementReviews] TO cfis

GO

