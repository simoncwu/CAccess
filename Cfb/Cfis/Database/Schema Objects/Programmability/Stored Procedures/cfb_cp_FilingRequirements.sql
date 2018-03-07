IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'cfb_cp_FilingRequirements')
	BEGIN
		DROP  Procedure  [dbo].[cfb_cp_FilingRequirements]
	END

GO

CREATE Procedure [dbo].[cfb_cp_FilingRequirements]
(
	@candidateID char(5),
	@electionCycle char(5)
)

AS

/******************************************************************************/
/**
 * Retrieves for each statement in a given election cycle whether or not a 
 * particular candidate has filed yet and whether or not the statement is
 * required for that candidate. This is done in two phases:
 * 
 *   I.  Gather relevant data from tables.
 *   II. Compile data into final result set.
 */
/******************************************************************************/

SET NOCOUNT ON;

--------------------------------------------------------------------------------
/**
 * [PHASE I]
 * Gather data from all relevant tables, filtering for:
 *   - candidate ID
 *   - election cycle
 *   - non-deleted data
 *   - any additional table-specific criteria
 */
--------------------------------------------------------------------------------
/**
 * Election Cycle
 * --------------
 * Get election cycle dates as a variable (single result).
 */
DECLARE	@OptInDate datetime
DECLARE @PrimaryDate datetime

SET ROWCOUNT 1

SELECT	@OptInDate = Opt_In_Date,
		@PrimaryDate = Primary_Date
FROM	Election
WHERE	(election_cycle = @electionCycle)
-- End Election Cycle

/**
 * Active Candidate
 * ----------------
 * Get filing-related active candidate information as variables (single result).
 */
DECLARE @OfficeCD char(6)
DECLARE @BoroCD char(6)
DECLARE @DistrictCD char(6)
DECLARE @CertifiedDate datetime
DECLARE @FilerDate datetime
DECLARE @TerminationDate datetime
DECLARE @StartOfActivityStatement char(1)

SET ROWCOUNT 1

SELECT	@OfficeCD = Office_CD, 
		@BoroCD = Boro_CD, 
		@DistrictCD = District_CD, 
		@CertifiedDate = Certified_Date, 
		@FilerDate = File_Info_Date, 
		@TerminationDate = Termination_Date, 
		@StartOfActivityStatement = Start_Of_Activity_Statement_No
FROM	Active_Candidate
WHERE	(Cand_ID = @candidateID) AND 
		(Election_Cycle = @electionCycle) AND 
		(Deleted_Ind = 'N')
-- End Active Candidate

/**
 * [Intermediate Result Set]
 * Ballot Status
 * -------------------------
 * Get current ballot status data.
 */
DECLARE @BallotStatus TABLE(
	PartyCode char(6) not null,
	ActionDate datetime null, 
	ActionCode char(6) not null, 
	OffBallotDate datetime null
)

SET ROWCOUNT 0

INSERT INTO @BallotStatus
SELECT	Party_CD, Action_Date, Action_CD, Off_Ballot_Date
FROM	Ballot_Status
WHERE	(Cand_ID = @candidateID) AND 
		(Election_Cycle = @electionCycle) AND 
		(Deleted_Ind = 'N') AND 
		(Action_CD <> '050') -- omit ballot declinations
-- End Ballot Status

/**
 * [Intermediate Result Set]
 * Ballot Competition
 * -------------------------
 * Get competitors on balllot.
 */
DECLARE @Competitors TABLE(
	CandidateID char(5) not null, 
	PartyCode char(6) not null, 
	ActionDate datetime null, 
	OffBallotDate datetime null
)

SET ROWCOUNT 0

INSERT INTO @Competitors
SELECT	bs.Cand_ID, bs.Party_CD, Action_Date, Off_Ballot_Date 
FROM	Ballot_Status AS bs 
		INNER JOIN Active_Candidate AS ac
		ON	bs.Cand_ID = ac.Cand_ID AND 
			bs.Election_Cycle = ac.Election_Cycle AND 
			bs.Cand_ID <> @candidateID AND -- omit current candidate
			bs.Election_Cycle = @electionCycle AND 
			bs.Deleted_Ind = 'N' AND 
			bs.Action_CD <> '050' AND -- omit ballot declinations
			ac.Cand_ID <> @candidateID AND 
			ac.Election_Cycle = @electionCycle AND  
			ac.Office_CD = @OfficeCD AND 
			ac.Boro_CD = @BoroCD AND 
			ac.District_CD = @DistrictCD AND 
			ac.Deleted_Ind = 'N'
-- End Competitors

/**
 * [Intermediate Result Set]
 * Documents Received Information
 * ------------------------------
 * Get all disclosure statements received by the CFB that were not rejected.
 */
DECLARE @FiledDocuments TABLE(
	StatementNumber smallint not null,
	DateReceived datetime null
)

SET ROWCOUNT 0

INSERT INTO @FiledDocuments

SELECT	Statement_No, MAX(
		CASE
			WHEN (Postmark_Date < Date_Received) THEN Postmark_Date
			ELSE Date_Received
		END)
FROM	Document_Tracking AS dt
		INNER JOIN Authorized_Committee AS ac
		ON	(dt.Doc_Type_CD = 'DS') AND 
			(dt.Submission_CD = 'REG') AND 
			(dt.Audit_Response_Ind = 'N') AND 
			(dt.Status_CD <> 'REJECT') AND 
			(dt.Cand_ID = @candidateID) AND 
			(dt.Election_Cycle = @electionCycle) AND 
			(dt.Receipt_Complete_Date IS NOT NULL) AND 
			(dt.Comm_ID = ac.Comm_ID) AND
			(ac.Cand_ID = @candidateID) AND 
			(ac.Election_Cycle = @electionCycle) AND 
			(ac.Active_Ind = 'Y') AND 
			(ac.Deleted_Ind = 'N')
GROUP BY Statement_No


-- End Documents Received Information

/**
 * [Intermediate Result Set]
 * Statement Dates
 * -------------------------
 * Get dates and flags for all statements in this election cycle, plus received status.
 */
DECLARE @StatementDates TABLE(
	StatementNumber tinyint not null, 
	StartDate datetime null, 
	EndDate datetime null, 
	DueDate datetime null, 
	PriGenInd char(1) not null, 
	PostOptInRequiredStatement char(1) not null, 
	CatchUpStatementInd char(1) not null,
	DateReceived datetime null
)

SET ROWCOUNT 0

INSERT INTO @StatementDates
SELECT	Statement_No, Start_Date, End_Date, Due_Date, Pri_Gen_Ind, Post_Opt_In_Required_Statement, Catch_Up_Statement_Ind, fd.DateReceived
FROM	Statement_Date AS sd
		LEFT OUTER JOIN @FiledDocuments AS fd
		ON	sd.Statement_No = fd.StatementNumber
WHERE	(Election_Cycle = @electionCycle)
-- End Statement Dates

--------------------------------------------------------------------------------
/**
 * [PHASE II]
 * Select identifying information for all statements and compute values for:
 *   - mandatory status
 *   - received status
 */
--------------------------------------------------------------------------------

SELECT	sd.StatementNumber, DueDate, DateReceived, 
		CASE -- filing of statement is required for candidate?
			WHEN -- pre-opt-in
				(PriGenInd = 'Y') AND 
				(CatchUpStatementInd = 'N') AND 
				(EndDate < @OptInDate)
				THEN 'Y'
			WHEN -- post-opt-in
				(PostOptInRequiredStatement = 'Y') 
				THEN 'Y'
			WHEN -- primary election
				(PriGenInd = 'Y') AND 
				(CatchUpStatementInd = 'N') AND 
				(EndDate >= @OptInDate) AND 
				EXISTS (
					-- candidate is on ballot during period
					SELECT	ActionDate
					FROM	@BallotStatus AS bs
					WHERE	(
								(OffBallotDate > DueDate) OR 
								(OffBallotDate IS NULL) OR
								-- include primary election losers
								(OffBallotDate >= @PrimaryDate AND ActionCode = '110')
							) AND
							EXISTS (
								-- candidate has same-party opposition during period
								SELECT	CandidateID
								FROM	@Competitors AS c
								WHERE	(c.PartyCode = bs.PartyCode) AND
										(c.OffBallotDate > DueDate OR c.OffBallotDate IS NULL)
							)
				)
				THEN 'Y'
			WHEN -- general election
				(PriGenInd = 'N') AND 
				EXISTS (
					-- candidate is on ballot during period
					SELECT	ActionDate
					FROM	@BallotStatus
					WHERE	(OffBallotDate > DueDate) OR 
							(OffBallotDate IS NULL)
				) AND 
				EXISTS (
					-- candidate has opposition during period
					SELECT	CandidateID
					FROM	@Competitors
					WHERE	((OffBallotDate > DueDate) OR 
							(OffBallotDate IS NULL)) AND 
							PartyCode NOT IN (
								SELECT	PartyCode 
								FROM	@BallotStatus
								WHERE	(OffBallotDate > DueDate) OR 
										(OffBallotDate IS NULL)
							)
				)
				THEN 'Y'
			ELSE 'N'
		END AS RequiredInd
FROM	@StatementDates AS sd
WHERE	((@TerminationDate IS NULL) OR (@TerminationDate >= StartDate)) AND 
		(@CertifiedDate <= EndDate) OR 
		(@FilerDate <= EndDate) OR 
		(@StartOfActivityStatement BETWEEN 1 AND sd.StatementNumber) OR
		((	SELECT	MIN(ActionDate) -- current ballot status, petition filed
			FROM	@BallotStatus
			WHERE	ActionCode = '003'
		) <= EndDate) OR
		((	SELECT	MIN(Action_Date) -- past ballot status, petition filed
			FROM	Ballot_Status_History
			WHERE	(Cand_ID = @candidateID) AND 
					(Election_Cycle = @electionCycle) AND 
					(Deleted_Ind = 'N') AND
					(Action_CD = '003')
		) <= EndDate)
ORDER BY StatementNumber DESC

GO

GRANT EXEC ON [dbo].[cfb_cp_FilingRequirements] TO cfis

GO

