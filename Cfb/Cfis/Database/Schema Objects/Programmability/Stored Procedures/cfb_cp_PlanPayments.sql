IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PlanPayments')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PlanPayments]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PlanPayments]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Transaction_CD, Transaction_Date, Update_Date, Election_Cycle, Election_Type_CD, Check_No, Reason_CD, Amount
FROM	Candidate_Payment_Account
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(Payment_Plan_Ind = 'Y')
ORDER BY Transaction_Date

GO

GRANT EXEC ON [dbo].[cfb_cp_PlanPayments] TO cfis

GO

