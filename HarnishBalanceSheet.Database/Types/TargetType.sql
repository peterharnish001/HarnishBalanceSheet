CREATE TYPE dbo.TargetType AS TABLE
(
    UserId INT NOT NULL,
    AssetCategoryId INT NOT NULL,
    [Percentage] DECIMAL NOT NULL
);