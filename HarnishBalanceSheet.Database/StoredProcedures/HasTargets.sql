CREATE PROCEDURE [dbo].[HasTargets]
	@UserId INT
AS
	SELECT
		AC.AssetCategoryId
		,AC.[Name]
	FROM AssetCategory AC
	WHERE NOT EXISTS (SELECT 1 FROM [Target] WHERE UserId = @UserId); 
