CREATE TABLE [dbo].[MetalPosition]
(
	[MetalPositionId] INT IDENTITY(1,1) PRIMARY KEY, 
    [BalanceSheetId] INT NOT NULL,
	[NumOunces] DECIMAL(10, 2) NOT NULL, 
    [PricePerOunce] DECIMAL(15, 2) NOT NULL, 
    [PreciousMetalId] INT NULL, 
    Constraint FK_BalanceSheet_MetalPosition FOREIGN KEY (BalanceSheetId)
		References dbo.BalanceSheet (BalanceSheetId),
    Constraint FK_PreciousMetal_MetalPosition FOREIGN KEY (PreciousMetalId)
        References dbo.PreciousMetal (PreciousMetalId),
    Constraint UK_MetalPosition_BalanceSheetId_PreciousMetalId UNIQUE (BalanceSheetId, PreciousMetalId)
);
GO
CREATE NONCLUSTERED INDEX IX_MetalPosition_BalanceSheetId
ON dbo.MetalPosition (BalanceSheetId);
GO
