CREATE PROCEDURE [dbo].[CreateBalanceSheet]
	@UserId INT,
	@AssetPortions dbo.AssetPortionType READONLY,
	@Bullion dbo.BullionType READONLY,
	@Liabilities dbo.LiabilityType READONLY,
	@BalanceSheetId INT OUTPUT
AS
	INSERT INTO dbo.BalanceSheet ([Date], UserId)
	VALUES (GetDate(), @UserId);

	SELECT @BalanceSheetId = SCOPE_IDENTITY();

	INSERT INTO dbo.Asset (BalanceSheetId, [Name], IsPercent)
	SELECT DISTINCT @BalanceSheetId, [Name], IsPercent
	FROM @AssetPortions;	

	INSERT INTO dbo.AssetPortion (AssetId, AssetCategoryId, [Value])
	SELECT A.AssetId, AP.AssetCategoryId, AP.[Value]
	FROM @AssetPortions AP
	INNER JOIN Asset A ON AP.[Name] = A.[Name] AND A.BalanceSheetId = @BalanceSheetId;

	INSERT INTO dbo.MetalPosition (BalanceSheetId, NumOunces, PricePerOunce, PreciousMetalId)
	SELECT @BalanceSheetId, NumOunces, PricePerOunce, PreciousMetalId
	FROM @Bullion;

	INSERT INTO dbo.Liability (BalanceSheetId, [Name], [Value])
	SELECT @BalanceSheetId, [Name], [Value]
	FROM @Liabilities;

RETURN 0
