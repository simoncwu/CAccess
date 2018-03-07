IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_PostElectionInadequate')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_PostElectionInadequate]
	END

GO

CREATE Procedure [dbo].[cfb_cp_PostElectionInadequate]
(
	@candidateID char(5),
	@electionCycle char(5),
	@isDar bit,
	@additionals bit
)

AS

SET NOCOUNT ON;

SELECT	[Source_CD] AS [SourceCode],
		[Event_No] AS [EventNumber],
		[Event_CD] AS [EventCode],
		[Type_CD] AS [TypeCode],
		[Long_Description] AS [Description],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Request_Sent_Date])) AS [StartDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [End_Date])) AS [EndDate],
		[Request_Sent_Date] AS [SentDate],
		[Receipt_Conf_Date] AS [ReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Original_Due_Date])) AS [DueDate],
		[Request_2_Sent_Date] AS [SecondSentDate],
		[Receipt_2_Conf_Date] AS [SecondReceiptDate],
		DATEADD(dd, 0, DATEDIFF(dd, 0, [Request_2_Due_Date])) AS [SecondDueDate],
		[Reply_Received_Date] AS [ResponseReceivedDate],
		[Reply_Postmark_Date] AS [ResponseSubmittedDate],
		[End_Reason_CD] AS [EndReasonCode], 
		CAST(
			CASE [Event_CD]
				WHEN 'INARES' THEN 0
				ELSE 1
			END AS bit
		) AS [AdditionalFlag]
FROM	[FA_Inadaquate_Event] AS [E]
		LEFT OUTER JOIN [Code] AS [C]
		ON	[E].[Event_CD] = [C].[Code] AND
			[C].[Election_Cycle] = @electionCycle AND
			[Category] = 'DOCEVE'
WHERE	[Cand_ID] = @candidateID AND
		[E].[Election_Cycle] = @electionCycle AND
		[Request_Sent_Date] IS NOT NULL AND
		[End_Reason_CD] <> 'CANCEL' AND
		[Type_CD] = 'INAD' AND
		(
			-- initial
			(@additionals = 0 AND [Event_CD] = 'INARES') OR 
			-- additional
			(@additionals = 1 AND [Event_CD] = 'ADDINA') OR 
			-- both
			@additionals IS NULL
		) AND 
		(
			-- IDR
			(@isDar = 0 AND [Source_CD] = 'DOCINA') OR
			-- DAR
			(@isDar = 1 AND [Source_CD] = 'DARINS')
		)

GO

GRANT EXEC ON [dbo].[cfb_cp_PostElectionInadequate] TO cfis

GO

