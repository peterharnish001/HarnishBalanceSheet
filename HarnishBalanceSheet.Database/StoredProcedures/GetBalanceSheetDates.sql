CREATE PROCEDURE [dbo].[GetBalanceSheetDates]
	@UserId INT,
	@Count INT
AS
	SELECT TOP (@Count) BalanceSheetId,
		[Date]
	FROM BalanceSheet
	ORDER BY [Date] DESC;
