/*
 * Retrieves Doing Business tracking information.
 */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_DoingBusiness')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_DoingBusiness]
	END

GO

CREATE Procedure [dbo].[cfb_cp_DoingBusiness]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	db.Cand_ID AS CandidateID,
		db.Election_Cycle AS ElectionCycle,
		db.Comm_ID AS CommitteeID,
		db.Statement_No AS StatementNumber,
		db.Start_Date AS StartDate,
		db.Complete_Date AS CompletionDate,
		db.Auditor_Assigned_CD AS AssignedAuditorCode,
		db.Review_Sent_Date AS SentDate,
		-- workaround for CFIS requiring a response due date for sent reviews
		CASE
			WHEN db.Response_Received_Date <= db.Review_Sent_Date THEN NULL
			ELSE db.Original_Response_Due_Date
		END AS ResponseDueDate,
		db.Number_of_Extensions AS ExtensionsCount,
		db.Response_Due_Date AS ExtensionDueDate,
		-- workaround for CFIS requiring a response due date for sent reviews
		CASE
			WHEN db.Response_Received_Date <= db.Review_Sent_Date THEN NULL
			ELSE db.Response_Received_Date
		END AS ResponseReceivedDate,
		db.Update_Date AS LastUpdated,
		c.Name AS CommitteeName
FROM	Doing_Business_Review_Tracking AS db
		INNER JOIN Committee AS c
		ON	db.Cand_ID = c.Cand_ID AND
			db.Comm_ID = c.Comm_ID AND
			db.Cand_ID = @candidateID AND
			db.Election_Cycle = @electionCycle AND
			db.Complete_Date IS NOT NULL AND
			c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y'
ORDER BY db.Comm_ID, Statement_No DESC


GO

GRANT EXEC ON [dbo].[cfb_cp_DoingBusiness] TO cfis

GO

