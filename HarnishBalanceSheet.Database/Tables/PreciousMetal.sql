CREATE TABLE [dbo].[PreciousMetal]
(
	[PreciousMetalId] INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL,
	Constraint UK_PreciousMetal_Name UNIQUE ([Name])
)
