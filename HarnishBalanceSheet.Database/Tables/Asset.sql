CREATE TABLE [dbo].[Asset]
(
	[AssetId] INT IDENTITY(1,1) PRIMARY KEY, 
    [BalanceSheetId] INT NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [IsPercent] BIT NOT NULL,
    Constraint FK_BalanceSheet_Asset FOREIGN KEY (BalanceSheetId)
		REFERENCES dbo.BalanceSheet (BalanceSheetId),
    Constraint UK_Asset_BalanceSheetId_Name UNIQUE (BalanceSheetId, [Name])
);
GO
CREATE NONCLUSTERED INDEX IX_Asset_BalanceSheetId
ON dbo.Asset (BalanceSheetId);
GO
