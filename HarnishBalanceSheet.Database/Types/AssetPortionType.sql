CREATE TYPE dbo.AssetPortionType AS TABLE
(
    AssetPortionId INT,
    AssetId INT,
    [Name] VARCHAR(100) NOT NULL,
    IsPercent BIT NOT NULL,
    AssetCategoryId INT NOT NULL,
    [Value] DECIMAL(15,2) NOT NULL
);