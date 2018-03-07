/*
 * Retrieves the IDs for the latest major post election audit messages .
 */
CREATE PROCEDURE dbo.cfb_cp_Cmo_GetPostElectionRequestByCategory
(
	@candidateId varchar(5),
	@electionCycle varchar(5),
	@category tinyint
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	[R].[CandidateId], [R].[MessageId], [R].[Repost], [R].[SecondRequest]
FROM	[CmoMessages] AS [M]
		INNER JOIN CmoPostElectionRequests AS [R]
		ON	[M].[CandidateId] = [R].[CandidateId] AND 
			[M].[MessageId] = [R].[MessageId] AND
			[M].[CandidateId] = @candidateId AND
			[M].[ElectionCycle] = @electionCycle AND
			[Category] = @category AND
			[PostDate] IS NOT NULL
ORDER BY [SecondRequest] DESC, [Repost] DESC, [PostDate] DESC

