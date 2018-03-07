IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PreElectionDisclosures')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PreElectionDisclosures]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PreElectionDisclosures]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	dt.Cand_ID, dt.Election_Cycle, dt.Doc_Type_CD, dt.Comm_ID, dt.Doc_No, dt.Date_Received, dt.Postmark_Date, dt.Status_Date, dt.No_of_Pages, dt.Submission_CD, dt.Status_CD, dt.Status_Reason_CD, 
		dt.Delivery_Type_CD, dt.Update_Date, c.Name AS Committee_Name
FROM	Document_Tracking AS dt
		INNER JOIN Committee AS c
		ON	dt.Cand_ID = c.Cand_ID AND
			dt.Comm_ID = c.Comm_ID AND
			dt.Cand_ID = @candidateID AND
			dt.Election_Cycle = @electionCycle AND
			dt.Doc_Type_CD IN ('SPG', 'SPP') AND
			dt.Deleted_Ind <> 'Y' AND
			c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y'
ORDER BY dt.Comm_ID, Doc_Type_CD DESC, Doc_No DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_PreElectionDisclosures] TO cfis

GO

