CREATE TYPE dbo.LiabilityType AS TABLE
(
    LiabilityId INT,
    [Name] VARCHAR(100) NOT NULL,
    [Value] DECIMAL(15,2) NOT NULL
);