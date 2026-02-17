CREATE PROCEDURE [dbo].[GetNetWorthChart]
	@UserId INT,
	@Count INT
AS
		SELECT
			BS.[Date]
			,Assets.TotalAssets + Metals.TotalMetalPrice - Liabilities.TotalLiabilities AS NetWorth
		FROM (SELECT TOP (@Count) BalanceSheetId, [Date] 
				FROM BalanceSheet 
				WHERE UserId = @UserId
				ORDER BY [Date] DESC) BS
		LEFT JOIN (SELECT ISNULL(SUM(AP.[Value]), 0) AS TotalAssets, A.BalanceSheetId
					FROM AssetPortion AP
					INNER JOIN Asset A ON AP.AssetId = A.AssetId
					GROUP BY A.BalanceSheetId) Assets ON BS.BalanceSheetId = Assets.BalanceSheetId
		LEFT JOIN (SELECT ISNULL(SUM(MP.NumOunces * MP.PricePerOunce), 0) AS TotalMetalPrice, MP.BalanceSheetId
					FROM MetalPosition MP
					GROUP BY MP.BalanceSheetId) Metals ON BS.BalanceSheetId = Metals.BalanceSheetId 
		LEFT JOIN (SELECT ISNULL(SUM([L].[Value]), 0) AS TotalLiabilities, [L].BalanceSheetId
					FROM Liability [L]
					GROUP BY [L].BalanceSheetId) Liabilities ON BS.BalanceSheetId = Liabilities.BalanceSheetId
		ORDER BY BS.[Date] DESC;
