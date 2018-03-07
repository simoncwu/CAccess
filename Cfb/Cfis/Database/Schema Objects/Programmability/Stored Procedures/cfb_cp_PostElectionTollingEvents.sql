IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionTollingEvents')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionTollingEvents]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionTollingEvents]
(
	@candidateID char(5),
	@electionCycle char(5),
	@isFar bit
)

AS

SET NOCOUNT ON;

DECLARE @EventCodes TABLE(
	[Code] char(6) not null,
	[Description] varchar(60) not null
)

INSERT INTO @EventCodes
SELECT	[Code],
		[Long_Description]
FROM	[Code]
WHERE	[Election_Cycle] = @electionCycle AND
		[Category] = 'DOCEVE'

SELECT	[Source_CD] AS [SourceCode],
		[Event_CD] AS [EventCode],
		[Type_CD] AS [TypeCode],
		[Event_No] AS [EventNumber],
		[Description],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Start_Date])) AS [StartDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [End_Date])) AS [EndDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Due_Date])) AS [DueDate],
		[End_Reason_CD] AS [EndReasonCode],
		[Ref_Source_CD] AS [RefSourceCode],
		[Ref_Event_CD] AS [RefEventCode],
		[Ref_Type_CD] AS [RefTypeCode],
		[Ref_Event_No] AS [RefEventNumber]
FROM	[FA_Extension_Tolling_Event]
		LEFT OUTER JOIN @EventCodes
		ON	[Event_CD] = [Code]
WHERE	[Cand_ID] = @candidateID AND
		[Election_Cycle] = @electionCycle AND
		(
			-- DAR
			(@isFar = 0 AND ([Source_CD] IN ('IDRREQ','DOCINA') OR ([Source_CD] = 'DARRPT' AND [Event_CD] IN ('XTCFB','XTMUTU')))) OR 
			-- FAR
			(@isFar = 1 AND ([Source_CD] IN ('FARRPT','DARINS') OR ([Source_CD] = 'DARRPT' AND [Event_CD] NOT IN ('XTCFB','XTMUTU'))))
		) AND
		[End_Reason_CD] <> 'CANCEL'
ORDER BY [Start_Date] DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionTollingEvents] TO cfis

GO

