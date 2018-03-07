IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PublicFundsHistory')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PublicFundsHistory]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PublicFundsHistory]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

-- get finalized payments ONLY
SELECT	Cand_ID AS CandidateID, 
		Election_Cycle AS ElectionCycle, 
		Transaction_Date AS Date,
		Check_No AS CheckNumber, 
		Reason_CD AS ReasonCode, 
		Amount, 
		Run_ID AS Run, 
		Election_Type_CD AS ElectionTypeCode
FROM	Candidate_Payment_Account
WHERE	Cand_ID = @candidateID AND
		Election_Cycle = @electionCycle AND
		Transaction_CD = 'CPAY' AND
		(	/* hide payments for the current day until after 5 PM (EFT cutoff time) */
			DATEPART(HOUR, GETDATE()) >= 17 OR 
			DATEDIFF(DAY, Transaction_Date, GETDATE()) > 0
		)

UNION

SELECT	pl.Cand_ID AS CandidateID, 
		pl.Election_Cycle AS ElectionCycle, 
		pl.Create_Date AS Date, 
		'' AS CheckNumber, 
		'' AS ReasonCode, 
		0 AS Amount,
		pl.Run_ID AS Run,
		pl.Election_Type_CD AS ElectionTypeCode
FROM	Candidate_Payment_Letter AS pl
		INNER JOIN Candidate_Payment_Run_Info AS pr
		ON	pl.Cand_ID = pr.Cand_ID AND
			pl.Election_Cycle = pr.Election_Cycle AND
			pl.Run_ID = pr.Run_ID AND
			pl.Election_Type_CD = pr.Election_Type_CD AND
			pl.Cand_ID = @candidateID AND
			pl.Election_Cycle = @electionCycle AND
			pl.Status_CD = 'FINAL' AND
			pr.Payment = 0 /* non-payment */

ORDER BY Date DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_PublicFundsHistory] TO cfis

GO

