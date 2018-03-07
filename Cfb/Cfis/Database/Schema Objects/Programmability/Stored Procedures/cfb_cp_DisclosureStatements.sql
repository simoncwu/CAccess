IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_DisclosureStatements')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_DisclosureStatements]
	END

GO

CREATE Procedure [dbo].[cfb_cp_DisclosureStatements]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	dt.Cand_ID,
		dt.Election_Cycle,
		dt.Comm_ID,
		dt.Statement_No,
		dt.Doc_No,
		dt.Small_Campaign_Ind,
		dt.Deferred_Filing_Ind,
		dt.Audit_Response_Ind,
		dt.Submission_CD,
		dt.Submission_Received_Ind,
		dt.Date_Received,
		dt.Postmark_Date,
		dt.Delivery_Type_CD,
		dt.Backup_Doc_Received_Ind,
		dt.Backup_Delivery_Type_CD,
		dt.Backup_Postmark_Date,
		dt.Backup_Received_Date,
		dt.Status_Reason_CD,
		dt.Status_Date,
		dt.Paper_Ind,
		dt.No_of_Pages,
		dt.Cd_Ind,
		dt.Disk_Ind,
		dt.No_of_Disks,
		dt.Internet_ind,
		dt.Csmart_Web_Ind,
		dt.Receipt_Complete_Date,
		dt.Update_Date,
		c.Name AS Committee_Name,
		CASE
			WHEN (dt.Status_CD = 'DISREG') AND (dt.Doc_Type_CD = 'DS') AND (dt.Submission_CD IN ('AMEND', 'DOCONL')) THEN 'ACCEPT'
			ELSE dt.Status_CD
		END AS Status_CD
FROM	Document_Tracking AS dt
		INNER JOIN Authorized_Committee AS ac
		ON	dt.Cand_ID = ac.Cand_ID AND
			dt.Comm_ID = ac.Comm_ID AND
			dt.Cand_ID = @candidateID AND
			dt.Election_Cycle = @electionCycle AND
			dt.Doc_Type_CD ='DS' AND
			dt.Deleted_Ind <> 'Y' AND
			ac.Cand_ID = @candidateID AND
			ac.Election_Cycle = @electionCycle AND
			ac.Deleted_Ind <> 'Y'
		INNER JOIN Committee AS c
		ON	dt.Cand_ID = c.Cand_ID AND
			dt.Comm_ID = c.Comm_ID AND
			dt.Cand_ID = @candidateID AND
			dt.Election_Cycle = @electionCycle AND
			dt.Doc_Type_CD ='DS' AND
			dt.Deleted_Ind <> 'Y' AND
			c.Cand_ID = @candidateID AND
			c.Deleted_Ind <> 'Y'
ORDER BY ac.Principal_Comm_Ind DESC, dt.Comm_ID, dt.Statement_No DESC, dt.Doc_No DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_DisclosureStatements] TO caccess

GO

