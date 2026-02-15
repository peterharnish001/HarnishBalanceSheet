CREATE TABLE [dbo].[Liability]
(
	[LiabilityId] INT NOT NULL PRIMARY KEY, 
    [BalanceSheetId] INT NOT NULL,
	[Name] VARCHAR(100) NOT NULL, 
    [Value] DECIMAL(15, 2) NOT NULL, 
    Constraint FK_BalanceSheet_Liability FOREIGN KEY (BalanceSheetId)
		References dbo.BalanceSheet (BalanceSheetId)
)
