IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_StatementsOfNeed')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_StatementsOfNeed]
	END

GO

CREATE Procedure [dbo].[cfb_cp_StatementsOfNeed]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Doc_No, Date_Received, Postmark_Date, Status_Date, No_of_Pages, Submission_CD, Status_CD, Status_Reason_CD, 
		Delivery_Type_CD, Doc_Type_CD, Update_Date
FROM	Document_Tracking
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(Deleted_Ind <> 'Y') AND
		(Doc_Type_CD IN ('STMGEN', 'STMPRI'))
ORDER BY Doc_Type_CD DESC, Doc_No DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_StatementsOfNeed] TO cfis

GO

