IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ActiveCandidateList')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ActiveCandidateList]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ActiveCandidateList]
(
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	c.Cand_ID, c.Salutation_CD, c.Last_Name, c.First_Name, c.MI, c.Street_No, c.Street_Name, c.Apt_No, c.City, c.State_CD, c.Zip_CD, c.Telephone, 
		c.Telephone_Ext, c.Fax, c.Eve_Telephone, c.Eve_Ext, c.Email_Address, c.Emp_Name, c.Emp_Street_No, c.Emp_Street_Name, c.Emp_City, c.Emp_Zip_CD, 
		c.Emp_Telephone, c.Emp_Telephone_Ext, c.Emp_Fax, c.Emp_State_CD, c.Update_Date
FROM	Candidate AS c
		INNER JOIN Active_Candidate AS a
		ON	c.Cand_ID = a.Cand_ID AND
			c.Deleted_Ind <> 'Y' AND
			a.Election_Cycle = @electionCycle AND
			a.Deleted_Ind <> 'Y'
ORDER BY c.First_Name, c.Last_Name, c.MI, c.Cand_ID

GO

GRANT EXEC ON [dbo].[cfb_cp_ActiveCandidateList] TO cfis

GO

