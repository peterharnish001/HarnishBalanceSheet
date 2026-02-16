CREATE PROCEDURE [dbo].[GetLiabilityChart]
	@UserId INT,
	@Count INT
AS
	SELECT TOP (@Count)
		[Date]
		,COALESCE(SUM([L].[Value]), 0) AS TotalLiabilities
	FROM BalanceSheet BS
	INNER JOIN Liability [L] ON BS.BalanceSheetId = [L].BalanceSheetId AND BS.UserId = @UserId
	GROUP BY BS.[Date]
	ORDER BY BS.[Date] DESC;
