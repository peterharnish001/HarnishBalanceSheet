CREATE TABLE [dbo].[Liability]
(
	[LiabilityId] INT IDENTITY(1,1) PRIMARY KEY, 
    [BalanceSheetId] INT NOT NULL,
	[Name] VARCHAR(100) NOT NULL, 
    [Value] DECIMAL(15, 2) NOT NULL, 
    Constraint FK_BalanceSheet_Liability FOREIGN KEY (BalanceSheetId)
		References dbo.BalanceSheet (BalanceSheetId),
	Constraint UK_Liabliity_BalanceSheetId_Name UNIQUE (BalanceSheetId, [Name])
);
GO
CREATE NONCLUSTERED INDEX IX_Liability_BalanceSheetId
ON dbo.Liability (BalanceSheetId);
GO
