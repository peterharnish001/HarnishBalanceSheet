CREATE PROCEDURE [dbo].[GetDetails]
	@UserId INT,
	@BalanceSheetId INT
AS
	SELECT [Name] FROM AssetCategory;

	SELECT AC.[Name] AS AssetCategoryName
		,A.[Name] AS AssetName
		,AP.[Value]
		,A.IsPercent
	FROM AssetCategory AC
	INNER JOIN AssetPortion AP ON AC.AssetCategoryId = AP.AssetCategoryId
	INNER JOIN Asset A ON AP.AssetId = A.AssetId
	INNER JOIN BalanceSheet BS ON A.BalanceSheetId = BS.BalanceSheetId
	WHERE BS.UserId = @UserId AND BS.BalanceSheetId = @BalanceSheetId;

	SELECT [Date] FROM BalanceSheet BS WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT
		MP.NumOunces
		,PM.[Name] AS PreciousMetalName
		,MP.PricePerOunce
	FROM MetalPosition MP
	INNER JOIN PreciousMetal PM ON MP.PreciousMetalId = PM.PreciousMetalId
	INNER JOIN BalanceSheet BS ON MP.BalanceSheetId = BS.BalanceSheetId
	WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT
		[L].[Name]
		,[L].[Value]
	FROM Liability [L]
	INNER JOIN BalanceSheet BS ON [L].BalanceSheetId = BS.BalanceSheetId
	WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT PM.[Name]
	FROM PreciousMetal PM;

	SELECT AC.[Name] AS AssetCategoryName
	,T.[Percentage]
	FROM AssetCategory AC
	INNER JOIN [Target] T ON T.AssetCategoryId = AC.AssetCategoryId AND T.UserId = @UserId;

