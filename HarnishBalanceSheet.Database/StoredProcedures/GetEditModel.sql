CREATE PROCEDURE [dbo].[GetEditModel]
	@UserId INT,
	@BalanceSheetId INT
AS
	SELECT [Name]
	,AssetCategoryId
	FROM AssetCategory;

	SELECT AC.[Name] AS AssetCategoryName
		,A.AssetId
		,A.[Name] AS AssetName
		,AP.AssetPortionId
		,AP.[Value]
	FROM AssetCategory AC
	INNER JOIN AssetPortion AP ON AC.AssetCategoryId = AP.AssetCategoryId
	INNER JOIN Asset A ON AP.AssetId = A.AssetId
	INNER JOIN BalanceSheet BS ON A.BalanceSheetId = BS.BalanceSheetId
	WHERE BS.UserId = @UserId AND BS.BalanceSheetId = @BalanceSheetId;

	SELECT
		MP.MetalPositionId
		,MP.NumOunces
		,PM.[Name] AS PreciousMetalName
		,PM.PreciousMetalId
	FROM MetalPosition MP
	INNER JOIN PreciousMetal PM ON MP.PreciousMetalId = PM.PreciousMetalId
	INNER JOIN BalanceSheet BS ON MP.BalanceSheetId = BS.BalanceSheetId
	WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT [Date] FROM BalanceSheet BS WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT
		[L].LiabilityId
		,[L].[Name]
		,[L].[Value]
	FROM Liability [L]
	INNER JOIN BalanceSheet BS ON [L].BalanceSheetId = BS.BalanceSheetId
	WHERE BS.BalanceSheetId = @BalanceSheetId AND BS.UserId = @UserId;

	SELECT PM.[Name]
	,PM.PreciousMetalId
	FROM PreciousMetal PM;	
