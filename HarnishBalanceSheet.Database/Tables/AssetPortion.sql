CREATE TABLE [dbo].[AssetPortion]
(
	[AssetPortionId] INT NOT NULL PRIMARY KEY, 
    [AssetId] INT NOT NULL,
	[AssetCategoryId] INT NOT NULL, 
    [Value] DECIMAL(15, 2) NOT NULL, 
    CONSTRAINT FK_Asset_AssetPortion FOREIGN KEY (AssetId)
		REFERENCES dbo.Asset (AssetId),
	CONSTRAINT FK_AssetCategory_AssetPortion FOREIGN KEY (AssetCategoryId)
		REFERENCES dbo.AssetCategory (AssetCategoryId)
)
