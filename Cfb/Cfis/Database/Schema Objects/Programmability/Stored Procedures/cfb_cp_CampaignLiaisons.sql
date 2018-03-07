IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_CampaignLiaisons')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_CampaignLiaisons]
	END

GO

CREATE Procedure [dbo].[cfb_cp_CampaignLiaisons]
(
	@candidateID char(5),
	@committeeID char(5)
)

AS

SET NOCOUNT ON;

SELECT	Cand_ID,
		Comm_ID,
		Liason_ID,
		Contact_Order_CD,
		Salutation_CD,
		First_Name,
		Last_Name,
		MI,
		Street_No,
		Street_Name,
		Apt_No,
		City,
		State_CD, 
		Zip_CD,
		Day_Telephone,
		Day_Telephone_Ext,
		Evening_Telephone,
		Evening_Telephone_Ext,
		Fax,
		Contact_Type_CD,
		Email_Address, 
		Entity_Name,
		Update_Date,
		Managerial_Control_Ind,
		Voter_Guide_Liaison_Ind
FROM	Candidate_Liason
WHERE	(Cand_ID = @candidateID) AND
		(Comm_ID = @committeeID) AND
		(Deleted_Ind <> 'Y')
ORDER BY Last_Name, First_Name, MI ASC

GO

GRANT EXEC ON [dbo].[cfb_cp_CampaignLiaisons] TO caccess

GO

