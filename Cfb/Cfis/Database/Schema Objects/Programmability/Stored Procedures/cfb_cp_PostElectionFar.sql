IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionFar')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionFar]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionFar]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	DATEADD(dd, 0, DATEDIFF(dd, 0, [Rpt_Final_Issued_Date])) AS [SentDate]
FROM	[Final_Audit_Tracking]
WHERE	[Cand_ID] = @candidateID AND
		[Election_Cycle] = @electionCycle

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionFar] TO cfis

GO

