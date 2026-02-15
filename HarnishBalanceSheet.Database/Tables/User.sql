CREATE TABLE [dbo].[User]
(
	[UserId] INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Email] VARCHAR(100) NOT NULL,
    Constraint UK_User_Email UNIQUE (Email)
)
