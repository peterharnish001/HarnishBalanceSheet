CREATE PROCEDURE [dbo].[GetNetWorthChart]
	@UserId INT,
	@Count INT
AS
	SELECT
		BS.[Date],
		COALESCE(SUM(AP.[Value]), 0) + COALESCE(SUM(MP.NumOunces * MP.PricePerOunce), 0) - COALESCE(SUM(L.[Value]), 0) AS NetWorth
	FROM (SELECT TOP (@Count) BalanceSheetId, [Date] 
			FROM BalanceSheet 
			WHERE UserId = @UserId
			ORDER BY [Date] DESC) BS
	LEFT JOIN dbo.Asset A ON BS.BalanceSheetId = A.BalanceSheetId
	LEFT JOIN dbo.AssetPortion AP ON A.AssetId = AP.AssetId
	LEFT JOIN dbo.MetalPosition MP ON BS.BalanceSheetId = MP.BalanceSheetId
	LEFT JOIN dbo.Liability [L] ON BS.BalanceSheetId = L.BalanceSheetId
	GROUP BY BS.[Date]
	ORDER BY BS.[Date] DESC;
