SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ComplianceVisits')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ComplianceVisits]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ComplianceVisits]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	ct.Cand_ID, ct.Election_Cycle, ct.Comm_ID, ct.Compliance_Visit_No, ct.Visit_Scheduled_Date, ct.Visit_Start_Date, ct.Visit_Complete_Date, 
		ct.Memo_Written_Date, ct.Letter_Sent_Date, ct.Letter_Sent_Cert_Date, ct.Letter_Original_Reply_Date, ct.Letter_Extension_Date, 
		ct.Letter_Extension_Due_Date, ct.Letter_Received_Date, ct.Update_Date, ct.Auditor_Assigned_CD, cm.Name AS Committee_Name
FROM	Compliance_Tracking AS ct
		INNER JOIN Committee AS cm
		ON	ct.Comm_ID = cm.Comm_ID AND
			ct.Cand_ID = cm.Cand_ID AND
			ct.Cand_ID = @candidateID AND
			ct.Election_Cycle = @electionCycle AND
			cm.Cand_ID = @candidateID AND
			cm.Deleted_Ind <> 'Y'
ORDER BY ct.Comm_ID, Compliance_Visit_No DESC
-- sort by committee ID, then actual due/visit date, then letter sent date, then visit number --
--		COALESCE(Letter_Extension_Due_Date, Letter_Original_Reply_Date, Visit_Scheduled_Date) DESC, 
--		CASE
--			WHEN Letter_Sent_Cert_Date IS NOT NULL THEN Letter_Sent_Cert_Date
--			ELSE Letter_Sent_Date
--		END DESC, 
--		Compliance_Visit_No DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_ComplianceVisits] TO cfis

GO

