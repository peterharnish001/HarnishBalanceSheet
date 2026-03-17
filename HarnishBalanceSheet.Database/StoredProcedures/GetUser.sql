CREATE PROCEDURE [dbo].[GetUser]
	@email varchar(100)
AS
	SELECT
		UserId
		,[Name]
		,Email
		,PasswordHash
	FROM dbo.[User]
	WHERE Email = @email;
