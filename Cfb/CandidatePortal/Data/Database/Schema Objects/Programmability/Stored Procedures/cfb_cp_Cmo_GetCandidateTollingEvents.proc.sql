/*
 * Retrieves all audit review messages for a single candidate by category.
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetCandidateTollingEvents
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@isFar bit
)
AS

SET NOCOUNT ON

-- temporary table for holding DAR/FAR tolling letter IDs
DECLARE @LetterIDs TABLE(
	[LetterId] tinyint not null
)

INSERT INTO @LetterIDs
SELECT	[LetterId]
FROM	[TollingLetters] AS [l]
		INNER JOIN [TollingSources] AS [s] ON [l].[SourceId] = [s].[SourceId]
		INNER JOIN [TollingEvents] AS [e] ON [l].[EventId] = [e].[EventId]
WHERE	(
			-- DAR
			(@isFar = 0 AND ([s].[Code] IN ('IDRREQ','DOCINA') OR ([s].[Code] = 'DARRPT' AND [e].[Code] = 'XTCFB'))) OR 
			-- FAR
			(@isFar = 1 AND ([s].[Code] IN ('FARRPT','DARINS') OR ([s].[Code] = 'DARRPT' AND [e].[Code] <> 'XTCFB')))
		)

SELECT	[l].[CandidateId], [l].[MessageId], [EventNumber], [l].[LetterId]
FROM	[CmoMessages] AS [m] 
		INNER JOIN [CmoTollingLetters] AS [l] 
		ON	[m].[CandidateId] = [l].[CandidateId] AND 
			[m].[MessageId] = [l].[MessageId] AND 
			[m].[CandidateId] = @candidateId AND 
			[m].[ElectionCycle] = @electionCycle AND 
			[m].[PostDate] IS NOT NULL 
		INNER JOIN @LetterIDs AS [i] ON [l].[LetterId] = [i].[LetterId]
ORDER BY [EventNumber] DESC

