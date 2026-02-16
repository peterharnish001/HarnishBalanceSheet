CREATE PROCEDURE [dbo].[GetLatest]
	@UserId INT
AS
	DECLARE @BalanceSheetId INT;
	SET @BalanceSheetId = (SELECT TOP (1) BalanceSheetId FROM BalanceSheet WHERE UserId = @UserId ORDER BY [Date] DESC);

	SELECT [Name]
	,AssetCategoryId
	FROM AssetCategory;

	SELECT AC.[Name] AS AssetCategoryName
		,A.[Name] AS AssetName
		,AP.[Value]
	FROM AssetCategory AC
	INNER JOIN AssetPortion AP ON AC.AssetCategoryId = AP.AssetCategoryId
	INNER JOIN Asset A ON AP.AssetId = A.AssetId
	INNER JOIN BalanceSheet BS ON A.BalanceSheetId = BS.BalanceSheetId
	WHERE BS.UserId = @UserId AND BS.BalanceSheetId = @BalanceSheetId;

	SELECT
		MP.NumOunces
		,PM.[Name] AS PreciousMetalName
		,PM.PreciousMetalId
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
	,PM.PreciousMetalId
	FROM PreciousMetal PM;
