CREATE PROCEDURE [dbo].[EditBalanceSheet]
	@UserId INT,
	@BalanceSheetId INT,
	@AssetPortions dbo.AssetPortionType READONLY,
	@Bullion dbo.BullionType READONLY,
	@Liabilities dbo.LiabilityType READONLY	
AS
	DECLARE @SavedUserId INT;
	SELECT @SavedUserId = UserId FROM BalanceSheet WHERE BalanceSheetId = @BalanceSheetId;

	IF (@SavedUserId <> @UserId)
		THROW 51000, 'You cannot edit another user''s balance sheet.', 1;

	MERGE dbo.Asset AS target
	USING (SELECT DISTINCT AssetId, [NAME], IsPercent FROM @AssetPortions) AS source
	ON target.AssetId = source.AssetId AND target.BalanceSheetId = @BalanceSheetId
	WHEN MATCHED THEN
	UPDATE SET
	target.IsPercent = source.IsPercent
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (BalanceSheetId, [Name], IsPercent)
	VALUES (@BalanceSheetId, source.[Name], source.IsPercent);

	UPDATE @AssetPortions
	SET AssetId = A.AssetId
	FROM dbo.Asset A
	INNER JOIN @AssetPortions AP ON A.[Name] = AP.[Name] AND A.BalanceSheetId = @BalanceSheetId;

	MERGE dbo.AssetPortion AS target
	USING @AssetPortions AS source
	ON target.AssetId = source.AssetId AND target.AssetCategoryId = source.AssetCategoryId
	WHEN MATCHED THEN
	UPDATE SET
	target.[Value] = source.[Value]
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (AssetId, AssetCategoryId, [Value])
	VALUES (source.AssetId, source.AssetCategoryId, source.[Value]);

	MERGE dbo.MetalPosition AS target
	USING @Bullion AS source
	ON target.MetalPositionId = source.MetalPositionId AND target.BalanceSheetId = @BalanceSheetId
	WHEN MATCHED THEN
	UPDATE SET
	target.NumOunces = source.NumOunces,
	target.PricePerOunce = source.PricePerOunce
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (BalanceSheetId, NumOunces, PricePerOunce, PreciousMetalId)
	VALUES (@BalanceSheetId, source.NumOunces, source.PricePerOunce, source.PreciousMetalId);

	MERGE dbo.Liability AS target
	USING @Liabilities AS source
	ON target.LiabilityId = source.LiabilityId AND target.BalanceSheetId = @BalanceSheetId
	WHEN MATCHED THEN
	UPDATE SET
	target.[Value] = source.[Value]
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (BalanceSheetId, [Name], [Value])
	VALUES (@BalanceSheetId, source.[Name], source.[Value]);
