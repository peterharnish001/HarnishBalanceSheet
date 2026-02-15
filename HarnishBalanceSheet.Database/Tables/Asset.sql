CREATE TABLE [dbo].[Asset]
(
	[AssetId] INT NOT NULL PRIMARY KEY, 
    [BalanceSheetId] INT NOT NULL, 
    [Name] VARCHAR(100) NOT NULL, 
    [IsPercent] BIT NOT NULL,
    Constraint FK_BalanceSheet_Asset FOREIGN KEY (BalanceSheetId)
		REFERENCES dbo.BalanceSheet (BalanceSheetId)
)
