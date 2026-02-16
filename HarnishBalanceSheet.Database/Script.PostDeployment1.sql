/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

WITH cte AS (
    SELECT 'Bonds' AS [Name] UNION ALL
    SELECT 'Cash' AS [Name] UNION ALL
    SELECT 'Precious Metals' AS [Name] UNION ALL
    SELECT 'Real Estate' AS [Name] UNION ALL
    SELECT 'Stocks' AS [Name]
)
MERGE dbo.AssetCategory AS target
	USING cte AS source
	ON target.[Name] = source.[Name]
	WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Name])
	VALUES (source.[Name]);

WITH cte AS (
    SELECT 'Gold' AS [Name] UNION ALL
    SELECT 'Silver' AS [Name] UNION ALL
    SELECT 'Platinum' AS [Name] UNION ALL
    SELECT 'Palladium' AS [Name] UNION ALL
    SELECT 'Rhodium' AS [Name]
)
MERGE dbo.PreciousMetal AS target
	USING cte AS source
	ON target.[Name] = source.[Name]
	WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Name])
	VALUES (source.[Name]);