SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
-- =============================================
-- Author:		Simon C. Wu
-- Create date: 4/25/2013
-- Description:	Gets the starting date of post-election events for a given election cycle.
-- =============================================
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'fn_cp_GetCutoffEC')
	BEGIN
		DROP  Function  [fn_cp_GetCutoffEC]
	END
GO

CREATE FUNCTION fn_cp_GetCutoffEC
(
)
RETURNS char(5)
AS
BEGIN
	RETURN '2005'
END
GO

GRANT EXEC ON [dbo].[fn_cp_GetCutoffEC] TO caccess
GO
