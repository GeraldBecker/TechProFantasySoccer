/*
Author: Gerald
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTotalFantasyPointsByUserAndMonth]    Script Date: 11/29/2015 10:47:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gerald Becker
-- Create date: November 25 2015
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[GetTotalFantasyPointsByUserAndMonth] 
(
	-- Add the parameters for the function here
	@UserId nvarchar(128),
	@Month int
)
RETURNS DECIMAL(10,1)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Sum DECIMAL(10, 1),
	@Temp DECIMAL(10,1)

    SET @Sum = 0
	SET @Temp = 0

	-- Add the T-SQL statements to compute the return value here
	


	-- Create a cursor to loop through all the rows in the data
	DECLARE DataCursor CURSOR FOR
	SELECT    
    dbo.CalculateTotalFantasyPoints(SUM([PlayerStats].Goals),   
             		SUM([PlayerStats].Shots),    
             		SUM([PlayerStats].Assists),   
             		SUM([PlayerStats].MinPlayed),   
             		SUM([PlayerStats].Fouls),   
             		SUM([PlayerStats].YellowCards),   
             		SUM([PlayerStats].RedCards),   
             		SUM([PlayerStats].GoalsAllowed),   
             		SUM([PlayerStats].SavesMade),   
             		SUM([PlayerStats].CleanSheets),   
             		Players.PositionRef) AS 'Total Fantasy Pts'    
    FROM LineupHistory    
    INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId    
    INNER JOIN PlayerStats ON PlayerStats.PlayerId = LineupHistory.PlayerId AND PlayerStats.Month = LineupHistory.Month    
    WHERE LineupHistory.UserId = @UserId AND LineupHistory.Active = 'True' AND LineupHistory.Month = @Month   
    GROUP BY Players.PositionRef

	OPEN DataCursor

	FETCH NEXT FROM DataCursor INTO
		@Temp
			

	-- Start the loop through the rows
	WHILE @@FETCH_STATUS = 0
	BEGIN 
		SET @Sum = @Sum + @Temp
		
		
		-- Fetch the next row of data from the table
		FETCH NEXT FROM DataCursor INTO
			@Temp
	END

	CLOSE DataCursor
	DEALLOCATE DataCursor


	-- Return the result of the function
	RETURN @Sum

END
