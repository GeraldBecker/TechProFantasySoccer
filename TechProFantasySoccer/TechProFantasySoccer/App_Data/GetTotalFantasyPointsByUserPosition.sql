/*
Author: Gerald
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTotalFantasyPointsByUserPosition]    Script Date: 11/29/2015 10:47:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Gerald Becker
-- Create date: November 25 2015
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[GetTotalFantasyPointsByUserPosition] 
(
	-- Add the parameters for the function here
	@UserId nvarchar(128),
	@Position int
)
RETURNS DECIMAL(10,1)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Sum DECIMAL(10, 1)

    SET @Sum = 0

	-- Add the T-SQL statements to compute the return value here
	
	SET @Sum = (SELECT    
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
    WHERE LineupHistory.UserId = @UserId AND LineupHistory.Active = 'True' AND Players.PositionRef = @Position
    GROUP BY Players.PositionRef)

	IF(@Sum IS NULL)
		SET @Sum = 0


	-- Return the result of the function
	RETURN @Sum

END
