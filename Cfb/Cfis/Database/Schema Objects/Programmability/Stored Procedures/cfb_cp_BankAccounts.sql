IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_BankAccounts')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_BankAccounts]
	END

GO

CREATE Procedure [dbo].[cfb_cp_BankAccounts]
(
	@candidateID char(5),
	@electionCycle char(5),
	@committeeID char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Comm_ID, Bank_Account_ID, Election_Cycle, Bank_Name, City, State_CD, Zip_CD, ABA_No, Account_No, Account_Name, Open_Date,
		Close_Date, Curr_Balance_Date, Curr_Balance_Amount, Account_Type_CD, Account_Type_CD_Other, Purpose_CD, Purpose_CD_Other, Direct_Deposit_Ind,
		Update_Date
FROM	Bank_Account
WHERE	(Cand_ID = @candidateID) AND
		(Election_Cycle = @electionCycle) AND
		(Comm_ID = @committeeID) AND
		(Deleted_Ind <> 'Y')
ORDER BY Bank_Name

GO

GRANT EXEC ON [dbo].[cfb_cp_BankAccounts] TO cfis

GO

