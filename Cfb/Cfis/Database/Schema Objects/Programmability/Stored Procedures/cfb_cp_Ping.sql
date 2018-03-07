IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_Ping')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_Ping]
	END

GO

CREATE Procedure [dbo].[cfb_cp_Ping]

AS

SET NOCOUNT ON;
SET ROWCOUNT 1;

GO

GRANT EXEC ON [dbo].[cfb_cp_Ping] TO cfis

GO

