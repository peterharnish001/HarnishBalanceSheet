CREATE PROCEDURE [dbo].[EditBalanceSheet]
	@UserId INT,
	@BalanceSheetId INT,
	@AssetPortions dbo.AssetPortionType READONLY,
	@Bullion dbo.BullionType READONLY,
	@Liabilities dbo.LiabilityType READONLY	
AS
	MERGE dbo.Asset AS target
	USING (SELECT DISTINCT [NAME], IsPercent FROM @AssetPortions) AS source
	ON target.BalanceSheetId = @BalanceSheetId AND target.[Name] = source.[Name]
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
