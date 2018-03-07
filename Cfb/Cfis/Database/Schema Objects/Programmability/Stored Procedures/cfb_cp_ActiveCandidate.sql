IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ActiveCandidate')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ActiveCandidate]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ActiveCandidate]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	c.Cand_ID, c.Salutation_CD, c.Last_Name, c.First_Name, c.MI, c.Street_No, c.Street_Name, c.Apt_No, c.City, c.State_CD, c.Zip_CD, c.Telephone, 
		c.Telephone_Ext, c.Fax, c.Eve_Telephone, c.Eve_Ext, c.Email_Address, c.Emp_Name, c.Emp_Street_No, c.Emp_Street_Name, c.Emp_City, c.Emp_Zip_CD, 
		c.Emp_Telephone, c.Emp_Telephone_Ext, c.Emp_Fax, c.Emp_State_CD, a.Election_Cycle, a.Office_CD, a.Boro_CD, a.District_CD, a.Certified_Date, 
		a.File_Info_Date, a.Termination_Date, a.Termination_Reason_CD, a.Candidate_Classification_CD, a.Internet_Filing_Email_Address, 
		a.Direct_Deposit_Auth_Ind, a.RR_Direct_Deposit_Auth_Ind, cm.Principal_Committee, cm.Principal_ID, p.Long_Description AS Party, 
		al.Long_Description AS AuditorName, cl.Long_Description AS CsuLiaisonName, 
		CASE
			WHEN c.Update_Date > a.Update_Date THEN c.Update_Date
			ELSE a.Update_Date
		END AS Update_Date
FROM	Candidate AS c
		INNER JOIN Active_Candidate AS a
		ON	c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y' AND
			a.Cand_ID = @candidateID AND
			a.Election_Cycle = @electionCycle AND
			a.Deleted_Ind <> 'Y'
		-- party info
		LEFT OUTER JOIN Code AS p
		ON	p.Election_Cycle = @electionCycle AND
			p.Category = 'PARTY' AND
			a.Party_Registration_CD = p.Code
		-- committee info
		LEFT OUTER JOIN (
			SELECT	c.Name AS Principal_Committee, c.Comm_ID AS Principal_ID, c.Cand_ID
			FROM	Committee AS c
					INNER JOIN Authorized_Committee AS a
					ON	c.Comm_ID = a.Comm_ID AND
						c.Cand_ID = @candidateID AND
						c.Deleted_Ind <> 'Y' AND
						a.Cand_ID = @candidateID AND
						a.Election_Cycle = @electionCycle AND
						a.principal_comm_ind = 'Y' AND
						a.Deleted_Ind <> 'Y'
		) AS cm
		ON	c.Cand_ID = cm.Cand_ID
		-- audit liaison info
		LEFT OUTER JOIN Code AS al
		ON	al.Election_Cycle = @electionCycle AND
			al.Category = 'AUDID' AND
			a.Auditor_Assigned_CD = al.Code
		-- csu liaison info
		LEFT OUTER JOIN Code AS cl
		ON	cl.Election_Cycle = @electionCycle AND
			cl.Category = 'CSUID' AND
			a.CSU_Assigned_CD = cl.Code

GO

GRANT EXEC ON [dbo].[cfb_cp_ActiveCandidate] TO cfis

GO

