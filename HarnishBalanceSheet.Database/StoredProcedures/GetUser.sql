CREATE PROCEDURE [dbo].[GetUser]
	@email varchar(100)
AS
	SELECT
		UserId
		,[Name]
		,Email
	FROM dbo.[User]
	WHERE Email = @email;
