/*
Author: Gerald
Edited By: Wilson
*/

USE [FantasySoccer]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSalaryCap]    Script Date: 11/29/2015 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[GetSalaryCap] 
(
	-- Add the parameters for the function here
	@UserId nvarchar(128)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Sum INT, @SalaryCap INT

	SET @Sum = 0
	SET @SalaryCap = 0

	SET @SalaryCap = (SELECT Value FROM Settings WHERE KeyId = 2)

	-- Add the T-SQL statements to compute the return value here
	SET @Sum = (SELECT SUM(Players.Cost)
	FROM LineupHistory 
    INNER JOIN Players ON LineupHistory.PlayerId = Players.PlayerId AND Month = DATEPART(MONTH, GETDATE())
    WHERE LineupHistory.UserId = @UserId)

	IF(@Sum IS NULL)
		SET @Sum = 0
	-- Return the result of the function
	RETURN (@SalaryCap - @Sum)

END
