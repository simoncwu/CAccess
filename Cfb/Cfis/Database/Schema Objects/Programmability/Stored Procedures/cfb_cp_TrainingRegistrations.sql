IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_TrainingRegistrations')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_TrainingRegistrations]
	END

GO

CREATE Procedure [dbo].[cfb_cp_TrainingRegistrations]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	CAST([Course_ID] AS int) AS [SessionID],
		CAST([crt].[Trainee_ID] AS smallint) AS [TraineeID],
		[crt].[Update_Date] AS [LastUpdated],
		[Enrolling_Candidate_ID] AS [RegisteringCandidateID],
		CASE [Confirmation_Ind]
			WHEN 'Y' THEN CAST(1 AS bit)
			ELSE CAST(0 AS bit)
		END AS [IsReservation],
		[Confirmation_Date] AS [ReservationDate],
		CASE [Attended_Ind]
			WHEN 'Y' THEN CAST(1 AS bit)
			ELSE CAST(0 AS bit)
		END AS [Attended],
		CASE [Completed_Ind]
			WHEN 'Y' THEN CAST(1 AS bit)
			ELSE CAST(0 AS bit)
		END AS [Completed]
FROM	[Course_Trainee] AS [crt] INNER JOIN [Candidate_Trainee] AS [cat]
		ON	[crt].[Trainee_ID] = [cat].[Trainee_ID] AND
			[crt].[Election_Cycle] = @electionCycle AND
			[crt].[Election_Cycle] = [cat].[Election_Cycle] AND
			[cat].[Cand_ID] = @candidateID

GO

GRANT EXEC ON [dbo].[cfb_cp_TrainingRegistrations] TO cfis

GO

