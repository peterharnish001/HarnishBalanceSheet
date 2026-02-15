CREATE TABLE [dbo].[AssetCategory]
(
	[AssetCategoryId] INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL,
	Constraint UK_AssetCategory_Name UNIQUE ([Name])
)
