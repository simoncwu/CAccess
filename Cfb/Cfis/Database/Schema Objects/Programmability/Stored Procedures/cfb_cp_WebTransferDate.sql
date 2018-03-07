IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_WebTransferDate')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_WebTransferDate]
	END

GO

CREATE Procedure [dbo].[cfb_cp_WebTransferDate]
(
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

SELECT	Transfer_date 
FROM	Election_Cycle_Transfer 
WHERE	Election_Cycle = @electionCycle

GO

GRANT EXEC ON [dbo].[cfb_cp_WebTransferDate] TO cfis

GO

