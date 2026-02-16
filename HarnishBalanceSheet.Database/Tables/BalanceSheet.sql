CREATE TABLE [dbo].[BalanceSheet]
(
	[BalanceSheetId] INT IDENTITY(1,1) PRIMARY KEY, 
    [Date] DATETIME NOT NULL, 
    [UserId] INT NOT NULL,
    Constraint FK_User_BalanceSheet FOREIGN KEY (UserId)
        References dbo.[User] (UserId),
    Constraint UK_BalanceSheet_Date_UserId UNIQUE ([Date], UserId)
);
GO
CREATE NONCLUSTERED INDEX IX_BalanceSheet_UserId
ON dbo.BalanceSheet (UserId);
GO
