SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PrincipalStatementReviews')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PrincipalStatementReviews]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PrincipalStatementReviews]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	sr.Cand_ID, sr.Election_Cycle, sr.Comm_ID, sr.Statement_No, sr.Start_Date, sr.Complete_Date, sr.Auditor_Assigned_CD, sr.Review_Sent_Date, 
		sr.Original_Response_Due_Date, sr.Number_of_Extensions, sr.Response_Due_Date, sr.Response_Received_Date, sr.Update_Date, 
		'' AS Committee_Name
FROM	Statement_Review_Tracking AS sr
		INNER JOIN Authorized_Committee AS ac
		ON	sr.Cand_ID = ac.Cand_ID AND
			sr.Comm_ID = ac.Comm_ID
WHERE	(sr.Election_Cycle = @electionCycle) AND
		(sr.Cand_ID = @candidateID) AND
		(sr.Complete_Date IS NOT NULL) AND
		(ac.Election_Cycle = @electionCycle) AND
		(ac.Principal_Comm_Ind = 'Y') AND
		(ac.Deleted_Ind <> 'Y')
ORDER BY sr.Comm_ID, Statement_No DESC


GO

GRANT EXEC ON [dbo].[cfb_cp_PrincipalStatementReviews] TO cfis

GO

