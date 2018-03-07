/*
 * Retrieves an extension request by its primary key.
 */
CREATE PROCEDURE [dbo].[cfb_cp_GetExtensionRequest]
(
	@candidateID varchar(5),
	@electionCycle varchar(5),
	@extensionType tinyint,
	@reviewnumber tinyint,
	@iteration tinyint
)
AS

SET NOCOUNT ON
SET ROWCOUNT 1

SELECT	CandidateID, ElectionCycle, ExtensionType, ReviewNumber, Iteration, Date, RequestedDueDate, Reason, Version
FROM	ExtensionRequests
WHERE	(CandidateID = @candidateID) AND 
		(ElectionCycle = @electionCycle) AND 
		(ExtensionType = @extensionType) AND 
		(ReviewNumber = @reviewnumber) AND 
		(Iteration = @iteration)

