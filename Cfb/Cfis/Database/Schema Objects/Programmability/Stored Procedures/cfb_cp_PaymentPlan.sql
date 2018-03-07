IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PaymentPlan')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PaymentPlan]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PaymentPlan]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Plan_Opening_Date, Plan_Total_Amount, Plan_Number_of_Periods, Plan_Period_Type_CD, 
		Plan_Calculated_Payment_Amount, Grace_Period, Responsible_Lawyer_ID, Update_Date
FROM	Candidate_Payment_Plan
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle)

GO

GRANT EXEC ON [dbo].[cfb_cp_PaymentPlan] TO cfis

GO

