/*
Author: Gerald
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[TotalFantasyPoints]    Script Date: 11/29/2015 10:47:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[TotalFantasyPoints] (@input int)
RETURNS DECIMAL(10,1)
AS BEGIN
    DECLARE @Sum DECIMAL(10, 1)

    SET @Sum = 0

	DECLARE @Position VARCHAR(100),
			@Goals int,
			@Shots int, 
			@Assists int,
			@MinPlayed int,
			@Fouls int,
			@YC int,
			@RC int,
			@GA int,
			@Saves int,
			@Player int,
			@First VARCHAR(100),
			@Last VARCHAR(100)
    
	-- Create a cursor to loop through all the rows in the data
	DECLARE DataCursor CURSOR FOR
	SELECT Players.[PlayerId],
	FirstName AS First,
	LastName AS Last,
	[Positions].[PositionName] AS Position,
	SUM([PlayerStats].Goals) AS Goals,
	SUM([PlayerStats].Shots) AS Shots,
	SUM([PlayerStats].Assists) AS Assists,
	SUM([PlayerStats].MinPlayed) AS 'Min Played',
	SUM([PlayerStats].Fouls) AS Fouls,
	SUM([PlayerStats].YellowCards) AS YC,
	SUM([PlayerStats].RedCards) AS RC,
	SUM([PlayerStats].GoalsAllowed) AS GA,
	SUM([PlayerStats].SavesMade) AS Saves
	FROM [Players]
	INNER JOIN LineupHistory ON LineupHistory.PlayerId = Players.PlayerId
	INNER JOIN [Positions] ON [Positions].[PositionRef] = Players.PositionRef
	LEFT OUTER JOIN PlayerStats ON PlayerStats.PlayerId = Players.PlayerId
	WHERE Players.PlayerId = 1
	GROUP BY Players.PlayerId, FirstName, LastName, Positions.PositionName

	OPEN DataCursor

	FETCH NEXT FROM DataCursor INTO
		@Player,
		@First,
		@Last,
		@Position,
			@Goals,
			@Shots, 
			@Assists,
			@MinPlayed,
			@Fouls,
			@YC,
			@RC,
			@GA,
			@Saves
			

	-- Start the loop through the rows
	WHILE @@FETCH_STATUS = 0
	BEGIN 



		-- Fetch the next row of data from the table
		FETCH NEXT FROM DataCursor INTO
			@Player,
			@First,
			@Last,
			@Position,
			@Goals,
			@Shots, 
			@Assists,
			@MinPlayed,
			@Fouls,
			@YC,
			@RC,
			@GA,
			@Saves
	END

	CLOSE DataCursor
	DEALLOCATE DataCursor



	
	



    RETURN @Sum
END