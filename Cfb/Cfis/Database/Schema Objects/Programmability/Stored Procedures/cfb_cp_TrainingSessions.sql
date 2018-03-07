IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_TrainingSessions')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_TrainingSessions]
	END

GO

CREATE Procedure [dbo].[cfb_cp_TrainingSessions]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

/*
 * Store the distinct IDs of a candidate's courses based on course registrations and trainee affiliations in a temporary table.
 */
DECLARE @CourseIDs TABLE(
	CourseID char(5) NOT NULL
)

INSERT INTO @CourseIDs
SELECT	DISTINCT [c].[Course_ID]
FROM	[Course] AS [c] INNER JOIN [Course_Trainee] AS [crt]
		ON	[c].[Course_ID] = [crt].[Course_ID] AND
			[c].[Election_Cycle] = [crt].[Election_Cycle] AND
			[c].[Election_Cycle] = @electionCycle
		INNER JOIN [Candidate_Trainee] AS [cat]
		ON	[crt].[Trainee_ID] = [cat].[Trainee_ID] AND
			[cat].[Cand_ID] = @candidateID

SELECT	CAST([Course_ID] AS int) AS [SessionID],
		[Type_CD] AS [TypeCode],
		[Start_Time] AS [StartTime],
		[End_Time] AS [EndTime],
		[Location_CD] AS [LocationCode],
		CASE [Status_CD]
			WHEN 'CANCEL' THEN CAST(1 AS bit)
			ELSE CAST(0 AS bit)
		END AS [Cancelled],
		[Max_No_Trainees] AS [MaxCapacity],
		CAST([Assigned_Trainer_CD_1] AS tinyint) AS [TrainerID1],
		CAST([Assigned_Trainer_CD_2] AS tinyint) AS [TrainerID2],
		CAST([Assigned_Trainer_CD_3] AS tinyint) AS [TrainerID3],
		CAST([Assigned_Trainer_CD_4] AS tinyint) AS [TrainerID4],
		CAST([Assigned_Trainer_CD_5] AS tinyint) AS [TrainerID5],
		[Trainer1_Role_CD] AS [TRoleCode1],
		[Trainer2_Role_CD] AS [TRoleCode2],
		[Trainer3_Role_CD] AS [TRoleCode3],
		[Trainer4_Role_CD] AS [TRoleCode4],
		[Trainer5_Role_CD] AS [TRoleCode5]
FROM	[Course] AS [c] INNER JOIN @CourseIDs AS [i]
		ON	[c].[Course_ID] = [i].[CourseID] AND
			[c].[Election_Cycle] = @electionCycle
ORDER BY [StartTime] DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_TrainingSessions] TO cfis

GO

