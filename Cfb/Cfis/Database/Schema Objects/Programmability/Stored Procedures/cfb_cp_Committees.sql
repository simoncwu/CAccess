IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_Committees')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_Committees]
	END

GO

CREATE Procedure [dbo].[cfb_cp_Committees]
(
	@candidateID char(5),
	@committeeID char(1)
)

AS

SET NOCOUNT ON;

SELECT	[Cand_ID], [Comm_ID], [Name], [Short_Name], [Street_No], [Street_Name], [Apt_No], [City], [State_CD], [Zip_CD], [Day_Telephone], [Day_Ext], 
		[Eve_Telephone], [Eve_Ext], [Fax], [Auth_Date], [Term_Date], [Website], [Mailing_Address_Line_1], [Mailing_Street_No], [Mailing_Street_Name], 
		[Mailing_Apt_No], [Mailing_City], [Mailing_State_CD], [Mailing_Zip_CD], [Email_Address], [Update_Date],
		-- dummy columns for compatibility with authorized committees schema
		'' AS [Election_Cycle], 
		0 AS [Contact_Order_CD], 
		NULL AS [Sworn_Date], 
		'' AS [Active_Ind], 
		'' AS [Principal_Comm_Ind], 
		'' AS [Treas_Salutation_CD], 
		'' AS [Treas_Last_Name],
		'' AS  [Treas_First_Name], 
		'' AS [Treas_MI], 
		'' AS [Treas_Street_No], 
		'' AS [Treas_Street_Name], 
		'' AS [Treas_APT_No], 
		'' AS [Treas_City], 
		'' AS [Treas_State_CD], 
		'' AS [Treas_Zip_CD], 
		'' AS [Treas_Day_Telephone], 
		'' AS [Treas_Day_Ext], 
		'' AS [Treas_Eve_Telephone], 
		'' AS [Treas_Eve_Ext], 
		'' AS [Treas_Email_Address], 
		'' AS [Treas_Fax], 
		'' AS [Treas_Emp_Name], 
		'' AS [Treas_Emp_Street_No], 
		'' AS [Treas_Emp_Street_Name], 
		'' AS [Treas_Emp_City], 
		'' AS [Treas_Emp_State_CD], 
		'' AS [Treas_Emp_Zip_CD], 
		'' AS [Treas_Emp_Telephone], 
		'' AS [Treas_Emp_Telephone_Ext], 
		'' AS [Treas_Emp_Fax], 
		NULL AS [Last_Election_Date], 
		'' AS [Last_Election_Office], 
		'' AS [Last_Election_District], 
		NULL AS Last_Primary_Party
FROM	Committee
WHERE	[Cand_ID] = @candidateID AND
		(@committeeID IS NULL OR [Comm_ID] = @committeeID) AND
		[Deleted_Ind] <> 'Y'
ORDER BY [Comm_ID]

GO

GRANT EXEC ON [dbo].[cfb_cp_Committees] TO cfis

GO

