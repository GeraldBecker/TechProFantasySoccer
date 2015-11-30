/*
Author: Gerald
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateFantasyPointsByPosition]    Script Date: 11/29/2015 10:47:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[CalculateFantasyPointsByPosition] (@ScoringType int,
														@Scoring int,
														@Position int)
RETURNS DECIMAL(10,1)
AS BEGIN
    DECLARE @Sum DECIMAL(10, 1),
	@Value DECIMAL(10,1)

    SET @Sum = 0
	SET @Value = 0


			
    
	SET @Value = (SELECT [Value]
				FROM [ScoringValues]
				WHERE (PositionRef = @Position OR PositionRef = 0) AND ScoringType = @ScoringType)

	IF(@Value IS NULL)
		SET @Value = 0

	IF(@ScoringType = 10) --MINUTES PLAYED
		SET @Sum = (@Scoring / 90.0 * @Value)
	ELSE
		SET @Sum = @Scoring * @Value;
	


    RETURN @Sum
END