/*
 * Retrieves a tolling letter by tolling codes.
 */
CREATE PROCEDURE dbo.cfb_cp_GetTollingLetterByCodes
(
	@source varchar(6),
	@event varchar(6),
	@type varchar(6)
)
AS

SET NOCOUNT OFF
SET ROWCOUNT 1

SELECT	l.LetterId, l.SourceId, l.EventId, l.TypeId, l.Description, l.Title
FROM	TollingLetters AS l
		INNER JOIN TollingSources AS s 
		ON	l.SourceId = s.SourceId AND 
			s.Code = @source 
		INNER JOIN TollingEvents AS e 
		ON	l.EventId = e.EventId AND 
			e.Code = @event 
		INNER JOIN TollingTypes AS t 
		ON	l.TypeId = t.TypeId AND 
			t.Code = @type 

