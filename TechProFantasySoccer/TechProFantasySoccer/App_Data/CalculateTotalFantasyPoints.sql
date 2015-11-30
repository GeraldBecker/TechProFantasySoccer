/*
Author: Gerald
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateTotalFantasyPoints]    Script Date: 11/29/2015 10:47:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[CalculateTotalFantasyPoints] (@Goals int,
													@Shots int, 
													@Assists int,
													@MinPlayed int,
													@Fouls int,
													@YC int,
													@RC int,
													@GA int,
													@Saves int,
													@CleanSheets int,
													@Position int)
RETURNS DECIMAL(10,1)
AS BEGIN
    DECLARE @Sum DECIMAL(10, 1),
	@Temp DECIMAL(10,1)

    SET @Sum = 0
	SET @Temp = 0

	DECLARE --@GoalValue int,
	--		@ShotValue int,
	--		@AssistValue int,
	--		@MinPlayedValue int,
	--		@FoulValue int,
	--		@YCValue int,
	--		@RCValue int,
	--		@GAValue int,
	--		@SavesValue int,
			@Value DECIMAL(3, 1),
			@ScoringType int

			
    
	-- Create a cursor to loop through all the rows in the data
	DECLARE DataCursor CURSOR FOR
	SELECT [Value]
		  ,[ScoringType]
	  FROM [ScoringValues]
	  WHERE PositionRef = @Position OR PositionRef = 0

	OPEN DataCursor

	FETCH NEXT FROM DataCursor INTO
		@Value, @ScoringType
			

	-- Start the loop through the rows
	WHILE @@FETCH_STATUS = 0
	BEGIN 
		IF(@ScoringType = 1) --GOAL
			SET @Sum = @Sum + (@Goals * @Value)
		ELSE IF(@ScoringType = 2) --SHOT
			SET @Sum = @Sum + (@Shots * @Value)
		ELSE IF(@ScoringType = 3) --FOUL
			SET @Sum = @Sum + (@Fouls * @Value)
		ELSE IF(@ScoringType = 4) --YC
			SET @Sum = @Sum + (@YC * @Value)
		ELSE IF(@ScoringType = 5) --RC
			SET @Sum = @Sum + (@RC * @Value)
		ELSE IF(@ScoringType = 6) --ASSIST
			SET @Sum = @Sum + (@Assists * @Value)
		ELSE IF(@ScoringType = 7) --CLEAN SHEET
			SET @Sum = @Sum + (@CleanSheets * @Value)
		ELSE IF(@ScoringType = 8) --GOAL CONCEDED
			SET @Sum = @Sum + (@GA * @Value)
		ELSE IF(@ScoringType = 9) --SAVES
			SET @Sum = @Sum + (@Saves * @Value)
		ELSE IF(@ScoringType = 10) --MINUTES PLAYED
			SET @Sum = @Sum + (@MinPlayed / 90.0 * @Value)
		
		-- Fetch the next row of data from the table
		FETCH NEXT FROM DataCursor INTO
			@Value, @ScoringType
	END

	CLOSE DataCursor
	DEALLOCATE DataCursor


    RETURN @Sum
END