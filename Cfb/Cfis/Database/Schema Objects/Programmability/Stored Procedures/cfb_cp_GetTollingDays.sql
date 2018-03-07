IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_GetTollingDays')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_GetTollingDays]
	END

GO

CREATE Procedure [dbo].[cfb_cp_GetTollingDays]
(
	@candidateID char(5),
	@electionCycle char(5),
	@isFar bit,
	@tollingDays int output
)

AS

SET NOCOUNT ON;

DECLARE @today datetime
SET @today = GETDATE()

-- collect tolling events
DECLARE @TollingEvents TABLE(
	StartDate datetime not null,
	EndDate datetime,
	EndReasonCode char(6) not null
)

INSERT INTO @TollingEvents
SELECT	[Start_Date],
		[End_Date],
		[End_Reason_CD]
FROM	[FA_Extension_Tolling_Event] 
WHERE	([Cand_ID] = @candidateID) AND 
		([Election_Cycle] = @electionCycle) AND 
		([Start_Date] IS NOT NULL) AND 
		(
			-- DAR
			(@isFar = 0 AND ([Source_CD] IN ('IDRREQ','DOCINA') OR ([Source_CD] = 'DARRPT' AND [Event_CD] IN ('XTCFB','XTMUTU')))) OR 
			-- FAR
			(@isFar = 1 AND ([Source_CD] IN ('FARRPT','DARINS') OR ([Source_CD] = 'DARRPT' AND [Event_CD] NOT IN ('XTCFB','XTMUTU'))))
		)
UNION ALL
SELECT	[Request_Sent_Date],
		[End_Date],
		[End_Reason_CD]
FROM	[FA_Inadaquate_Event]
WHERE	([Cand_ID] = @candidateID) AND
		([Election_Cycle] = @electionCycle) AND
		([Request_Sent_Date] IS NOT NULL) AND
		(
			-- DAR
			(@isFar = 0 AND [Source_CD] = 'DOCINA') OR 
			-- FAR
			(@isFar = 1 AND [Source_CD] = 'DARINS')
		)

-- compute number of days between start and end of tolling period for each event
DECLARE @TollingPeriod TABLE(
	Days int not null,
	EndReasonCode char(6) not null
)

DECLARE @startDate DateTime
SET @startDate = dbo.fn_GetTollingStartDate(@electionCycle)

INSERT INTO @TollingPeriod
SELECT	DATEDIFF(
			DAY,
			CASE
				WHEN [StartDate] < @startDate THEN @startDate
				ELSE [StartDate]
			END,
			CASE
				WHEN [EndDate] IS NULL THEN @today
				ELSE [EndDate]
			END
		),
		CASE
			WHEN [EndDate] IS NULL THEN '' -- force blank end reason for ongoing events
			ELSE [EndReasonCode]
		END
FROM	@TollingEvents

-- compute number of tolling days for each event from tolling end reason
DECLARE @TollingEventDays TABLE(
	Days int not null
)

INSERT INTO @TollingEventDays
SELECT	CASE
			-- include end date for extensions and latenesses
			WHEN [EndReasonCode] IN ('DUEDT','EXTGRT','EXPIRE','') THEN [Days] + 1
			ELSE [Days]
		END
FROM	@TollingPeriod

-- total tolling days from all events
SELECT	@tollingDays =
			ISNULL(
				SUM(
					CASE 
						WHEN [Days] < 0 THEN 0 
						ELSE [Days] 
					END
				), 
				0
			)
FROM	@TollingEventDays

GO

GRANT EXEC ON [dbo].[cfb_cp_GetTollingDays] TO caccess

GO

