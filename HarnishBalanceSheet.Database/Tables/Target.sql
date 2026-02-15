CREATE TABLE [dbo].[Target]
(
	[TargetId] INT IDENTITY(1,1) PRIMARY KEY, 
    [AssetCategoryId] INT NOT NULL,
	[Percentage] DECIMAL(2, 2) NOT NULL, 
    [UserId] INT NOT NULL, 
    Constraint FK_AssetCategory_Target FOREIGN KEY (AssetCategoryId)
		References dbo.AssetCategory (AssetCategoryId),
	Constraint FK_User_Target FOREIGN KEY (UserId)
		References dbo.[User] (UserId),
	Constraint UK_Target_AssetCategoryId_UserId UNIQUE (AssetCategoryId, UserId)
)	
