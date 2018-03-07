IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_TrainingTrainees')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_TrainingTrainees]
	END

GO

CREATE Procedure [dbo].[cfb_cp_TrainingTrainees]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

SET NOCOUNT ON;

SELECT	CAST([t].[Trainee_ID] AS smallint) AS [TraineeID],
		[Last_Name] AS [LastName],
		[First_Name] AS [FirstName],
		[MI] AS [MiddleInitial],
		[Phone_Contact_1] AS [Phone1],
		[Phone_Contact_2] AS [Phone2],
		[Email_Address] AS [Email],
		CASE 
			WHEN [c].[Update_Date] > [t].[Update_Date] THEN [c].[Update_Date]
			ELSE [t].[Update_Date]
		END AS [LastUpdated],
		[Relationship_CD] AS [RelationshipCode]
FROM	[Candidate_Trainee] AS [c]
		INNER JOIN [Trainee] AS [t]
		ON	[Cand_ID] = @candidateID AND
			[c].[Election_Cycle] = @electionCycle AND
			[Relationship_CD] <> 'REMOVE' AND
			[c].Election_Cycle = [t].[Election_Cycle] AND
			[c].[Trainee_ID] = [t].[Trainee_ID]

GO

GRANT EXEC ON [dbo].[cfb_cp_TrainingTrainees] TO cfis

GO

