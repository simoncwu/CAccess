IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_Candidate')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_Candidate]
	END

GO

CREATE Procedure [dbo].[cfb_cp_Candidate]
(
	@candidateID char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	Cand_ID, Salutation_CD, Last_Name, First_Name, MI, Street_No, Street_Name, Apt_No, City, State_CD, Zip_CD, Telephone, Telephone_Ext, Fax, 
		Eve_Telephone, Eve_Ext, Email_Address, Emp_Name, Emp_Street_No, Emp_Street_Name, Emp_City, Emp_Zip_CD, Emp_Telephone, 
		Emp_Telephone_Ext, Emp_Fax, Emp_State_CD, Update_Date
FROM	Candidate
WHERE	(Cand_ID = @candidateID) AND (Deleted_Ind <> 'Y')

GO

GRANT EXEC ON [dbo].[cfb_cp_Candidate] TO cfis

GO

