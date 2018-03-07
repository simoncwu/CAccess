IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PaymentSchedule')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PaymentSchedule]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PaymentSchedule]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Election_Cycle, Payment_No, Payment_Amount, Payment_Due_Date, Update_Date
FROM	Candidate_Payment_Plan_Detail
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle)

GO

GRANT EXEC ON [dbo].[cfb_cp_PaymentSchedule] TO cfis

GO

