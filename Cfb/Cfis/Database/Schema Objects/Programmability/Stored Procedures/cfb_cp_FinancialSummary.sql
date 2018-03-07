IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_FinancialSummary')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_FinancialSummary]
	END

GO

CREATE Procedure [dbo].[cfb_cp_FinancialSummary]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	Election_Cycle, Cand_ID, Last_Statement_No, Office_CD, Boro_District_CD, Cand_Name, Candidate_Classification_CD, No_of_Contributors, 
		Net_Contributions, Match_Amount, Other_Receipts, Loans_Received, Public_Funds_Received, Public_Funds_Returned, Net_Expenditures, 
		Loans_Paid, Outstanding_Bills,
		Net_Contributions + Other_Receipts + Loans_Received AS Private_Funds_Received, 
		Loans_Paid + Outstanding_Bills + Net_Expenditures AS Campaign_Spending
FROM	Financial_Summary
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle)

GO

GRANT EXEC ON [dbo].[cfb_cp_FinancialSummary] TO cfis

GO

