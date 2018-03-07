IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_AuthorizedCommittees')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_AuthorizedCommittees]
	END

GO

CREATE Procedure [dbo].[cfb_cp_AuthorizedCommittees]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	c.Cand_ID, c.Comm_ID, c.Name, c.Short_Name, c.Street_No, c.Street_Name, c.Apt_No, c.City, c.State_CD, c.Zip_CD, c.Day_Telephone, c.Day_Ext, 
		c.Eve_Telephone, c.Eve_Ext, c.Fax, c.Auth_Date, c.Term_Date, c.Website, c.Mailing_Address_Line_1, c.Mailing_Street_No, c.Mailing_Street_Name, 
		c.Mailing_Apt_No, c.Mailing_City, c.Mailing_State_CD, c.Mailing_Zip_CD, c.Email_Address, ac.Election_Cycle, ac.Contact_Order_CD, ac.Sworn_Date, 
		ac.Active_Ind, ac.Principal_Comm_Ind, ac.Treas_Salutation_CD, ac.Treas_Last_Name, ac.Treas_First_Name, ac.Treas_MI, ac.Treas_Street_No, 
		ac.Treas_Street_Name, ac.Treas_APT_No, ac.Treas_City, ac.Treas_State_CD, ac.Treas_Zip_CD, ac.Treas_Day_Telephone, ac.Treas_Day_Ext, 
		ac.Treas_Eve_Telephone, ac.Treas_Eve_Ext, ac.Treas_Email_Address, ac.Treas_Fax, ac.Treas_Emp_Name, ac.Treas_Emp_Street_No, 
		ac.Treas_Emp_Street_Name, ac.Treas_Emp_City, ac.Treas_Emp_State_CD, ac.Treas_Emp_Zip_CD, ac.Treas_Emp_Telephone, 
		ac.Treas_Emp_Telephone_Ext, ac.Treas_Emp_Fax, ac.Last_Election_Date, ac.Last_Election_Office, ac.Last_Election_District, 
		cd.Long_Description AS Last_Primary_Party,
		CASE
			WHEN c.Update_Date > ac.Update_Date THEN c.Update_Date
			ELSE ac.Update_Date
		END AS Update_Date
FROM	Committee AS c
		INNER JOIN Authorized_Committee AS ac
		ON	c.Comm_ID = ac.Comm_ID AND
			ac.Cand_ID = @candidateID AND
			ac.Election_Cycle = @electionCycle AND
			ac.Deleted_Ind <> 'Y' AND
			c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y'
		LEFT OUTER JOIN Code AS cd
		ON	ac.Last_Election_Party_Primary_CD = cd.Code AND
			cd.Election_Cycle = @electionCycle AND
			cd.Category = 'PARTY'
ORDER BY Principal_Comm_Ind DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_AuthorizedCommittees] TO cfis

GO

