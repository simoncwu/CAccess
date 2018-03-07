-- =============================================
-- Author:		Simon C. Wu
-- Create date: 4/25/2013
-- Description:	Gets the starting date of post-election events for a given election cycle.
-- =============================================
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'fn_GetTollingStartDate')
	BEGIN
		DROP  Function  [fn_GetTollingStartDate]
	END
GO

CREATE FUNCTION fn_GetTollingStartDate
(
	@electionCycle char(5)
)
RETURNS DateTime
AS
BEGIN
	DECLARE @result DateTime

	DECLARE @specialFlag bit
	SET @specialFlag =
		CASE
			WHEN @electionCycle > '2012' AND EXISTS (
					SELECT	*
					FROM	[Election]
					WHERE	[Election_Cycle] = @electionCycle AND [Election_Type_CD] = 'S'
				) THEN 1
			ELSE 0
		END

	IF @specialFlag = 1
	BEGIN
		DECLARE @electionDate DateTime
		SELECT	@electionDate = [Election_Date]
		FROM	[Election]
		WHERE	[Election_Cycle] = @electionCycle

		SELECT	@result = MIN([Due_Date])
		FROM	[Statement_Date]
		WHERE	[Election_Cycle] = @electionCycle AND [Due_Date] >= @electionDate
	END
	ELSE
	BEGIN
		SELECT	@result = MAX([Due_Date])
		FROM	[Statement_Date]
		WHERE	[Election_Cycle] = @electionCycle
	END

	RETURN @result
END
GO

GRANT EXEC ON [dbo].[fn_GetTollingStartDate] TO caccess
GO
