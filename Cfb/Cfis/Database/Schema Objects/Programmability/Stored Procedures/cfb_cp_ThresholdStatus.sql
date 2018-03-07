IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_ThresholdStatus')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_ThresholdStatus]
	END

GO

CREATE Procedure [dbo].[cfb_cp_ThresholdStatus]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	Statement_No, 
		Type_Cd, 
		Entry_Date, 
		Required_Threshold_Amount, 
		Required_Threshold_Number, 
		Candidate_Threshold_Amount, 
		Candidate_Threshold_Number, 
		Office_CD AS OfficeCode, 
		Boro_CD AS BoroughCode
FROM	Candidate_Threshold
WHERE	(Cand_ID = @candidateID) AND 
		(Election_Cycle = @electionCycle)
ORDER BY Statement_No DESC, Entry_Date DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_ThresholdStatus] TO cfis

GO

