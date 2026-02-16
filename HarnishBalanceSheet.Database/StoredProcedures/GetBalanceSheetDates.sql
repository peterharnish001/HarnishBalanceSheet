CREATE PROCEDURE [dbo].[GetBalanceSheetDates]
	@UserId INT,
	@Count INT
AS
	SELECT TOP (@Count) BalanceSheetId,
		[Date]
	FROM BalanceSheet
	WHERE UserId = @UserId
	ORDER BY [Date] DESC;
