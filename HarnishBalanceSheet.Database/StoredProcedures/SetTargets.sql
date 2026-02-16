CREATE PROCEDURE [dbo].[SetTargets]
	@Targets dbo.TargetType READONLY
AS
	MERGE dbo.[Target] AS target
	USING @Targets AS source
	ON target.UserId = source.UserId AND target.AssetCategoryId = source.AssetCategoryId	
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (AssetCategoryId, [Percentage], UserId)
	VALUES (source.AssetCategoryId, source.[Percentage], source.UserId);
