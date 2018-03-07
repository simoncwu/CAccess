IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_CandidateList')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_CandidateList]
	END

GO

CREATE Procedure [dbo].[cfb_cp_CandidateList]

AS

SET NOCOUNT ON;

SELECT	Cand_ID, Salutation_CD, Last_Name, First_Name, MI, Street_No, Street_Name, Apt_No, City, State_CD, Zip_CD, Telephone, Telephone_Ext, Fax, 
		Eve_Telephone, Eve_Ext, Email_Address, Emp_Name, Emp_Street_No, Emp_Street_Name, Emp_City, Emp_Zip_CD, Emp_Telephone, 
		Emp_Telephone_Ext, Emp_Fax, Emp_State_CD, Update_Date
FROM	Candidate AS c
WHERE	(Deleted_Ind <> 'Y') AND 
		(EXISTS(
			SELECT	ac.Cand_ID
			FROM	Active_Candidate AS ac
			WHERE	(c.Cand_ID = ac.Cand_ID) AND
					(ac.Election_Cycle >= dbo.fn_cp_GetCutoffEC())
		))
ORDER BY First_Name, Last_Name, MI, Cand_ID

GO

GRANT EXEC ON [dbo].[cfb_cp_CandidateList] TO caccess

GO

